using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PythonLearn.Domain.ViewModel.Course
{
    public class LectureViewModel
    {
        public int LessonId { get; set; }
        public string Name { get; set; }

        [ValidateNever]
        public string Text { get; set; }
    }
}
