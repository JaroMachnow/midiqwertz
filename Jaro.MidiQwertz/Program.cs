using System.Runtime.InteropServices;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;

var devices = InputDevice.GetAll().ToList();

if (devices.Count == 0)
{
    Console.WriteLine("No MIDI input devices found.");
    return;
}

Console.WriteLine("MIDI input devices:");

for (var index = 0; index < devices.Count; index++)
{
    Console.WriteLine($"{index}: {devices[index].Name}");
}

Console.Write("Select device number: ");

if (!int.TryParse(Console.ReadLine(), out var deviceIndex) ||
    deviceIndex < 0 ||
    deviceIndex >= devices.Count)
{
    Console.WriteLine("Invalid device number.");

    foreach (var device in devices)
    {
        device.Dispose();
    }

    return;
}

var inputDevice = devices[deviceIndex];

foreach (var device in devices.Where(device => device != inputDevice))
{
    device.Dispose();
}

using (inputDevice)
using (var recording = new Recording(TempoMap.Default, inputDevice))
{
    inputDevice.SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOff;

    inputDevice.EventReceived += (_, eventArguments) =>
    {
        switch (eventArguments.Event)
        {
            case NoteOnEvent noteOnEventSwitch:
                Console.WriteLine(
                    $"ON  channel={noteOnEventSwitch.Channel} note={noteOnEventSwitch.NoteNumber} velocity={noteOnEventSwitch.Velocity}");
                break;

            case NoteOffEvent noteOffEvent:
                Console.WriteLine(
                    $"OFF channel={noteOffEvent.Channel} note={noteOffEvent.NoteNumber} velocity={noteOffEvent.Velocity}");
                break;
        }

        NoteOnEvent noteOnEvent = eventArguments.Event as NoteOnEvent;

        if (noteOnEvent.NoteNumber == (SevenBitNumber)60)
        {
            MacKeyboard.PressKey(0);
        }
        if (noteOnEvent.NoteNumber == (SevenBitNumber)66)
        {
            MacKeyboard.PressKey(0);
        }
    };

    inputDevice.StartEventsListening();
    recording.Start();

    Console.WriteLine($"Recording from: {inputDevice.Name}");
    Console.WriteLine("Play some notes. Press Enter to stop.");
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

    public static void PressKey(ushort keyCode)
    {
        var keyDownEvent =
            CreateKeyboardEvent(IntPtr.Zero, keyCode, true);

        var keyUpEvent =
            CreateKeyboardEvent(IntPtr.Zero, keyCode, false);

        if (keyDownEvent == IntPtr.Zero || keyUpEvent == IntPtr.Zero)
        {
            throw new InvalidOperationException("Could not create keyboard event.");
        }

        PostEvent(0, keyDownEvent);
        PostEvent(0, keyUpEvent);

        Release(keyDownEvent);
        Release(keyUpEvent);
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