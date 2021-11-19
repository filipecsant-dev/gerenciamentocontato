using Microsoft.EntityFrameworkCore;
using teste.Models;

namespace teste.Data
{
    public class DataContext : DbContext
    {
        //Definindo construtor DbContext
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }


        //Instanciando conexões com tabelas
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<Phone> phone { get; set; }
    }
}