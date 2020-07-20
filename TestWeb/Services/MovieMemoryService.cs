using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeb.Models;

namespace TestWeb.Services
{
    public class MovieMemoryService : IMovieService
    {
        private readonly List<Movie> _movies = new List<Movie>();
        public MovieMemoryService()
        {
            _movies.Add(new Movie { CinemaId = 1, Id = 1, Name = "Superman", ReleaseDate = new DateTime(2018, 10, 1), Starring = "Nick" });
            _movies.Add(new Movie { CinemaId = 1, Id = 2, Name = "Ghost", ReleaseDate = new DateTime(1997, 5, 4), Starring = "Michael Jackson" });
            _movies.Add(new Movie { CinemaId = 2, Id = 3, Name = "Fight", ReleaseDate = new DateTime(2018, 12, 3), Starring = "Tommy" });
        }
        public Task AddAsync(Movie model)
        {
            var maxId = _movies.Max(a => a.Id);
            model.Id = maxId;
            _movies.Add(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Movie>> GetByCinemaAsync(int CinemaId)
        {
            return Task.Run(() => _movies.Where(x => x.CinemaId == CinemaId));
        }

        public Task<Movie> GetMovieById(int MovieId)
        {
            return Task.Run(() => _movies.FirstOrDefault(a => a.Id == MovieId));
        }
    }
}
