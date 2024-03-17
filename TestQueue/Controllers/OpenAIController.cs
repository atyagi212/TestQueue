using Microsoft.AspNetCore.Mvc;

namespace TestQueue.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenAIController : ControllerBase
{
    private readonly ILogger<OpenAIController> _logger;

    public OpenAIController(ILogger<OpenAIController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "myexecute")]
    public async Task<IActionResult> Post(OpenAIModel model)
    {
        return Ok("Successfully processed Open AI request");
    }
}

