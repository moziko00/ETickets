using System.Linq.Expressions;

namespace ETickets._Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        void Create(T entity);
        void Edit(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> Get(Expression<Func<T, bool>> expression, params
            Expression<Func<T, object>>[] properties);
        void Commit();
    }
}
