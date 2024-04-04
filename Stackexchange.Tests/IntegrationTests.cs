namespace Stackexchange.Tests;

public class IntegrationTests
{
    [Fact]
    public async Task GetContentTest()
    {
        var tags = await ConnectionHandler.GetApiTags("http://localhost:5163/api/Tag?page=1&count=4&sortField=percentage&asc=false");
        Assert.Equal("javascript", tags.First().Name);
    }
    
    [Fact]
    public async Task GetBadRequestTest()
    {
        var code = await ConnectionHandler.GetApiHttpCode("http://localhost:5163/api/Tag?page=1&count=4&sortField=aaa&asc=false", true);
        Assert.Equal("BadRequest", code.ToString());
    }
    
    [Fact]
    public async Task PostCreatedTest()
    {
        var code = await ConnectionHandler.GetApiHttpCode("http://localhost:5163/api/Tag", false);
        Assert.Equal("Created", code.ToString());
    }
}