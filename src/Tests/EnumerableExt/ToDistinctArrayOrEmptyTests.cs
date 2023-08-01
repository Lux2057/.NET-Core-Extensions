namespace Tests;

#region << Using >>

using Extensions;

#endregion

public class ToDistinctArrayOrEmptyTests
{
    public static IEnumerable<object[]> AssertionData()
    {
        yield return new object[] { null, Array.Empty<object>() };
        yield return new object[] { new int[] { }, Array.Empty<int>() };
        yield return new object[] { new List<string>(), Array.Empty<string>() };
        yield return new object[] { new List<int> { 1, 2, 3, 4, 2, 3, 4 }, new[] { 1, 2, 3, 4 } };
        yield return new object[] { new[] { 1, 2, 3, 4, 2, 3, 4 }, new[] { 1, 2, 3, 4 } };
        yield return new object[] { new Queue<string>(new[] { "", "1", "2", "2", "1" }), new[] { "", "1", "2" } };
    }

    [Theory]
    [MemberData(nameof(AssertionData))]
    public void Should_be_equal_to_expected<T>(IEnumerable<T> enumerable, T[] expected)
    {
        Assert.Equal(enumerable.ToDistinctArrayOrEmpty(), expected);
    }
}