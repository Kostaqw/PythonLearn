using PythonLearn.DAL.Repositories;

namespace University.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public ArticleCommentRepository ArticleCommentRepository { get; }
        public ArticleRepository ArticleRepositories{ get; }
        public CourseRepository CourseRepositories { get; }
        public ElementRepository ElementRepositories { get; }
        public LessonCommentRepository LessonCommentsRepositories { get; }
        public LessonRepository LessonRepositories { get; }
        public RoleRepository RoleRepositories { get; }
        public SolutionRepository SolutionRepositories { get; }
        public TitleRepository TitleRepositories { get; }
        public UserRepository UserRepositories { get; }
        public void Save();

        public void Dispose();
    }
}