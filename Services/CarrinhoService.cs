using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Exceptions;
using GerenciadorRecebiveisAPI.Models;
using GerenciadorRecebiveisAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorRecebiveisAPI.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly ICheckoutRepository _checkoutRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, INotaFiscalRepository notaFiscalRepository, ICheckoutRepository checkoutRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _notaFiscalRepository = notaFiscalRepository;
            _checkoutRepository = checkoutRepository;
        }

        public async Task<bool> AdicionarNotaFiscalAsync(int carrinhoId, int notaFiscalId)
        {
            Carrinho carrinho = await _carrinhoRepository.GetCarrinhoAsync(carrinhoId);
            NotaFiscal notaFiscal = await _notaFiscalRepository.GetNotaFiscalAsync(notaFiscalId);            
            
            if (notaFiscal.Vencida())
            {
                throw new ValidationException("Nota fiscal vencida.");
            }

            if (carrinho.Checkout is not null)
            {
                throw new ValidationException("Checkout já realizado.");
            }

            carrinho.AdicionarNotaFiscal(notaFiscal);

            return await _carrinhoRepository.AdicionarNotaFiscalAsync(carrinhoId, notaFiscal);
        }

        public async Task<bool> RemoverNotaFiscalAsync(int carrinhoId, int notaFiscalId)
        {
            var notaFiscal = await _notaFiscalRepository.GetNotaFiscalAsync(notaFiscalId);
            Carrinho carrinho = await _carrinhoRepository.GetCarrinhoAsync(carrinhoId);


            if (carrinho.Checkout is not null)
            {
                throw new ValidationException("Checkout já realizado.");
            }
            
            return await _carrinhoRepository.RemoverNotaFiscalAsync(carrinhoId, notaFiscal);
        }

        public async Task<ResponseCheckout> RealizarCheckoutAsync(int carrinhoId)
        {
            Carrinho carrinho = await _carrinhoRepository.GetCarrinhoAsync(carrinhoId);

            if (carrinho is null)
            {
                throw new ValidationException("Carrinho não encontrado.");
            }

            if (carrinho.Checkout is not null)
            {
                throw new ValidationException("Checkout já realizado.");
            }

            decimal taxa = 4.65m;
            carrinho.Checkout = new Checkout(taxa, carrinho);

            var notasFiscaisDesagio = carrinho.Checkout.ExecutarCheckout();
            await _carrinhoRepository.UpdateAsync(carrinho);
            ResponseCheckout responseCheckout = new ResponseCheckout()
            {
                Empresa = carrinho.Empresa.Nome,
                Cnpj = carrinho.Empresa.CNPJ,
                Limite = carrinho.Checkout.CalcularLimite(carrinho.Empresa),
                NotasFiscais = notasFiscaisDesagio
            };

            return responseCheckout;
        }

        public async Task<ResponseCarrinho> GetCarrinhoAsync(int id)
        {
            Carrinho carrinho = await _carrinhoRepository.GetCarrinhoAsync(id);

            if (carrinho is null)
            {
                throw new ValidationException("Carrinho não encontrado.");
            }

            return ResponseCarrinho.fromEntity(carrinho);
        }

        public async Task<ActionResult<List<ResponseCarrinho>>> GetCarrinhosByEmpresaIdAsync(int empresaId)
        {
            var carrinhos = await _carrinhoRepository.GetCarrinhosByEmpresaIdAsync(empresaId);            

            List<ResponseCarrinho> responseCarrinhos = new List<ResponseCarrinho>();

            foreach (var carrinho in carrinhos)
            {
                responseCarrinhos.Add(ResponseCarrinho.fromEntity(carrinho));
            }

            return responseCarrinhos;
        }

        public async Task<ResponseCarrinho> CreateAsync(int empresaId)
        {
            Carrinho carrinho = new Carrinho()
            {
                EmpresaId = empresaId
            };

            await _carrinhoRepository.CreateAsync(carrinho);
            return ResponseCarrinho.fromEntity(carrinho);
        }        
    }
}
