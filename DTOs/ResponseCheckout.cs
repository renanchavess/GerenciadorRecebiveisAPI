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
        public List<NotaFiscalCheckout>? NotasFiscais { get; set; }    

    }
}