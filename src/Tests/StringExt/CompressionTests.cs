namespace Tests;

using Extensions;

public class CompressionTests
{
    [Fact]
    public void Should_return_the_same_string_after_compress_decompress()
    {
        var str = Guid.NewGuid().ToString();

        var result = str.CompressToBase64().DecompressFromBase64();

        Assert.Equal(str, result);
    }
}