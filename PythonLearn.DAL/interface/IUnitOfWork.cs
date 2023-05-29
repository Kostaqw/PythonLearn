using PythonLearn.DAL.Repositories;

namespace University.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public AnswerRepository AnswerRepository { get; }
        public ArticleCommentRepository ArticleCommentRepository { get; }
        public ArticleRepository ArticleRepositories{ get; }
        public CourseRepository CourseRepositories { get; }
        public LectureRepository LectureRepositories { get; }
        public LessonCommentRepository LessonCommentsRepositories { get; }
        public LessonRepository LessonRepositories { get; }
        public PracticeRepository PracticeRepositories { get; }
        public SolutionRepository SolutionRepositories { get; }
        public TitleRepository TitleRepositories { get; }
        public TestRepository TestRepositories { get; }
        public QuestionRepository QuestionRepositories { get; }
        public UserRepository UserRepositories { get; }
        public void Save();

        public void Dispose();
    }
}