using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinemas.Models;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;


namespace Cinemas.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly CinemasContext _context;

        public PeliculasController(CinemasContext context)
        {
            _context = context;
        }

        [Authorize]

        // GET: Peliculas
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Peliculas != null ? 
        //                  View(await _context.Peliculas.ToListAsync()) :
        //                  Problem("Entity set 'CinemasContext.Peliculas'  is null.");
        //}

        public async Task<ActionResult> Index(int? page, string searchString)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10; // Cantidad de elementos por página

            var peliculas = from p in _context.Peliculas
                            select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                peliculas = peliculas.Where(p => p.Titulo.Contains(searchString));
            }

            var model = await peliculas.OrderByDescending(p => p.Calificacion)
                                       .ToPagedListAsync(pageNumber, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Poster,Titulo,Sipnopsis,Director,Genero,Calificacion")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Poster,Titulo,Sipnopsis,Director,Genero,Calificacion")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'CinemasContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
