using PythonLearn.Domain.Enum;

namespace PythonLearn.Domain.Interface
{
    public interface IBaseResponse<T>
    {
        string Description { get; set; }
        StatusCode StatusCode { get; set; }
        T Data { get; }
    }
}
