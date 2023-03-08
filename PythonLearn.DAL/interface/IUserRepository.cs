using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL
{
    public interface IUserRepository: IBaseRepository<User>
    {
        public Task<List<User>?> GetByNameAsync(string name);
        public Task<List<User>?> GetBySecondNameAsync(string secondName);
    }
}
