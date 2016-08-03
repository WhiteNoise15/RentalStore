using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Context.Configurations
{
    public class RoleConfiguration : BaseConfuguration<Role>
    {
        public RoleConfiguration()
        {
            Property(r => r.Name).IsRequired().HasMaxLength(50);
        }
    }
}
