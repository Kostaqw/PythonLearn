using University.DAL.Interfaces;

namespace PythonLearn.DAL.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private ArticleCommentRepository articleCommentRepository;
        private ArticleRepository articleRepository;
        private CourseRepository courseRepository;
        private ElementRepository elementRepository;
        private LessonCommentRepository lessonCommentsRepositories;
        private LessonRepository lessonRepository;
        private UserRepository userRepository;
        private SolutionRepository solutionRepository;
        private TitleRepository titleRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ArticleCommentRepository ArticleCommentRepository
        {
            get
            {
                if (articleCommentRepository == null)
                {
                    articleCommentRepository = new ArticleCommentRepository(context);
                }
                return articleCommentRepository;
            }
        }
        public ArticleRepository ArticleRepositories
        {
            get
            {
                if (articleRepository == null)
                {
                    articleRepository = new ArticleRepository(context);
                }
                return articleRepository;
            }
        }

        public CourseRepository CourseRepositories
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository(context);
                }
                return courseRepository;
            }
        }

        public ElementRepository ElementRepositories
        {
            get
            {
                if (elementRepository == null)
                {
                    elementRepository = new ElementRepository(context);
                }
                return elementRepository;
            }
        }
        public LessonCommentRepository LessonCommentsRepositories
        {
            get
            {
                if (lessonCommentsRepositories == null)
                {
                    lessonCommentsRepositories = new LessonCommentRepository(context);
                }
                return lessonCommentsRepositories;
            }
        }
        public LessonRepository LessonRepositories
        {
            get
            {
                if (lessonRepository == null)
                {
                    lessonRepository = new LessonRepository(context);
                }
                return lessonRepository;
            }
        }


        public SolutionRepository SolutionRepositories
        {
            get
            {
                if (solutionRepository == null)
                {
                    solutionRepository = new SolutionRepository(context);
                }
                return solutionRepository;
            }
        }

        public TitleRepository TitleRepositories
        {
            get
            {
                if (titleRepository == null)
                {
                    titleRepository = new TitleRepository(context);
                }
                return titleRepository;
            }
        }

        public UserRepository UserRepositories
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
