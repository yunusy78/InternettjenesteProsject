using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NuGet.Packaging.Signing;

namespace InternettjenesteProsject.Models;

public class Posts
{
    public Posts(string title, string summary, string content,  ApplicationUser user)
    {
        
        Title = title;
        Content = content;
        Summary = summary;
        User = user;
        
    }
       

    public Posts() {}
    public int Id { get; set; }
    public DateTime Timestamp = DateTime.Now;
    public string UserId { get; set; }= string.Empty;
    
    [Required ]
    [StringLength(50)]
    [DisplayName("Title")]
    public string Title { get; set; } = string.Empty;
    
    [Required] 
    [StringLength(50)]
    [DisplayName("Summary")]
    public string Summary { get; set; } = string.Empty;
    
    [Required] 
    [StringLength(1000)]
    [DisplayName("Content")]
    public string Content { get; set; } = string.Empty;

    
    public ApplicationUser User { get; set; }

   
}