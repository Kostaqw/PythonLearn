using PythonLearn.DAL.Repositories;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Enum;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.Response;
using PythonLearn.Domain.ViewModel.Course;
using PythonLearn.Service.interfaces;
using University.DAL.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace PythonLearn.Service.implementation
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _context;

        public CourseService(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<IBaseResponse<Course>> GetCourse(int id)
        {
            try
            {
                Course course = await CourseRetrieval(id);

                if (course != null)
                {
                    return new BaseResponse<Course>
                    {
                        StatusCode = StatusCode.OK,
                        Data = course
                    };
                }
                else
                {
                    return new BaseResponse<Course>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Course not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Course>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while getting the course: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Lecture>> GetLecture(int id)
        {
            try
            {
                var lecture = await LectureRetrieval(id);

                if (lecture != null)
                {
                    return new BaseResponse<Lecture>
                    {
                        StatusCode = StatusCode.OK,
                        Data = lecture
                    };
                }
                else
                {
                    return new BaseResponse<Lecture>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Lecture not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Lecture>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while getting the lecture: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<FullCourseViewModel>> GetFullCourse(int id)
        {
            try
            {
                FullCourseViewModel fullCourses = await FullCourseRetrieval(id);

                if (fullCourses != null)
                {
                    return new BaseResponse<FullCourseViewModel>
                    {
                        StatusCode = StatusCode.OK,
                        Data = fullCourses
                    };
                }
                else
                {
                    return new BaseResponse<FullCourseViewModel>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Full courses not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<FullCourseViewModel>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while getting the full courses: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<List<Course>>> GetCourses()
        {
            try
            {
                List<Course> courses = await CourseListRetrieval();

                if (courses != null && courses.Any())
                {
                    return new BaseResponse<List<Course>>
                    {
                        StatusCode = StatusCode.OK,
                        Data = courses
                    };
                }
                else
                {
                    return new BaseResponse<List<Course>>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Courses not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Course>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while getting the courses: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<List<Lesson>>> GetLessons()
        {
            try
            {
                var lessons = await LessonRetrieval();

                if (lessons != null)
                {
                    return new BaseResponse<List<Lesson>>
                    {
                        StatusCode = StatusCode.OK,
                        Data = lessons
                    };
                }
                else
                {
                    return new BaseResponse<List<Lesson>>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Lessons not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Lesson>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while getting the lessons: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateCourse(Course course)
        {
            try
            {
                await CourseCreation(course);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the course: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateLesson(Lesson lesson)
        {
            try
            {
                await LessonCreation(lesson);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the lesson: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateLecture(LectureViewModel lecture)
        {
            try
            {
                await LectureCreation(lecture);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the lecture: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateTest(Test test)
        {
            try
            {
                await TestCreation(test);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the test: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreatePractice(Practice practice)
        {
            try
            {
                await PracticeCreation(practice);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the practice: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateQuest(Question question)
        {
            try
            {
                await QuestionCreation(question);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the question: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateAnswer(Answer answer)
        {
            try
            {
                await AnswerCreation(answer);

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"An error occurred while creating the answer: {ex.Message}"
                };
            }
        }
        private async Task<Course> CourseRetrieval(int id)
        {
            var course = await _context.CourseRepositories.GetAsync(id);
            return course;
        }

        private async Task<FullCourseViewModel> FullCourseRetrieval(int id)
        {
            var course = await _context.CourseRepositories.GetAsync(id);
            var lessons = _context.LessonRepositories.GetAllAsync().Where(c => c.CourseId == id).ToList();
            var fullCourse = new FullCourseViewModel()
            {
                Name = course.Name,
                Description = course.Description,
                Lessons = course.Lessons,
            };
            return fullCourse;
        }

        private async Task<List<Course>> CourseListRetrieval()
        {
            var courses = _context.CourseRepositories.GetAllAsync();
            return courses.ToList();
        }

        private async Task<List<Lesson>> LessonRetrieval()
        {
            var lessons = _context.LessonRepositories.GetAllAsync();
            return lessons.ToList();
        }

        private async Task<Lecture> LectureRetrieval(int id)
        {
            var lecture = await _context.LectureRepositories.GetAsync(id);
            return lecture;
        }
        private async Task CourseCreation(Course course)
        {
            await _context.CourseRepositories.CreateAsync(course);
        }
        private async Task LectureCreation(LectureViewModel lecture)
        {
            var newLecture = new Lecture()
            {
                Text = lecture.Text,
                Name = lecture.Name,
                LessonId = lecture.LessonId,
                IsComplited = false
            };

            await _context.LectureRepositories.CreateAsync(newLecture);
        }

        private async Task LessonCreation(Lesson lesson)
        {
            await _context.LessonRepositories.CreateAsync(lesson);
        }

        private async Task TestCreation(Test test)
        {
            await _context.TestRepositories.CreateAsync(test);
        }

        private async Task PracticeCreation(Practice practice)
        {
            await _context.PracticeRepositories.CreateAsync(practice);
        }
        private async Task QuestionCreation(Question question)
        {
            await _context.QuestionRepositories.CreateAsync(question);
        }
        private async Task AnswerCreation(Answer answer)
        {
            await _context.AnswerRepository.CreateAsync(answer);
        }
    }

}
