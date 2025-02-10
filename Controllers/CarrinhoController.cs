using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Models;
using GerenciadorRecebiveisAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepository _repository;
        private readonly INotaFiscalRepository _repositoryNotaFiscal;
        private readonly ICheckoutRepository _repositoryCheckout;

        public CarrinhoController(ICarrinhoRepository repository, INotaFiscalRepository repositoryNotaFiscal, ICheckoutRepository repositoryCheckout)
        {
            _repositoryNotaFiscal = repositoryNotaFiscal;
            _repositoryCheckout = repositoryCheckout;
            _repository = repository;
        }

        [HttpGet("{id:int}", Name = "GetCarrinho")]
        public async Task<ActionResult<ResponseCarrinho>> GetCarrinho(int id)
        {
            Carrinho carrinho = await _repository.GetCarrinhoAsync(id);
            ResponseCarrinho responseCarrinho = new ResponseCarrinho()
            {
                Id = carrinho.Id,
                EmpresaId = carrinho.EmpresaId,
                NotasFiscais = carrinho.NotasFiscais.Select(nf => new ResponseNotaFiscal()
                {
                    Id = nf.Id,
                    Numero = nf.Numero,
                    Valor = nf.Valor,
                    DataVencimento = nf.DataVencimento,
                    EmpresaId = nf.EmpresaId
                }).ToList()
            };
            
            return responseCarrinho;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseCarrinho>> PostCarrinho(RequestPostCarrinho carrinhoRequest)
        {
            Carrinho carrinho = new Carrinho()
            {
                EmpresaId = carrinhoRequest.EmpresaId                
            };

            await _repository.CreateAsync(carrinho);
            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

        [HttpPost("{id:int}/adicionarNotaFiscal")]
        public async Task<ActionResult> AdicionarNotaFiscal(int id, [Required] int notaFiscalId)
        {
            var notaFiscal = await _repositoryNotaFiscal.GetNotaFiscalAsync(notaFiscalId);
            Checkout checkout = await _repositoryCheckout.GetCheckoutByCarrinhoId(id);
            Carrinho carrinho = await _repository.GetCarrinhoAsync(id);

            if (notaFiscal.EmpresaId != carrinho.EmpresaId)
            {
                return BadRequest("Nota fiscal não pertence a empresa!");
            }

            if (notaFiscal.Vencida())
            {
                return BadRequest("Nota fiscal vencida!");
            }

            if (checkout is not null)
            {
                return BadRequest("Checkout já realizado carrinho não pode ser alterado!");
            }
            
            await _repository.AdicionarNotaFiscalAsync(id, notaFiscal);
            return Ok();
        }

        [HttpDelete("{id:int}/removerNotaFiscal")]
        public async Task<ActionResult> RemoverNotaFiscal(int id, int notaFiscalId)
        {
            var notaFiscal = await _repositoryNotaFiscal.GetNotaFiscalAsync(notaFiscalId);
            Checkout checkout = await _repositoryCheckout.GetCheckoutByCarrinhoId(id);

            if (notaFiscal.CarrinhoId != id)
            {
                return BadRequest("Nota fiscal não pertence ao carrinho!");
            }
            
            if (checkout is not null)
            {
                return BadRequest("Checkout já realizado carrinho não pode ser alterado!");
            }

            await _repository.RemoverNotaFiscalAsync(id, notaFiscal);
            
            return Ok();
        }

    }
}