using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class ResponseEmpresa
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public Ramo Ramo { get; set; }

        public static ResponseEmpresa fromEntity(Empresa empresa)
        {
            return new ResponseEmpresa()
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Cnpj = empresa.CNPJ,
                Faturamento = empresa.Faturamento,
                Ramo = empresa.Ramo
            };
        }
    }
}