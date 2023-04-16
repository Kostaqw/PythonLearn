using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Interface;

namespace PythonLearn.Service.interfaces
{
    public interface IArticleService
    {
        Task<IBaseResponse<IEnumerable<Article>>> GetArticlesAsync();
        Task<IBaseResponse<IEnumerable<Title>>> GetTitlesAsync();
        Task<IBaseResponse<bool>> CreateArticle(Article article);
        Task<IBaseResponse<bool>> CreateTitle(Title title);
        Task<IBaseResponse<Article>> GetArticleAsync(int id);
        Task<IBaseResponse<Title>> GetTitleAsync(int id);
        Task<IBaseResponse<bool>> DeleteArticleAsync(int id);
        Task<IBaseResponse<bool>> DeleteTitleAsync(int id);
        Task<IBaseResponse<Article>> EditArticleAsync(int id, Article article);
        Task<IBaseResponse<Title>> EditTitleAsync(int id, Title title);

    }
}
