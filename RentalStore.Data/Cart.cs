using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Data
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
