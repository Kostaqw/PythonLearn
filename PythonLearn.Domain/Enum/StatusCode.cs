using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Enum
{
    public enum StatusCode
    {
        [Display(Name = "Успех")]
        OK = 200,
        [Display(Name = "Ошибка запроса")]
        BadRequest = 400,
        [Display(Name = "Страница не найдена")]
        NotFound = 404,
        [Display(Name = "Ошибка на стороне сервера")]
        InternalServerError = 500
    }
}
