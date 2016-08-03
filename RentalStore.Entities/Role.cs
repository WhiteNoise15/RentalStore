using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Data
{
    public class Role : IEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
