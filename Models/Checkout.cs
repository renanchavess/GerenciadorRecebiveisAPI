using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GerenciadorRecebiveisAPI.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        public int CarrinhoId { get; set; }
        
        [JsonIgnore]
        public Carrinho? Carrinho { get; set; }      

        public DateTime Data { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Taxa { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorBruto { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorLiquido { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Desagio { get; set; }

        protected Checkout() { }

        public Checkout(decimal taxa, Carrinho carrinho)
        {
            Taxa = taxa;
            ValorBruto = carrinho.ValorTotalNotas();
            Data = DateTime.Now;
            Carrinho = carrinho;
        }

        public bool ValidarLimiteAntecipacao()
        {
            if (this.Carrinho == null)
                return false;
            
            decimal totalNotas = this.Carrinho.ValorTotalNotas();
            decimal limite = CalcularLimite(this.Carrinho.Empresa);

            return  !(totalNotas <= limite);
        }

        public decimal CalcularLimite(Empresa empresa)
        {
            if (empresa.Faturamento < 10000m)
                return 0;

            var percentual = (empresa.Faturamento, empresa.Ramo) switch
            {
                ( < 50001m, _) => 0.5m,
                ( < 100001m, Ramo.Servicos) => 0.55m,
                ( < 100001m, Ramo.Produtos) => 0.6m,
                ( >= 100001m, Ramo.Servicos) => 0.6m,
                ( >= 100001m, Ramo.Produtos) => 0.65m,
                _ => 0m
            };

            return empresa.Faturamento * percentual;
        }

        public List<NotaFiscalCheckout> CalcularDesagio(double taxa)
        {
            var desatioNotas = new List<NotaFiscalCheckout>();
            decimal desagioTotal = 0;
            taxa = taxa / 100;
            DateTime dataInicio = DateTime.Today.AddDays(1);
            
            foreach (var notaFiscal in Carrinho.NotasFiscais)
            {
                double prazo = (notaFiscal.DataVencimento.Date - dataInicio).TotalDays;                                                
                double desagioNota = (double)notaFiscal.Valor / Math.Pow((1.00 + taxa), (prazo / 30.0));                
                decimal desagio= decimal.Round(notaFiscal.Valor -(decimal)desagioNota, 2);
                desagioTotal +=  desagio;
                decimal valorLiquido =notaFiscal.Valor - desagio;

                NotaFiscalCheckout desagioNotaFiscal = new NotaFiscalCheckout(notaFiscal.Numero, notaFiscal.Valor, valorLiquido);
                desatioNotas.Add(desagioNotaFiscal);
            }

            this.Desagio = decimal.Round(desagioTotal, 2);
            this.ValorLiquido = decimal.Round(Carrinho.ValorTotalNotas() - this.Desagio, 2);

            return desatioNotas;
        }
    }
}