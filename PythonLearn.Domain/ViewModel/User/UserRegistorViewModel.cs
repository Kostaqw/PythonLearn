using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PythonLearn.Domain.ViewModel.User
{
    public class UserRegistorViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SecondName { get; set; }
        
        [Required]
        public DateTime? BirthDay { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        public string AboutMe { get; set; }

        [Required]
        public IFormFile avatar { get; set; }
    }
}
