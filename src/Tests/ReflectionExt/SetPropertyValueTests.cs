namespace Tests;

using Extensions;

public class SetPropertyValueTests
{
    [Fact]
    public void Should_set_property_value()
    {
        var obj = new TestObj();

        var stringProp = Guid.NewGuid().ToString();

        obj.SetPropertyValue(r => r.StringProperty, stringProp);

        Assert.Equal(stringProp, obj.StringProperty);
    }

    [Fact]
    public void Should_ignore_null()
    {
        var obj = (TestObj)null;

        obj.SetPropertyValue(r => r.StringProperty, Guid.NewGuid().ToString());

        Assert.Null(obj);
    }
}