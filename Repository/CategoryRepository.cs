using ETickets._Repository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repositor.IRepository;

namespace ETickets.Repositor
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
