using ETickets._Repository.IRepository;
using ETickets.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ETickets._Repository
{
    public class Repository <T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
            Commit();
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            Commit();
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
            Commit();
        }
        //public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = dbSet;

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    return query.ToList();
        //}
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.ToList();
        }
        public IEnumerable<T> Get(Expression<Func<T, bool>> expression, params
           Expression<Func<T, object>>[] properties)
        {
            IQueryable<T> value = dbSet;
            foreach (var property in properties)
            {
                value = value.Include(property);
            }
            value = value.Where(expression);
            return value;
        }
    }
}
