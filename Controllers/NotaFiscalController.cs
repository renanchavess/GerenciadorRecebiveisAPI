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
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalRepository _repository;

        public NotaFiscalController(INotaFiscalRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}", Name = "GetNotaFiscal")]
        public ActionResult<NotaFiscal> GetNotaFiscal(int id)
        {
            var notaFiscal = _repository.GetNotaFiscal(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            return notaFiscal;
        }

        [HttpPost]
        public ActionResult<NotaFiscal> PostNotaFiscal(NotaFiscal notaFiscal)
        {
            if (notaFiscal.DataVencimento.Date <= DateTime.Now.Date)
                return BadRequest("Data de vencimento inválida!");

            _repository.Create(notaFiscal);
            
            return CreatedAtAction("GetNotaFiscal", new { id = notaFiscal.Id }, notaFiscal);
        }

    }
}