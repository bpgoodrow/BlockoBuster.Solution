using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using BlockBuster.Models;
using System.Linq;

namespace BlockBuster.Controllers
{
  public class MoviesController : Controller
  {
    private readonly BlockBusterContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public MoviesController(UserManager<ApplicationUser> userManager, BlockBusterContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Movies.ToList());
    }

    [Authorize]
    public ActionResult Create()
    {
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "GenreName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Movie movie)
    {
      _db.Movies.Add(movie);
      _db.SaveChanges();
      for (int i=0; i  <= movie.MovieCopies; i++)
      {
        _db.Copies.Add(new Copy(movie.MovieName + "Copy" + (i + 1).ToString(), movie.MovieId));
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMovie = _db.Movies
          .Include(movie => movie.JoinEntities)
          .ThenInclude(join => join.Genre)
          .FirstOrDefault(movie => movie.MovieId == id);

      ViewBag.CopiesList = _db.Copies.Where(copy => copy.MovieId == id).ToList();

      return View(thisMovie);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "GenreName");
      return View(thisMovie);
    }

    [HttpPost]
    public ActionResult Edit(Movie movie, int GenreId)
    {
      if (GenreId != 0)
      {
        _db.GenreMovie.Add(new GenreMovie() { GenreId = GenreId, MovieId = movie.MovieId });
      }
      _db.Entry(movie).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddGenre(int id)
    {
      var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "GenreName");
      return View(thisMovie);
    }

    [HttpPost]
    public ActionResult AddGenre(Movie movie, int GenreId)
    {
      if (GenreId != 0)
      {
      _db.GenreMovie.Add(new GenreMovie() { GenreId = GenreId, MovieId = movie.MovieId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
      return View(thisMovie);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
      _db.Movies.Remove(thisMovie);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteGenre(int joinId)
    {
      var joinEntry = _db.GenreMovie.FirstOrDefault(entry => entry.GenreMovieId == joinId);
      _db.GenreMovie.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}