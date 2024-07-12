using Microsoft.AspNetCore.SignalR;
using OpenAI_API;
using OpenAI_API.Chat;

namespace WebApplication2.Services
{
    public class ChatHub : Hub
    {
        private readonly OpenAIAPI _openAIAPI;
        private static Dictionary<string, List<ChatMessage>> userConversations = new Dictionary<string, List<ChatMessage>>();

        public ChatHub(OpenAIAPI openAIAPI)
        {
            _openAIAPI = openAIAPI;
        }

        public async Task SendMessage(string userInput)
        {
            var connectionId = Context.ConnectionId;

            // Initialize conversation history if not present
            if (!userConversations.ContainsKey(connectionId))
            {
                userConversations[connectionId] = new List<ChatMessage>
                {
                    new ChatMessage(ChatMessageRole.System, "You are Bartender, a helpful assistant to provide instructions about cocktail making and other related knowledges, such as advices for cocktail glasses, cocktail ice making."),
                    new ChatMessage(ChatMessageRole.System, "When providing drink recipes, a recipe should include following contents: a name, a description, a list of ingredients, a list of instructions, and a list of tags."),
                    new ChatMessage(ChatMessageRole.System, "Your drink recipes should be in html."),
                    new ChatMessage(ChatMessageRole.System, "If a user asks something not related to drink making, politely remind the user to ask another question."),
                };
            }

            // Add user's message to conversation history
            var conversationHistory = userConversations[connectionId];
            conversationHistory.Add(new ChatMessage(ChatMessageRole.User, userInput));

            // Get bot response
            var botResponse = await GetChatbotResponse(conversationHistory);
            conversationHistory.Add(new ChatMessage(ChatMessageRole.Assistant, botResponse));

            await Clients.All.SendAsync("ReceiveMessage", "Bartender", botResponse);
        }

        private async Task<string> GetChatbotResponse(List<ChatMessage> conversationHistory)
        {
            var chatResponse = await _openAIAPI.Chat.CreateChatCompletionAsync(
                new ChatRequest()
                {
                    Messages = conversationHistory
                }
            );

            var response = chatResponse.Choices[0].Message.Content;
            Console.WriteLine(response);
            return response;
        }
    }
}
