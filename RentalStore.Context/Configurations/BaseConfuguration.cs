using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Context.Configurations
{
    public class BaseConfuguration<T> : EntityTypeConfiguration<T> where T : class, IEntity
    {
        public BaseConfuguration()
        {
            HasKey(x => x.Id);
        }
    }
}
