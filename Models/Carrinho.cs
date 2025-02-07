using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.Models
{
    public class Carrinho
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        public ICollection<NotaFiscal>? NotasFiscais { get; set; }

        public Checkout? Checkout { get; set; }

        public Carrinho()
        {
            NotasFiscais = new List<NotaFiscal>();
        }

        public decimal ValorTotalNotas()
        {
            decimal total = 0;
            foreach (NotaFiscal nota in NotasFiscais)
            {
                total += nota.Valor;
            }
            return total;
        }
    }
}