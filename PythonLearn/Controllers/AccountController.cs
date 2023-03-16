using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PythonLearn.Domain.ViewModel.User;
using PythonLearn.Service.interfaces;
using System.Security.Claims;

namespace PythonLearn.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        /// <summary>
        /// Конструктор для dependence injactive
        /// </summary>
        /// <param name="context">Контекст</param>
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Отобразить окно регистрации
        /// </summary>
        /// <returns>Представление Register</returns>
        [HttpGet]
        public IActionResult Register() => View();


        /// <summary>
        /// Отправить данные для попытки регистрации
        /// </summary>
        /// <param name="model">Вью модель регистрации пользователя</param>
        /// <returns>Перенаправялет на домашнию страницу</returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserRegistorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.Register(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        /// <summary>
        /// Отобразить окно входа на сайт
        /// </summary>
        /// <returns>Представление Login</returns>
        [HttpGet]
        public IActionResult Login() => View();

        /// <summary>
        /// Отправить данные для попытки входа
        /// </summary>
        /// <param name="model">Логин вью модель</param>
        /// <returns>Перенаправялет на домашнию страницу</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        /// <summary>
        /// Выход из аккаунта
        /// </summary>
        /// <returns>Перенаправялет на домашнию страницу</returns>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
