using Microsoft.AspNetCore.Identity;

namespace PersonalBlog2.Models;

public class ApplicationUserModel : IdentityUser
{
    public string CustomProperty { get; set; }
}