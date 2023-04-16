using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PythonLearn.Domain.Entity
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int TitleId { get; set; }
        
        [Required]
        public string? ArticleText { get; set; }

        [Required]
        [ForeignKey("TitleId")]
        public virtual Title Title { get; set; }
        
        [Required]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
