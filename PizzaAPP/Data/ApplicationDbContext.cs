using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaAPP.Models;

namespace PizzaAPP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PizzaAPP.Models.Articolo>? Articolo { get; set; }
        public DbSet<PizzaAPP.Models.Cliente>? Cliente { get; set; }
    }
}