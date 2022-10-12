using System.Security.Principal;
using InternettjenesteProsject.Models;
using Microsoft.AspNetCore.Identity;

namespace InternettjenesteProsject.Data;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {
        // Delete the database before we initialize it. This is common to do during development.
        // Delete the database before we initialize it. This is common to do during development.
        db.Database.EnsureDeleted();
        // Recreate the database and tables according to our models
        db.Database.EnsureCreated();
        // Add test data to simplify debugging and testing

        var adminRole = new IdentityRole("Admin");
        rm.CreateAsync(adminRole).Wait();
        
        
        var admin = new ApplicationUser { UserName = "Yunusy@uia.no", Email = "Yunusy@uia.no", Nickname = "Yunus", EmailConfirmed = true};
        
        var user = new ApplicationUser { UserName = "user@uia.no", Email = "user@uia.no", Nickname = "Yunus User", EmailConfirmed = true };
        um.CreateAsync(admin, "Password1.").Wait();

        um.AddToRoleAsync(admin, "Admin").Wait();
        um.CreateAsync(user, "Password1.").Wait();
        
        
        var post = new[]
        {
            new Posts("Kemal", "Uzun Ince Bir Yoldayim", "Merhaba Dunya",user)
        };
        
       
       // db.Posts.AddRange(post);

        db.SaveChanges();


    }
}