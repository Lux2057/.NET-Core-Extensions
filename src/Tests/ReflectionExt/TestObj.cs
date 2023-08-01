namespace Tests;

public class TestObj
{
    #region Properties

    public string StringProperty { get; set; }

    public string StringMethod() => Guid.NewGuid().ToString();

    public string StringField;

    #endregion
}