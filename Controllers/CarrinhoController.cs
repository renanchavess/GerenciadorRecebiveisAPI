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
        public ActionResult<Carrinho> GetCarrinho(int id)
        {
            var carrinho = _repository.GetCarrinho(id);
            return carrinho;
        }

        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            _repository.Create(carrinho);
            return CreatedAtAction("GetCarrinho", new { id = carrinho.Id }, carrinho);
        }

        [HttpPost("{id:int}/adicionarNotaFiscal")]
        public ActionResult AdicionarNotaFiscal(int id, int notaFiscalId)
        {
            var notaFiscal = _repositoryNotaFiscal.GetNotaFiscal(notaFiscalId);

            if (notaFiscal.EmpresaId != id && notaFiscal.DataVencimento.Date > DateTime.Now.AddDays(1).Date)
            {
                throw new ArgumentException();
            }
            
             _repository.AdicionarNotaFiscal(id, notaFiscal);
            return Ok();
        }

        [HttpDelete("{id:int}/removerNotaFiscal")]
        public ActionResult RemoverNotaFiscal(int id, int notaFiscalId)
        {
            var notaFiscal = _repositoryNotaFiscal.GetNotaFiscal(notaFiscalId);

            if (notaFiscal.CarrinhoId != id)
            {
                throw new ArgumentException();
            }
            
            _repository.RemoverNotaFiscal(id, notaFiscal);
            return Ok();
        }

    }
}