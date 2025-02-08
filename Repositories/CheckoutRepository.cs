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

        public async Task<Checkout> CreateAsync(Checkout checkout)
        {
            if (checkout == null)            
                throw new ArgumentNullException(nameof(checkout));

            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();
            return checkout;
        }

        public async Task<Checkout> GetCheckoutAsync(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            
            if (checkout == null)
                throw new ArgumentNullException(nameof(checkout));

            return checkout;
        }
    }
}