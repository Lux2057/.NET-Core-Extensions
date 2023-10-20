namespace Tests;

#region << Using >>

using Extensions;

#endregion

public class StartIndexesOfEntriesTests
{
    public static IEnumerable<object[]> AssertionData()
    {
        yield return new object[] { null, null, Array.Empty<int>() };
        yield return new object[] { new byte[] { 1, 2, 3, 4, 5, 1, 2, 3, 6, 7, 8, 1, 2 }, new byte[] { 1, 2 }, new[] { 0, 5, 11 } };
        yield return new object[] { new byte[] { 0, 1, 1, 2, 4, 5, 1, 2, 8, 9, 1, 2, 12 }, new byte[] { 1, 2 }, new[] { 2, 6, 10 } };
        yield return new object[] { new byte[] { 0, 1, 1, 3, 4, 5, 1, 7, 8, 9, 1, 11, 1 }, new byte[] { 1 }, new[] { 1, 2, 6, 10, 12 } };
        yield return new object[] { new byte[] { 0, 1, 1, 3, 4, 5, 1, 7, 8, 9, 1, 11, 1 }, new byte[] { 1, 2, 3, 4 }, Array.Empty<int>() };
        yield return new object[] { new byte[] { 0, 1, 2, 3, 4, 5, 1, 2, 3, 9, 1, 2, 3 }, new byte[] { 1, 2, 3, 4 }, new[] { 1 } };
    }

    [Theory]
    [MemberData(nameof(AssertionData))]
    public void Should_be_equal_to_expected(IEnumerable<byte> source, IEnumerable<byte> subArray, int[] expected)
    {
        Assert.Equal(source.StartIndexesOfEntries(subArray), expected);
    }
}