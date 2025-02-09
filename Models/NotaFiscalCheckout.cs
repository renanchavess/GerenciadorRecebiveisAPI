using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.Models
{
    public class NotaFiscalCheckout
    {
        public string Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }

        public NotaFiscalCheckout(string numero, decimal valorBruto, decimal valorLiquido)
        {
            Numero = numero;
            ValorBruto = valorBruto;
            ValorLiquido = valorLiquido;
        }
    }
}