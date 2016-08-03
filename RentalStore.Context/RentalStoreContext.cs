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
            Database.SetInitializer<RentalStoreContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new CartConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
        }

        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Genre> Genres { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Cart> Carts { get; set; }
    }
}
