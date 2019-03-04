﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionLib
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the first element of the sequence or null if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> from which to return an element.</param>
        /// <returns>
        /// null if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source) where TSource : struct
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Any())
            {
                return source.First();
            }
            return null;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or null if no such
        /// element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// null if <paramref name="source"/> is empty or if no element passes the test specified by
        /// <paramref name="predicate"/>; otherwise, the first element in <paramref name="source"/>
        /// that passes the test specified by <paramref name="predicate"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TSource : struct
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMax<TSource>(this IEnumerable<TSource> source) where TSource : IComparable
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var maxIndex = -1;
            var max = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (enumerator.Current.CompareTo(max) > 0)
                {
                    max = enumerator.Current;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var maxIndex = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var maxIndex = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var maxIndex = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var maxIndex = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="comparer">A comparer used to compare instances of the sequence.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="comparer"/> is null.
        /// </exception>
        public static int IndexOfMax<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var maxIndex = -1;
            var max = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (comparer.Compare(enumerator.Current, max) > 0)
                {
                    max = enumerator.Current;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMax<TSource>(this IEnumerable<TSource> source) where TSource : IComparable
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var maxIndex = -1L;
            var max = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (enumerator.Current.CompareTo(max) > 0)
                {
                    max = enumerator.Current;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var maxIndex = -1L;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var maxIndex = -1L;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var maxIndex = -1L;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var maxIndex = -1L;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the maximum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="comparer">A comparer used to compare instances of the sequence.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="comparer"/> is null.
        /// </exception>
        public static long LongIndexOfMax<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var maxIndex = -1L;
            var max = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (comparer.Compare(enumerator.Current, max) > 0)
                {
                    max = enumerator.Current;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <returns>The index of the first occurrence of the minimum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMin<TSource>(this IEnumerable<TSource> source) where TSource : IComparable
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var minIndex = -1;
            var min = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (enumerator.Current.CompareTo(min) < 0)
                {
                    min = enumerator.Current;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var minIndex = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var minIndex = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var minIndex = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static int IndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var minIndex = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="comparer">A comparer used to compare instances of the sequence.</param>
        /// <returns>The index of the first occurrence of the minimum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="comparer"/> is null.
        /// </exception>
        public static int IndexOfMin<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1;
            var minIndex = -1;
            var min = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (comparer.Compare(enumerator.Current, min) < 0)
                {
                    min = enumerator.Current;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <returns>The index of the first occurrence of the minimum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMin<TSource>(this IEnumerable<TSource> source) where TSource : IComparable
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var minIndex = -1L;
            var min = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (enumerator.Current.CompareTo(min) < 0)
                {
                    min = enumerator.Current;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var minIndex = -1L;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var minIndex = -1L;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var minIndex = -1L;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The index of the first occurrence of the maximum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static long LongIndexOfMin<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var minIndex = -1L;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the index of the first occurrence of the minimum value of a sequence, or -1 if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="comparer">A comparer used to compare instances of the sequence.</param>
        /// <returns>The index of the first occurrence of the minimum value of the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="comparer"/> is null.
        /// </exception>
        public static long LongIndexOfMin<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (!source.Any())
            {
                return -1;
            }

            var index = -1L;
            var minIndex = -1L;
            var min = source.First();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                if (comparer.Compare(enumerator.Current, min) < 0)
                {
                    min = enumerator.Current;
                    minIndex = index;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the maximum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the maximum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource maxItem = default;

            if (!source.Any())
            {
                return maxItem;
            }

            var index = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxItem = enumerator.Current;
                }
            }
            return maxItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the maximum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the maximum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource maxItem = default;

            if (!source.Any())
            {
                return maxItem;
            }

            var index = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxItem = enumerator.Current;
                }
            }
            return maxItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the maximum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the maximum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource maxItem = default;

            if (!source.Any())
            {
                return maxItem;
            }

            var index = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxItem = enumerator.Current;
                }
            }
            return maxItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the maximum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the maximum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource maxItem = default;

            if (!source.Any())
            {
                return maxItem;
            }

            var index = -1;
            var max = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value > max)
                {
                    max = value;
                    maxItem = enumerator.Current;
                }
            }
            return maxItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the minimum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the minimum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMin<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource minItem = default;

            if (!source.Any())
            {
                return minItem;
            }

            var index = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minItem = enumerator.Current;
                }
            }
            return minItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the minimum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the minimum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMin<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource minItem = default;

            if (!source.Any())
            {
                return minItem;
            }

            var index = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minItem = enumerator.Current;
                }
            }
            return minItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the minimum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the minimum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMin<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource minItem = default;

            if (!source.Any())
            {
                return minItem;
            }

            var index = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minItem = enumerator.Current;
                }
            }
            return minItem;
        }

        /// <summary>
        /// Finds the first element for whom the given function produces the minimum value, or the <see
        /// langword="default"/> value of <typeparamref name="TSource"/> if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return an element from.</param>
        /// <param name="selector">A function which selects a value from each element.</param>
        /// <returns>The first element in the sequence with the minimum value for <paramref
        /// name="selector"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static TSource ItemWithMin<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource minItem = default;

            if (!source.Any())
            {
                return minItem;
            }

            var index = -1;
            var min = selector.Invoke(source.First());
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                index++;
                var value = selector.Invoke(enumerator.Current);
                if (value < min)
                {
                    min = value;
                    minItem = enumerator.Current;
                }
            }
            return minItem;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form, omitting <see langword="null"/>
        /// results and returning the values in non-nullable form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectHasValue<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> selector) where TResult : struct
            => source.Select(selector).Where(x => x.HasValue).Select(x => x.Value);

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's
        /// index, omitting <see langword="null"/> results and returning the values in non-nullable
        /// form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element; the second
        /// parameter of the function represents the index of the source element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectHasValue<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult?> selector) where TResult : struct
            => source.Select(selector).Where(x => x.HasValue).Select(x => x.Value);

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the
        /// resulting sequences into one sequence, omitting <see langword="null"/>
        /// results and returning the values in non-nullable form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// one-to-many transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectManyHasValue<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult?>> selector) where TResult : struct
            => source.SelectMany(selector).Where(x => x.HasValue).Select(x => x.Value);

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the
        /// resulting sequences into one sequence, omitting <see langword="null"/>
        /// results and returning the values in non-nullable form. The index of each source element
        /// is used in the projected form of that element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element; the second
        /// parameter of the function represents the index of the source element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// one-to-many transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectManyHasValue<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult?>> selector) where TResult : struct
            => source.SelectMany(selector).Where(x => x.HasValue).Select(x => x.Value);

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the
        /// resulting sequences into one sequence, omitting <see langword="null"/>
        /// results.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// one-to-many transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectManyNonNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector) where TResult : class
            => source.SelectMany(selector).Where(x => x != null);

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the
        /// resulting sequences into one sequence, omitting <see langword="null"/>
        /// results and returning the values in non-nullable form. The index of each source element
        /// is used in the projected form of that element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element; the second
        /// parameter of the function represents the index of the source element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// one-to-many transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectManyNonNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector) where TResult : class
            => source.SelectMany(selector).Where(x => x != null);

        /// <summary>
        /// Projects each element of a sequence into a new form, omitting <see langword="null"/>
        /// results.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectNonNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) where TResult : class
            => source.Select(selector).Where(x => x != null);

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's
        /// index, omitting <see langword="null"/> results.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref
        /// name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref
        /// name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element; the second
        /// parameter of the function represents the index of the source element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the result of invoking the
        /// transform function on each element of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref
        /// name="selector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TResult> SelectNonNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector) where TResult : class
            => source.Select(selector).Where(x => x != null);
    }
}
