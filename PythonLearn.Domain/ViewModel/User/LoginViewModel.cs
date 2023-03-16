using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.ViewModel.User
{
    public class LoginViewModel
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required(ErrorMessage = "Введите логин")]
        [MaxLength(30, ErrorMessage ="Имя должно быть менее 30 символов")]
        [MinLength(3, ErrorMessage = "Имя должно быть длинее 3 символов")]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

    }
}
