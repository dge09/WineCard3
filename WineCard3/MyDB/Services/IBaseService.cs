namespace WineCard3.MyDB.Services
{
    public interface IBaseService<T>
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIDAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);

    }
}
