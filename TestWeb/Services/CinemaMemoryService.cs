using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeb.Models;

namespace TestWeb.Services
{
    public class CinemaMemoryService : ICinemaService
    {
        private readonly List<Cinema> _cinemas = new List<Cinema>();
        public CinemaMemoryService()
        {
            _cinemas.Add(new Cinema {Id=1, Name = "City Cinema", Capacity = 1000, Location = "Road ABC ,No.123" });
            _cinemas.Add(new Cinema {Id=2, Name = "Fly Cinema", Location = "Road hello,No.1024", Capacity = 500 });
        }
        public Task AddAsync(Cinema model)
        {
            var maxId = _cinemas.Max(x => x.Id);
            model.Id = maxId;
            _cinemas.Add(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Cinema>> GetAllAsync()
        {
            return Task.Run(() => _cinemas.AsEnumerable());
        }

        public Task<Cinema> GetByIdAsync(int Id)
        {
            return Task.Run(() => _cinemas.FirstOrDefault(a => a.Id == Id));
        }

        //public Task<Sales> GetSalesAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
