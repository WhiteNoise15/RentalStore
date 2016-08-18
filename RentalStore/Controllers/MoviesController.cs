using RentalStore.Context;
using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace RentalStore.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private readonly RentalStoreContext _rentalStoreContext;

        public MoviesController()
        {
            _rentalStoreContext = new RentalStoreContext();
        }

        [HttpGet]
        [Route("details/{id:int}")]
        public HttpResponseMessage GetSingle(int id)
        {
            HttpResponseMessage response = null;
            Movie movie = _rentalStoreContext.Movies.FirstOrDefault(m => m.Id == id);

            response = Request.CreateResponse(HttpStatusCode.OK, movie);
            return response;
        }

        [HttpGet]
        [Route("latest")]
        public ICollection<Movie> GetLatest()
        {
            List<Movie> movies = null;

            try
            {
                movies = _rentalStoreContext.Movies.OrderByDescending(m => m.Id).Take(8).ToList();
            }
            catch (Exception e)
            {
                movies = new List<Movie>();
            }
            

            return movies;
        }


        //currentPage = текущая страница в списке всех фильмов
        //itemsPerPage = количество фильмов, отображенных на одной странице
        //filter = строка поиска фильма

        [HttpGet]
        [Route("{currentPage:int=1}/{itemsPerPage=12}/{filter?}")]
        public HttpResponseMessage GetMovies(HttpRequestMessage request, int? currentPage, int? itemsPerPage, string filter = null)
        {
            int page = currentPage.Value == 1 ? 0 : currentPage.Value - 1;
            int moviesPerPage = itemsPerPage.Value;

            HttpResponseMessage response = null;
            List<Movie> movies = null;

            int moviesTotal = 0;

            if (!String.IsNullOrEmpty(filter))
            {
                movies = _rentalStoreContext.Movies
                    .Where(m => m.Title.Contains(filter.ToLower().Trim()))
                    .OrderBy(m => m.Id)
                    .Skip(page * moviesPerPage)
                    .Take(moviesPerPage)
                    .ToList();

                moviesTotal = _rentalStoreContext.Movies
                    .Where(m => m.Title.Contains(filter.ToLower().Trim()))
                    .Count();
            }
            else
            {
                movies = _rentalStoreContext.Movies
                     .OrderBy(m => m.Id)
                     .Skip(page * moviesPerPage)
                     .Take(moviesPerPage)
                     .ToList();

                moviesTotal = _rentalStoreContext.Movies.Count();
            }

            var movieSet = new
            {
                moviesToShow = movies,
                totalItems = moviesTotal
            };

            response = Request.CreateResponse(HttpStatusCode.OK, movieSet);

            return response;
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Edit(Movie movie)
        {
            HttpResponseMessage response = null;
            if (movie == null)
            {
                throw new ArgumentNullException("movie");
            }
            else
            {
                Movie movieToUpdate = _rentalStoreContext.Movies.First(m => m.Id == movie.Id);
                if (movieToUpdate == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.Created, "Не удалось отредактировать");
                    return response;
                }

                movieToUpdate.Title = movie.Title;
                movieToUpdate.Image = movie.Image;
                movieToUpdate.GenreId = movie.GenreId;
                movieToUpdate.Description = movie.Description;
                movieToUpdate.Count = movie.Count;
                movieToUpdate.Director = movie.Director;
                movieToUpdate.Producer = movie.Producer;
                movieToUpdate.Writer = movie.Writer;
                movieToUpdate.TrailerURL = movie.TrailerURL;
                movieToUpdate.ReleaseDate = movie.ReleaseDate;
                movieToUpdate.Price = movie.Price;

                _rentalStoreContext.SaveChanges();

                response = Request.CreateResponse(HttpStatusCode.Created, movie);
            }

                
            return response;
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(Movie movie)
        {
            HttpResponseMessage response = null;

            try
            {
                _rentalStoreContext.Movies.Add(movie);
                _rentalStoreContext.SaveChanges();
                response = Request.CreateResponse(HttpStatusCode.Created, movie);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            return response;
        }


    }
}
