using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int TitleId { get; set; }
        
        [Required]
        public string? ArticleText { get; set; }

        /*[ValidateNever]
        public List<User> Users { get; set; }*/
    }
}
