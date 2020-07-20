using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeb.Services;

namespace TestWeb.ViewComponents
{
    public class MovieCountViewComponent:ViewComponent
    {
        private readonly IMovieService _movieService;

        public MovieCountViewComponent(IMovieService movieService)
        {
            this._movieService = movieService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int cinemaId)
        {
            var movies = await _movieService.GetByCinemaAsync(cinemaId);
            var count = movies.Count();
            //var movies2 = await _movieService.GetByCinemaAsync(2);
            //count += movies2.Count();
            return View(count);
        }
    }
}
