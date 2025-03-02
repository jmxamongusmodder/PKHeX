﻿using System;
using System.ComponentModel;

namespace PKHeX.Core;

/// <summary>
/// Exposes information about Box Names and which box is the first box to show when the UI is opened.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public sealed class BoxLayout8a : SaveBlock, IBoxDetailName
{
    public const int BoxCount = 32;

    private const int StringMaxLength = SAV6.LongStringLength / 2; // 0x22 bytes

    public BoxLayout8a(SAV8LA sav, SCBlock block) : base(sav, block.Data) { }

    private static int GetBoxNameOffset(int box) => SAV6.LongStringLength * box;
    private Span<byte> GetBoxNameSpan(int box) => Data.AsSpan(GetBoxNameOffset(box), SAV6.LongStringLength);
    public string GetBoxName(int box) => SAV.GetString(GetBoxNameSpan(box));
    public void SetBoxName(int box, string value) => SAV.SetString(GetBoxNameSpan(box), value.AsSpan(), StringMaxLength, StringConverterOption.ClearZero);

    public string this[int i]
    {
        get => GetBoxName(i);
        set => SetBoxName(i, value);
    }

    public int CurrentBox
    {
        get => ((SAV8LA)SAV).GetValue<byte>(SaveBlockAccessor8LA.KCurrentBox);
        set => ((SAV8LA)SAV).SetValue(SaveBlockAccessor8LA.KCurrentBox, (byte)value);
    }
}
