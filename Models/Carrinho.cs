using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.Models
{
    public class Carrinho
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        [JsonIgnore]
        public Empresa? Empresa { get; set; }

        [JsonIgnore]
        public ICollection<NotaFiscal>? NotasFiscais { get; set; }

        [JsonIgnore]
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