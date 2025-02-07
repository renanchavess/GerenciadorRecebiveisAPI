using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly Context.RecebiveisDbContext _context;

        public CarrinhoController(Context.RecebiveisDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetCarrinho")]
        public async Task<ActionResult<Carrinho>> GetCarrinho(int id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);

            if (carrinho == null)
            {
                return NotFound();
            }

            return carrinho;
        }

        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

        [HttpPost("{id:int}/adicionarNotaFiscal")]
        public async Task<ActionResult<Carrinho>> AdicionarNotaFiscal(int id, int notaFiscalId)
        {
            Carrinho carrinho = await _context.Carrinhos.FirstOrDefaultAsync<Carrinho>((c => c.Id == id));
            NotaFiscal notaFiscal = await _context.NotasFiscais.FirstOrDefaultAsync<NotaFiscal>((n => n.Id == notaFiscalId));

            if (carrinho == null || notaFiscal == null)
            {
                return NotFound();
            }

            if (notaFiscal.EmpresaId != carrinho.EmpresaId && notaFiscal.DataVencimento.Date > DateTime.Now.Date)
            {
                return BadRequest();
            }

            carrinho.NotasFiscais.Add(notaFiscal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

        [HttpDelete("{id:int}/removerNotaFiscal")]
        public async Task<ActionResult<Carrinho>> RemoverNotaFiscal(int id, int notaFiscalId)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);
            var notaFiscal = await _context.NotasFiscais.FindAsync(notaFiscalId);

            if (carrinho == null || notaFiscal == null)
            {
                return NotFound();
            }

            if (notaFiscal.EmpresaId != carrinho.EmpresaId)
            {
                return BadRequest();
            }

            carrinho.NotasFiscais.Remove(notaFiscal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

        [HttpPost("{id:int}/checkout")]
        public async Task<ActionResult<Carrinho>> checkout(int id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);

            if (carrinho == null)
            {
                return NotFound();
            }

            if (carrinho.NotasFiscais.Count == 0)
            {
                return BadRequest();
            }

            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

    }
}