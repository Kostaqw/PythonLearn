using Microsoft.AspNetCore.Mvc;
using PythonLearn.Domain.Entity;
using PythonLearn.DAL;
using PythonLearn.Models;
using System.Diagnostics;
using PythonLearn.DAL.Repositories;
using University.DAL.Interfaces;
using PythonLearn.Service.implementation;
using PythonLearn.Service.interfaces;
using PythonLearn.Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.Xml;

namespace PythonLearn.Controllers
{
    public class UserController : Controller
    {
         private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _service.GetUserAsync(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            { 
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.avatar.OpenReadStream()))
                    { 
                        imageData= binaryReader.ReadBytes((int)model.avatar.Length);
                    }
                        await _service.CreateUser(model, imageData);
                }
                else
                {
                    await _service.EditAsync(model.Id, model);
                }
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public IActionResult Register(string name, string secondName, DateTime Birthday, string login, string email, string password, string aboutMe, byte[] avatar)
        {
            return Redirect("./RegisterSuccess");
        }

        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _service.GetUsersAsync();
            return View();
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