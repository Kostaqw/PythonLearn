using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Required]
        public DateTime? BirthDay { get; set; }

        [Required]
        [MaxLength(30)]
        public string Login { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        [MaxLength(300)]
        public string Password { get; set; }

        [MaxLength(400)]
        public string AboutMe { get; set; }

        public byte[] avatar { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ValidateNever]
        public List<Solution> Solutions { get; set; }
    }
}
