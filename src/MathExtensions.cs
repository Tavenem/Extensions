using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tavenem
{
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
        private static readonly Dictionary<byte, char> _Subscripts = new()
        {
            { 0, '\u2080' },
            { 1, '\u2081' },
            { 2, '\u2082' },
            { 3, '\u2083' },
            { 4, '\u2084' },
            { 5, '\u2085' },
            { 6, '\u2086' },
            { 7, '\u2087' },
            { 8, '\u2088' },
            { 9, '\u2089' },
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
        private static readonly Dictionary<byte, char> _Superscripts = new()
        {
            { 0, '\u2070' },
            { 1, '\u00b9' },
            { 2, '\u00b2' },
            { 3, '\u00b3' },
            { 4, '\u2074' },
            { 5, '\u2075' },
            { 6, '\u2076' },
            { 7, '\u2077' },
            { 8, '\u2078' },
            { 9, '\u2079' },
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
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <c>6.Clamp(3, 5)</c>
        /// would.
        /// </para>
        /// </remarks>
        public static byte Clamp(this byte value, byte min, byte max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <c>6.Clamp(3, 5)</c>
        /// would.
        /// </para>
        /// </remarks>
        public static byte Clamp(this byte value, long min, long max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min < byte.MinValue)
                {
                    return byte.MinValue;
                }
                else if (min > byte.MaxValue)
                {
                    return byte.MaxValue;
                }
                else
                {
                    return (byte)min;
                }
            }
            else if (value > max)
            {
                if (max < byte.MinValue)
                {
                    return byte.MinValue;
                }
                else if (max > byte.MaxValue)
                {
                    return byte.MaxValue;
                }
                else
                {
                    return (byte)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static byte Clamp(this byte value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < byte.MinValue)
                {
                    return byte.MinValue;
                }
                else if (r > byte.MaxValue)
                {
                    return byte.MaxValue;
                }
                else
                {
                    return (byte)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < byte.MinValue)
                {
                    return byte.MinValue;
                }
                else if (r > byte.MaxValue)
                {
                    return byte.MaxValue;
                }
                else
                {
                    return (byte)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static decimal Clamp(this decimal value, decimal min, decimal max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static double Clamp(this double value, double min, double max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static float Clamp(this float value, float min, float max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static float Clamp(this float value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min < float.MinValue)
                {
                    return float.MinValue;
                }
                else if (min > float.MaxValue)
                {
                    return float.MaxValue;
                }
                else
                {
                    return (float)min;
                }
            }
            else if (value > max)
            {
                if (max < float.MinValue)
                {
                    return float.MinValue;
                }
                else if (max > float.MaxValue)
                {
                    return float.MaxValue;
                }
                else
                {
                    return (float)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static int Clamp(this int value, int min, int max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static int Clamp(this int value, long min, long max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min < int.MinValue)
                {
                    return int.MinValue;
                }
                else if (min > int.MaxValue)
                {
                    return int.MaxValue;
                }
                else
                {
                    return (int)min;
                }
            }
            else if (value > max)
            {
                if (max < int.MinValue)
                {
                    return int.MinValue;
                }
                else if (max > int.MaxValue)
                {
                    return int.MaxValue;
                }
                else
                {
                    return (int)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static int Clamp(this int value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < int.MinValue)
                {
                    return int.MinValue;
                }
                else if (r > int.MaxValue)
                {
                    return int.MaxValue;
                }
                else
                {
                    return (int)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < int.MinValue)
                {
                    return int.MinValue;
                }
                else if (r > int.MaxValue)
                {
                    return int.MaxValue;
                }
                else
                {
                    return (int)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static long Clamp(this long value, long min, long max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static long Clamp(this long value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < long.MinValue)
                {
                    return long.MinValue;
                }
                else if (r > long.MaxValue)
                {
                    return long.MaxValue;
                }
                else
                {
                    return (long)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < long.MinValue)
                {
                    return long.MinValue;
                }
                else if (r > long.MaxValue)
                {
                    return long.MaxValue;
                }
                else
                {
                    return (long)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static sbyte Clamp(this sbyte value, sbyte min, sbyte max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static sbyte Clamp(this sbyte value, long min, long max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min < sbyte.MinValue)
                {
                    return sbyte.MinValue;
                }
                else if (min > sbyte.MaxValue)
                {
                    return sbyte.MaxValue;
                }
                else
                {
                    return (sbyte)min;
                }
            }
            else if (value > max)
            {
                if (max < sbyte.MinValue)
                {
                    return sbyte.MinValue;
                }
                else if (max > sbyte.MaxValue)
                {
                    return sbyte.MaxValue;
                }
                else
                {
                    return (sbyte)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static sbyte Clamp(this sbyte value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < sbyte.MinValue)
                {
                    return sbyte.MinValue;
                }
                else if (r > sbyte.MaxValue)
                {
                    return sbyte.MaxValue;
                }
                else
                {
                    return (sbyte)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < sbyte.MinValue)
                {
                    return sbyte.MinValue;
                }
                else if (r > sbyte.MaxValue)
                {
                    return sbyte.MaxValue;
                }
                else
                {
                    return (sbyte)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static short Clamp(this short value, short min, short max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static short Clamp(this short value, long min, long max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min < short.MinValue)
                {
                    return short.MinValue;
                }
                else if (min > short.MaxValue)
                {
                    return short.MaxValue;
                }
                else
                {
                    return (short)min;
                }
            }
            else if (value > max)
            {
                if (max < short.MinValue)
                {
                    return short.MinValue;
                }
                else if (max > short.MaxValue)
                {
                    return short.MaxValue;
                }
                else
                {
                    return (short)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static short Clamp(this short value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < short.MinValue)
                {
                    return short.MinValue;
                }
                else if (r > short.MaxValue)
                {
                    return short.MaxValue;
                }
                else
                {
                    return (short)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < short.MinValue)
                {
                    return short.MinValue;
                }
                else if (r > short.MaxValue)
                {
                    return short.MaxValue;
                }
                else
                {
                    return (short)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static uint Clamp(this uint value, uint min, uint max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static uint Clamp(this uint value, ulong min, ulong max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min > uint.MaxValue)
                {
                    return uint.MaxValue;
                }
                else
                {
                    return (uint)min;
                }
            }
            else if (value > max)
            {
                if (max > uint.MaxValue)
                {
                    return uint.MaxValue;
                }
                else
                {
                    return (uint)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static uint Clamp(this uint value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < uint.MinValue)
                {
                    return uint.MinValue;
                }
                else if (r > uint.MaxValue)
                {
                    return uint.MaxValue;
                }
                else
                {
                    return (uint)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < uint.MinValue)
                {
                    return uint.MinValue;
                }
                else if (r > uint.MaxValue)
                {
                    return uint.MaxValue;
                }
                else
                {
                    return (uint)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static ulong Clamp(this ulong value, ulong min, ulong max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static ulong Clamp(this ulong value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < ulong.MinValue)
                {
                    return ulong.MinValue;
                }
                else if (r > ulong.MaxValue)
                {
                    return ulong.MaxValue;
                }
                else
                {
                    return (ulong)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < ulong.MinValue)
                {
                    return ulong.MinValue;
                }
                else if (r > ulong.MaxValue)
                {
                    return ulong.MaxValue;
                }
                else
                {
                    return (ulong)r;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static ushort Clamp(this ushort value, ushort min, ushort max) => Math.Clamp(value, min, max);

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static ushort Clamp(this ushort value, ulong min, ulong max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                if (min > ushort.MaxValue)
                {
                    return ushort.MaxValue;
                }
                else
                {
                    return (ushort)min;
                }
            }
            else if (value > max)
            {
                if (max > ushort.MaxValue)
                {
                    return ushort.MaxValue;
                }
                else
                {
                    return (ushort)max;
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <paramref name="value"/> if <paramref name="min"/> ≤ value ≤ <paramref name="max"/>.
        /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/> -or-
        /// <paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> or <paramref name="max"/> are beyond the limits of the return
        /// type, the value is truncated at the min or max value for the type.
        /// </para>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <code>6.Clamp(3,
        /// 5)</code> would.
        /// </para>
        /// </remarks>
        public static ushort Clamp(this ushort value, double min, double max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
            }
            if (value < min)
            {
                var r = Math.Round(min);
                if (r < ushort.MinValue)
                {
                    return ushort.MinValue;
                }
                else if (r > ushort.MaxValue)
                {
                    return ushort.MaxValue;
                }
                else
                {
                    return (ushort)r;
                }
            }
            else if (value > max)
            {
                var r = Math.Round(max);
                if (r < ushort.MinValue)
                {
                    return ushort.MinValue;
                }
                else if (r > ushort.MaxValue)
                {
                    return ushort.MaxValue;
                }
                else
                {
                    return (ushort)r;
                }
            }
            else
            {
                return value;
            }
        }

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
        public static decimal Cube(this decimal value) => value * value * value;

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
        /// Determines if a floating-point value is nearly zero (closer to zero than 1e-15).
        /// </summary>
        /// <param name="value">A value to test.</param>
        /// <returns>
        /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsNearlyZero(this double value) => value is < NearlyZero and > (-NearlyZero);

        /// <summary>
        /// Determines if a floating-point value is nearly zero (closer to zero than 1e-6).
        /// </summary>
        /// <param name="value">A value to test.</param>
        /// <returns>
        /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsNearlyZero(this float value) => value is < NearlyZeroFloat and > (-NearlyZeroFloat);

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
        /// the given epsilon value.
        /// </summary>
        /// <param name="value">This value.</param>
        /// <param name="other">The other value.</param>
        /// <param name="epsilon">
        /// An epsilon value which determines the tolerance for near-equality between the values.
        /// </param>
        /// <returns><see langword="true"/> if this value and the other are nearly equal; otherwise
        /// <see langword="false"/>.</returns>
        public static bool IsNearlyEqualTo(this double value, double other, double epsilon)
            => value == other || Math.Abs(value - other) < epsilon;

        /// <summary>
        /// Determines if floating-point values are nearly equal, within a tolerance determined by
        /// the given epsilon value.
        /// </summary>
        /// <param name="value">This value.</param>
        /// <param name="other">The other value.</param>
        /// <param name="epsilon">
        /// An epsilon value which determines the tolerance for near-equality between the values.
        /// </param>
        /// <returns><see langword="true"/> if this value and the other are nearly equal; otherwise
        /// <see langword="false"/>.</returns>
        public static bool IsNearlyEqualTo(this float value, float other, float epsilon)
            => value == other || Math.Abs(value - other) < epsilon;

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
        /// Gets an appropriate epsilon for floating-point equality comparisons based on the
        /// magnitude of the given value.
        /// </summary>
        /// <param name="value">The value whose equality-comparison epsilon should be
        /// calculated.</param>
        private static double GetEpsilon(this double value) => value * NearlyZero;

        /// <summary>
        /// Gets an appropriate epsilon for floating-point equality comparisons based on the
        /// magnitude of the given value.
        /// </summary>
        /// <param name="value">The value whose equality-comparison epsilon should be
        /// calculated.</param>
        private static double GetEpsilon(this float value) => value * NearlyZeroFloat;

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
        /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
        /// <see cref="IsNearlyEqualTo(double, double)"/>), or <paramref name="value"/> itself if not.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <param name="target">The value to snap to.</param>
        /// <returns><paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
        /// otherwise <paramref name="value"/>.</returns>
        public static double SnapTo(this double value, double target)
            => value.IsNearlyEqualTo(target) ? target : value;

        /// <summary>
        /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
        /// <see cref="IsNearlyEqualTo(float, float)"/>), or <paramref name="value"/> itself if not.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <param name="target">The value to snap to.</param>
        /// <returns><paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
        /// otherwise <paramref name="value"/>.</returns>
        public static float SnapTo(this float value, float target)
            => value.IsNearlyEqualTo(target) ? target : value;

        /// <summary>
        /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
        /// cref="IsNearlyZero(double)"/>), or <paramref name="value"/> itself if not.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns>Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
        /// name="value"/>.</returns>
        public static double SnapToZero(this double value)
            => value.IsNearlyZero() ? 0 : value;

        /// <summary>
        /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
        /// cref="IsNearlyZero(float)"/>), or <paramref name="value"/> itself if not.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns>Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
        /// name="value"/>.</returns>
        public static float SnapToZero(this float value)
            => value.IsNearlyZero() ? 0 : value;

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
        public static decimal Square(this decimal value) => value * value;

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
        /// Uses the <see cref="Math.Sqrt(double)"/> function, then corrects the reuslt using
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
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this byte value)
        {
            var sb = new StringBuilder();

            foreach (var digit in GetDigits(value))
            {
                sb.Insert(0, _Subscripts[digit]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this int value)
        {
            var sb = new StringBuilder();

            foreach (var digit in GetDigits(value))
            {
                sb.Insert(0, _Subscripts[digit]);
            }
            if (value < 0)
            {
                sb.Insert(0, '\u208B');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this long value)
        {
            var sb = new StringBuilder();

            foreach (var digit in GetDigits(value))
            {
                sb.Insert(0, _Subscripts[digit]);
            }
            if (value < 0)
            {
                sb.Insert(0, '\u208B');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this sbyte value) => ((int)value).ToSubscript();

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this short value) => ((int)value).ToSubscript();

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this uint value)
        {
            var sb = new StringBuilder();

            foreach (var digit in GetDigits(value))
            {
                sb.Insert(0, _Subscripts[digit]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this ulong value)
        {
            var sb = new StringBuilder();

            foreach (var digit in GetDigits(value))
            {
                sb.Insert(0, _Subscripts[digit]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a subscript representation.
        /// </summary>
        /// <param name="value">A number to render as a subscript string.</param>
        /// <returns>The subscript representation of <paramref name="value"/>.</returns>
        public static string ToSubscript(this ushort value) => ((uint)value).ToSubscript();

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this byte value, bool positiveSign = false, bool postfixSign = false)
        {
            var sb = new StringBuilder();

            if (positiveSign && !postfixSign)
            {
                sb.Append('\u207A');
            }
            foreach (var digit in GetDigits(value))
            {
                sb.Insert(positiveSign && !postfixSign ? 1 : 0, _Superscripts[digit]);
            }
            if (postfixSign)
            {
                sb.Append('\u207A');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this int value, bool positiveSign = false, bool postfixSign = false)
        {
            var sb = new StringBuilder();

            var hasSign = value < 0 || positiveSign;
            if (hasSign && !postfixSign)
            {
                sb.Append(value < 0
                    ? '\u207B'
                    : '\u207A');
            }
            foreach (var digit in GetDigits(value))
            {
                sb.Insert(hasSign && !postfixSign ? 1 : 0, _Superscripts[digit]);
            }
            if (postfixSign)
            {
                sb.Append(value < 0
                    ? '\u207B'
                    : '\u207A');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this long value, bool positiveSign = false, bool postfixSign = false)
        {
            var sb = new StringBuilder();

            var hasSign = value < 0 || positiveSign;
            if (hasSign && !postfixSign)
            {
                sb.Append(value < 0
                    ? '\u207B'
                    : '\u207A');
            }
            foreach (var digit in GetDigits(value))
            {
                sb.Insert(hasSign && !postfixSign ? 1 : 0, _Superscripts[digit]);
            }
            if (postfixSign)
            {
                sb.Append(value < 0
                    ? '\u207B'
                    : '\u207A');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this sbyte value, bool positiveSign = false, bool postfixSign = false)
            => ((int)value).ToSuperscript(positiveSign, postfixSign);

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this short value, bool positiveSign = false, bool postfixSign = false)
            => ((int)value).ToSuperscript(positiveSign, postfixSign);

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this uint value, bool positiveSign = false, bool postfixSign = false)
        {
            var sb = new StringBuilder();

            if (positiveSign && !postfixSign)
            {
                sb.Append('\u207A');
            }
            foreach (var digit in GetDigits(value))
            {
                sb.Insert(positiveSign && !postfixSign ? 1 : 0, _Superscripts[digit]);
            }
            if (postfixSign)
            {
                sb.Append('\u207A');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this ulong value, bool positiveSign = false, bool postfixSign = false)
        {
            var sb = new StringBuilder();

            if (positiveSign && !postfixSign)
            {
                sb.Append('\u207A');
            }
            foreach (var digit in GetDigits(value))
            {
                sb.Insert(positiveSign && !postfixSign ? 1 : 0, _Superscripts[digit]);
            }
            if (postfixSign)
            {
                sb.Append('\u207A');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a superscript representation.
        /// </summary>
        /// <param name="value">A number to render as a superscript string.</param>
        /// <param name="positiveSign">
        /// Causes the superscript string to contain a '+' when non-negative.
        /// </param>
        /// <param name="postfixSign">
        /// Causes the superscript string to end with a sign (otherwise it begins with the sign, when present).
        /// </param>
        /// <returns>The superscript representation of <paramref name="value"/>.</returns>
        public static string ToSuperscript(this ushort value, bool positiveSign = false, bool postfixSign = false)
            => ((uint)value).ToSuperscript(positiveSign, postfixSign);

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

        private static IEnumerable<byte> GetDigits(byte value) => GetDigits((uint)value);

        private static IEnumerable<byte> GetDigits(int value)
        {
            if (value == 0)
            {
                yield return 0;
                yield break;
            }
            value = Math.Abs(value);
            while (value > 0)
            {
                yield return (byte)(value % 10);
                value /= 10;
            }
        }

        private static IEnumerable<byte> GetDigits(long value)
        {
            if (value == 0)
            {
                yield return 0;
                yield break;
            }
            value = Math.Abs(value);
            while (value > 0)
            {
                yield return (byte)(value % 10);
                value /= 10;
            }
        }

        private static IEnumerable<byte> GetDigits(uint value)
        {
            if (value == 0)
            {
                yield return 0;
                yield break;
            }
            while (value > 0)
            {
                yield return (byte)(value % 10);
                value /= 10;
            }
        }

        private static IEnumerable<byte> GetDigits(ulong value)
        {
            if (value == 0)
            {
                yield return 0;
                yield break;
            }
            while (value > 0)
            {
                yield return (byte)(value % 10);
                value /= 10;
            }
        }
    }
}
