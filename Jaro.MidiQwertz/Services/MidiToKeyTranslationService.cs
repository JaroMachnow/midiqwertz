using Jaro.MidiQwertz.Enums;
using Jaro.MidiQwertz.Models;

namespace Jaro.MidiQwertz.Services;

public class MidiToKeyTranslationService
{
    public KeyStroke? Translate(PianoStroke pianoStroke)
    {
        return pianoStroke switch
        {
            { Note: PianoNote.C, Pressure: PianoPressure.I } => new KeyStroke(MacKey.C),
            { Note: PianoNote.C, Pressure: PianoPressure.II } => new KeyStroke(MacKey.C, MacModifier.Shift),
            { Note: PianoNote.D, Pressure: PianoPressure.I } => new KeyStroke(MacKey.D),
            { Note: PianoNote.D, Pressure: PianoPressure.II } => new KeyStroke(MacKey.D,  MacModifier.Shift),
            { Note: PianoNote.E, Pressure: PianoPressure.I } => new KeyStroke(MacKey.E),
            { Note: PianoNote.E, Pressure: PianoPressure.II } => new KeyStroke(MacKey.E,   MacModifier.Shift),
            { Note: PianoNote.F, Pressure: PianoPressure.I } => new KeyStroke(MacKey.F),
            { Note: PianoNote.F, Pressure: PianoPressure.II } => new KeyStroke(MacKey.F,  MacModifier.Shift),
            { Note: PianoNote.G, Pressure: PianoPressure.I } => new KeyStroke(MacKey.G),
            { Note: PianoNote.G, Pressure: PianoPressure.II } => new KeyStroke(MacKey.G,   MacModifier.Shift),
            { Note: PianoNote.A, Pressure: PianoPressure.I } => new KeyStroke(MacKey.A),
            { Note: PianoNote.A, Pressure: PianoPressure.II } => new KeyStroke(MacKey.A,  MacModifier.Shift),
            { Note: PianoNote.B, Pressure: PianoPressure.I } => new KeyStroke(MacKey.B),
            { Note: PianoNote.B, Pressure: PianoPressure.II } => new KeyStroke(MacKey.B,  MacModifier.Shift),
           
            _ => null
        };
    }
}