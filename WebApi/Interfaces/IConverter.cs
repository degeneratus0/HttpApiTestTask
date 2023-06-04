namespace WebApi.Interfaces
{
    public interface IConverter<T, DTO>
    {
        DTO ToDTO(T obj);
        T FromDTO(DTO obj);
    }
}
