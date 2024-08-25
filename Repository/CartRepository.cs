using ETickets._Repository;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repository.IRepository;

namespace ETickets.Repository
{
    public class CartRepository : Repository<ShoppingCart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
