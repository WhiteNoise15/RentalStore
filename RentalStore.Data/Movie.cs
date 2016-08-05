using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Data
{
    public class Movie : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrailerURL { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Producer { get; set; }
        public virtual Genre Genre { get; set; }
        public string Image { get; set; }
        public int GenreId { get; set; }
        public string ReleaseDate { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
