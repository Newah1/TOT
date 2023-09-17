#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Chat;
using TheOregonTrailAI.Models;

namespace TheOregonTrailAI.Services;

public class TOTOpenAiService : IAiService
{
    private readonly APIAuthentication _openAiConfiguration;
    private readonly OpenAIAPI _totOpenAiService;
    private readonly string _model;

    public TOTOpenAiService(string? secretKey = null, string model = "gpt-3.5-turbo")
    {
        _openAiConfiguration = new APIAuthentication(secretKey);
        _totOpenAiService = new OpenAIAPI(_openAiConfiguration);
        _model = model;
    }
    
    public async Task<AIResponse> GetResponse(AIRequest aiRequest)
    {
        var response = new AIResponse();
        response.ResponseSuccessful = false;

        var completionResult = await _totOpenAiService.Chat.CreateChatCompletionAsync(
            new ChatRequest()
            {
                Model = _model,
                Temperature = aiRequest.Temperature,
                Messages = aiRequest.MessagesToChatMessage()
            }
        );

        if (completionResult.Choices.Any())
        {
            response.ResponseSuccessful = true;
            response.Response = completionResult.Choices.FirstOrDefault()?.Message.Content;
        }

        return response;
    }

    public async Task<AIResponse> GetResponseSimple(string message)
    {
        var completionResult = await _totOpenAiService.Chat.CreateChatCompletionAsync(
            new ChatRequest()
            {
                Model = _model,
                Temperature = 0.8d,
                Messages = new List<ChatMessage>()
                { 
                    new (ChatMessageRole.FromString("user"), message)
                }
            }
        );
        
        var response = new AIResponse();
        response.ResponseSuccessful = false;
        
        if (completionResult.Choices.Any())
        {
            response.ResponseSuccessful = true;
            response.Response = completionResult.Choices.FirstOrDefault()?.Message.Content;
        }

        return response;
    }
}