using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Interfaces
{
    public interface IRepository<T> where T : new()
    {
        Task UpdateAsync(T obj);
        Task RemoveAsync(T obj);
        Task<Int64> InsertAsync(T obj);
        Int64 Insert(T obj);
        Task<T> GetObjectAsync(Int64 Id);
        Task<IEnumerable<T>> GetListAsync();
        Task SaveChangesAsync();
    }
}
