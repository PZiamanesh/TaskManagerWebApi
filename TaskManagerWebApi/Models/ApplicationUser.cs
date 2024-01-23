using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerWebApi.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [NotMapped]
        public string? SecurityToken { get; set; }
    }
}
