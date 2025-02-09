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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaController(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{id:int}", Name = "GetEmpresa")]
        public async Task<ActionResult<ResponseEmpresa>> GetEmpresa(int id)
        {
            var empresa = await _repository.GetEmpresaAsync(id);

            ResponseEmpresa responseEmpresa = new ResponseEmpresa()
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Cnpj = empresa.CNPJ,
                Faturamento = empresa.Faturamento,
                Ramo = empresa.Ramo
            };

            return responseEmpresa;
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(RequestPostEmpresa empresaPost)
        {
            if (empresaPost == null)
                return BadRequest();

            Empresa empresa = new Empresa()
            {
                Nome = empresaPost.Nome,
                CNPJ = empresaPost.Cnpj,
                Faturamento = empresaPost.Faturamento,
                Ramo = empresaPost.Ramo
            };
            
            await _repository.CreateAsync(empresa);
            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }
    }
}