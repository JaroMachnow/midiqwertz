using Jaro.MidiQwertz.Enums;

namespace Jaro.MidiQwertz.Services;

public static class MacBoneKeyTranslator
{
    public static MacKey BoneToQwertz(this MacKey key)
    {
        return key switch
        {
            MacKey.A => MacKey.R,
            MacKey.B => MacKey.H,
            MacKey.C => MacKey.A,
            MacKey.D => MacKey.W,
            MacKey.E => MacKey.F,
            MacKey.F => MacKey.Y,
            MacKey.G => MacKey.UmlautO,
            MacKey.H => MacKey.U,
            MacKey.I => MacKey.D,
            MacKey.J => MacKey.Q,
            MacKey.K => MacKey.Minus,
            MacKey.L => MacKey.I,
            MacKey.M => MacKey.O,
            MacKey.N => MacKey.J,
            MacKey.O => MacKey.G,
            MacKey.P => MacKey.Z,
            MacKey.Q => MacKey.UmlautA,
            MacKey.R => MacKey.K,
            MacKey.S => MacKey.L,
            MacKey.T => MacKey.S,
            MacKey.U => MacKey.E,
            MacKey.V => MacKey.X,
            MacKey.W => MacKey.P,
            MacKey.X => MacKey.T,
            MacKey.Y => MacKey.N,
            MacKey.Z => MacKey.M,
            MacKey.Sz => MacKey.UmlautU,
            MacKey.UmlautA => MacKey.V,
            MacKey.UmlautO => MacKey.B,
            MacKey.UmlautU => MacKey.C,
            _ => key
        };
    }
}