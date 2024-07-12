using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ChatController : Controller
    {
        private readonly OpenAIService _openAIService;

        public ChatController(OpenAIService chatService)
        {
            _openAIService = chatService;
        }

        public IActionResult Index()
        {
            return View(new ChatViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetResponse(ChatViewModel model)
        {
            string Response = await _openAIService.GetFakeResponse(model.UserInput);
            model.BotResponse = Response;
            return RedirectToAction("Index");
        }
    }
}
