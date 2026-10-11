using Jaro.MidiQwertz.Enums;

namespace Jaro.MidiQwertz.Models;

public struct KeyStroke(MacKey key, MacModifier modifiers = MacModifier.None)
{
    public MacKey Key { get; } = key;
    public MacModifier Modifiers { get; } = modifiers;
}