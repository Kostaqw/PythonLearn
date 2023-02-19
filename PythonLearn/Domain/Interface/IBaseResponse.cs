namespace PythonLearn.Domain.Interface
{
    public interface IBaseResponse<T>
    {
        T Data { get; }
    }
}
