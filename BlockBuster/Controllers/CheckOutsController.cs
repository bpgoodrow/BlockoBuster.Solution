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
using System;
using System.Data;
using System.Diagnostics;

namespace BlockBuster.Controllers
{
  [Authorize(Roles="Patron")]
  public class CheckOutsController : Controller 
  {
    private readonly BlockBusterContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 
    public CheckOutsController(UserManager<ApplicationUser> userManager, BlockBusterContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View(thisCopy);
    }

    [HttpPost]
    public ActionResult Index(Copy copy, int PatronId)
    {

      int copyId = copy.CopyId;

      var thisCopy = _db.Copies.FirstOrDefault(c => c.CopyId == copyId);

      Console.WriteLine(thisCopy.CheckedOut);
      thisCopy.CheckedOut = true;
      Console.WriteLine(thisCopy.CheckedOut);
      Debug.WriteLine(thisCopy.CheckedOut);
      _db.SaveChanges();

      if (PatronId != 0)
      {
        _db.CheckOuts.Add(new CheckOut() { PatronId = PatronId, CopyId = copy.CopyId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", "Patrons", new { id = PatronId});
    }
    
    [HttpPost]
    public ActionResult ReturnCopy(int joinId)//joinId
    {
      var joinEntry = _db.CheckOuts.FirstOrDefault(entry => entry.CheckOutId == joinId);
      var movieId = joinEntry.Copy.MovieId;
      var copyId = joinEntry.CopyId;
      var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == copyId);
      thisCopy.CheckedOut = false;
      _db.CheckOuts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", "Movies", new { id = movieId});
    }
  }
}