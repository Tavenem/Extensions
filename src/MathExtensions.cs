using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tavenem;

/// <summary>
/// A collection of mathematical extension methods.
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// A floating-point value close to zero, intended to determine near-equivalence to 0.
    /// </summary>
    private const double NearlyZero = 1e-15;

    /// <summary>
    /// <para>
    /// A floating-point value close to zero, intended to determine near-equivalence to 0.
    /// </para>
    /// <para>
    /// This value has less precision than <see cref="NearlyZero"/>, and is better suited when
    /// checking <see cref="float"/> values.
    /// </para>
    /// </summary>
    private const float NearlyZeroFloat = 1e-6f;

    private static readonly Dictionary<char, byte> _Digits = new()
    {
        { '0', 0 },
        { '1', 1 },
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 },
    };
    private static readonly Dictionary<char, char> _DigitToSubscript = new()
    {
        { '0', '\u2080' },
        { '1', '\u2081' },
        { '2', '\u2082' },
        { '3', '\u2083' },
        { '4', '\u2084' },
        { '5', '\u2085' },
        { '6', '\u2086' },
        { '7', '\u2087' },
        { '8', '\u2088' },
        { '9', '\u2089' },
        { '+', '\u208A' },
        { '-', '\u208B' },
    };
    private static readonly Dictionary<char, char> _DigitToSuperscript = new()
    {
        { '0', '\u2070' },
        { '1', '\u00b9' },
        { '2', '\u00b2' },
        { '3', '\u00b3' },
        { '4', '\u2074' },
        { '5', '\u2075' },
        { '6', '\u2076' },
        { '7', '\u2077' },
        { '8', '\u2078' },
        { '9', '\u2079' },
        { '+', '\u207A' },
        { '-', '\u207B' },
    };
    private static readonly Dictionary<char, byte> _SubscriptDigits = new()
    {
        { '\u2080', 0 },
        { '\u2081', 1 },
        { '\u2082', 2 },
        { '\u2083', 3 },
        { '\u2084', 4 },
        { '\u2085', 5 },
        { '\u2086', 6 },
        { '\u2087', 7 },
        { '\u2088', 8 },
        { '\u2089', 9 },
    };
    private static readonly Dictionary<char, byte> _SuperscriptDigits = new()
    {
        { '\u2070', 0 },
        { '\u00b9', 1 },
        { '\u00b2', 2 },
        { '\u00b3', 3 },
        { '\u2074', 4 },
        { '\u2075', 5 },
        { '\u2076', 6 },
        { '\u2077', 7 },
        { '\u2078', 8 },
        { '\u2079', 9 },
    };

    /// <summary>
    /// Computes the angle between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The angle between the vectors, in radians.</returns>
    public static double Angle(this Vector3 value1, Vector3 value2)
        => Math.Atan2(Vector3.Cross(value1, value2).Length(), Vector3.Dot(value1, value2));

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this byte value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this double value) => IsNearlyZero(value) ? 0 : value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static float Cube(this float value) => IsNearlyZero(value) ? 0 : value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this int value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this long value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this sbyte value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this short value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this uint value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this ulong value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this ushort value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static T Cube<T>(this T value) where T : IMultiplyOperators<T, T, T> => value * value * value;

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The weight cannot be computed for the
    /// given parameters.</exception>
    /// <remarks>
    /// An <see cref="ArgumentOutOfRangeException"/> will be thrown if the given values are
    /// nearly equal, but the given result is not also nearly equal to them, since the
    /// calculation in that case would require a division by zero.
    /// </remarks>
    public static decimal InverseLerp(this decimal first, decimal second, decimal result)
    {
        var difference = second - first;
        if (difference == 0)
        {
            if (result == first)
            {
                return 0.5m;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
    /// cref="double.NaN"/> if the weight cannot be computed for the given parameters.</returns>
    /// <remarks>
    /// <see cref="double.NaN"/> will be returned if the given values are nearly equal, but the
    /// given result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public static double InverseLerp(this double first, double second, double result)
    {
        var difference = second - first;
        if (difference.IsNearlyZero())
        {
            if (result.IsNearlyEqualTo(first))
            {
                return 0.5;
            }
            else
            {
                return double.NaN;
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
    /// cref="float.NaN"/> if the weight cannot be computed for the given parameters.</returns>
    /// <remarks>
    /// <see cref="float.NaN"/> will be returned if the given values are nearly equal, but the
    /// given result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public static float InverseLerp(this float first, float second, float result)
    {
        var difference = second - first;
        if (difference.IsNearlyZero())
        {
            if (result.IsNearlyEqualTo(first))
            {
                return 0.5f;
            }
            else
            {
                return float.NaN;
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>
    /// The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
    /// cref="IFloatingPoint{TSelf}.NaN"/> if the weight cannot be computed for the given parameters.
    /// </returns>
    /// <remarks>
    /// <see cref="IFloatingPoint{TSelf}.NaN"/> will be returned if the given values are nearly equal, but the
    /// given result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public static T InverseLerp<T>(this T first, T second, T result) where T : IComparableToZero<T>
    {
        var difference = second - first;
        if (difference.IsNearlyZero())
        {
            if (result.IsNearlyEqualTo(first))
            {
                return T.One / (T.One + T.One);
            }
            else
            {
                return T.NaN;
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NearlyZero"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero(this double value) => value is < NearlyZero and > (-NearlyZero);

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NearlyZeroFloat"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero(this float value) => value is < NearlyZeroFloat and > (-NearlyZeroFloat);

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="IComparableToZero{T}.NearlyZero"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero<T>(this T value) where T : IComparableToZero<T>
        => value < value.NearlyZero && value > (-value.NearlyZero);

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns><see langword="true"/> if the values are nearly equal; otherwise <see
    /// langword="false"/>.</returns>
    public static bool IsNearlyEqualTo(this double value, double other)
        => IsNearlyEqualTo(value, other, Math.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns><see langword="true"/> if the values are nearly equal; otherwise <see
    /// langword="false"/>.</returns>
    public static bool IsNearlyEqualTo(this float value, float other)
        => IsNearlyEqualTo(value, other, Math.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns>
    /// <see langword="true"/> if the values are nearly equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyEqualTo<T>(this T value, T other) where T : INumber<T>, IComparableToZero<T>
        => IsNearlyEqualTo(value, other, T.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// the given epsilon value.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <param name="epsilon">
    /// An epsilon value which determines the tolerance for near-equality between the values.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this value and the other are nearly equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyEqualTo<T>(this T value, T other, T epsilon) where T : INumber<T>
        => value == other || T.Abs(value - other) < epsilon;

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public static decimal Lerp(this decimal first, decimal second, decimal amount)
        => first + ((second - first) * amount);

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public static double Lerp(this double first, double second, double amount)
        => first + ((second - first) * amount);

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public static float Lerp(this float first, float second, float amount)
        => first + ((second - first) * amount);

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>
    /// If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.
    /// </para>
    /// </remarks>
    public static T Lerp<T>(this T first, T second, T amount)
        where T : IAdditionOperators<T, T, T>,
            ISubtractionOperators<T, T, T>,
            IMultiplyOperators<T, T, T>
        => first + ((second - first) * amount);

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="int"/>. Truncates to <see
    /// cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="int"/> to this value.</returns>
    public static int RoundToInt(this double value)
    {
        if (value < int.MinValue)
        {
            return int.MinValue;
        }
        else if (value > int.MaxValue)
        {
            return int.MaxValue;
        }
        else
        {
            return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="int"/>. Truncates to <see
    /// cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="int"/> to this value.</returns>
    public static int RoundToInt(this float value)
    {
        if (value < int.MinValue)
        {
            return int.MinValue;
        }
        else if (value > int.MaxValue)
        {
            return int.MaxValue;
        }
        else
        {
            return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="int"/>. Truncates to <see
    /// cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="int"/> to this value.</returns>
    public static int RoundToInt<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.Parse(int.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value < min)
        {
            return int.MinValue;
        }

        var max = T.Parse(int.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value > max)
        {
            return int.MaxValue;
        }

        return int.Parse(T.Round(value, MidpointRounding.AwayFromZero).ToString() ?? string.Empty);
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="long"/>. Truncates to <see
    /// cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="long"/> to this value.</returns>
    public static long RoundToLong(this double value)
    {
        if (value < long.MinValue)
        {
            return long.MinValue;
        }
        else if (value > long.MaxValue)
        {
            return long.MaxValue;
        }
        else
        {
            return Convert.ToInt64(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="long"/>. Truncates to <see
    /// cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="long"/> to this value.</returns>
    public static long RoundToLong(this float value)
    {
        if (value < long.MinValue)
        {
            return long.MinValue;
        }
        else if (value > long.MaxValue)
        {
            return long.MaxValue;
        }
        else
        {
            return Convert.ToInt64(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="long"/>. Truncates to <see
    /// cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="long"/> to this value.</returns>
    public static long RoundToLong<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.Parse(long.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value < min)
        {
            return long.MinValue;
        }

        var max = T.Parse(long.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value > max)
        {
            return long.MaxValue;
        }

        return long.Parse(T.Round(value, MidpointRounding.AwayFromZero).ToString() ?? string.Empty);
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="uint"/>. Truncates to 0 or
    /// <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="uint"/> to this value.</returns>
    public static uint RoundToUInt(this double value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > uint.MaxValue)
        {
            return uint.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="uint"/>. Truncates to 0 or
    /// <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="uint"/> to this value.</returns>
    public static uint RoundToUInt(this float value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > uint.MaxValue)
        {
            return uint.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="uint"/>. Truncates to 0 or
    /// <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="uint"/> to this value.</returns>
    public static uint RoundToUInt<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.Parse(uint.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value < min)
        {
            return uint.MinValue;
        }

        var max = T.Parse(uint.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value > max)
        {
            return uint.MaxValue;
        }

        return uint.Parse(T.Round(value, MidpointRounding.AwayFromZero).ToString() ?? string.Empty);
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="ulong"/>. Truncates to 0 or
    /// <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="ulong"/> to this value.</returns>
    public static ulong RoundToULong(this double value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > ulong.MaxValue)
        {
            return ulong.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="ulong"/>. Truncates to 0 or
    /// <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="ulong"/> to this value.</returns>
    public static ulong RoundToULong(this float value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > ulong.MaxValue)
        {
            return ulong.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="ulong"/>. Truncates to 0 or
    /// <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="ulong"/> to this value.</returns>
    public static ulong RoundToULong<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.Parse(ulong.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value < min)
        {
            return ulong.MinValue;
        }

        var max = T.Parse(ulong.MinValue.ToString(), NumberFormatInfo.InvariantInfo);
        if (value > max)
        {
            return ulong.MaxValue;
        }

        return ulong.Parse(T.Round(value, MidpointRounding.AwayFromZero).ToString() ?? string.Empty);
    }

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo(double, double)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static double SnapTo(this double value, double target)
        => value.IsNearlyEqualTo(target) ? target : value;

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo(float, float)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static float SnapTo(this float value, float target)
        => value.IsNearlyEqualTo(target) ? target : value;

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo{T}(T, T)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static T SnapTo<T>(this T value, T target) where T : INumber<T>, IComparableToZero<T>
        => value.IsNearlyEqualTo(target) ? target : value;

    /// <summary>
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
    /// cref="IsNearlyZero(double)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static double SnapToZero(this double value)
        => value.IsNearlyZero() ? 0 : value;

    /// <summary>
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
    /// cref="IsNearlyZero(float)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static float SnapToZero(this float value)
        => value.IsNearlyZero() ? 0 : value;

    /// <summary>
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
    /// cref="IsNearlyZero{T}(T)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static T SnapToZero<T>(this T value) where T : IComparableToZero<T>
        => value.IsNearlyZero() ? T.Zero : value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this byte value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this double value) => IsNearlyZero(value) ? 0 : value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static float Square(this float value) => IsNearlyZero(value) ? 0 : value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this int value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this long value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this sbyte value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this short value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this uint value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this ulong value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this ushort value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static T Square<T>(this T value) where T : IMultiplyOperators<T, T, T> => value * value;

    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="value">The number whose square root is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="value"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Zero or positive</term>
    /// <description>The positive square root of <paramref name="value"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description>An <see cref="ArgumentOutOfRangeException"/></description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// Uses the <see cref="Math.Sqrt(double)"/> function, then corrects the result using
    /// addition and division in a loop until the result is accurate to within the precision of
    /// a <see cref="decimal"/> value. In the worst case, the loop will iterate at most three
    /// times.
    /// </remarks>
    public static decimal Sqrt(this decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        var current = (decimal)Math.Sqrt((double)value);
        decimal previous;
        do
        {
            previous = current;
            if (previous == 0m)
            {
                return 0;
            }
            current = (previous + (value / previous)) / 2;
        } while (Math.Abs(previous - current) > 0);
        return current;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a subscript representation.
    /// </summary>
    /// <param name="value">A number to render as a subscript string.</param>
    /// <param name="formatProvider">
    /// The provider to use to format the value. -or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.
    /// </param>
    /// <returns>The subscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSubscript<T>(this T value, IFormatProvider? formatProvider = null) where T : IFormattable
    {
        var s = value.ToString(null, formatProvider);

        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSubscript.TryGetValue(c, out var digit))
            {
                sb.Insert(0, digit);
            }
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a subscript representation.
    /// </summary>
    /// <param name="value">A number to render as a subscript string.</param>
    /// <param name="format">
    /// The format to use. -or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the <see cref="IFormattable"/> implementation.
    /// </param>
    /// <param name="formatProvider">
    /// The provider to use to format the value. -or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.
    /// </param>
    /// <returns>The subscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSubscript<T>(this T value, string? format = null, IFormatProvider? formatProvider = null) where T : IFormattable
    {
        var s = value.ToString(format, formatProvider);

        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSubscript.TryGetValue(c, out var digit))
            {
                sb.Append(digit);
            }
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a superscript representation.
    /// </summary>
    /// <param name="value">A number to render as a superscript string.</param>
    /// <param name="format">
    /// The format to use. -or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the <see cref="IFormattable"/> implementation.
    /// </param>
    /// <param name="formatProvider">
    /// The provider to use to format the value. -or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.
    /// </param>
    /// <param name="positiveSign">
    /// Causes the superscript string to contain the character '⁺' when non-negative.
    /// </param>
    /// <param name="postfixSign">
    /// Causes the superscript string to end with a sign, overriding the value of <see cref="NumberFormatInfo.NumberNegativePattern"/>.
    /// </param>
    /// <returns>The superscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSuperscript<T>(
        this T value,
        string? format = null,
        IFormatProvider? formatProvider = null,
        bool positiveSign = false,
        bool postfixSign = false) where T : ISignedNumber<T>, IFormattable
    {
        var formatInfo = NumberFormatInfo.GetInstance(formatProvider);
        if (postfixSign)
        {
            formatInfo.NumberNegativePattern = formatInfo.NumberNegativePattern == 2
                ? 4
                : 3;
        }

        var s = value.ToString(format, formatInfo);

        var separator = formatInfo.NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        var showPositive = value.CompareTo(T.Zero) > 0
            && positiveSign;
        if (showPositive
            && formatInfo.NumberNegativePattern < 3)
        {
            sb.Append('\u207A');
        }
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSuperscript.TryGetValue(c, out var digit))
            {
                sb.Append(digit);
            }
        }
        if (showPositive
            && formatInfo.NumberNegativePattern >= 3)
        {
            sb.Append('\u207A');
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a superscript representation.
    /// </summary>
    /// <param name="value">A number to render as a superscript string.</param>
    /// <param name="formatProvider">
    /// The provider to use to format the value. -or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.
    /// </param>
    /// <param name="positiveSign">
    /// Causes the superscript string to contain the character '⁺' when non-negative.
    /// </param>
    /// <param name="postfixSign">
    /// Causes the superscript string to end with a sign, overriding the value of <see cref="NumberFormatInfo.NumberNegativePattern"/>.
    /// </param>
    /// <returns>The superscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSuperscript<T>(
        this T value,
        IFormatProvider? formatProvider = null,
        bool positiveSign = false,
        bool postfixSign = false) where T : ISignedNumber<T>, IFormattable
    {
        var formatInfo = NumberFormatInfo.GetInstance(formatProvider);
        if (postfixSign)
        {
            formatInfo.NumberNegativePattern = formatInfo.NumberNegativePattern == 2
                ? 4
                : 3;
        }

        var s = value.ToString(null, formatInfo);

        var separator = formatInfo.NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        var showPositive = value.CompareTo(T.Zero) > 0
            && positiveSign;
        if (showPositive
            && formatInfo.NumberNegativePattern < 3)
        {
            sb.Append('\u207A');
        }
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSuperscript.TryGetValue(c, out var digit))
            {
                sb.Append(digit);
            }
        }
        if (showPositive
            && formatInfo.NumberNegativePattern >= 3)
        {
            sb.Append('\u207A');
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a superscript representation.
    /// </summary>
    /// <param name="value">A number to render as a superscript string.</param>
    /// <param name="positiveSign">
    /// Causes the superscript string to contain the character '⁺' when non-negative.
    /// </param>
    /// <param name="postfixSign">
    /// Causes the superscript string to end with a sign, overriding the value of <see cref="NumberFormatInfo.NumberNegativePattern"/>.
    /// </param>
    /// <returns>The superscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSuperscript<T>(
        this T value,
        bool positiveSign = false,
        bool postfixSign = false) where T : ISignedNumber<T>, IFormattable
    {
        var formatInfo = NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture);
        if (postfixSign)
        {
            formatInfo.NumberNegativePattern = formatInfo.NumberNegativePattern == 2
                ? 4
                : 3;
        }

        var s = value.ToString(null, formatInfo);

        var separator = formatInfo.NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        var showPositive = value.CompareTo(T.Zero) > 0
            && positiveSign;
        if (showPositive
            && formatInfo.NumberNegativePattern < 3)
        {
            sb.Append('\u207A');
        }
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSuperscript.TryGetValue(c, out var digit))
            {
                sb.Append(digit);
            }
        }
        if (showPositive
            && formatInfo.NumberNegativePattern >= 3)
        {
            sb.Append('\u207A');
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a superscript representation.
    /// </summary>
    /// <param name="value">A number to render as a superscript string.</param>
    /// <param name="format">
    /// The format to use. -or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the <see cref="IFormattable"/> implementation.
    /// </param>
    /// <param name="formatProvider">
    /// The provider to use to format the value. -or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.
    /// </param>
    /// <returns>The superscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSuperscript<T>(
        this T value,
        string? format = null,
        IFormatProvider? formatProvider = null) where T : IFormattable
    {
        var s = value.ToString(format, formatProvider);

        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSuperscript.TryGetValue(c, out var digit))
            {
                sb.Append(digit);
            }
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Converts <paramref name="value"/> to a superscript representation.
    /// </summary>
    /// <param name="value">A number to render as a superscript string.</param>
    /// <param name="formatProvider">
    /// The provider to use to format the value. -or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.
    /// </param>
    /// <returns>The superscript representation of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Due to Unicode limitations, only digits, the '+' character, and the '-' character are converted.
    /// Any other characters produced by the string representation of <paramref name="value"/> will be discarded.
    /// If this would result in an empty string, the original string is returned unaltered.
    /// </para>
    /// <para>
    /// To preserve accuracy, any characters following the <see cref="NumberFormatInfo.NumberDecimalSeparator"/>
    /// (if present) will be discarded, since the <see cref="NumberFormatInfo.NumberDecimalSeparator"/> itself
    /// cananot be represented.
    /// </para>
    /// </remarks>
    public static string ToSuperscript<T>(
        this T value,
        IFormatProvider? formatProvider = null) where T : IFormattable
    {
        var s = value.ToString(null, formatProvider);

        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberDecimalSeparator.AsSpan();
        var sb = new StringBuilder();
        foreach (var c in s)
        {
            if (separator.Equals(new ReadOnlySpan<char>(new[] { c }), StringComparison.InvariantCulture))
            {
                break;
            }
            if (_DigitToSuperscript.TryGetValue(c, out var digit))
            {
                sb.Append(digit);
            }
        }
        return sb.Length > 0
            ? sb.ToString()
            : s;
    }

    /// <summary>
    /// Attempts to parse the given character as a digit.
    /// </summary>
    /// <param name="c">The character to parse.</param>
    /// <param name="value">
    /// The digit represented by <paramref name="c"/>, or 0 if <paramref name="c"/> could not be
    /// parsed as a digit.
    /// </param>
    /// <param name="superscript">
    /// <see langword="true"/> if the character is the superscript version of the parsed digit.
    /// </param>
    /// <param name="subscript">
    /// <see langword="true"/> if the character is the subscript version of the parsed digit.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the character could be successfully parsed as a digit;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryParseDigit(this char c, out byte value, out bool superscript, out bool subscript)
    {
        superscript = false;
        subscript = false;
        if (_Digits.TryGetValue(c, out value))
        {
            return true;
        }
        if (_SubscriptDigits.TryGetValue(c, out value))
        {
            subscript = true;
            return true;
        }
        if (_SuperscriptDigits.TryGetValue(c, out value))
        {
            superscript = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static float GetEpsilon(this float value) => value * NearlyZeroFloat;

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static double GetEpsilon(this double value) => value * NearlyZero;

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static T GetEpsilon<T>(this T value) where T : IMultiplyOperators<T, T, T>, IComparableToZero<T>
        => value * value.NearlyZero;
}
