using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        private readonly DbSet<T> DbSet;
        public readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public T Get(int id)
        {
            return this.DbSet.FirstOrDefault<T>((Expression<Func<T, bool>>) (c => c.Id == id));
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> where, int take, int skip, Expression<Func<T, object>> orderBy = null, bool? isDesc = null)
        {
            return orderBy == null || isDesc == null
                ? this.DbSet.Where<T>(where).Skip(skip).Take(take)
                : isDesc == true
                    ? this.DbSet.OrderByDescending(orderBy).Where(where).Skip(skip).Take(take)
                    : this.DbSet.OrderBy(orderBy).Where(where).Skip(skip).Take(take);
        }
    }
}
