namespace WebApi.Interfaces
{
    public interface IRepository <T>
    {
        T? Read(string id);
        IEnumerable<T> ReadAll();
        void Add(T obj);
        void Edit(string id, T obj);
        void Delete(string id);
    }
}
