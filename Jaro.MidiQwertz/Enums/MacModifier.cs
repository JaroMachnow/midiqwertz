namespace Jaro.MidiQwertz.Enums;

[Flags]
public enum MacModifier : ulong
{
    None = 0,
    LeftCommand = 1UL << 3,
    RightCommand = 1UL << 4,
    Shift = 1UL << 17,
    Control = 1UL << 18,
    Option = 1UL << 19,
    Command = 1UL << 20,
    CapsLock = 1UL << 16
}