namespace Extensions;

#region << Using >>

using System.Collections.Concurrent;
using System.Text;

#endregion

public static class EnumerableExt
{
    #region Constants

    public const int defaultPage = 1;

    public const int defaultPageSize = 10;

    #endregion

    #region Nested Classes

    public struct Range
    {
        #region Properties

        public int InclusiveStart { get; }

        public int ExclusiveEnd { get; }

        #endregion

        #region Constructors

        public Range(int inclusiveStart, int exclusiveEnd)
        {
            InclusiveStart = inclusiveStart;
            ExclusiveEnd = exclusiveEnd;
        }

        #endregion
    }

    #endregion

    /// <summary>
    ///     Returns an array which contains elements from the input sequence or
    ///     an empty array in case when the sequence is null or empty
    /// </summary>
    public static T[] ToArrayOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable == null ? Array.Empty<T>() : enumerable.ToArray();
    }

    /// <summary>
    ///     Returns an distinct array which contains elements from the input sequence or
    ///     an empty array in case when the sequence is null or empty
    /// </summary>
    public static T[] ToDistinctArrayOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable?.Distinct().ToArrayOrEmpty() ?? Array.Empty<T>();
    }

    /// <summary>
    ///     Returns a page IQueryable basing on LINQ Skip/Take functions
    /// </summary>
    public static IQueryable<TEntity> ToPage<TEntity>(this IQueryable<TEntity> queryable, int totalCount, int? page, int? pageSize)
    {
        var currentPageSize = new[] { pageSize.GetValueOrDefault(defaultPageSize), 1 }.Max();
        var maxPage = new[] { 1, (int)Math.Ceiling((decimal)totalCount / currentPageSize) }.Max();
        var currentPage = new[] { new[] { page.GetValueOrDefault(defaultPage), 1 }.Max(), maxPage }.Min() - 1;

        return queryable.Skip(currentPageSize * currentPage).Take(currentPageSize);
    }

    /// <summary>
    ///     Returns a HEX string from bytes enumerable
    /// </summary>
    public static string ToHexString(this IEnumerable<byte> bytes)
    {
        var bytesArray = bytes.ToArrayOrEmpty();
        if (!bytesArray.Any())
            return string.Empty;

        var hex = new StringBuilder(bytesArray.Length * 2);
        foreach (var b in bytesArray)
            hex.AppendFormat("{0:x2}", b);

        return hex.ToString();
    }

    /// <summary>
    ///     Returns start indexes of all entries of sub in source
    /// </summary>
    public static int[] StartIndexesOfEntries(this IEnumerable<byte> source, IEnumerable<byte> sub)
    {
        var sourceArray = source.ToArrayOrEmpty();
        var subArray = sub.ToArrayOrEmpty();

        if (sourceArray.Length == 0 || subArray.Length == 0)
            return Array.Empty<int>();

        var index = Enumerable.Range(0, sourceArray.Length - subArray.Length + 1);
        for (var i = 0; i < subArray.Length; i++)
            index = index.Where(n => sourceArray[n + i] == subArray[i]).ToArray();

        var result = index.ToArray();

        return result;
    }

    /// <summary>
    ///     Returns start/end indexes of sub-arrays started with separator
    /// </summary>
    public static Range[] QuotedSplit(this IEnumerable<byte> source, IEnumerable<byte> separator, bool excludeSeparator = true)
    {
        var sourceArray = source.ToArrayOrEmpty();
        var separatorArray = separator.ToArrayOrEmpty();

        var startIndexes = sourceArray.StartIndexesOfEntries(separatorArray);

        if (startIndexes.Length == 0)
            return Array.Empty<Range>();

        var result = new ConcurrentQueue<Range>();
        Parallel.For(0, startIndexes.Length,
                     index =>
                     {
                         var start = excludeSeparator ? startIndexes[index] + separatorArray.Length : startIndexes[index];
                         var end = index < startIndexes.Length - 1 ?
                                           startIndexes[index + 1] :
                                           sourceArray.Length - 1;

                         if (start == sourceArray.Length)
                             return;

                         result.Enqueue(new Range(start, end));
                     });

        return result.OrderBy(r => r.InclusiveStart).ToArray();
    }

    /// <summary>
    ///     Returns a sub array by specified offset and length
    /// </summary>
    public static T[] ToSubArray<T>(this IEnumerable<T> source, int offset, int length)
    {
        var array = source.ToArrayOrEmpty();

        if (array.Length == 0 || offset > array.Length - 1)
            return Array.Empty<T>();

        var result = new List<T>();
        for (var i = offset; i <= length; i++)
        {
            if (i >= array.Length)
                break;

            result.Add(array[i]);
        }

        return result.ToArray();
    }
}