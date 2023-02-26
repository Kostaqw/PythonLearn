using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
   
        public bool status { get; set; }

        public List<Element> Elements { get; set; }

        public List<LessonComment> LessonComments { get; set; }

        public List<Solution> Solutions { get; set; }
    }
}
