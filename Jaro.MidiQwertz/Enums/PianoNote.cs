namespace Jaro.MidiQwertz.Enums;

[Flags]
public enum PianoNote : int
{
    C = 1 << 0,
    CSharp = 1 << 1,
    D = 1 << 2,
    DSharp = 1 << 3,
    E = 1 << 4,
    F = 1 << 5,
    FSharp = 1 << 6,
    G = 1 << 7,
    GSharp = 1 << 8,
    A = 1 << 9,
    ASharp = 1 << 10,
    B = 1 << 11,

    /*C2 = 1 << 12,
    CSharp2 = 1 << 13,
    D2 = 1 << 14,
    DSharp2 = 1 << 15,
    E2 = 1 << 16,
    F2 = 1 << 17,
    FSharp2 = 1 << 18,
    G2 = 1 << 19,
    GSharp2 = 1 << 20,
    A2 = 1 << 21,
    ASharp2 = 1 << 22,
    B2 = 1 << 23,

    C3 = 1 << 24,*/
}