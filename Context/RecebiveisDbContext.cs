using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Context
{
    public class RecebiveisDbContext: DbContext
    {
        public RecebiveisDbContext(DbContextOptions<RecebiveisDbContext> options) : base(options)
        {
            
        }

        public DbSet<Models.Empresa> Empresas { get; set; }
        public DbSet<Models.NotaFiscal> NotasFiscais { get; set; }
        public DbSet<Models.Carrinho> Carrinhos { get; set; }
    }
}