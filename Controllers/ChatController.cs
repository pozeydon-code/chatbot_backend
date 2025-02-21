using API_ChatBot.Models;
using API_ChatBot.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_ChatBot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
    }

    [HttpPost]
    public ActionResult<ChatResponse> PostMessage([FromBody] ChatRequest request)
    {
        if (string.IsNullOrEmpty(request.Message))
        {
            return BadRequest("Message is required");
        }

        var response = _chatService.GetResponseAsync(request);
        return Ok(response);
    }
}
