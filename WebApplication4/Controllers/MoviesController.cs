using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MoviesController(MovieContext movieContext) {
            _movieContext = movieContext;
        }

        [HttpPost]
        [Route("AddMovie")]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            if (_movieContext == null) return NotFound();

            _movieContext.MoviesList.Add(movie);
            await _movieContext.SaveChangesAsync();
            return movie;

        }

        [HttpGet]
        [Route("GetAllMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            if (_movieContext == null) return NotFound();

            if (_movieContext.MoviesList.Count() == 0) return NotFound(); 

            return await _movieContext.MoviesList.ToListAsync();
                      

        }

        [HttpGet("GetByIdMovie")]
        public async Task<ActionResult<Movie>> GetByIdMovie(Guid id)
        {
            if (_movieContext == null) return NotFound();

            var movie = await _movieContext.MoviesList.FindAsync(id);

            if (movie == null) return NotFound();

            return movie;
        }

        [HttpPut]
        [Route("UpdateMovie")]
        public async Task<ActionResult<Movie>> UpdateMovie(Movie movie)
        { 
            if( movie == null ) return BadRequest();

            _movieContext.Entry(movie).State = EntityState.Modified;
            //_movieContext.MoviesList.Update(movie);
            await _movieContext.SaveChangesAsync();
            var updatedMovcie = _movieContext.MoviesList.FirstOrDefault(m => m.Id == movie.Id);
            return movie;
            
        }

        [HttpDelete]
        [Route("DeleteByIdMovie")]
        public async Task<ActionResult> DeleteByIdMovie([FromQuery] Guid id)
        {
            var movie = await _movieContext.MoviesList.FindAsync(id);
            if(movie == null) return NotFound();
            _movieContext.MoviesList.Remove(movie);
            await _movieContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
