using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalStore.Models
{
    public class UserMovie
    {
        public Movie Movie { get; set; }
        public int UserId { get; set; }
    }
}