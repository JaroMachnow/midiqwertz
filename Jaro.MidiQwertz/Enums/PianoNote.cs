namespace Jaro.MidiQwertz.Enums;

[Flags]
public enum PianoNote : int
{
    C1 = 1 << 0,
    CSharp1 = 1 << 1,
    D1 = 1 << 2,
    DSharp1 = 1 << 3,
    E1 = 1 << 4,
    F1 = 1 << 5,
    FSharp1 = 1 << 6,
    G1 = 1 << 7,
    GSharp1 = 1 << 8,
    A1 = 1 << 9,
    ASharp1 = 1 << 10,
    B1 = 1 << 11,

    C2 = 1 << 12,
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

    C3 = 1 << 24,
}