using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PythonLearn.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.ViewModel.Course
{
    public class FullCourseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [ValidateNever]
        public List<Lesson> Lessons { get; set; }

    }
}
