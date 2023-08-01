namespace Tests;

#region << Using >>

using System.Linq.Expressions;
using Extensions;

#endregion

public class GetPropertyInfoTests
{
    [Fact]
    public void Should_throw_null_argument_exception()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectionExt.GetPropertyInfo((Expression<Func<TestObj, string>>)null));
    }

    [Fact]
    public void Should_throw_argument_exceptions()
    {
        Assert.Throws<ArgumentException>(() =>
                                         {
                                             Expression<Func<TestObj, string>> func = obj => obj.StringMethod();

                                             return ReflectionExt.GetPropertyInfo(func);
                                         });

        Assert.Throws<ArgumentException>(() =>
                                         {
                                             Expression<Func<TestObj, string>> func = obj => obj.StringField;

                                             return ReflectionExt.GetPropertyInfo(func);
                                         });

        Assert.Throws<ArgumentException>(() =>
                                         {
                                             Expression<Func<TestObj, string>> func = obj => Guid.NewGuid().ToString();

                                             return ReflectionExt.GetPropertyInfo(func);
                                         });
    }
}