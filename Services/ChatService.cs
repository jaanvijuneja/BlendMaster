using OpenAI_API;

namespace BlendMaster.Services
{
    public class ChatService
    {
        private readonly OpenAIAPI? _API;

        public ChatService(OpenAIAPI? api)
        {
            _API = api;
        }

        public async Task<string> GetChatResponse(string userInput)
        {
            var result = await _API.Completions.CreateCompletionAsync(
                new OpenAI_API.Completions.CompletionRequest
                {
                    Prompt = userInput,
                    MaxTokens = 150
                }
            );
            return result.Completions[0].Text;
        }
    }
}
