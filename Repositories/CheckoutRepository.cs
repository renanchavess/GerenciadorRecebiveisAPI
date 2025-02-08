using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Context;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        public readonly RecebiveisDbContext _context;

        public CheckoutRepository(RecebiveisDbContext context)
        {
            _context = context;
        }

        public Checkout Create(Checkout checkout)
        {
            if (checkout == null)            
                throw new ArgumentNullException(nameof(checkout));

            _context.Checkouts.Add(checkout);
            _context.SaveChanges();
            return checkout;
        }

        public Checkout GetCheckout(int id)
        {
            var checkout = _context.Checkouts.Find(id);
            
            if (checkout == null)
                throw new ArgumentNullException(nameof(checkout));

            return checkout;
        }
    }
}