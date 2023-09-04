using System;
using System.Collections.Generic;

namespace Cinemas.Models
{
    public partial class Pelicula
    {
        public int Id { get; set; }
        public string Poster { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Sipnopsis { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Calificacion { get; set; }
    }
}
