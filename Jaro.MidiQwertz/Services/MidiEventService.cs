using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace Jaro.MidiQwertz.Services;

public class MidiEventService(InputDevice inputDevice)
{
    private const int GroupingTimeoutMs = 50;
    public async IAsyncEnumerable<NoteEvent> GetNoteEventsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(inputDevice);

        var events = Channel.CreateUnbounded<MidiEvent>();

        void OnEventReceived(object? sender, MidiEventReceivedEventArgs eventArguments) =>
            events.Writer.TryWrite(eventArguments.Event);

        inputDevice.SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff;
        inputDevice.EventReceived += OnEventReceived;
        inputDevice.StartEventsListening();

        try
        {
            await foreach (var midiEvent in events.Reader.ReadAllAsync(cancellationToken))
            {
                switch (midiEvent)
                {
                    case NoteOnEvent noteOnEvent:
                        yield return noteOnEvent;
                        break;

                    case NoteOffEvent noteOffEvent:
                        yield return noteOffEvent;
                        break;
                }
            }
        }
        finally
        {
            inputDevice.EventReceived -= OnEventReceived;
            inputDevice.StopEventsListening();
            events.Writer.TryComplete();
        }
    }

    public async IAsyncEnumerable<NoteEvent[]> AggregateNotesAsync(
        IAsyncEnumerable<NoteEvent> noteEvents,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var currentNotes = new List<NoteEvent>();

        await using var enumerator = noteEvents.GetAsyncEnumerator(cancellationToken);

        var nextEventTask = enumerator.MoveNextAsync().AsTask();
        Task? quietPeriodTask = null;

        while (true)
        {
            if (quietPeriodTask is null)
            {
                if (!await nextEventTask)
                    break;

                if (ProcessEvent(enumerator.Current, currentNotes) is { } events1) yield return events1;

                nextEventTask = enumerator.MoveNextAsync().AsTask();
                quietPeriodTask = Task.Delay(TimeSpan.FromMilliseconds(GroupingTimeoutMs), cancellationToken);
                continue;
            }

            var completedTask = await Task.WhenAny(nextEventTask, quietPeriodTask);

            if (completedTask == quietPeriodTask)
            {
                if (currentNotes.Count > 0)
                {
                    yield return currentNotes.ToArray();
                    currentNotes.Clear();
                }

                quietPeriodTask = null;
                continue;
            }

            if (!await nextEventTask)
                break;

            if (ProcessEvent(enumerator.Current, currentNotes) is { } events2) yield return events2;

            // A new event arrived: restart the 100 ms quiet-period timer.
            nextEventTask = enumerator.MoveNextAsync().AsTask();
            quietPeriodTask = Task.Delay(TimeSpan.FromMilliseconds(GroupingTimeoutMs), cancellationToken);
        }

        // Emit any remaining notes when the source completes.
        if (currentNotes.Count > 0)
            yield return currentNotes.ToArray();
    }

    private static NoteEvent[]? ProcessEvent(NoteEvent midiEvent, List<NoteEvent> currentNotes)
    {
        if (midiEvent is NoteOnEvent noteOnEvent)
        {
            currentNotes.Add(noteOnEvent);
        }
        else if (midiEvent is NoteOffEvent && currentNotes.Count > 0)
        {
            var array = currentNotes.ToArray();
            currentNotes.Clear();
            return array;
        }

        return null;
    }

    public async IAsyncEnumerable<NoteEvent[]> GetGroupedNoteEventsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        List<NoteEvent> currentNotes = new List<NoteEvent>();
        long lastEventMilliSeconds = 0;

        await foreach (var midiEvent in GetNoteEventsAsync(cancellationToken))
        {
            if (midiEvent is NoteOnEvent noteOnEvent)
            {
                var currentMs = stopwatch.ElapsedMilliseconds;

                if (currentMs - lastEventMilliSeconds > 100)
                {
                    yield return currentNotes.ToArray();
                    currentNotes.Clear();
                }

                lastEventMilliSeconds = currentMs;
                currentNotes.Add(noteOnEvent);
            }
            else if (midiEvent is NoteOffEvent _ && currentNotes.Count > 0)
            {
                yield return currentNotes.ToArray();
                currentNotes.Clear();
            }
        }
    }

    /*private bool IsSharp(int noteNumber)
    {
        var modulo = noteNumber % 12;
        return modulo switch
        {
            1 or 3 or 6 or 8 or 10 => true,
            _ => false
        };
    }*/
}