using Melanchall.DryWetMidi.Multimedia;

namespace Jaro.MidiQwertz.Services;

public class MidiConnectionService
{
    private string _standardMidiDeviceName = "Xkey Air25 BLE Bluetooth";
    private int _scanTimeoutMs = 3000;

    public InputDevice ConnectStandardDeviceAsync()
    {
        InputDevice? standardDevice = null;

        while (standardDevice is null)
        {
            var devices = InputDevice.GetAll().ToList();

            if (devices.Count == 0)
            {
                Console.WriteLine($"No MIDI input devices found. Retrying in {_scanTimeoutMs} ms.");
                Thread.Sleep(_scanTimeoutMs);
                continue;
            }

            standardDevice = devices.FirstOrDefault(device => device.Name.Contains(_standardMidiDeviceName));
            if (standardDevice is null)
            {
                Console.WriteLine($"Standard MIDI input device not found. Retrying in {_scanTimeoutMs} ms.");
                Thread.Sleep(_scanTimeoutMs);
                foreach (var device in devices)
                    device.Dispose();
                continue;
            }
            foreach (var device in devices.Where(device => !device.Name.Contains(_standardMidiDeviceName)))
                device.Dispose();
        }

        return standardDevice;
    }
}