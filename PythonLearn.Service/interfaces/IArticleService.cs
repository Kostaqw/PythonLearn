using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.ViewModel.Article;

namespace PythonLearn.Service.interfaces
{
    public interface IArticleService
    {
        Task<IBaseResponse<IEnumerable<Article>>> GetArticlesAsync();
        Task<IBaseResponse<IEnumerable<Title>>> GetTitlesAsync();
        Task<IBaseResponse<bool>> CreateArticle(Article article);
        Task<IBaseResponse<bool>> CreateTitle(Title title);
        Task<IBaseResponse<bool>> CreateArticleWithTitle(CompleteArticle article);
        Task<IBaseResponse<CompleteArticle>> GetCompleteArticle(int id);
        Task<IBaseResponse<Article>> GetArticleAsync(int id);
        Task<IBaseResponse<Title>> GetTitleAsync(int id);
        Task<IBaseResponse<bool>> DeleteArticleAsync(int id);
        Task<IBaseResponse<bool>> DeleteTitleAsync(int id);
        Task<IBaseResponse<Article>> EditArticleAsync(int id, Article article);
        Task<IBaseResponse<Title>> EditTitleAsync(int id, Title title);

    }
}
