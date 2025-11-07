using System.Numerics;

namespace Utilities.Extensions;

public static class NumberExtensions
{
    /// <summary>
    ///     Returns the signed remainder of the division:
    ///     <para>
    ///         <b><paramref name="a" />/<paramref name="modulus" /></b>
    ///     </para>
    /// </summary>
    /// <param name="a">The dividend</param>
    /// <param name="modulus">The divisor</param>
    /// <typeparam name="T">The type associated with <paramref name="a" /> and the <paramref name="modulus" /></typeparam>
    /// <exception cref="DivideByZeroException">The provided <paramref name="modulus" /> is zero</exception>
    public static T Modulo<T>(this T a, T modulus) where T : INumber<T>
    {
        if (T.IsZero(modulus))
        {
            throw new DivideByZeroException();
        }
        
        return (a % modulus + modulus) % modulus;
    }

    /// <summary>
    ///     Returns the ceiling of the division:
    ///     <para>
    ///         <b><paramref name="a" />/<paramref name="divisor" /></b>
    ///     </para>
    /// </summary>
    /// <param name="a">The dividend</param>
    /// <param name="divisor">The divisor</param>
    /// <typeparam name="T">The type associated with <paramref name="a"/> and <paramref name="divisor"/></typeparam>
    /// <exception cref="DivideByZeroException">The provided <paramref name="divisor"/> is zero</exception>
    public static T CeilDiv<T>(this T a, T divisor) where T : IBinaryInteger<T>
    {
        if (divisor == T.Zero)
        {
            throw new DivideByZeroException();
        }
        
        return (a + divisor - T.One) / divisor;
    }
}