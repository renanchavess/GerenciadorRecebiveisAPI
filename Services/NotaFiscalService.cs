using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Exceptions;
using GerenciadorRecebiveisAPI.Models;
using GerenciadorRecebiveisAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorRecebiveisAPI.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _repository;

        public NotaFiscalService(INotaFiscalRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseNotaFiscal> GetNotaFiscal(int id)
        {
            var notaFiscal = await _repository.GetNotaFiscalAsync(id);

            if (notaFiscal == null)
            {
                throw new ValidationException("Nota fiscal não encontrada");
            }

            return ResponseNotaFiscal.fromEntity(notaFiscal);
        }

        public async Task<ResponseNotaFiscal> CreateNotaFiscal(RequestPostNotaFiscal notaFiscalPost)
        {
            if (notaFiscalPost.DataVencimento <= DateOnly.FromDateTime(DateTime.Now))
                throw new ValidationException("Data de vencimento inválida!");

            NotaFiscal notaFiscal = new NotaFiscal()
            {
                Numero = notaFiscalPost.Numero,
                Valor = notaFiscalPost.Valor,
                DataVencimento = notaFiscalPost.DataVencimento,
                EmpresaId = notaFiscalPost.EmpresaId
            };

            await _repository.CreateAsync(notaFiscal);

            return ResponseNotaFiscal.fromEntity(notaFiscal);
        }

        public async Task<List<ResponseNotaFiscal>> GetNotaFiscalByEmpresaId(int empresaId)
        {
            var notasFiscais = await _repository.GetNotaFiscalByEmpresaId(empresaId);

            List<ResponseNotaFiscal> responseNotas = new List<ResponseNotaFiscal>();
            foreach (var nota in notasFiscais)
            {
                responseNotas.Add(ResponseNotaFiscal.fromEntity(nota));
            }

            return responseNotas;
        }

    }
}
