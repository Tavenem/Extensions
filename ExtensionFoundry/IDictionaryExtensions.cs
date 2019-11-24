using System.Collections.Generic;

namespace NeverFoundry
{
    /// <summary>
    /// Extensions for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key, or <see langword="null"/> if no such key
        /// exists.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}"/> from which to
        /// retrieve a value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <returns>
        /// The value associated with the specified key, or a default value if no such key exists.
        /// </returns>
        public static TValue? GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Gets the value associated with the specified key, or a default value if no such key
        /// exists.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}"/> from which to
        /// retrieve a value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <returns>
        /// The value associated with the specified key, or a default value if no such key exists.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : struct
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            return default;
        }

        /// <summary>
        /// Gets the value associated with the specified key, or a default value if no such key
        /// exists.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}"/> from which to
        /// retrieve a value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="defaultValue">The default value to retrieve, if no such key exists.</param>
        /// <returns>
        /// The value associated with the specified key, or the specified default value if no such
        /// key exists.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Deconstructs <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="keyValuePair">A <see cref="KeyValuePair{TKey, TValue}"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
        {
            key = keyValuePair.Key;
            value = keyValuePair.Value;
        }
    }
}
