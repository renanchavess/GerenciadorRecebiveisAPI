using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class ResponseCheckout
    {
        public string Empresa { get; set; }
        public string Cnpj { get; set; }
        public decimal Limite { get; set; }
        public decimal TotalLiquido { get; set; }
        public decimal TotalBruto { get; set; }
        public List<NotaFiscalDesagio>? NotasFiscaisDesagio { get; set; }    

        public ResponseCheckout() {
            
        }

        public ResponseCheckout(string empresa, string cnpj, decimal limite, decimal totalLiquido, decimal totalBruto, List<NotaFiscalDesagio> notasFiscaisDesagio)
        {
            Empresa = empresa;
            Cnpj = cnpj;
            Limite = limite;
            TotalLiquido = totalLiquido;
            TotalBruto = totalBruto;
            NotasFiscaisDesagio = notasFiscaisDesagio;
        }        
    }
}