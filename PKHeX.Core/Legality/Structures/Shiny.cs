﻿namespace PKHeX.Core;

/// <summary>
/// Specification for <see cref="PKM.IsShiny"/>, used for creating and validating.
/// </summary>
public enum Shiny : byte
{
    /// <summary> PID is purely random; can be shiny or not shiny. </summary>
    Random = 0,

    /// <summary> PID is randomly created and forced to be not shiny. </summary>
    Never,

    /// <summary> PID is randomly created and forced to be shiny. </summary>
    Always,

    /// <summary> PID is randomly created and forced to be shiny as Stars. </summary>
    AlwaysStar,

    /// <summary> PID is randomly created and forced to be shiny as Squares. </summary>
    AlwaysSquare,

    /// <summary> PID is fixed to a specified value. </summary>
    FixedValue,
}

public static class ShinyExtensions
{
    public static bool IsValid(this Shiny s, PKM pkm) => s switch
    {
        Shiny.Always => pkm.IsShiny,
        Shiny.Never => !pkm.IsShiny,
        Shiny.AlwaysSquare => pkm.ShinyXor == 0,
        Shiny.AlwaysStar => pkm.ShinyXor == 1,
        _ => true,
    };

    public static bool IsShiny(this Shiny s) => s switch
    {
        Shiny.Always => true,
        Shiny.AlwaysSquare => true,
        Shiny.AlwaysStar => true,
        _ => false,
    };
}
