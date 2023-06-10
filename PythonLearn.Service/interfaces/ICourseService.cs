using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.ViewModel.Course;

namespace PythonLearn.Service.interfaces
{
    public interface ICourseService
    {
        Task<IBaseResponse<Course>> GetCourse(int id);
        Task<IBaseResponse<Lecture>> GetLecture(int id);
        Task<IBaseResponse<FullCourseViewModel>> GetFullCourse(int id);
        Task<IBaseResponse<List<Course>>> GetCourses();
        Task<IBaseResponse<List<Lesson>>> GetLessons();
        Task<IBaseResponse<bool>> CreateCourse(Course course);
        Task<IBaseResponse<bool>> CreateLesson(Lesson lesson);
        Task<IBaseResponse<bool>> CreateLecture(LectureViewModel lecture);
        Task<IBaseResponse<bool>> CreateTest(Test test);
        Task<IBaseResponse<bool>> CreatePractice(Practice practice);
        Task<IBaseResponse<bool>> CreateQuest(Question question);
        Task<IBaseResponse<bool>> CreateAnswer(Answer answer);
    }
}