using RentalStore.Context;
using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalStore.Controllers
{
    [RoutePrefix("api")]
    public class GenreController : ApiController
    {
        private readonly RentalStoreContext _rentalStoreConext;

        public GenreController()
        {
            _rentalStoreConext = new RentalStoreContext();
        }

        [HttpGet]
        [Route("genres")]
        public ICollection<Genre> GetAll()
        {
            return _rentalStoreConext.Genres.ToList();
        }
    }
}
