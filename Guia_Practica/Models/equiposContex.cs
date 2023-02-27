using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace Guia_Practica.Models
{
    public class equiposContex : DbContext
    {
        public equiposContex(DbContextOptions<equiposContex> options) : base(options)
        {
        
        }
        public DbSet<equipos> equipos { get; set; }
    }
}
