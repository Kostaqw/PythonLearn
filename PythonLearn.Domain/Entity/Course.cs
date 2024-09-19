using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Entity
{
    public class Course
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }

        [ValidateNever]
        public List<Lesson> Lessons { get; set; }

    }
}
