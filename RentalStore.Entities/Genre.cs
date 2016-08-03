using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Data
{
    public class Genre : IEntity
    {
        public int Id { get; set; }
        public Genre()
        {
            Movies = new List<Movie>();
        }
        public virtual ICollection<Movie> Movies { get; set; }
        public string Name { get; set; }
    }
}
