using System;
using System.Collections.Generic;
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
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalRepository _repository;

        public NotaFiscalController(INotaFiscalRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}", Name = "GetNotaFiscal")]
        public async Task<ActionResult<ResponseNotaFiscal>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _repository.GetNotaFiscalAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            ResponseNotaFiscal responseNota = new ResponseNotaFiscal()
            {
                Id = notaFiscal.Id,
                Numero = notaFiscal.Numero,
                Valor = notaFiscal.Valor,
                DataVencimento = DateOnly.FromDateTime(notaFiscal.DataVencimento),
                EmpresaId = notaFiscal.EmpresaId
            };

            return responseNota;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseNotaFiscal>> PostNotaFiscal(RequestPostNotaFiscal notaFiscalPost)
        {
            if (notaFiscalPost.DataVencimento <= DateOnly.FromDateTime(DateTime.Now))
                return BadRequest("Data de vencimento inválida!");
            
            NotaFiscal notaFiscal = new NotaFiscal()
            {
                Numero = notaFiscalPost.Numero,
                Valor = notaFiscalPost.Valor,
                DataVencimento = notaFiscalPost.DataVencimento.ToDateTime(TimeOnly.MinValue),
                EmpresaId = notaFiscalPost.EmpresaId
            };
    
            await _repository.CreateAsync(notaFiscal);
            
            return CreatedAtAction("GetNotaFiscal", new { id = notaFiscal.Id }, notaFiscal);
        }

    }
}