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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaController(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{id:int}", Name = "GetEmpresa")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _repository.GetEmpresaAsync(id);
            return empresa;
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            if (empresa == null)
                return BadRequest();
            
            await _repository.CreateAsync(empresa);
            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }
    }
}