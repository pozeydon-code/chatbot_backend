using API_ChatBot.Models;

namespace API_ChatBot.Services.Interfaces
{
    public interface IChatService
    {
        ChatResponse GetResponseAsync(ChatRequest request);
    }
}
