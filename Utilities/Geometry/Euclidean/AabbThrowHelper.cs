using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Utilities.Geometry.Euclidean;

internal static class AabbThrowHelper
{
    internal static void ThrowIfMinGreaterThanMax<T>(T min, T max) where T : INumber<T>
    {
        if (min > max)
        {
            ThrowMinMaxException(min, max);
        }
    }
    
    [DoesNotReturn]
    private static void ThrowMinMaxException<T>(T min, T max)
    {
        throw new ArgumentException($"Cannot construct AABB, min '{min}' is greater than max '{max}'");
    }
}