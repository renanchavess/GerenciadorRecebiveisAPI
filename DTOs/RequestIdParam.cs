using System.ComponentModel.DataAnnotations;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class RequestIdParam
    {
        [Required]
        public int Id { get; set; }
    }
}
