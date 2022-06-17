using Microsoft.EntityFrameworkCore;

namespace DocumentacaoSwagger.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Serie> Series { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
