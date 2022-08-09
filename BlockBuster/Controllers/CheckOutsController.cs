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
      // copy.CheckedOut = true;
      // _db.Entry(copy).State = EntityState.Modified;
      // _db.SaveChanges();
      if (PatronId != 0)
      {
        _db.CheckOuts.Add(new CheckOut() { PatronId = PatronId, CopyId = copy.CopyId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", "Patrons", new { id = PatronId});
    }
  }
}