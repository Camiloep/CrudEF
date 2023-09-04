using Cinemas.Models;
using Cinemas.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cinemas.Controllers
{
    public class HomeController : Controller
    {
        private readonly CinemasContext _context;

        
        public HomeController(CinemasContext context)
        {
            _context = context;
        }


        public IActionResult resumenCalificaciones()
        {

            List<VMPeliculas> Lista = (from pelicula in _context.Peliculas
                                       group pelicula by pelicula.Titulo into grupo
                                       orderby grupo.Max(pelicula => pelicula.Calificacion) descending
                                       select new VMPeliculas
                                       {
                                           Titulo = grupo.Key,
                                           calificacion = grupo.Max(p => p.Calificacion),

                                       }).Take(5).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}