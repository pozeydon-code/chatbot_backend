using System.Text.Json;
using API_ChatBot.Models;
using API_ChatBot.Services.Interfaces;

namespace API_ChatBot.Services.Implementations;

public class ChatService : IChatService
{
    /*private readonly List<BotQA> _botQAs = new List<BotQA>*/
    /*{*/
    /*    new BotQA { Question = "Hello", Answer = "Hi there!" },*/
    /*    new BotQA { Question = "How are you?", Answer = "I'm doing well, thank you!" },*/
    /*    new BotQA { Question = "What's your name?", Answer = "I'm a chatbot!" },*/
    /*    new BotQA { Question = "What's the weather like today?", Answer = "I'm not sure, but you can check the weather app!" },*/
    /*    new BotQA { Question = "What's the time?", Answer = DateTime.Now.ToString("hh:mm tt") },*/
    /*    new BotQA { Question = "What's the date?", Answer = DateTime.Now.ToString("MMMM dd, yyyy") },*/
    /*    new BotQA { Question = "Goodbye", Answer = "Goodbye! Have a great day!" }*/
    /*};*/
    /**/
    /*public ChatResponse GetResponseAsync(ChatRequest request)*/
    /*{*/
    /*    var response = _botQAs.FirstOrDefault(qa => qa.Question.ToLower() == request.Message.ToLower())?.Answer ?? "I'm sorry, I don't understand that question.";*/
    /*    return new ChatResponse { Response = response };*/
    /*}*/

    private readonly List<BotQA> _botData;

    public ChatService(IWebHostEnvironment env)
    {
        if (env is null)
            throw new ArgumentNullException(nameof(env));

        //Upload the JSON data from the BotData.json file
        var filePath = Path.Combine(env.ContentRootPath, "Data", "BotData.json");
        var json = File.ReadAllText(filePath);

        //Deserialize the JSON data to a list of BotQA objects
        _botData = JsonSerializer.Deserialize<List<BotQA>>(json) ?? throw new ArgumentNullException(nameof(json));
    }

    public ChatResponse GetResponseAsync(ChatRequest request)
    {
        string normalizedMessage = request.Message.Trim().ToLower();

        BotQA? match = _botData.FirstOrDefault(qa => qa.Question.ToLower() == normalizedMessage);

        if (match == null)
            return new ChatResponse { Response = "I'm sorry, I don't understand that question." };

        return new ChatResponse { Response = match.Answer };
    }
}
