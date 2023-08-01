namespace Tests;

#region << Using >>

using Extensions;

#endregion

public class GetPropertyValueOrDefaultTests
{
    public static IEnumerable<object[]> AssertionData()
    {
        yield return new object[] { null, null };
        yield return new object[] { new TestObj(), null };
        yield return new object[] { new TestObj { StringProperty = "test" }, "test" };
    }

    [Theory]
    [MemberData(nameof(AssertionData))]
    public void Should_be_equal_to_expected(TestObj obj, string expected)
    {
        Assert.Equal(expected, obj.GetPropertyValueOrDefault(r => r.StringProperty));
    }
}