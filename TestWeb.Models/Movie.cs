using System;
using System.Collections.Generic;
using System.Text;

namespace TestWeb.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CinemaId { get; set; }
        public string Starring { get; set; }//主演
        public DateTime ReleaseDate { get; set; }
    }
}
