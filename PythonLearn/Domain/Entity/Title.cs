using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace PythonLearn.Domain.Entity
{
    public class Title
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string ShortDescription { get; set; }

        [ValidateNever]
        public List<Article> Articles { get; set; }
    }
}
