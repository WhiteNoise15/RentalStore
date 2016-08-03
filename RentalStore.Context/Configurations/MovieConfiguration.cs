using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Context.Configurations
{
    public class MovieConfiguration : BaseConfuguration<Movie>
    {
        public MovieConfiguration()
        {
            Property(m => m.Title).IsRequired().HasMaxLength(100);
            HasRequired(m => m.Genre).WithMany(g => g.Movies).HasForeignKey(m => m.GenreId);

            Property(m => m.GenreId).IsRequired();
            Property(m => m.Director).IsRequired().HasMaxLength(100);
            Property(m => m.Writer).IsRequired().HasMaxLength(50);
            Property(m => m.Producer).IsRequired().HasMaxLength(50);
            Property(m => m.Writer).HasMaxLength(50);
            Property(m => m.Producer).HasMaxLength(50);
            Property(m => m.Description).IsRequired().HasMaxLength(2000);
            Property(m => m.TrailerURL).HasMaxLength(200);
            Property(m => m.ReleaseDate).IsRequired();
            Property(m => m.Price).IsRequired();

        }
    }
}
