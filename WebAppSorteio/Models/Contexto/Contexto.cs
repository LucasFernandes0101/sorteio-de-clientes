using Microsoft.EntityFrameworkCore;
using WebAppSorteio.Models.Entidades;

namespace WebAppSorteio.Models.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}