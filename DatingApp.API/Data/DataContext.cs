using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Value> ValuesN { get; set; } //ValuesN rep the name of the table name
        public DbSet<User> Users { get; set; } //ValuesN rep the name of the table name
        public DbSet<Photo> Photos { get; set; }

    }
}