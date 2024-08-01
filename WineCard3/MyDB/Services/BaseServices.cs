using Microsoft.EntityFrameworkCore;
using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public class BaseServices<T> : IBaseService<T> where T : BaseModel
    {
        private readonly DataContext _cont;

        public BaseServices(DataContext cont)
        {
                _cont = cont;
        }

        public async Task CreateAsync(T entity)
        {
            await _cont.Set<T>().AddAsync(entity);
            await _cont.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            T t = await _cont.FindAsync<T>(entity.ID);

            if (t != null)
            {
                _cont.Entry(t).CurrentValues.SetValues(entity);
                await _cont.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(T entity)
        {
            T t = await _cont.FindAsync<T>(entity);

            if (t != null)
            {
                _cont.Set<T>().Remove(t);
                await _cont.SaveChangesAsync();
            }
        }

        public async Task DeleteByIDAsync(int id)
        {
            T t = await _cont.FindAsync<T>(id);

            if (t != null)
            {
                _cont.Set<T>().Remove(t);
                await _cont.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _cont.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _cont.Set<T>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }


    }
}
