using System.Collections.Generic;

namespace ExtensionFoundry
{
    /// <summary>
    /// Extensions for <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// A deconstructor for <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="keyValuePair">A <see cref="KeyValuePair{TKey, TValue}"/> to deconstruct.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
        {
            key = keyValuePair.Key;
            value = keyValuePair.Value;
        }
    }
}
