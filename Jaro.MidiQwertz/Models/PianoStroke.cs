using Jaro.MidiQwertz.Enums;

namespace Jaro.MidiQwertz.Models;

public struct PianoStroke(PianoNote note, PianoPressure pressure)
{
    public PianoNote Note { get; } = note;
    public PianoPressure Pressure { get; } = pressure;
}