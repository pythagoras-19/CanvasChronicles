using Microsoft.AspNetCore.Identity;

namespace CanvasChronicles.Models;

public class ApplicationUserModel : IdentityUser
{
    public string CustomProperty { get; set; }
}