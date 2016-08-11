using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalStore.Data;
using RentalStore.Context.Configurations;

namespace RentalStore.Context
{
    public class RentalStoreContext : DbContext
    {
        public RentalStoreContext()
            :base("Data Source=Ян-Пк;Initial Catalog=RentalStore;Integrated Security=True")
        {
            Database.SetInitializer<RentalStoreContext>(new DropCreateDatabaseIfModelChanges<RentalStoreContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new CartConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
