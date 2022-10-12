using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using InternettjenesteProsject.Data;
using InternettjenesteProsject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace InternettjenesteProsject.Controllers;

public class BlogController : Controller
{
    private readonly ILogger<BlogController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;

    public BlogController(ILogger<BlogController> logger, ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _logger = logger;
        _db = db;
        _um = um;
    }
    
    [AllowAnonymous]
    public IActionResult Index()
    {
        var objPostList = _db.Posts.OrderByDescending(b=>b.Id).ToList();
        
        
        var post =  _db.Posts
            .Include(blog => blog.User)
            .ToListAsync();
        return View(objPostList);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([Bind("Title, Summary, Content, User, UserId") ]Posts posts)
    {
        posts.Timestamp = DateTime.Now;
        var user = _um.GetUserAsync(User).Result;
        posts.User = user;
        posts.UserId = user.Id ;
        
        
        if (!ModelState.IsValid)
        {
            _db.Posts.Add(posts);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(posts);
    }
    
   
 
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = _um.GetUserAsync(User).Result;
      
       
        //var posts = await _db.Posts.FirstOrDefaultAsync(m=>m.Id==id);
        var posts = await _db.Posts.FindAsync(id);
        if (posts == null || posts.UserId !=user.Id )
        {
            return RedirectToAction(nameof(Index));
        }
        return View(posts);
    }
    
   
 
   
   [HttpPost]
   [ValidateAntiForgeryToken]
   public async Task<IActionResult> Edit([Bind("Id,Title, Summary, Content, User, UserId") ]Posts posts)
   {
       posts.Timestamp = DateTime.Now;
       var user = _um.GetUserAsync(User).Result;
       posts.User = user;
       posts.UserId = user.Id ;
        
        
       if (!ModelState.IsValid)
       {
           _db.Posts.Update(posts);
           _db.SaveChanges();
           return RedirectToAction("Index");
       }

       return View(posts);
   }

   [Authorize]
   [HttpGet]
   public async Task<IActionResult> Delete(int? id)
   {
       if (id == null)
       {
           return NotFound();
       }
       var user = _um.GetUserAsync(User).Result;
       var posts = await _db.Posts.FindAsync(id);
       if (posts == null || posts.UserId !=user.Id )
       {
           return NotFound();
       }

       return View(posts);
   }

// POST: Movies/Delete/5
   [HttpPost, ActionName("Delete")]
   [ValidateAntiForgeryToken]
   public async Task<IActionResult> Delete(int id, [Bind("Id,Title, Summary, Content, User, UserId") ]Posts posts)
   {
       posts.Timestamp = DateTime.Now;
       var user = _um.GetUserAsync(User).Result;
       posts.User = user;
       posts.UserId = user.Id ;
       
       
       var movie = await _db.Posts.FindAsync(id);
       _db.Posts.Remove(movie);
       await _db.SaveChangesAsync();
       return RedirectToAction(nameof(Index));
   }
  

}