using Microsoft.AspNetCore.Mvc;

namespace Invoice.RAG.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
