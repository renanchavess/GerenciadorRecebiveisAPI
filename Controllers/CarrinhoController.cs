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

        public CarrinhoController(ICarrinhoRepository repository, INotaFiscalRepository repositoryNotaFiscal)
        {
            _repositoryNotaFiscal = repositoryNotaFiscal;
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
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);

            if (notaFiscal.EmpresaId != id && notaFiscal.Vencida())
            {
                throw new ArgumentException();
            }
            
            await _repository.AdicionarNotaFiscalAsync(id, notaFiscal);
            return Ok();
        }

        [HttpDelete("{id:int}/removerNotaFiscal")]
        public async Task<ActionResult> RemoverNotaFiscal(int id, int notaFiscalId)
        {
            var notaFiscal = await _repositoryNotaFiscal.GetNotaFiscalAsync(notaFiscalId);

            if (notaFiscal.CarrinhoId != id)
            {
                throw new ArgumentException();
            }
            
            await _repository.RemoverNotaFiscalAsync(id, notaFiscal);
            
            return Ok();
        }

    }
}