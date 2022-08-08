using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BlockBuster.Models;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BlockBuster.Controllers
{
  public class GenresController : Controller
  {
    private readonly BlockBusterContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public GenresController(UserManager<ApplicationUser> userManager, BlockBusterContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Genres.ToList());
    }

    [Authorize]
    public ActionResult Create()
    {
      ViewBag.MovieId = new SelectList(_db.Movies, "MovieId", "Name");
      return View();
    }
    
    [HttpPost]
    public ActionResult Create(Genre genre)
    {
      _db.Genres.Add(genre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisGenre = _db.Genres
          .Include(genre => genre.JoinEntities)
          .ThenInclude(join => join.Movie)
          .FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);
    }
    
    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      ViewBag.MovieId = new SelectList(_db.Movies, "MovieId", "Name");
      return View(thisGenre);
    }

    [HttpPost]
    public ActionResult Edit(Genre genre, int MovieId)
    {
      if (MovieId != 0)
      {
        _db.GenreMovie.Add(new GenreMovie() { MovieId = MovieId, GenreId = genre.GenreId });
      }
      _db.Entry(genre).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    [Authorize]
    public ActionResult AddMovie(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      ViewBag.MovieId = new SelectList(_db.Movies, "MovieId", "Name");
      return View(thisGenre);
    }

    [HttpPost]
    public ActionResult AddMovie(Genre genre, int MovieId)
    {
      if (MovieId != 0)
      {
      _db.GenreMovie.Add(new GenreMovie() { MovieId = MovieId, GenreId = genre.GenreId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      _db.Genres.Remove(thisGenre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteMovie(int joinId)
    {
      var joinEntry = _db.GenreMovie.FirstOrDefault(entry => entry.GenreMovieId == joinId);
      _db.GenreMovie.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}