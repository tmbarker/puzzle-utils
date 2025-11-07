using Utilities.Extensions;

namespace Utilities.Tests.Extensions;

/// <summary>
///     Tests associated with <see cref="NumberExtensions"/>.
/// </summary>
public class NumberExtensionsTests
{
    [Theory]
    [InlineData(10, 3, 1)]
    [InlineData(-10, 3, 2)]
    [InlineData(10, -3, -2)]
    [InlineData(-10, -3, -1)]
    [InlineData(10, 5, 0)]
    public void Modulo_ReturnsCorrectResult(int a, int modulus, int expected)
    {
        // Act
        var result = a.Modulo(modulus);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Modulo_ZeroModulus_ThrowsDivideByZeroException()
    {
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => 10.Modulo(0));
    }

    [Theory]
    [InlineData(10, 3, 4)]
    [InlineData(9, 3, 3)]
    [InlineData(8, 3, 3)]
    [InlineData(7, 3, 3)]
    [InlineData(1, 3, 1)]
    [InlineData(0, 3, 0)]
    [InlineData(-1, 3, 0)]
    [InlineData(-2, 3, 0)]
    [InlineData(-3, 3, 0)]
    [InlineData(-4, 3, 0)]
    [InlineData(10, -3, -2)]
    [InlineData(-10, -3, 4)]
    public void CeilDiv_ReturnsCorrectResult(int a, int divisor, int expected)
    {
        // Act
        var result = a.CeilDiv(divisor);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CeilDiv_ZeroDivisor_ThrowsDivideByZeroException()
    {
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => 10.CeilDiv(0));
    }
}