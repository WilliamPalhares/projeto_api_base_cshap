using API_Cadastro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly ILogger logger;
        protected readonly DbSet<T> dbSet;
        protected readonly ApplicationContext context;

        public BaseRepository(ApplicationContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this.logger = logger;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            try
            {
                return await this.dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao consultar: {ex.Message}");
                throw ex;
            }
        }

        public virtual async Task<T> GetObjectAsync(Int64 Id)
        {
            try
            {
                return await this.dbSet.FindAsync(Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao consultar: {ex.Message}");
                throw ex;
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<long> InsertAsync(T obj)
        {
            try
            {
                this.dbSet.Add(obj);
                await SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao inserir: {ex.Message}");
                throw ex;
            }
        }

        public virtual long Insert(T obj)
        {
            Task<long> r = InsertAsync(obj);
            r.Wait();
            return r.Result;
        }

        public virtual async Task UpdateAsync(T obj)
        {
            try
            {
                var entry = this.dbSet.Update(obj);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao atualizar: {ex.Message}");
                throw ex;
            }
        }

        public virtual async Task RemoveAsync(T obj)
        {
            try
            {
                this.dbSet.Attach(obj);
                this.dbSet.Remove(obj);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao remover: {ex.Message}");
                throw ex;
            }
        }
    }
}
