using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Stackexchange.API.Controllers;

namespace Stackexchange.Tests;

public class UnitTests
{
    private readonly TagController _tagController;
    
    public UnitTests()
    {
        Mock<IMediator> mediator = new();
        Mock<IConfiguration> config = new();
        _tagController = new TagController(config.Object, mediator.Object);
    }
    
    [Fact]
    public async Task ControllerGetOkTest()
    {
        var response = await _tagController.GetPage(1, 5, "name", true);
        Assert.IsType<OkObjectResult>(response);
    }
    
    [Fact]
    public async Task ControllerGetBadRequestTest()
    {
        var response = await _tagController.GetPage(1, 5, "", true);
        Assert.IsType<BadRequestResult>(response);
    }
    
    [Fact]
    public async Task ControllerGetBadRequestTest2()
    {
        var response = await _tagController.GetPage(1, 101, "none", true);
        Assert.IsType<BadRequestObjectResult>(response);
    }
    
    [Fact]
    public async Task ControllerPostCreatedTest()
    {
        var response = await _tagController.Post();
        Assert.IsType<CreatedAtActionResult>(response);
    }
}