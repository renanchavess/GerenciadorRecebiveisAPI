using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<Carrinho>> GetCarrinho(int id)
        {
            var carrinho = await _repository.GetCarrinhoAsync(id);
            return carrinho;
        }

        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            await _repository.CreateAsync(carrinho);
            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

        [HttpPost("{id:int}/adicionarNotaFiscal")]
        public async Task<ActionResult> AdicionarNotaFiscal(int id, int notaFiscalId)
        {
            var notaFiscal = await _repositoryNotaFiscal.GetNotaFiscalAsync(notaFiscalId);

            if (notaFiscal.EmpresaId != id && notaFiscal.DataVencimento.Date > DateTime.Now.AddDays(1).Date)
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