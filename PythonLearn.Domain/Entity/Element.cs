using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PythonLearn.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Element
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public int LessonId { get; set; }

        public ElementType Type {get; set;}

        [Required]
        [MaxLength(60)]
        public string Discription { get; set; }
        
        public bool status { get; set; }

        public string Answers { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
