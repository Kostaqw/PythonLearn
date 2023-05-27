using PythonLearn.DAL.Repositories;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Enum;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.Response;
using PythonLearn.Domain.ViewModel.Course;
using PythonLearn.Service.interfaces;

namespace PythonLearn.Service.implementation
{
    public class CourseService : ICourseService
    {
        private readonly UnitOfWork _context;

        public CourseService(UnitOfWork context)
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

        public async Task<IBaseResponse<List<FullCourseViewModel>>> GetFullCourses(int id)
        {
            try
            {
                List<FullCourseViewModel> fullCourses = await FullCourseRetrieval(id);

                if (fullCourses != null && fullCourses.Any())
                {
                    return new BaseResponse<List<FullCourseViewModel>>
                    {
                        StatusCode = StatusCode.OK,
                        Data = fullCourses
                    };
                }
                else
                {
                    return new BaseResponse<List<FullCourseViewModel>>
                    {
                        StatusCode = StatusCode.NotFound,
                        Description = "Full courses not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<FullCourseViewModel>>
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

        private async Task<Course> CourseRetrieval(int id)
        {
            var course = await _context.CourseRepositories.GetAsync(id);
            return course;
        }

        private async Task<List<FullCourseViewModel>> FullCourseRetrieval(int id)
        {
            // Ваша реализация логики получения полной информации о курсах
            throw new NotImplementedException();
        }

        private async Task<List<Course>> CourseListRetrieval()
        {
            var courses = _context.CourseRepositories.GetAllAsync();
            return courses.ToList();
        }

        private async Task CourseCreation(Course course)
        {
            // Ваша реализация логики создания курса
            throw new NotImplementedException();
        }
    }

}
