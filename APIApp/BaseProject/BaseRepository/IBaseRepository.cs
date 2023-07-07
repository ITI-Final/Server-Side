namespace APIApp.Repositories.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {

         Task<IEnumerable<T>> GetAll();
         Task<T> GetById(int id);
         Task<T> IsExist(int id);
         Task Update(int id, T entity);
         Task DeleteById(int id);
         Task Add(T entity);
    }
}
