using Microsoft.AspNetCore.Mvc;
using PythonLearn.Domain.Entity;
using PythonLearn.DAL;
using PythonLearn.Models;
using System.Diagnostics;
using PythonLearn.DAL.Repositories;
using University.DAL.Interfaces;
using PythonLearn.Domain.ViewModel.User;

namespace PythonLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(UserViewModel viewModel)
        {
            return View(viewModel);
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