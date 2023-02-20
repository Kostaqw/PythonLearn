using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Solution
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string SolutionText { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int LessonId { get; set; }

        [ValidateNever]
        public List<User> Users { get; set; }

        [ValidateNever]
        public List<Lesson> Lessons { get; set; }
    }
}
