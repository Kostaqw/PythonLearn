using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Enum;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.Response;
using PythonLearn.Domain.ViewModel.User;
using PythonLearn.Service.interfaces;
using University.DAL.Interfaces;

namespace PythonLearn.Service.implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _context;

        public UserService(IUnitOfWork context)
        {
            _context = context;
        }


        //CREATE

        public async Task<IBaseResponse<bool>> CreateUser(UserViewModel user)
        { 
            var response = new BaseResponse<bool>();
            try
            {
                var newUser = new User()
                {
                    AboutMe = user.AboutMe,
                    avatar = user.avatar,
                    BirthDay = user.BirthDay,
                    Email = user.Email,
                    Login = user.Login,
                    Name = user.Name,
                    Password = user.Password,
                    SecondName = user.SecondName
                };
                await _context.UserRepositories.CreateAsync(newUser);

                response.Description = $"Пользователй успешно создан";
                response.StatusCode = StatusCode.OK;
                response.Data = true;

                return response;
            }
            catch (Exception ex) 
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] CreateUser(UserViewModel user): {ex.Message}"
                };
            }
        }

        //READ

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns>Стандартный ответ со списком всех пользователй</returns>
        public async Task<IBaseResponse<IEnumerable<User>>> GetUsersAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _context.UserRepositories.GetAllAsync();
                if (users.Count == 0)
                {
                    baseResponse.StatusCode = StatusCode.Warn;
                    baseResponse.Description = $"Найдено 0 элементов";
                    return baseResponse;
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Description = $"Найдено {users.Count} пользователей";
                    baseResponse.Data = users;
                    return baseResponse;
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] GetUsersAsync(): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param int="id">id пользователя</param>
        /// <returns>Стандартный ответ c пользователй c заданным id </returns>
        public async Task<IBaseResponse<User>> GetUserAsync(int id)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await _context.UserRepositories.GetAsync(id);
                if (user == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description =$"Пользователь с id {id} не найден в БД";
                    return response;
                }
                else
                { 
                    response.StatusCode= StatusCode.OK;
                    response.Description = $"Пользователь {user.Name} с id {id} найден";
                    response.Data = user;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] GetUserAsync(): {ex.Message}"
                };
            }

        }

        /// <summary>
        /// Получить список всех пользователей по имени
        /// </summary>
        /// <param string="Name">имя пользователя</param>
        /// <returns>Стандартный ответ со списком всех пользователй с заданным именем</returns>
        public async Task<IBaseResponse<IEnumerable<User>>> GetUserByNameAsync(string name)
        {
            var response = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _context.UserRepositories.GetByNameAsync(name);
                if (users.Count==0)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Пользователи с именем {name} не найдены в БД";
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Найдено {users.Count} пользователей с именем {name}";
                    response.Data = users;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] GetUserByNameAsync(string name): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Получить список всех пользователей по фамилии
        /// </summary>
        /// <param string="secondName">фамилия пользователя</param>
        /// <returns>Стандартный ответ со списком всех пользователй с заданной фамилией</returns>
        public async Task<IBaseResponse<IEnumerable<User>>> GetUserBySecondNameAsync(string secondName)
        {
            var response = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _context.UserRepositories.GetByNameAsync(secondName);
                if (users.Count == 0)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Пользователи с фамилией {secondName} не найдены в БД";
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Найдено {users.Count} пользователей с фамилией {secondName}";
                    response.Data = users;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] GetUserBySecondNameAsync(string secondName): {ex.Message}"
                };
            }
        }

        //UPDATE

        /// <summary>
        /// Изменить поля пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="model">модель пользователя</param>
        /// <returns>Измененный пользователь</returns>
        public async Task<IBaseResponse<User>> EditAsync(int id, UserViewModel model)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await _context.UserRepositories.GetAsync(id);
                if (user == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Пользователь с id {id} не найден в БД";
                    return response;
                }
                else
                { 
                    user.Name= model.Name;
                    user.Email= model.Email;
                    user.Password= model.Password;
                    user.AboutMe = model.AboutMe;
                    user.SecondName= model.SecondName;
                    user.BirthDay= model.BirthDay;
                    user.avatar = model.avatar;
                    user.Login= model.Login;
                    await _context.UserRepositories.UpdateAsync(user);

                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Пользователь изменён";
                    response.Data = user;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] EditAsync(int id, UserViewModel model): {ex.Message}"
                };
            }
        }
        //DELETE
        /// <summary>
        /// Удалить пользователя по id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>стандартный ответ с данными : true в случае успеха false в случае неудачи</returns>
        public async Task<IBaseResponse<bool>> DeleteUserAsync(int id) 
        {
            var response = new BaseResponse<bool>();
            try
            {
                var user = await _context.UserRepositories.GetAsync(id);
                if (user == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Пользователь с id {id} не найден в БД";
                    response.Data = false;
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Пользователь {user.Name} с id {id} удален";
                    response.Data = true;
                    await _context.UserRepositories.DeleteAsync(id);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[UserService] DeleteUserAsync(int id): {ex.Message}",
                    Data = false
                    
                };
            }
        }


    }
}
