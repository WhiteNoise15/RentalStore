using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Context.Configurations
{
    public class UserConfiguration : BaseConfuguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(100);
            Property(u => u.Password).IsRequired().HasMaxLength(200);
            Property(u => u.Email).IsRequired().HasMaxLength(200);
            HasRequired(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

        }
    }
}
