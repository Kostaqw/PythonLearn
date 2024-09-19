using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.other;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.Response;
using PythonLearn.Domain.ViewModel.User;
using PythonLearn.Service.interfaces;
using System.Security.Claims;
using University.DAL.Interfaces;

namespace PythonLearn.Service.implementation
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _context;

        /// <summary>
        /// Конструктор для dependence injactive
        /// </summary>
        /// <param name="context">Контекст</param>
        public AccountService(IUnitOfWork context)
        {
            _context = context;
        }


        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="model">Вью модель логина пользователя</param>
        /// <returns>Стандартный ответ с результатом операции</returns>
        public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _context.UserRepositories.GetAllAsync().FirstOrDefaultAsync(x=>x.Login == model.UserName);
                if (user == null) 
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Domain.Enum.StatusCode.BadRequest
                    };
                }

                if (user.Password != CreateHash.CreateMD5Hash(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль",
                        StatusCode = Domain.Enum.StatusCode.BadRequest
                    };
                }

                var result = Authentificate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data= result,
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Description = "Успешная атуентификация"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[AccountService] Login(LoginViewModel model): {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Вью модель пользователя</param>
        /// <returns>Возвращает стандартный ответ с резульатом операции</returns>
        public async Task<IBaseResponse<ClaimsIdentity>> Register(UserRegistorViewModel model)
        {
            try
            {
                var user = await _context.UserRepositories.GetAllAsync().FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserAlreaydyExists,
                        Description = "Пользователь с таким логином уже существует"
                    };
                }

                byte[] avatarFromFile = null;
                using(var binaryReader = new BinaryReader(model.avatar.OpenReadStream())) 
                {
                    avatarFromFile = binaryReader.ReadBytes((int)model.avatar.Length);
                }

                user = new Domain.Entity.User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    AboutMe = model.AboutMe,
                    BirthDay = model.BirthDay,
                    Login = model.Login,
                    Password = CreateHash.CreateMD5Hash(model.Password),
                    Role = Domain.Enum.Roles.User,
                    SecondName = model.SecondName,
                    avatar = avatarFromFile,
                };

                await _context.UserRepositories.CreateAsync(user);

                var result = Authentificate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Пользователь создан",
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[AccountService] Register(UserRegistorViewModel model): {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// редактирование аккаунта
        /// </summary>
        /// <param name="model">Вью модель</param>
        /// <returns>Возвращает true в случае успеха</returns>
        public async Task<BaseResponse<bool>> EditAccount(UserViewModel model)
        {
            try
            {
                var user = await _context.UserRepositories.GetAllAsync().FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Description = "Пользователь с таким логином уже существует",
                        Data = false
                    };
                }

                user = new Domain.Entity.User()
                {
                    Id= model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    AboutMe = model.AboutMe,
                    BirthDay = model.BirthDay,
                    Login = model.Login,
                    SecondName = model.SecondName,
                };

                
                if (model.avatar != null)
                {
                    byte[] avatarFromFile = null;
                    using (var binaryReader = new BinaryReader(model.avatar.OpenReadStream()))
                    {
                        avatarFromFile = binaryReader.ReadBytes((int)model.avatar.Length);
                    }
                    user.avatar= avatarFromFile;
                }



                await _context.UserRepositories.UpdateAsync(user);


                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Пользователь изменен",
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[AccountService] EditAccount(UserRegistorViewModel model): {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };
            }
        }


        /// <summary>
        /// Метод аутентификации пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        private ClaimsIdentity Authentificate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
