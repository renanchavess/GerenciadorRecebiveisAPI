using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<ResponseEmpresa>> GetEmpresas()
        {
            return await _service.GetEmpresasAsync();
        }

        [HttpGet("{id:int}", Name = "GetEmpresa")]
        public async Task<ActionResult<ResponseEmpresa>> GetEmpresa(int id)
        {
            return await _service.GetEmpresaByIdAsync(id);
        }

        [HttpPost]
        public async Task<ResponseEmpresa> PostEmpresa([FromBody] RequestPostEmpresa empresaPost)
        {
            return await _service.CreateEmpresaAsync(empresaPost);
        }
    }
}