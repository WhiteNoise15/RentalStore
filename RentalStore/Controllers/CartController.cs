using RentalStore.Context;
using RentalStore.Data;
using RentalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalStore.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly RentalStoreContext _rentalStoreConext;

        public CartController()
        {
            _rentalStoreConext = new RentalStoreContext();
        }


        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(UserMovie movie)
        {
            HttpResponseMessage response = null;
            Cart cart = new Cart();

            Movie movieToChange = _rentalStoreConext.Movies.First(m => m.Id == movie.Movie.Id);
            if (movieToChange.Count == 0 || movieToChange == null)
            {
                
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Этого филмьа больше нет в наличии");
                return response;
            }
            else
            {
                cart.Movie = movieToChange;
                movieToChange.Count -= 1;
            }
                
            try
            {
                User user = _rentalStoreConext.Users.First(u => u.Id == movie.UserId);
                cart.User = user;
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "При добавлении фильма в корзину возникла ошибка");
            }
           

            try
            {
                _rentalStoreConext.Carts.Add(cart);
                _rentalStoreConext.SaveChanges();
                response = Request.CreateResponse(HttpStatusCode.Created, movie);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            return response;

        }

        [HttpGet]
        [Route("all/{userId:int}")]
        public HttpResponseMessage GetSingle(int userId)
        {
            HttpResponseMessage response = null;
            List<Movie> usersMovies = new List<Movie>();


            try
            {
                var carts = _rentalStoreConext.Carts.Where(c => c.User.Id == userId).ToList();
                var movies = _rentalStoreConext.Movies.ToList();

                var query = from cart in carts
                            join movie in movies
                            on cart.Movie.Id equals movie.Id
                            select movie;

                
                foreach (Movie movie in query)
                {
                    usersMovies.Add(movie);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, usersMovies);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadGateway, "Не удалось найти фильмы");
            }

            return response;
                          
        }

        [HttpGet]
        [Route("{userId:int}/price")]
        public HttpResponseMessage CountPrice(int userId)
        {
            HttpResponseMessage response = null;
            List<Movie> usersMovies = new List<Movie>();


            try
            {
                var carts = _rentalStoreConext.Carts.Where(c => c.User.Id == userId).ToList();
                var movies = _rentalStoreConext.Movies.ToList();

                var moviesInCart = from cart in carts
                            join movie in movies
                            on cart.Movie.Id equals movie.Id
                            select movie;


                double price = 0;
                foreach (Movie movie in moviesInCart)
                {
                    price += movie.Price;
                    movie.Count -= 1;
                }

                foreach(var userCart in carts)
                {
                    _rentalStoreConext.Carts.Remove(userCart);
                }

                _rentalStoreConext.SaveChanges();
                               
                response = Request.CreateResponse(HttpStatusCode.OK, price);

            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadGateway, "Не удалось вычислить цену");
            }

            return response;
        }
        

        [HttpDelete]
        [Route("{userId:int}/remove/{movieId:int}")]
        public HttpResponseMessage Remove(int movieId)
        {
            HttpResponseMessage response = null;

            try
            {
                Cart cartToDelete = _rentalStoreConext.Carts.First(c => c.Movie.Id == movieId);
                Movie movieToChange = _rentalStoreConext.Movies.First(c => c.Id == movieId);
                movieToChange.Count += 1;

                _rentalStoreConext.Carts.Remove(cartToDelete);
                _rentalStoreConext.SaveChanges();
                response = Request.CreateResponse(HttpStatusCode.OK, "Удалено");
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadGateway, "Не удалось удалить");
            }

            return response;
        }


    }
}
