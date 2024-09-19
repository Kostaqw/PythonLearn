using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using PythonLearn.Domain.Entity;

namespace PythonLearn.Domain.ViewModel.Article
{
    public class CompleteArticle
    {
        public int TitleId { get; set; }
        public int ArticleId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string ShortDescription { get; set; }

        [Required]
        [ValidateNever]
        public string ArticleText { get; set; }

        [Required]
        [ValidateNever]
        public DateTime CreatedDate { get; set; }

        [Required]
        [ValidateNever]
        public PythonLearn.Domain.Entity.User user  { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
