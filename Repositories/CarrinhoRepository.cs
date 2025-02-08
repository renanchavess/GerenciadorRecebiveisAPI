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
        
        public Carrinho AdicionarNotaFiscal(int id, NotaFiscal notafiscal)
        {
            var carrinho = _context.Carrinhos.Include(c => c.NotasFiscais).FirstOrDefault(c => c.Id == id);
            if (carrinho == null || notafiscal == null)
            {
                throw new ArgumentNullException(nameof(carrinho));
            }            

            carrinho.NotasFiscais.Add(notafiscal);
            _context.Entry(carrinho).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return carrinho;
        }

        public Carrinho Create(Carrinho carrinho)
        {
            if (carrinho == null)            
                throw new ArgumentNullException(nameof(carrinho));

            _context.Carrinhos.Add(carrinho);
            _context.SaveChanges();
            return carrinho;
        }

        public Carrinho GetCarrinho(int id)
        {
            var carrinho = _context.Carrinhos
                                    .Include(c => c.Empresa)
                                    .Include(c => c.NotasFiscais)
                                    .FirstOrDefault(c => c.Id == id);

            if (carrinho == null)            
                throw new ArgumentNullException(nameof(carrinho));

            return carrinho;
        }

        public Carrinho RemoverNotaFiscal(int id, NotaFiscal notafiscal)
        {
            var carrinho = _context.Carrinhos.Include(c => c.NotasFiscais).FirstOrDefault(c => c.Id == id);
            if (carrinho == null || notafiscal == null)
            {
                throw new ArgumentNullException(nameof(carrinho));
            }

            carrinho.NotasFiscais.Remove(notafiscal);
            _context.Entry(carrinho).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return carrinho;
        }
    }
}