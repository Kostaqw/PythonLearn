using Microsoft.AspNetCore.Mvc;
using PythonLearn.Domain.Entity;
using PythonLearn.DAL;
using PythonLearn.Models;
using System.Diagnostics;
using PythonLearn.DAL.Repositories;
using University.DAL.Interfaces;
using PythonLearn.Domain.ViewModel.User;
using PythonLearn.Service.interfaces;
using PythonLearn.Domain.ViewModel.Article;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using PythonLearn.Service.implementation;
using Microsoft.AspNetCore.Identity;
using PythonLearn.DAL.Migrations;

namespace PythonLearn.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _service;
        private readonly IUserService _userService;

        public ArticleController(ILogger<HomeController> logger, IArticleService service, IUserService userService)
        {
            _logger = logger;
            _service = service;
            _userService= userService;
        }
        
        /*
        public async Task<IActionResult> Show()
        {
            var article = await _service.GetArticleAsync(4);
            return View(article.Data);
        }*/

        [HttpGet]
        public async Task<IActionResult> Articles()
        {
            var articles = await _service.GetArticlesAsync();
            return View(articles.Data);
        }

       


        [HttpGet]
        public async Task<IActionResult> Article(int id)
        {
            var fullArticle = await _service.GetCompleteArticle(id);
            return View(fullArticle.Data);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CompleteArticle article)
        {
            var TxtInfo = HttpContext.Request.Form["content"].ToString();
            var user = _userService.GetUsersAsync().Result.Data.FirstOrDefault(x => x.Login == User.Identity.Name);
            ModelState.ClearValidationState(nameof(CompleteArticle));

            article.ArticleText = TxtInfo;
            article.UserId = user.Id;
 
            
            var isValid = TryValidateModel(article, nameof(CompleteArticle));
            var test = ModelState.IsValid;
            if (isValid)
            {
                var response = await _service.CreateArticleWithTitle(article);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                { 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(article);
        }

        [HttpPost]
        public ActionResult UplodImage(List<IFormFile> files, string folder)
        {

            if (folder == null)
            { 
                folder = Path.GetRandomFileName();
            }
           
            string folderPath = Path.Combine("wwwroot", "images/articles/", folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine("wwwroot", "images/articles/", folder, photo.FileName);
                using (var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    photo.CopyToAsync(stream);
                }
                filePath = "https://localhost:7265/images/articles/" + folder +"/" + photo.FileName;
            }
            return Json(new { url = filePath });
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