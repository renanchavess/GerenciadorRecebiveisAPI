using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;
using GerenciadorRecebiveisAPI.Exceptions;
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

        public List<NotaFiscalCheckout> ExecutarCheckout()
        {
            var notaFiscalCheckout = new List<NotaFiscalCheckout>();
            decimal desagioTotal = 0;
            double taxa = (double)Taxa / 100;

            if (taxa <= 0) {
                throw new ValidationException("Taxa de deságio inválida.");
            }

            if (Carrinho.NotasFiscais.Count == 0)
            {
                throw new ValidationException("Carrinho sem notas fiscais.");
            }

            if (CalcularLimite(Carrinho.Empresa) < Carrinho.ValorTotalNotas())
            {
                throw new ValidationException("Faturamento das notas é superior ao limite da empresa.");
            }

            foreach (var notaFiscal in Carrinho.NotasFiscais)
            {
                double desagioNota = (double)notaFiscal.Valor / Math.Pow((1.00 + taxa), (notaFiscal.Prazo() / 30.00));                
                decimal desagio= decimal.Round(notaFiscal.Valor -(decimal)desagioNota, 2);
                desagioTotal +=  desagio;
                decimal valorLiquido =notaFiscal.Valor - desagio;
                
                notaFiscalCheckout.Add(new NotaFiscalCheckout(){
                    Numero = notaFiscal.Numero,
                    ValorBruto = notaFiscal.Valor,
                    ValorLiquido = valorLiquido
                });
            }

            this.Desagio = decimal.Round(desagioTotal, 2);
            this.ValorLiquido = decimal.Round(Carrinho.ValorTotalNotas() - this.Desagio, 2);

            return notaFiscalCheckout;
        }
    }
}