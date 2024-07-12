using OpenAI_API.Chat;

namespace WebApplication2.Models
{
    public class ChatViewModel
    {
        public string UserInput { get; set; }

        public string BotResponse { get; set; }

        public List<ChatMessage> ChatHistoryMessages { get; set; }
    }
}
