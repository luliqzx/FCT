using FCT.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FCT.WebAPI.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Item> Items { get; set; }

    }
}