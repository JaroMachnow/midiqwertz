using Jaro.MidiQwertz.Enums;
using Jaro.MidiQwertz.Models;
using Melanchall.DryWetMidi.Core;

namespace Jaro.MidiQwertz.Services;

public class MidiEventToPianoStrokeTranslationService
{
    public PianoStroke GetPianoStrokeFromNoteEventArray(NoteEvent[] noteEvents)
    {
        PianoNote pianoNote = 0;

        foreach (var noteEvent in noteEvents)
        {
            pianoNote |= TranslateNoteNumberToPianoNote(noteEvent.NoteNumber);
        }

        var averageVelocity = noteEvents.Select(e => (int)e.Velocity).Average();
        var pianoPressure = TranslateVelocityToPianoPressure(averageVelocity);

        return new PianoStroke(pianoNote, pianoPressure);
    }

    private PianoNote TranslateNoteNumberToPianoNote(int noteNumber)
    {
        var modulo = noteNumber % 24;
        return (PianoNote)(1 << modulo);
    }

    private PianoPressure TranslateVelocityToPianoPressure(double velocity)
    {
        return velocity switch
        {
            0 => PianoPressure.None,
            >0 and <32 => PianoPressure.I,
            >=32 and <64 => PianoPressure.II,
            >=64  => PianoPressure.III,
            _ => PianoPressure.None
        };
    }
}