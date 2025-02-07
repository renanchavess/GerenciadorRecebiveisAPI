using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly Context.RecebiveisDbContext _context;

        public NotaFiscalController(Context.RecebiveisDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaFiscal>>> GetNotasFiscais()
        {
            return await _context.NotasFiscais.ToListAsync<NotaFiscal>();
        }

        [HttpGet("{id:int}", Name = "GetNotaFiscal")]
        public async Task<ActionResult<NotaFiscal>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            return notaFiscal;
        }

        [HttpPost]
        public async Task<ActionResult<NotaFiscal>> PostNotaFiscal(NotaFiscal notaFiscal)
        {
            if (notaFiscal.DataVencimento.Date <= DateTime.Now.Date)
            {
                return BadRequest("Data de vencimento inválida!");
            }

            _context.NotasFiscais.Add(notaFiscal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotaFiscal", new { id = notaFiscal.Id }, notaFiscal);
        }

    }
}