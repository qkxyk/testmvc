using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestWeb.Models;
using TestWeb.Services;

namespace TestWeb.Controllers
{
    public class MovieController:Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;

        public MovieController(IMovieService movieService,ICinemaService cinemaService)
        {
            this._movieService = movieService;
            this._cinemaService = cinemaService;
        }

        public async Task<IActionResult> Index(int cinemaId)
        {           
            var cinema = await _cinemaService.GetByIdAsync(cinemaId);
            ViewBag.Title = $"{cinema.Name}这个电影院上映的电影有：";
            ViewBag.CinemaId = cinemaId;
            return View(await _movieService.GetByCinemaAsync(cinemaId));
        }

        public IActionResult Add(int cinemaId)
        {
            ViewBag.Title = "添加电影";
            return View(new Movie() { CinemaId = cinemaId });
        }
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddAsync(movie);
            }
            return RedirectToAction("Index", new { cinemaId = movie.CinemaId });
        }
        public async Task< IActionResult> Edit(int Id)
        {
            var movie = await _movieService.GetMovieById(Id);
            return View(movie);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id,Movie movie)
        {
            if (ModelState.IsValid)
            {
                var m = await _movieService.GetMovieById(Id);
                if (m==null)
                {
                    return NotFound();
                }
                m.Name = movie.Name;
                m.Starring = movie.Starring;
                m.ReleaseDate = movie.ReleaseDate;
                
            }
            return RedirectToAction("Index",new { cinemaId = movie.CinemaId });
        }
    }
}
