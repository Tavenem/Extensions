﻿namespace System.Collections.Generic;

/// <summary>
/// Extensions for <see cref="IDictionary{TKey, TValue}"/>.
/// </summary>
public static class TavenemIDictionaryExtensions
{
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
    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        => dictionary.TryGetValue(key, out var value) ? value : default;

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
    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue)
        => dictionary.TryGetValue(key, out var value) ? value : defaultValue;
}
