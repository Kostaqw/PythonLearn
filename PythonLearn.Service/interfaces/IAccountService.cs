using PythonLearn.Domain.Interface;
using PythonLearn.Domain.Response;
using PythonLearn.Domain.ViewModel.User;
using System.Security.Claims;

namespace PythonLearn.Service.interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Register(UserRegistorViewModel model);
        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<BaseResponse<bool>> EditAccount(UserViewModel model);
    }
}
