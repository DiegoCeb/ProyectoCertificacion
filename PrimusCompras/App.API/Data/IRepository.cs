using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.API.Data
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(object id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(object id);
    }
}
