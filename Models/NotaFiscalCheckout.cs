using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.Models
{
    public class NotaFiscalCheckout
    {
        public int Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }

        public NotaFiscalCheckout(int numero, decimal valorBruto, decimal valorLiquido)
        {
            Numero = numero;
            ValorBruto = valorBruto;
            ValorLiquido = valorLiquido;
        }
    }
}