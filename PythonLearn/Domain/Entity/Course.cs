using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
   

        [ValidateNever]
        public List<User> Users { get; set; }

        [ValidateNever]
        public List<Lesson> Lessons { get; set; }
    }
}
