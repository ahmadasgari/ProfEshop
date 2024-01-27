using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProEShop.Web.Pages;

public class PageBase:PageModel
{
    public JsonResult Json(Object input)
    {
        
        return new(input);
    }
}
