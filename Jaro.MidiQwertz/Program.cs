using Jaro.MidiQwertz.Services;

var midiConnectionService = new MidiConnectionService();
var inputDevice = midiConnectionService.ConnectStandardDeviceAsync();

var midiEventService = new MidiEventService(inputDevice);

await foreach (var ev in midiEventService.GetGroupedNoteEventsAsync())
{
    
    Console.WriteLine($"{string.Join(',', ev.Select(n => n.NoteNumber))}");
}

/*
var chordTracker = new PianoChordTracker();
var mappings = MidiKeyMap.CreateDefault();

using (inputDevice)
using (var recording = new Recording(TempoMap.Default, inputDevice))
{
    inputDevice.SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff;

    inputDevice.EventReceived += (_, eventArguments) =>
    {
        var chordChanged = false;
        PianoChord chord;

        switch (eventArguments.Event)
        {
            case NoteOnEvent noteOnEvent:
                chordChanged = chordTracker.ApplyNoteOn(
                    (byte)noteOnEvent.NoteNumber,
                    (byte)noteOnEvent.Velocity,
                    out chord);
                break;

            case NoteOffEvent noteOffEvent:
                chordChanged = chordTracker.ApplyNoteOff(
                    (byte)noteOffEvent.NoteNumber,
                    out chord);
                break;
        }

        if (chordChanged && mappings.TryGetValue(chordTracker.Current, out var shortcut))
        {
            MacKeyboard.PressShortcut(shortcut);
        }
    };

    inputDevice.StartEventsListening();
    recording.Start();

    Console.WriteLine($"Recording from: {inputDevice.Name}");
    Console.WriteLine("Play mapped chords. Press Enter to stop.");
    Console.ReadLine();

    recording.Stop();
    inputDevice.StopEventsListening();

    recording.ToFile().Write("recorded.mid", overwriteFile: true);
}

Console.WriteLine("Saved recorded.mid");

internal static class MacKeyboard
{
    private const string CoreGraphics =
        "/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics";

    private const string CoreFoundation =
        "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

    public static void PressShortcut(MacShortcut shortcut)
    {
        var keyDownEvents = new List<IntPtr>(shortcut.Keys.Count);
        var keyUpEvents = new List<IntPtr>(shortcut.Keys.Count);

        try
        {
            foreach (var key in shortcut.Keys)
            {
                keyDownEvents.Add(CreateRequiredKeyboardEvent(key.VirtualKeyCode, keyDown: true));
                keyUpEvents.Add(CreateRequiredKeyboardEvent(key.VirtualKeyCode, keyDown: false));
            }

            foreach (var keyDownEvent in keyDownEvents)
            {
                PostEvent(0, keyDownEvent);
            }

            for (var index = keyUpEvents.Count - 1; index >= 0; index--)
            {
                PostEvent(0, keyUpEvents[index]);
            }
        }
        finally
        {
            foreach (var keyboardEvent in keyDownEvents.Concat(keyUpEvents))
            {
                if (keyboardEvent != IntPtr.Zero)
                {
                    Release(keyboardEvent);
                }
            }
        }
    }

    private static IntPtr CreateRequiredKeyboardEvent(ushort keyCode, bool keyDown)
    {
        var keyboardEvent = CreateKeyboardEvent(IntPtr.Zero, keyCode, keyDown);

        if (keyboardEvent == IntPtr.Zero)
        {
            throw new InvalidOperationException("Could not create keyboard event.");
        }

        return keyboardEvent;
    }

    [DllImport(CoreGraphics, EntryPoint = "CGEventCreateKeyboardEvent")]
    private static extern IntPtr CreateKeyboardEvent(
        IntPtr source,
        ushort virtualKey,
        [MarshalAs(UnmanagedType.I1)] bool keyDown);

    [DllImport(CoreGraphics, EntryPoint = "CGEventPost")]
    private static extern void PostEvent(
        uint location,
        IntPtr keyboardEvent);

    [DllImport(CoreFoundation, EntryPoint = "CFRelease")]
    private static extern void Release(IntPtr value);
}
*/
