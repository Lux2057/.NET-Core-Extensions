namespace Tests;

#region << Using >>

using Extensions;

#endregion

public class QuotedSplitTests
{
    public static IEnumerable<object[]> AssertionData()
    {
        yield return new object[] { null, null, true, Array.Empty<EnumerableExt.Range>() };
        yield return new object[] { null, null, false, Array.Empty<EnumerableExt.Range>() };

        yield return new object[]
                     {
                             new byte[] { 1, 2, 2, 3, 4, 1, 2, 7, 8, 9, 10, 1, 2 }, new byte[] { 1, 2 }, true,
                             new EnumerableExt.Range[] { new(2, 5), new(7, 11) }
                     };

        yield return new object[]
                     {
                             new byte[] { 1, 2, 2, 3, 4, 1, 2, 7, 8, 9, 10, 1, 2 }, new byte[] { 1, 2 }, false,
                             new EnumerableExt.Range[] { new(0, 5), new(5, 11), new(11, 12) }
                     };

        yield return new object[]
                     {
                             new byte[] { 1, 2, 2, 3, 4, 1, 2, 7, 8, 9, 10, 11, 12 }, new byte[] { 1, 2 }, true,
                             new EnumerableExt.Range[] { new(2, 5), new(7, 12) }
                     };

        yield return new object[]
                     {
                             new byte[] { 1, 2, 2, 3, 4, 1, 2, 7, 8, 9, 10, 11, 12 }, new byte[] { 1, 2 }, false,
                             new EnumerableExt.Range[] { new(0, 5), new(5, 12) }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3, 4, 1, 6, 7, 8, 9, 10, 1, 2 }, new byte[] { 1, 2 }, true,
                             new EnumerableExt.Range[] { new(3, 11) }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3, 4, 1, 6, 7, 8, 9, 10, 1, 2 }, new byte[] { 1, 2 }, false,
                             new EnumerableExt.Range[] { new(1, 11), new(11, 12) }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3, 4, 1, 6, 7, 8, 9, 10, 1, 2 }, new byte[] { 1, 2 }, true,
                             new EnumerableExt.Range[] { new(3, 11) }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3, 4, 1, 6, 7, 8, 9, 10, 1, 2 }, new byte[] { 1, 2 }, false,
                             new EnumerableExt.Range[] { new(1, 11), new(11, 12) }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 99, 2, 3, 4, 99, 6, 7, 8, 99, 10, 11, 99 }, new byte[] { 99 }, true,
                             new EnumerableExt.Range[] { new(2, 5), new(6, 9), new(10, 12) }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 99, 2, 3, 4, 99, 6, 7, 8, 99, 10, 11, 99 }, new byte[] { 99 }, false,
                             new EnumerableExt.Range[] { new(1, 5), new(5, 9), new(9, 12), new(12, 12) }
                     };
    }

    [Theory]
    [MemberData(nameof(AssertionData))]
    public void Should_be_equal_to_expected(IEnumerable<byte> source, IEnumerable<byte> separator, bool excludeSeparator, EnumerableExt.Range[] expected)
    {
        var result = source.QuotedSplit(separator, excludeSeparator);

        Assert.Equal(result.Length, expected.Length);

        for (var i = 0; i < result.Length; i++)
        {
            Assert.Equal(result[i].InclusiveStart, expected[i].InclusiveStart);
            Assert.Equal(result[i].ExclusiveEnd, expected[i].ExclusiveEnd);
        }
    }
}