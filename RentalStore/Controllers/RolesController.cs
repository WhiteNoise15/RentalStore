using RentalStore.Context;
using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalStore.Controllers
{
    [RoutePrefix("api")]
    public class RolesController : ApiController
    {
        private readonly RentalStoreContext _rentalStoreConext;

        public RolesController()
        {
            _rentalStoreConext = new RentalStoreContext();
        }

        [HttpGet]
        [Route("roles")]
        public ICollection<Role> GetAll()
        {
            return _rentalStoreConext.Roles.ToList();
        }
    }
}
