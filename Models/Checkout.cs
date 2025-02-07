using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GerenciadorRecebiveisAPI.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        public int CarrinhoId { get; set; }
        
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

            decimal faturamento = this.Carrinho.Empresa.Faturamento;
            decimal totalNotas = this.Carrinho.ValorTotalNotas();
            Ramo ramo = this.Carrinho.Empresa.Ramo;
            

            if (faturamento < 10000)
                return false;            

            if (faturamento < 50001) {
                if (totalNotas > (faturamento * 0.5m))
                    return false;
            }

            if (faturamento <= 100001) {
                if (ramo == Ramo.Produtos && totalNotas > (faturamento * 0.55m))
                    return false;
                if (ramo == Ramo.Servicos && totalNotas > (faturamento * 0.6m))
                    return false;
            }

            if (ramo == Ramo.Produtos && totalNotas > (faturamento * 0.60m))
                return false;
            if (ramo == Ramo.Servicos && totalNotas > (faturamento * 0.65m))
                return false;

            return true;
        }    

        public void CalcularDesagio(decimal taxa)
        {
            decimal desagioTotal = 0;
            foreach (var notaFiscal in Carrinho.NotasFiscais)
            {
                double prazo = (notaFiscal.DataVencimento.Date - DateTime.Now.Date).TotalDays;                
                double taxaDecimal = (double)taxa / 100;                
                double expoente = prazo / 30.0;
                double denominador = Math.Pow(1 + taxaDecimal, expoente);
                decimal valorPresente = notaFiscal.Valor / (decimal)denominador;                
                desagioTotal += notaFiscal.Valor - valorPresente;
            }

            this.Desagio = decimal.Round(desagioTotal, 2);
            this.ValorLiquido = this.ValorBruto - this.Desagio;
        }
    }
}