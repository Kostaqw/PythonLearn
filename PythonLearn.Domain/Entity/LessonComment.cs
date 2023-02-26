using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class LessonComment
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int LessonId { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string CommentText { get; set; }

        [ValidateNever]
        public List<Lesson> Lessons { get; set; }
    }
}
