using System.Collections.Generic;
using System.Linq;
using OpenAI_API.Chat;
using TheOregonTrailAI.Models;

namespace TheOregonTrailAI.Services;

public static class AiMessagesExtension
{
    public static List<ChatMessage> MessagesToChatMessage(this AIRequest request)
    {
        return request.Messages.Select(message =>
        {
            var chatMessage = new ChatMessage()
            {
                Content = message.Message,
                Role = ChatMessageRole.FromString(message.Role)
            };
            return chatMessage;
        }).ToList();
    }
}