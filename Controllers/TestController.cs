using BlendMaster.Models;
using BlendMaster.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlendMaster.Controllers
{
    public class TestController : Controller
    {
        private readonly ChatService _ChatService;

        public TestController(ChatService chatService)
        {
            _ChatService = chatService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View(new TestViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(TestViewModel model)
        {
            var Response = await _ChatService.GetChatResponse(model.Req);
            model.Res = Response;
            return View(model);
        }
    }
}
