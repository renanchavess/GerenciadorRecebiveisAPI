using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Enum;
using GerenciadorRecebiveisAPI.Models;
using GerenciadorRecebiveisAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly ICarrinhoRepository _carrinhoRepository;

        public CheckoutController(ICheckoutRepository checkoutRepository, ICarrinhoRepository carrinhoRepository)
        {
            _checkoutRepository = checkoutRepository;
            _carrinhoRepository = carrinhoRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseCheckout>> PostCheckout(int carrinhoId)
        {
            Carrinho carrinho = await _carrinhoRepository.GetCarrinhoAsync(carrinhoId);

            if (carrinho.NotasFiscais.Count == 0)
            {
                return BadRequest("Carrinho sem notas fiscais!");
            }

            Checkout checkout = await _checkoutRepository.GetCheckoutByCarrinhoId(carrinhoId);

            if (checkout is not null)
            {
                return BadRequest("Checkout já realizado!");
            }

            decimal taxa = 4.65m;
            
            checkout = new Checkout(taxa, carrinho);

            if (checkout.ValidarLimiteAntecipacao()) {
                return BadRequest("Limite de antecipação excedido!");
            }

            var notasFiscaisDesagio = checkout.CalcularDesagio((double)taxa);

            await _checkoutRepository.CreateAsync(checkout);

            ResponseCheckout responseCheckout = new ResponseCheckout(){
                Empresa = carrinho.Empresa.Nome,
                Cnpj = carrinho.Empresa.CNPJ,
                Limite = checkout.CalcularLimite(carrinho.Empresa),
                NotasFiscais = notasFiscaisDesagio
            };

            return Ok(responseCheckout);
        }
    }
}