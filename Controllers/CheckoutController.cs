using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<string>> PostCheckout(int carrinhoId)
        {
            var carrinho = await _context.Carrinhos.FindAsync(carrinhoId);

            if (carrinho == null)
            {
                return NotFound();
            }

            
            if (carrinho.NotasFiscais == null)
            {
                return BadRequest("Carrinho sem notas fiscais!");
            }

            decimal taxa = 4.65m;

            Checkout checkout = new Checkout(taxa, carrinho);
            checkout.ValidarLimiteAntecipacao();
            checkout.CalcularDesagio(taxa);

            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();

            return Ok("Checkout realizado com sucesso!");
        }
    }
}