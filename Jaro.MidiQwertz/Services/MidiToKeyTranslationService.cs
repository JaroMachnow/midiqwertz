using Jaro.MidiQwertz.Enums;
using Jaro.MidiQwertz.Models;

namespace Jaro.MidiQwertz.Services;

public class MidiToKeyTranslationService
{
    private readonly bool IsBoneActive = true;
    private Dictionary<PianoStroke, KeyStroke> Mappings { get; set; } = new();

    public MidiToKeyTranslationService()
    {
        AddSimpleWithCapsAndModifiers(PianoNote.A, MacKey.A);
        AddSimpleWithCapsAndModifiers(PianoNote.B, MacKey.B);
        AddSimpleWithCapsAndModifiers(PianoNote.C, MacKey.C);
        AddSimpleWithCapsAndModifiers(PianoNote.D, MacKey.D);
        AddSimpleWithCapsAndModifiers(PianoNote.E, MacKey.E);
        AddSimpleWithCapsAndModifiers(PianoNote.F, MacKey.F);
        AddSimpleWithCapsAndModifiers(PianoNote.G, MacKey.G);

        AddSimpleWithCapsAndModifiers(PianoNote.A | PianoNote.B, MacKey.H);
        AddSimpleWithCapsAndModifiers(PianoNote.B | PianoNote.C, MacKey.I);
        AddSimpleWithCapsAndModifiers(PianoNote.C | PianoNote.D, MacKey.J);
        AddSimpleWithCapsAndModifiers(PianoNote.D | PianoNote.E, MacKey.K);
        AddSimpleWithCapsAndModifiers(PianoNote.E | PianoNote.F, MacKey.L);
        AddSimpleWithCapsAndModifiers(PianoNote.F | PianoNote.G, MacKey.M);
        AddSimpleWithCapsAndModifiers(PianoNote.G | PianoNote.A, MacKey.N);

        AddSimpleWithCapsAndModifiers(PianoNote.A | PianoNote.C, MacKey.O);
        AddSimpleWithCapsAndModifiers(PianoNote.B | PianoNote.D, MacKey.P);
        AddSimpleWithCapsAndModifiers(PianoNote.C | PianoNote.E, MacKey.Q);
        AddSimpleWithCapsAndModifiers(PianoNote.D | PianoNote.F, MacKey.R);
        AddSimpleWithCapsAndModifiers(PianoNote.E | PianoNote.G, MacKey.S);
        AddSimpleWithCapsAndModifiers(PianoNote.F | PianoNote.A, MacKey.T);
        AddSimpleWithCapsAndModifiers(PianoNote.G | PianoNote.B, MacKey.U);

        AddSimpleWithCapsAndModifiers(PianoNote.A | PianoNote.D, MacKey.V);
        AddSimpleWithCapsAndModifiers(PianoNote.B | PianoNote.E, MacKey.W);
        AddSimpleWithCapsAndModifiers(PianoNote.C | PianoNote.F, MacKey.X);
        AddSimpleWithCapsAndModifiers(PianoNote.D | PianoNote.G, MacKey.Y);
        AddSimpleWithCapsAndModifiers(PianoNote.E | PianoNote.A, MacKey.Z);
        AddSimpleWithCapsAndModifiers(PianoNote.F | PianoNote.B, MacKey.Sz);
        AddSimpleWithCapsAndModifiers(PianoNote.G | PianoNote.C, MacKey.UmlautA);

        AddSimpleWithModifiers(PianoNote.A | PianoNote.B | PianoNote.C, MacKey.Space);
        AddSimpleWithModifiers(PianoNote.B | PianoNote.C | PianoNote.D, MacKey.Space);
        AddSimpleWithModifiers(PianoNote.C | PianoNote.D | PianoNote.E, MacKey.Space);
        AddSimpleWithModifiers(PianoNote.D | PianoNote.E | PianoNote.F, MacKey.Space);
        AddSimpleWithModifiers(PianoNote.E | PianoNote.F | PianoNote.G, MacKey.Space);
        AddSimpleWithModifiers(PianoNote.F | PianoNote.G | PianoNote.A, MacKey.Space);
        AddSimpleWithModifiers(PianoNote.G | PianoNote.A | PianoNote.B, MacKey.Space);

        AddSimpleWithModifiers(PianoNote.A | PianoNote.C | PianoNote.E, MacKey.Escape);
        AddSimpleWithModifiers(PianoNote.B | PianoNote.D | PianoNote.F, MacKey.Escape);
        AddSimpleWithModifiers(PianoNote.C | PianoNote.E | PianoNote.G, MacKey.Escape);
        AddSimpleWithModifiers(PianoNote.D | PianoNote.F | PianoNote.A, MacKey.Escape);
        AddSimpleWithModifiers(PianoNote.E | PianoNote.G | PianoNote.B, MacKey.Escape);
        AddSimpleWithModifiers(PianoNote.F | PianoNote.A | PianoNote.C, MacKey.Escape);
        AddSimpleWithModifiers(PianoNote.G | PianoNote.B | PianoNote.D, MacKey.Escape);

        AddSimpleWithModifiers(PianoNote.A | PianoNote.B | PianoNote.C, MacKey.Tab, PianoPressure.II);
        AddSimpleWithModifiers(PianoNote.B | PianoNote.C | PianoNote.D, MacKey.Tab, PianoPressure.II);
        AddSimpleWithModifiers(PianoNote.C | PianoNote.D | PianoNote.E, MacKey.Tab, PianoPressure.II);
        AddSimpleWithModifiers(PianoNote.D | PianoNote.E | PianoNote.F, MacKey.Tab, PianoPressure.II);
        AddSimpleWithModifiers(PianoNote.E | PianoNote.F | PianoNote.G, MacKey.Tab, PianoPressure.II);
        AddSimpleWithModifiers(PianoNote.F | PianoNote.G | PianoNote.A, MacKey.Tab, PianoPressure.II);
        AddSimpleWithModifiers(PianoNote.G | PianoNote.A | PianoNote.B, MacKey.Tab, PianoPressure.II);

        AddSimpleWithCapsAndModifiers(PianoNote.A | PianoNote.B | PianoNote.C | PianoNote.D, MacKey.Backspace);
        AddSimpleWithCapsAndModifiers(PianoNote.B | PianoNote.C | PianoNote.D | PianoNote.E, MacKey.Backspace);
        AddSimpleWithCapsAndModifiers(PianoNote.C | PianoNote.D | PianoNote.E | PianoNote.F, MacKey.Backspace);
        AddSimpleWithCapsAndModifiers(PianoNote.D | PianoNote.E | PianoNote.F | PianoNote.G, MacKey.Backspace);
        AddSimpleWithCapsAndModifiers(PianoNote.E | PianoNote.F | PianoNote.G | PianoNote.A, MacKey.Backspace);
        AddSimpleWithCapsAndModifiers(PianoNote.F | PianoNote.G | PianoNote.A | PianoNote.B, MacKey.Backspace);
        AddSimpleWithCapsAndModifiers(PianoNote.G | PianoNote.A | PianoNote.B | PianoNote.C, MacKey.Backspace);

        AddSimple(PianoNote.A | PianoNote.B | PianoNote.C | PianoNote.D | PianoNote.E, MacKey.Return);
        AddSimple(PianoNote.B | PianoNote.C | PianoNote.D | PianoNote.E | PianoNote.F, MacKey.Return);
        AddSimple(PianoNote.C | PianoNote.D | PianoNote.E | PianoNote.F | PianoNote.G, MacKey.Return);
        AddSimple(PianoNote.D | PianoNote.E | PianoNote.F | PianoNote.G | PianoNote.A, MacKey.Return);
        AddSimple(PianoNote.E | PianoNote.F | PianoNote.G | PianoNote.A | PianoNote.B, MacKey.Return);
        AddSimple(PianoNote.F | PianoNote.G | PianoNote.A | PianoNote.B | PianoNote.C, MacKey.Return);
        AddSimple(PianoNote.G | PianoNote.A | PianoNote.B | PianoNote.C | PianoNote.D, MacKey.Return);

        AddSimple(PianoNote.GSharp | PianoNote.C, MacKey.Keypad1);
        AddSimple(PianoNote.GSharp | PianoNote.D, MacKey.Keypad2);
        AddSimple(PianoNote.GSharp | PianoNote.E, MacKey.Keypad3);
        AddSimple(PianoNote.GSharp | PianoNote.F, MacKey.Keypad4);
        AddSimple(PianoNote.GSharp | PianoNote.G, MacKey.Keypad5);
        AddSimple(PianoNote.GSharp | PianoNote.A, MacKey.Keypad6);
        AddSimple(PianoNote.GSharp | PianoNote.B, MacKey.Keypad7);
        AddSimple(PianoNote.GSharp | PianoNote.A | PianoNote.B, MacKey.Keypad8);
        AddSimple(PianoNote.GSharp | PianoNote.B | PianoNote.C, MacKey.Keypad9);
    }

