using System.Diagnostics;
using Jaro.MidiQwertz.Enums;
using Jaro.MidiQwertz.Models;

namespace Jaro.MidiQwertz.Services;

public class MidiToKeyTranslationService
{
    public KeyStroke? Translate(PianoStroke pianoStroke)
    {
        return pianoStroke switch
        {
            { Note: PianoNote.C1, Pressure: PianoPressure.I } => new KeyStroke(MacKey.Y),
            { Note: PianoNote.D1, Pressure: PianoPressure.I } => new KeyStroke(MacKey.X),
            { Note: PianoNote.E1, Pressure: PianoPressure.I } => new KeyStroke(MacKey.C),
            { Note: PianoNote.F1, Pressure: PianoPressure.I } => new KeyStroke(MacKey.V),
            { Note: PianoNote.G1, Pressure: PianoPressure.I } => new KeyStroke(MacKey.B),

            { Note: PianoNote.C1, Pressure: PianoPressure.II } => new KeyStroke(MacKey.A),
            { Note: PianoNote.D1, Pressure: PianoPressure.II } => new KeyStroke(MacKey.S),
            { Note: PianoNote.E1, Pressure: PianoPressure.II } => new KeyStroke(MacKey.D),
            { Note: PianoNote.F1, Pressure: PianoPressure.II } => new KeyStroke(MacKey.F),
            { Note: PianoNote.G1, Pressure: PianoPressure.II } => new KeyStroke(MacKey.G),

            { Note: PianoNote.C1, Pressure: PianoPressure.III } => new KeyStroke(MacKey.Q),
            { Note: PianoNote.D1, Pressure: PianoPressure.III } => new KeyStroke(MacKey.W),
            { Note: PianoNote.E1, Pressure: PianoPressure.III } => new KeyStroke(MacKey.E),
            { Note: PianoNote.F1, Pressure: PianoPressure.III } => new KeyStroke(MacKey.R),
            { Note: PianoNote.G1, Pressure: PianoPressure.III } => new KeyStroke(MacKey.T),


            { Note: PianoNote.B1 or PianoNote.C2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.Space),
            { Note: PianoNote.B1 or PianoNote.C2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.Tab),
            { Note: PianoNote.B1 or PianoNote.C2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.Return),
            { Note: PianoNote.GSharp2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.Backspace),
            { Note: PianoNote.GSharp2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.Backspace, MacModifier.Shift),
            { Note: PianoNote.GSharp2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.Backspace, MacModifier.Command),

            { Note: PianoNote.A2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.Minus),
            { Note: PianoNote.G2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.Period),
            { Note: PianoNote.F2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.Comma),
            { Note: PianoNote.E2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.M),
            { Note: PianoNote.D2, Pressure: PianoPressure.I } => new KeyStroke(MacKey.N),

            { Note: PianoNote.B2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.UmlautA),
            { Note: PianoNote.A2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.UmlautO),
            { Note: PianoNote.G2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.L),
            { Note: PianoNote.F2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.K),
            { Note: PianoNote.E2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.J),
            { Note: PianoNote.D2, Pressure: PianoPressure.II } => new KeyStroke(MacKey.H),

            { Note: PianoNote.B2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.UmlautU),
            { Note: PianoNote.A2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.P),
            { Note: PianoNote.G2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.O),
            { Note: PianoNote.F2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.I),
            { Note: PianoNote.E2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.U),
            { Note: PianoNote.D2, Pressure: PianoPressure.III } => new KeyStroke(MacKey.Z),
            _ => null
        };
    }
}