using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }


        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
   
        public bool status { get; set; }

        public virtual Course course { get; set; }

        public List<Lecture>? Lectures { get; set; }
        public List<Test>? Tests { get; set; }
        public List<Practice>? Practices { get; set; }

    }
}
