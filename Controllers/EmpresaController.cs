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
    public class EmpresaController : ControllerBase
    {
        private readonly Context.RecebiveisDbContext _context;

        public EmpresaController(Context.RecebiveisDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return await _context.Empresas.ToListAsync<Empresa>();
        }

        [HttpGet("{id:int}", Name = "GetEmpresa")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return empresa;
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }
    }
}