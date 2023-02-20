using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class ArticleComment
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int ArticleId { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string CommentText { get; set; }

        [ValidateNever]
        public List<Article> Articles { get; set; }
    }
}
