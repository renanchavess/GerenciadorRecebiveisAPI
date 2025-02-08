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
            decimal limite = calcularLimite(this.Carrinho.Empresa);

            return  !(totalNotas <= limite);
        }

        public decimal calcularLimite(Empresa empresa)
        {
            decimal faturamento = empresa.Faturamento;
            decimal limite = 0;

            Ramo ramo = empresa.Ramo;
            

            if (faturamento < 10000)
                return 0;

            if (faturamento < 50001)
            {
                limite = faturamento * 0.5m;
            }

            if (faturamento <= 100001)
            {
                if (ramo == Ramo.Produtos)
                    limite = faturamento * 0.55m;
                if (ramo == Ramo.Servicos)
                    limite = faturamento * 0.6m;
            }

            if (ramo == Ramo.Produtos)
                limite = faturamento * 0.60m;
            if (ramo == Ramo.Servicos)
                limite = faturamento * 0.65m;

            return limite;
        }

        public List<NotaFiscalDesagio> CalcularDesagio(double taxa)
        {
            var desatioNotas = new List<NotaFiscalDesagio>();
            decimal desagioTotal = 0;
            taxa = taxa / 100;

            foreach (var notaFiscal in Carrinho.NotasFiscais)
            {
                double prazo = (notaFiscal.DataVencimento.Date - DateTime.Now.AddDays(1).Date).TotalDays;                                                
                double desagioNota = (double)notaFiscal.Valor / Math.Pow((1.00 + taxa), (prazo / 30.0));                
                decimal desagio= decimal.Round(notaFiscal.Valor -(decimal)desagioNota, 2);
                desagioTotal +=  desagio;
                decimal valorLiquido =notaFiscal.Valor - desagio;

                NotaFiscalDesagio desagioNotaFiscal = new NotaFiscalDesagio(notaFiscal.Numero, notaFiscal.Valor, valorLiquido);
                desatioNotas.Add(desagioNotaFiscal);
            }

            this.Desagio = decimal.Round(desagioTotal, 2);
            this.ValorLiquido = decimal.Round(Carrinho.ValorTotalNotas() - this.Desagio, 2);

            return desatioNotas;
        }
    }
}