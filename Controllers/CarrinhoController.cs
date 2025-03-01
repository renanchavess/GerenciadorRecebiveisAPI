using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class CarrinhoController : ControllerBase
    {

        private readonly ICarrinhoService _service;

        public CarrinhoController(ICarrinhoService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}", Name = "GetCarrinho")]
        public async Task<ActionResult<ResponseCarrinho>> GetCarrinho(int id)
        {
            return await _service.GetCarrinhoAsync(id);
        }

        [HttpGet("empresa/{empresaId:int}")]
        public async Task<ActionResult<List<ResponseCarrinho>>> GetCarrinhoByEmpresaId(int empresaId)
        {
            return await _service.GetCarrinhosByEmpresaIdAsync(empresaId);

        }

        [HttpPost]
        public async Task<ActionResult<ResponseCarrinho>> PostCarrinho([Required][FromBody] RequestIdParam empresa)
        {   
            return await _service.CreateAsync(empresa.Id);
        }

        [HttpPost("{carrinhoId:int}/adicionarNotaFiscal")]
        public async Task<ActionResult> AdicionarNotaFiscal(int carrinhoId, [FromBody] RequestIdParam nota)
        {
            if (await _service.AdicionarNotaFiscalAsync(carrinhoId, nota.Id))
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{carrinhoId:int}/removerNotaFiscal")]
        public async Task<ActionResult> RemoverNotaFiscal(int carrinhoId, [FromBody] RequestIdParam nota)
        {
            if (await _service.RemoverNotaFiscalAsync(carrinhoId, nota.Id))
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPatch("{id:int}/checkout")]
        public async Task<ActionResult> Checkout(int id)
        {
            var checkout = await _service.RealizarCheckoutAsync(id);
            return Ok(checkout);
        }

    }
}