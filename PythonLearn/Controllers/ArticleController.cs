using Microsoft.AspNetCore.Mvc;
using PythonLearn.Domain.Entity;
using PythonLearn.DAL;
using PythonLearn.Models;
using System.Diagnostics;
using PythonLearn.DAL.Repositories;
using University.DAL.Interfaces;
using PythonLearn.Domain.ViewModel.User;
using PythonLearn.Service.interfaces;

namespace PythonLearn.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _service;

        public ArticleController(ILogger<HomeController> logger, IArticleService service)
        {
            _logger = logger;
            _service = service;
        }
        

        public async Task<IActionResult> Show()
        {
            var article = await _service.GetArticleAsync(4);
            return View(article.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}