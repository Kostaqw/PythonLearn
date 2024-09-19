using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Enum;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.Response;
using PythonLearn.Domain.ViewModel.Article;
using PythonLearn.Service.interfaces;
using University.DAL.Interfaces;

namespace PythonLearn.Service.implementation
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _context;

        /// <summary>
        /// Конструктор для dependence injactive 
        /// </summary>
        /// <param name="context">Объект реализующий интерфейс IUnitOfWork</param>
        public ArticleService(IUnitOfWork context)
        {
            _context = context;
        }

        //CREATE

        /// <summary>
        /// Создание статьи
        /// </summary>
        /// <param name="article">Статья</param>
        /// <returns>true в случае успешного создания</returns>
        public async Task<IBaseResponse<bool>> CreateArticle(Article article)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var newArticle = new Article()
                {
                    TitleId = article.TitleId,
                    ArticleText = article.ArticleText,
                    UserId = article.UserId
                };
                await _context.ArticleRepositories.CreateAsync(newArticle);

                response.Description = $"Статья успешно создана";
                response.StatusCode = StatusCode.OK;
                response.Data = true;

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] CreateArticle(Article article): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Создание заголовка
        /// </summary>
        /// <param name="title">Заголовок</param>
        /// <returns>true в случае успешного создания</returns>
        public async Task<IBaseResponse<bool>> CreateTitle(Title title)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var newTitle = new Title()
                {
                    Name = title.Name,
                    ShortDescription = title.ShortDescription
                };
                
                await _context.TitleRepositories.CreateAsync(newTitle);

                response.Description = $"Заголовок успешно создана";
                response.StatusCode = StatusCode.OK;
                response.Data = true;

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] CreateTitle(Title title): {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateArticleWithTitle(CompleteArticle article)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var newTitle = new Title()
                {
                    Name = article.Name,
                    ShortDescription = article.ShortDescription
                };

                await _context.TitleRepositories.CreateAsync(newTitle);

                article.TitleId = _context.TitleRepositories.GetAllAsync().FirstOrDefault(x => x.Name == article.Name).Id;
                var newArticle = new Article()
                {
                    TitleId = article.TitleId,
                    ArticleText = article.ArticleText,
                    UserId = article.UserId,
                    CreatedDate = DateTime.Now,
                };
                await _context.ArticleRepositories.CreateAsync(newArticle);
                
                response.Description = $"Статья успешно создана";
                response.StatusCode = StatusCode.OK;
                response.Data = true;

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] CreateArticleWithTitle(CompleteArticle article): {ex.Message}"
                };
            }
        }

        //READ
        /// <summary>
        /// Возвращает статью по id
        /// </summary>
        /// <param name="id">id статьи</param>
        /// <returns>Статью</returns>
        public async Task<IBaseResponse<Article>> GetArticleAsync(int id)
        {
            var response = new BaseResponse<Article>();
            try
            {
                var article = await _context.ArticleRepositories.GetAsync(id);
                if (article == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Статья с id {id} не найден в БД";
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Статья {article.Title.Name} с id {id} найдена";
                    response.Data = article;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] GetArticleAsync(int id): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Получить полную статью с заголовком и текстом статьи
        /// </summary>
        /// <param name="id"> Id статьи </param>
        /// <returns></returns>
        public async Task<IBaseResponse<CompleteArticle>> GetCompleteArticle(int id)
        {
            var response = new BaseResponse<CompleteArticle>();
            try
            {
                var article = await _context.ArticleRepositories.GetAsync(id);
                if (article == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Статья с id {id} не найден в БД";
                    return response;
                }
                else
                {
                    var title = await _context.TitleRepositories.GetAsync(article.TitleId);

                    var completeArticle = new CompleteArticle()
                    {
                        TitleId = title.Id,
                        ArticleId = article.Id,
                        Name = title.Name,
                        ShortDescription = title.ShortDescription,
                        ArticleText = article.ArticleText,
                        CreatedDate = article.CreatedDate,
                        UserId = article.UserId,
                        user = article.User
                    };
                    
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Статья {article.Title.Name} с id {id} найдена";

                    response.Data = completeArticle;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CompleteArticle>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] GetCompleteArticle(int id): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Получить все статьи
        /// </summary>
        /// <returns>Список статей</returns>
        public async Task<IBaseResponse<IEnumerable<Article>>> GetArticlesAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<Article>>();
            try
            {
                var articles = await _context.ArticleRepositories.GetAllAsync().ToListAsync();
                if (articles.Count == 0)
                {
                    baseResponse.StatusCode = StatusCode.Warn;
                    baseResponse.Description = $"Найдено 0 элементов";
                    return baseResponse;
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Description = $"Найдено {articles.Count} статей";
                    baseResponse.Data = articles;
                    return baseResponse;
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Article>>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] GetArticlesAsync(): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Получить заголовок по его id
        /// </summary>
        /// <param name="id">id заголовка</param>
        /// <returns>Заголовок</returns>
        public async Task<IBaseResponse<Title>> GetTitleAsync(int id)
        {
            var response = new BaseResponse<Title>();
            try
            {
                var title = await _context.TitleRepositories.GetAsync(id);
                if (title == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Заголовок с id {id} не найден в БД";
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Заголовок {title.Name} с id {id} найден";
                    response.Data = title;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Title>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] GetTitleAsync(int id): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Получить список всех заголовков
        /// </summary>
        /// <returns>Список заголовков</returns>
        public async Task<IBaseResponse<IEnumerable<Title>>> GetTitlesAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<Title>>();
            try
            {
                var titles = await _context.TitleRepositories.GetAllAsync().ToListAsync();
                if (titles.Count == 0)
                {
                    baseResponse.StatusCode = StatusCode.Warn;
                    baseResponse.Description = $"Найдено 0 элементов";
                    return baseResponse;
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Description = $"Найдено {titles.Count} заголовков";
                    baseResponse.Data = titles;
                    return baseResponse;
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Title>>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] GetTitlesAsync(): {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Изменить статью
        /// </summary>
        /// <param name="id">id статьи</param>
        /// <param name="article">новая статья</param>
        public async Task<IBaseResponse<Article>> EditArticleAsync(int id, Article newArticle)
        {
            var response = new BaseResponse<Article>();
            try
            {
                var article = await _context.ArticleRepositories.GetAsync(id);
                if (article == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"статья с id {id} не найден в БД";
                    return response;
                }
                else
                {
                    if (newArticle.ArticleText != null)
                    {
                        article.ArticleText = article.ArticleText;
                    }
                    if (newArticle.TitleId != 0)
                    {
                        article.TitleId = newArticle.TitleId;
                    }
                    if (newArticle.UserId != 0)
                    {
                        article.UserId = newArticle.UserId;
                    }

                    await _context.ArticleRepositories.UpdateAsync(article);

                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Статья изменена";
                    response.Data = article;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Article>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] EditArticleAsync(int id, Article newArticle): {ex.Message}"
                };
            }
        }


        /// <summary>
        /// Обновить заголовок
        /// </summary>
        /// <param name="id">id заголовка</param>
        /// <param name="newTitle">новый заголовок</param>
        public async Task<IBaseResponse<Title>> EditTitleAsync(int id, Title newTitle)
        {
            var response = new BaseResponse<Title>();
            try
            {
                var title = await _context.TitleRepositories.GetAsync(id);
                if (title == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Заголовок с id {id} не найден в БД";
                    return response;
                }
                else
                {
                    if (title.Name != null)
                    {
                        title.Name = newTitle.Name;
                    }
                    if (title.ShortDescription != null)
                    {
                        title.ShortDescription= newTitle.ShortDescription;
                    }

                    await _context.TitleRepositories.UpdateAsync(title);

                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Статья изменена";
                    response.Data = title;
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Title>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] EditTitleAsync(int id, Title newTitle): {ex.Message}"
                };
            }
        }



        //DELETE

        /// <summary>
        /// Удалить статью по её id
        /// </summary>
        /// <param name="id"> id статьи</param>
        /// <returns>true в случае успеха</returns>
        public async Task<IBaseResponse<bool>> DeleteArticleAsync(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var article = await _context.ArticleRepositories.GetAsync(id);
                if (article == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Статья с id {id} не найден в БД";
                    response.Data = false;
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Статья с именем {article.Title.Name} с id {id} удалена";
                    response.Data = true;
                    await _context.ArticleRepositories.DeleteAsync(id);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] DeleteArticleAsync(int id): {ex.Message}",
                    Data = false

                };
            }
        }

        /// <summary>
        /// Удалить заголовок статьи
        /// </summary>
        /// <param name="id">id заголовка</param>
        /// <returns>true в случае успеха</returns>
        public async Task<IBaseResponse<bool>> DeleteTitleAsync(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var title = await _context.TitleRepositories.GetAsync(id);
                if (title == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = $"Заголовок с id {id} не найден в БД";
                    response.Data = false;
                    return response;
                }
                else
                {
                    response.StatusCode = StatusCode.OK;
                    response.Description = $"Заголовок {title.Name} с id {id} удален";
                    response.Data = true;
                    await _context.TitleRepositories.DeleteAsync(id);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.BadRequest,
                    Description = $"[ArticleService] DeleteTitleAsync(int id): {ex.Message}",
                    Data = false
                };
            }
        }
    }
}
