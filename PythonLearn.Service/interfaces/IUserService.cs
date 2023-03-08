using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Interface;
using PythonLearn.Domain.ViewModel.User;

namespace PythonLearn.Service.interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<User>>> GetUsersAsync();

        Task<IBaseResponse<bool>> CreateUser(UserViewModel user);

        Task<IBaseResponse<User>> GetUserAsync(int id);
        Task<IBaseResponse<IEnumerable<User>>> GetUserByNameAsync(string name);
        Task<IBaseResponse<IEnumerable<User>>> GetUserBySecondNameAsync(string secondName);
        Task<IBaseResponse<bool>> DeleteUserAsync(int id);

        Task<IBaseResponse<User>> EditAsync(int id, UserViewModel model);

    }
}
