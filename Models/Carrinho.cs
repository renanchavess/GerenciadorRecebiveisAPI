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
        public ICollection<NotaFiscal>? NotasFiscais { get; set; }

        public Carrinho()
        {
            NotasFiscais = new List<NotaFiscal>();
        }

        public decimal Total()
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