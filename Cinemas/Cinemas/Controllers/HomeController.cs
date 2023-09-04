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

            List<VMPeliculas> Lista = (from tbpelicula in _context.Peliculas
                                       group tbpelicula by tbpelicula.Calificacion into grupo
                                       orderby grupo.Count() descending
                                       select new VMPeliculas
                                       {
                                           pelicula = grupo.Key,
                                           calificacion = grupo.Count(),

                                       }).Take(4).toList();

            return View();
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