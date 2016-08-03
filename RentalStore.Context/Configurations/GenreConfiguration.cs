using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Context.Configurations
{
    public class GenreConfiguration : BaseConfuguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
