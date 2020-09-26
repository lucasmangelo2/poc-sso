﻿using IdentityServer.SSO.Data.Interfaces.Context;
using IdentityServer.SSO.Data.Interfaces.Repository;
using IdentityServer.SSO.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.SSO.Data.Repository
{
    public class BaseRepository<TModel, TContext> : IRepository<TModel>
        where TModel : BaseModel
        where TContext : IDbContext
    {
        public readonly TContext Context;

        public BaseRepository(TContext context)
        {
            Context = context;
        }

        public Task DeleteAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TModel>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return Context.Set<TModel>().AsNoTracking();
            });
        }

        public Task<TModel> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                return Context.Set<TModel>().AsNoTracking().FirstOrDefault(x => x.Id == id);
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
                DetachLocal(model);

                Context.Set<TModel>().Update(model);

                Context.Entry(model).State = EntityState.Modified;

                SaveChanges();

                return model;
            });
        }

        private void DetachLocal(TModel model)
        {
            TModel local = Context.Set<TModel>().Local.FirstOrDefault(x => x.Id == model.Id);

            if (local != null)
                DetachEntity(local);
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
