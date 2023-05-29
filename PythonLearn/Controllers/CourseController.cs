using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.ViewModel.Course;
using PythonLearn.Service.interfaces;
using System.Text;

namespace PythonLearn.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Main(int id)
        {
            var course = await _service.GetFullCourse(id);
            return View(course.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Lecture(int id)
        {
            var lecture = await _service.GetLecture(id);
            return View(lecture.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTest()
        {
            var lessons = await _service.GetLessons();

            ViewBag.Lessons = new SelectList(lessons.Data, "Id", "Name");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateLecture()
        {
            var lessons = await _service.GetLessons();

            ViewBag.Lessons = new SelectList(lessons.Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] TestViewModel test)
        {
            var testEntity = new Test()
            {
                LessonId = test.Lesson,
                Name = test.Name
            };

            await _service.CreateTest(testEntity);

            foreach (var questionViewModel in test.Questions)
            {
                var correctAnswer = new StringBuilder();

                foreach (var answer in questionViewModel.Options)
                {
                    correctAnswer.Append(answer.ToString());
                }
                var questionEntity = new Question
                {
                    TestId = testEntity.Id,
                    Text = questionViewModel.Text,
                    CorrectAnswer = correctAnswer.ToString()
                };

                await _service.CreateQuest(questionEntity);

                foreach (var optionViewModel in questionViewModel.Options)
                {
                    var optionEntity = new Answer
                    {
                        QuestionId = questionEntity.Id,
                        Text = optionViewModel.Text,
                        IsCorrect = optionViewModel.IsCorrect
                    };

                    await _service.CreateAnswer(optionEntity);
                }
            }
            return Ok();
        }

    }
}
