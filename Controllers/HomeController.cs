using Microsoft.AspNetCore.Mvc;
using qa_Lab_1.Models;
using qa_Lab_1.Sevices;
using System.Diagnostics;

namespace qa_Lab_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly QAServices _qaService; // Referens till QA-tjänsten

        public HomeController(QAServices qaService)
        {
            _qaService = qaService; // Initiera QA-tjänsten via dependency injection
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AskQuestion(string question)
        {
            var answer = _qaService.GetAnswer(question); // Anropar tjänsten för att få svar på frågan
            ViewBag.Question = question;
            ViewBag.Answer = answer;
            return View("Index");
        }
    }
}
