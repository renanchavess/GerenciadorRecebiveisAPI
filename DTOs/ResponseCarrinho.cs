using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class ResponseCarrinho
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public List<ResponseNotaFiscal>? NotasFiscais { get; set; }
    }
}