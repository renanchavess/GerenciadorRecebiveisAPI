using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Enum;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly Context.RecebiveisDbContext _context;

        public CheckoutController(Context.RecebiveisDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetCheckout")]
        public async Task<ActionResult<Checkout>> GetCheckout(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);

            if (checkout == null)
            {
                return NotFound();
            }

            return checkout;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseCheckout>> PostCheckout(int carrinhoId)
        {
            Carrinho carrinho = await _context.Carrinhos
                                            .Include(c => c.Empresa)
                                            .Include(c => c.NotasFiscais)
                                            .FirstOrDefaultAsync<Carrinho>((c => c.Id == carrinhoId));
            
            if (carrinho == null)
            {
                return NotFound();
            }
            
            if (carrinho.NotasFiscais.Count == 0)
            {
                return BadRequest("Carrinho sem notas fiscais!");
            }

            decimal taxa = 4.65m;
            Checkout checkout = new Checkout(taxa, carrinho);

            if (checkout.ValidarLimiteAntecipacao()) {
                return BadRequest("Limite de antecipação excedido!");
            }

            var notasFiscaisDesagio = checkout.CalcularDesagio((double)taxa);

            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();

            ResponseCheckout responseCheckout = new ResponseCheckout(
                carrinho.Empresa.Nome,
                carrinho.Empresa.CNPJ,
                checkout.calcularLimite(carrinho.Empresa),
                checkout.ValorLiquido,
                checkout.ValorBruto,
                notasFiscaisDesagio
            );

            return Ok(responseCheckout);
        }
    }
}