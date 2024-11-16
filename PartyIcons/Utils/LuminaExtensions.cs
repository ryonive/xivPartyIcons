using Lumina.Text.ReadOnly;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PartyIcons.Utils;

public static class LuminaExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static string ParseString(this ReadOnlySeString readOnlySeString)
    {
        return Encoding.UTF8.GetString(readOnlySeString);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T? FirstOrNull<T>(this IEnumerable<T> values, Func<T, bool> predicate) where T : struct
    {
        foreach(var val in values)
            if (predicate(val))
                return val;

        return null;
    }
}