using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;

namespace ProEShop.Web.Pages.Identity;

[Authorize]
public class TestModel : PageModel
{
    public void OnGet()
    {
    }
}
