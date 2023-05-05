using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> Create(T entity);
        Task<T> FindById(int id);
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate, int limit, int offset);
        Task Remove(int id);
        Task<T> Update(T entity);
        //Task<ResponseModel<T>> GetWithIncludes(int limit, int offset, Func<T, bool> predicat, params Expression<Func<T, object>>[] includeProperties);
    }
}
