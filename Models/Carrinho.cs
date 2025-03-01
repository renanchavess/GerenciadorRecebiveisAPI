using GerenciadorRecebiveisAPI.Exceptions;
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

        public Empresa? Empresa { get; set; }

        public ICollection<NotaFiscal>? NotasFiscais { get; set; }

        public Checkout? Checkout { get; set; }

        public Carrinho()
        {
            NotasFiscais = new List<NotaFiscal>();
        }

        public void AdicionarNotaFiscal(NotaFiscal nota)
        {
            if (nota.EmpresaId != EmpresaId)
            {
                throw new ValidationException("Nota fiscal não pertence a empresa do carrinho.");
            }

            NotasFiscais.Add(nota);
        }

        public void RemoverNotaFiscal(NotaFiscal nota)
        {
            if (nota.CarrinhoId != Id)
            {
                throw new ValidationException("Nota fiscal não pertence ao carrinho.");
            }

            NotasFiscais.Remove(nota);
            nota.CarrinhoId = null;
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