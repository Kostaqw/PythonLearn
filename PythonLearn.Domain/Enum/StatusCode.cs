using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Enum
{
    public enum StatusCode
    {
        //users Codes 0-20
        [Display(Name = "Пользователь не найден")]
        UserNotFound = 0,
        UserAlreaydyExists = 1,

        //common codes
        [Display(Name = "Успех")]
        OK = 200,
        [Display(Name = "Предупреждение")]
        Warn = 201,
        [Display(Name = "Ошибка запроса")]
        BadRequest = 400,
        [Display(Name = "Страница не найдена")]
        NotFound = 404,
        [Display(Name = "Ошибка на стороне сервера")]
        InternalServerError = 500
    }
}
