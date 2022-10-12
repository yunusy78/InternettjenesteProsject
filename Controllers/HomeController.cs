using Microsoft.AspNetCore.Mvc;
using InternettjenesteProsject.Data;
using InternettjenesteProsject.Models;
using Microsoft.AspNetCore.Identity;

namespace InternettjenesteProsject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _logger = logger;
        _db = db;
        _um = um;
    }

    public IActionResult Index()
    {
      //  var user = _um.GetUserAsync(User).Result;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
}