using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PythonLearn.Domain.ViewModel.User;
using PythonLearn.Service.interfaces;
using System.Security.Claims;
using System.Linq;
using AutoMapper;
using PythonLearn.DAL.other;
using Microsoft.AspNetCore.Http;
using System.Collections;


namespace PythonLearn.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор для dependence injactive
        /// </summary>
        /// <param name="context">Контекст</param>
        public AccountController(IAccountService service, IUserService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
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

                    var userModel = _userService.GetUsersAsync().Result.Data.FirstOrDefault(x => x.Login == model.UserName);

                    var userViewModel = new UserViewModel()
                    {
                        Login = userModel.Login,
                        Email = userModel.Email,
                        Id = userModel.Id,
                        Name = userModel.Name,
                        SecondName = userModel.SecondName
                    };

                    return RedirectToAction("Index", "Home", userViewModel);
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


        /// <summary>
        /// Отобразить окно входа на сайт
        /// </summary>
        /// <returns>Представление Login</returns>
        [HttpGet]
        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.GetUsersAsync().Result.Data.FirstOrDefault(x => x.Login == User.Identity.Name);

                var userViewModel = _mapper.Map<UserViewModel>(user);
                return View(userViewModel);
            }
            return View();
        }


        /// <summary>
        /// Изменение профиля пользователя
        /// </summary>
        /// <param name="model">Вью модель пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            var user = _userService.GetUsersAsync().Result.Data.FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user != null)
            {
                model.Email = user.Email;
                model.Login= user.Login;
                model.Image = user.avatar;
                model.Id = user.Id;
                if (ModelState.IsValid)
                {
                    var response = await _service.EditAccount(model);
                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        return RedirectToAction("Index", "Home", model.Login);
                    }
                    else
                    {
                        ModelState.AddModelError("", response.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
