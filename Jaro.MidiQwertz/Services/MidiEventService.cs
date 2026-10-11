using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace Jaro.MidiQwertz.Services;

public class MidiEventService(InputDevice inputDevice)
{
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

    public async IAsyncEnumerable<NoteEvent[]> GetGroupedNoteEventsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<NoteEvent> currentNotes = new List<NoteEvent>();

        await foreach (var midiEvent in GetNoteEventsAsync(cancellationToken))
        {
            if (midiEvent is NoteOnEvent noteOnEvent)
                currentNotes.Add(noteOnEvent);
            else if (midiEvent is NoteOffEvent _ && currentNotes.Count > 0)
            {
                yield return currentNotes.ToArray();
                currentNotes.Clear();
            }
        }
    }
}