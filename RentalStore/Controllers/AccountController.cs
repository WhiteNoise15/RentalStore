using RentalStore.Context;
using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RentalStore.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly RentalStoreContext _rentalStoreContext;

        public AccountController()
        {
            _rentalStoreContext = new RentalStoreContext();
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(User user)
        {
            HttpResponseMessage response = null;
            if (user !=null)
            {
                try
                {
                    User currentUser = _rentalStoreContext.Users.First(u => u.Username == user.Username);
                    response = Request.CreateResponse(HttpStatusCode.OK, currentUser);
                   
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "bad");
                }

                return response;
            }
            else
            {
                return response;
            }
            

        }


        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(User user)
        {
            HttpResponseMessage response = null;

            try
            {
                _rentalStoreContext.Users.Add(user);
                _rentalStoreContext.SaveChanges();
                response = Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            return response;

        }
    }
}
