namespace System.Text;

/// <summary>
/// Extensions for <see cref="StringBuilder"/>.
/// </summary>
public static class TavenemStringBuilderExtensions
{
    /// <summary>
    /// Remove the given char, or whitespace, from the end of a <see cref="StringBuilder"/> instance.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="trimChar">
    /// An optional <see cref="char"/> to trim.
    /// </param>
    /// <returns>
    /// The trimmed <see cref="StringBuilder"/> instance.
    /// </returns>
    public static StringBuilder TrimEnd(this StringBuilder sb, char? trimChar = null)
    {
        if (sb.Length == 0)
        {
            return sb;
        }

        var i = sb.Length - 1;
        for (; i >= 0; i--)
        {
            if (trimChar.HasValue)
            {
                if (sb[i] != trimChar.Value)
                {
                    break;
                }
            }
            else if (!char.IsWhiteSpace(sb[i]))
            {
                break;
            }
        }
        if (i < sb.Length - 1)
        {
            sb.Length = i + 1;
        }

        return sb;
    }

    /// <summary>
    /// Removes the given string from the end of a <see cref="StringBuilder"/> instance.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="trimString">
    /// A <see cref="string"/> to trim.
    /// </param>
    /// <param name="trimmed">
    /// When the method returns, will be set to <see langword="true"/> if anything was trimmed,
    /// or <see langword="false"/> if not.
    /// </param>
    /// <returns>
    /// The trimmed <see cref="StringBuilder"/> instance.
    /// </returns>
    public static StringBuilder TrimEnd(this StringBuilder sb, string trimString, out bool trimmed)
    {
        trimmed = false;
        if (string.IsNullOrEmpty(trimString))
        {
            return sb;
        }

        if (string.IsNullOrWhiteSpace(trimString))
        {
            var length = sb.Length;
            sb = sb.TrimEnd();
            trimmed = sb.Length != length;
            return sb;
        }

        do
        {
            var offset = sb.Length - trimString.Length;
            for (var i = 0; i < sb.Length - offset; i++)
            {
                if (sb[offset + i] != trimString[i])
                {
                    return sb;
                }
            }

            sb.Length -= trimString.Length;
            trimmed = true;
        }
        while (sb.Length >= trimString.Length);

        return sb;
    }

    /// <summary>
    /// Copies the given <see cref="StringBuilder"/> instance to a <see cref="ReadOnlySpan{T}"/>
    /// of <see cref="char"/>.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance to copy.</param>
    /// <returns>
    /// A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>.
    /// </returns>
    /// <remarks>
    /// Does not perform an intermediate <see cref="string"/> allocation. Instead, this copies the
    /// <see cref="StringBuilder"/>'s character array to a new <see cref="char"/> array, then
    /// returns that array using an implicit cast to <see cref="ReadOnlySpan{T}"/>.
    /// </remarks>
    public static ReadOnlySpan<char> ToSpan(this StringBuilder sb)
    {
        var chars = new char[sb.Length];
        sb.CopyTo(0, chars, 0, sb.Length);
        return chars;
    }
}
