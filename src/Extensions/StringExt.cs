namespace Extensions;

#region << Using >>

using System.IO.Compression;
using System.Text;

#endregion

public static class StringExt
{
    /// <summary>
    ///     A wrapper for String.IsNullOrEmpty function
    /// </summary>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///     A wrapper for String.IsNullOrWhiteSpace function
    /// </summary>
    public static bool IsNullOrWhitespace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     A wrapper for Enum.Parse function
    /// </summary>
    public static T ToEnum<T>(this string value) where T : struct, Enum
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    /// <summary>
    ///     A wrapper for String.Join function
    /// </summary>
    public static string ToJoinedString(this IEnumerable<string> enumerable, string separator = default)
    {
        var enumerableArray = enumerable.ToArrayOrEmpty();
        var currentSeparator = separator.IsNullOrEmpty() ? string.Empty : separator;

        return !enumerableArray.Any() ? string.Empty : string.Join(currentSeparator, enumerableArray);
    }

    static void copyTo(Stream src, Stream dest)
    {
        var bytes = new byte[4096];

        int cnt;

        while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            dest.Write(bytes, 0, cnt);
    }

    /// <summary>
    ///     Returns compressed base64 string
    /// </summary>
    public static string CompressToBase64(this string value)
    {
        return Convert.ToBase64String(value.Zip());
    }

    /// <summary>
    ///     Returns decompressed string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string DecompressFromBase64(this string value)
    {
        return Convert.FromBase64String(value).Unzip();
    }

    /// <summary>
    ///     Returns zipped string
    /// </summary>
    public static byte[] Zip(this string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);

        using (var msi = new MemoryStream(bytes))
        {
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    copyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }
    }

    /// <summary>
    ///     Returns unzipped string
    /// </summary>
    public static string Unzip(this byte[] bytes)
    {
        using (var msi = new MemoryStream(bytes))
        {
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    copyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
    }
}