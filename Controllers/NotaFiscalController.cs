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
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _service;

        public NotaFiscalController(INotaFiscalService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}", Name = "GetNotaFiscal")]
        public async Task<ActionResult<ResponseNotaFiscal>> GetNotaFiscal(int id)
        {
            return await _service.GetNotaFiscal(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseNotaFiscal>> PostNotaFiscal(RequestPostNotaFiscal notaFiscalPost)
        {
            return await _service.CreateNotaFiscal(notaFiscalPost);
        }

        [HttpGet("empresa/{empresaId:int}")]
        public async Task<ActionResult<List<ResponseNotaFiscal>>> GetNotaFiscalByEmpresaId(int empresaId)
        {
            return await _service.GetNotaFiscalByEmpresaId(empresaId);
        }

    }
}