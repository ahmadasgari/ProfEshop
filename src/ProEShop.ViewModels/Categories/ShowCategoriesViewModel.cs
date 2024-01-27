using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.ViewModels.Categories;
public class ShowCategoriesViewModel
{
    public List<ShowCategoryViewModel> Categories { get; set; }
}

public class ShowCategoryViewModel
{
    [Display(Name="عنوان")]
    public string Title { get; set; }
    [Display(Name ="والد")]
    public string Parent { get; set; }
    [Display(Name = "نمایش در منوهای اصلی")]
    public bool ShowInMenus { get; set; }

}