    private void AddSimple(PianoNote note, MacKey key, PianoPressure pressure = PianoPressure.I)
    {
        Mappings.Add(
            new PianoStroke(note, pressure),
            CreateKeyStroke(key));
    }

    private void AddSimpleWithModifiers(PianoNote note, MacKey key, PianoPressure pressure = PianoPressure.I)
    {
        AddSimple(note, key, pressure);
        Mappings.Add(
            new PianoStroke(note | PianoNote.CSharp, pressure),
            CreateKeyStroke(key, MacModifier.Command));
        Mappings.Add(
            new PianoStroke(note | PianoNote.DSharp, pressure),
            CreateKeyStroke(key, MacModifier.Option));
        Mappings.Add(
            new PianoStroke(note | PianoNote.FSharp, pressure),
            CreateKeyStroke(key, MacModifier.Control));
        /*Mappings.Add(
            new PianoStroke(note | PianoNote.GSharp, pressure),
            CreateKeyStroke(key, MacModifier.RightCommand));*/
    }

    private void AddSimpleWithCapsAndModifiers(PianoNote note, MacKey key)
    {
        AddSimpleWithModifiers(note, key);
        Mappings.Add(
            new PianoStroke(note, PianoPressure.II),
            CreateKeyStroke(key, MacModifier.Shift));
    }

    private KeyStroke CreateKeyStroke(MacKey key, MacModifier modifier = MacModifier.None)
    {
        if (IsBoneActive)
            return new KeyStroke(key.BoneToQwertz(), modifier);

        return new KeyStroke(key, modifier);
    }

    public KeyStroke? Translate(PianoStroke pianoStroke)
    {
        if (Mappings.TryGetValue(pianoStroke, out KeyStroke value))
            return value;
        return null;
    }
}