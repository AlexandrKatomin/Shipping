using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public interface IRepository<T> where T : ModelBase
    {

        T Get(int id);

        IQueryable<T> GetAll(
            Expression<Func<T, bool>> where, 
            int take, 
            int skip, 
            Expression<Func<T, object>> orderBy,
            bool? isDesc = null);
    }
}
