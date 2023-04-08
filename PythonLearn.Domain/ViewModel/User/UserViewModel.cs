using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PythonLearn.Domain.ViewModel.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SecondName { get; set; }

        public DateTime? BirthDay { get; set; }

        [ValidateNever]
        public string Login { get; set; }

        [ValidateNever]
        public string Email { get; set; }

        public string AboutMe { get; set; }


        [ValidateNever]
        public IFormFile avatar { get; set; }
        [ValidateNever]
        public byte[] Image { get; set; }
    }
}
