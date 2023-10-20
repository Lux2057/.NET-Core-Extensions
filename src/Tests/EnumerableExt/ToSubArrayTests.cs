namespace Tests;

#region << Using >>

using Extensions;

#endregion

public class ToSubArrayTests
{
    public static IEnumerable<object[]> AssertionData()
    {
        yield return new object[]
                     {
                             (int[])null, 1, 5,
                             Array.Empty<int>()
                     };

        yield return new object[]
                     {
                             Array.Empty<int>(), 1, 5,
                             Array.Empty<int>()
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, 1, 5,
                             new byte[] { 1, 2, 3, 4, 5 }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3 }, 1, 5,
                             new byte[] { 1, 2, 3 }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, 1, 5,
                             new byte[] { 1, 2, 3, 4, 5 }
                     };

        yield return new object[]
                     {
                             new byte[] { 0, 1, 2, 3 }, 5, 5,
                             Array.Empty<byte>()
                     };
    }

    [Theory]
    [MemberData(nameof(AssertionData))]
    public void Should_be_equal_to_expected<T>(IEnumerable<T> source, int offset, int length, T[] expected)
    {
        Assert.Equal(source.ToSubArray(offset, length), expected);
    }
}