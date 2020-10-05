using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Repository
{
    public class BaseRepository<TModel, TContext> : IRepository<TModel>
        where TModel : class
        where TContext : IDbContext
    {
        public readonly TContext Context;

        public BaseRepository(TContext context)
        {
            Context = context;
        }

        public Task DeleteAsync(TModel model)
        {
            return Task.Run(() =>
            {
                if (model != null)
                {
                    Context.Set<TModel>().Remove(model);

                    SaveChanges();
                }
            });
        }

        public Task<IQueryable<TModel>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return Context.Set<TModel>().AsNoTracking();
            });
        }

        public async Task<TModel> InsertAsync(TModel model)
        {
            await Context.Set<TModel>().AddAsync(model);

            SaveChanges();

            return model;
        }

        public Task<TModel> UpdateAsync(TModel model)
        {
            return Task.Run(() =>
            {
                Context.Set<TModel>().Update(model);

                Context.Entry(model).State = EntityState.Modified;

                SaveChanges();

                return model;
            });
        }

        public void DetachEntity(TModel model)
        {
            (Context as DbContext).Entry(model).State = EntityState.Detached;
        }

        public bool SaveChanges()
        {
            return Context.SaveChanges() > 0;
        }
    }
}
