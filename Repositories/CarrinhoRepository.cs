using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Context;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        public readonly RecebiveisDbContext _context;

        public CarrinhoRepository(RecebiveisDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AdicionarNotaFiscalAsync(int id, NotaFiscal notafiscal)
        {
            Carrinho carrinho = await _context.Carrinhos.Include(c => c.NotasFiscais).FirstOrDefaultAsync(c => c.Id == id);

            if (carrinho == null || notafiscal == null)
            {
                throw new ArgumentNullException(nameof(carrinho));
            }            

            carrinho.NotasFiscais.Add(notafiscal);            
            _context.Entry(carrinho).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Carrinho> CreateAsync(Carrinho carrinho)
        {
            if (carrinho == null)            
                throw new ArgumentNullException(nameof(carrinho));

            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();
            return carrinho;
        }

        public async Task<Carrinho> GetCarrinhoAsync(int id)
        {
            var carrinho = await _context.Carrinhos
                                    .Include(c => c.Empresa)
                                    .Include(c => c.NotasFiscais)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (carrinho == null)            
                throw new ArgumentNullException(nameof(carrinho));

            return carrinho;
        }

        public async Task<bool> RemoverNotaFiscalAsync(int id, NotaFiscal notafiscal)
        {
            var carrinho = _context.Carrinhos.Include(c => c.NotasFiscais).FirstOrDefault(c => c.Id == id);
            if (carrinho == null || notafiscal == null)
            {
                throw new ArgumentNullException(nameof(carrinho));
            }

            carrinho.NotasFiscais.Remove(notafiscal);
            _context.Entry(carrinho).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}