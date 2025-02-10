using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Checkout> GetAsync(int id)
        {
            Checkout checkout = await _context.Checkouts.FindAsync(id);

            return checkout;
        }

        public async Task<Checkout> GetCheckoutByCarrinhoId(int carrinhoId)
        {
            Checkout checkout = await _context.Checkouts.FirstOrDefaultAsync(c => c.CarrinhoId == carrinhoId);

            return checkout;
        }
    }
}