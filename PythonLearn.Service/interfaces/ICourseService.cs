using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.ViewModel.Course;

namespace PythonLearn.Service.interfaces
{
    public interface ICourseService
    {
        Task<IBaseResponse<Course>> GetCourse(int id);
        Task<IBaseResponse<List<FullCourseViewModel>>> GetFullCourses(int id);
        Task<IBaseResponse<List<Course>>> GetCourses();
        Task<IBaseResponse<bool>> CreateCourse(Course course);
    }
}