using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using System.Web.Mvc;

namespace ProEShop.Web.Pages.Identity;

[Authorize]
public class TestModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public TestModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {

        var products = _context.Set<Product>().Where(x => x.Category.Parent.ParentId == 6);
    }
}
