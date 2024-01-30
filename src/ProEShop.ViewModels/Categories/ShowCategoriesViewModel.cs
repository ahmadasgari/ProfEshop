using ProEShop.Common.Constants;
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
    public List<SearchCategoriesViewModel> SearchCategories { get; set; } = new();
}

public class ShowCategoryViewModel
{
    [Display(Name="عنوان")]
    public string Title { get; set; }
    [Display(Name ="والد")]
    public string Parent { get; set; }
    [Display(Name = "ادرس دسته بندی")]
    public string Slug { get; set; }
    [Display(Name = "نمایش در منوهای اصلی")]
    public bool ShowInMenus { get; set; }

}
public class SearchCategoriesViewModel
{
    [Display(Name ="عنوان")]
    [MaxLength(100,ErrorMessage =AttributesErrorMessages.MaxLengthMessage)]
    public string Title { get; set; }
    [Display(Name ="آدرس دسته بندی")]
    [MaxLength(130,ErrorMessage =AttributesErrorMessages.MaxLengthMessage)]
    public string Slug { get; set; }
    [Display(Name ="نمایش در منوهای اصلی")]
    public ShowInMenusStatus ShowInMenusStatus { get; set; }
    [Display(Name = "وضعیت حذف شده ها")]
    public DeletedStatus DeletedStatus { get; set; }
}

public enum ShowInMenusStatus
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "بله")]
    True,
    [Display(Name = "خیر")]
    False
}