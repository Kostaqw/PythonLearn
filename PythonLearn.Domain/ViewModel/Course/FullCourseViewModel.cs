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
        
        /*
        [ValidateNever]
        public List<Test> Tests { get; set; }
        
        [ValidateNever]
        public List<Lecture> Lectures { get; set; }
        
        [ValidateNever]
        public List<Practice> Practices { get; set; }
        */
    }
}
