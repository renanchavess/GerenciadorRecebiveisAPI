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

        [HttpGet("{id:int}", Name = "GetCheckout")]
        public ActionResult<Checkout> GetCheckout(int id)
        {
            var checkout = _checkoutRepository.GetCheckout(id);
            return checkout;
        }

        [HttpPost]
        public ActionResult<ResponseCheckout> PostCheckout(int carrinhoId)
        {
            Carrinho carrinho = _carrinhoRepository.GetCarrinho(carrinhoId);

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

            _checkoutRepository.Create(checkout);

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