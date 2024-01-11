using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.Web.Pages.Identity;

public class RegisterLoginModel : PageModel
{
    #region Constructor
    private readonly IApplicationUserManager _userManager;
    private readonly ILogger<RegisterLoginModel> _logger;
    private readonly SiteSettings _siteSettings;

    public RegisterLoginModel(
        IApplicationUserManager userManager,
        ILogger<RegisterLoginModel> logger,
        IOptionsMonitor<SiteSettings> siteSettings)
    {
        _logger = logger;
        _userManager = userManager;
        _siteSettings = siteSettings.CurrentValue;
            
    }
    #endregion
    public RegisterLoginViewModel RegisterLogin { get; set; }
    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPost(RegisterLoginViewModel registerLogin)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty,
                PublicConstantStrings.ModelStateErrorMessage);
            return Page();
        }
        var isInputEmail = registerLogin.PhoneNumberOrEmail.IsEmail();
        if (!isInputEmail)
        {
            var addNewUser = false;
            var user = await _userManager.FindByNameAsync(registerLogin.PhoneNumberOrEmail);
            if (user is null)
            {
                var user1 = new User
                {
                    UserName = registerLogin.PhoneNumberOrEmail,
                    PhoneNumber = registerLogin.PhoneNumberOrEmail,
                    Avatar = _siteSettings.UserDefaultAvatar,
                    Email = $"{StringHelpers.GenerateGuid()}@test.com",
                    SendSmsLastTime = DateTime.Now,
                
                };
                var result = await _userManager.CreateAsync(user1);
                if (result.Succeeded)
                {
                    _logger.LogInformation(LogCodes.RegisterCode,
                    $"{user1.UserName} created a new account with phone number");
                    addNewUser = true;

                }
                else
                {
                    ModelState.AddErrorsFromResult(result);
                    return Page();
                }

            }

            if (DateTime.Now > user.SendSmsLastTime.AddMinutes(3) ||
                addNewUser)
            {
                var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, registerLogin.PhoneNumberOrEmail);
                
            }
            
                
        }
        return RedirectToPage("./LoginWithPhoneNumber", new
        {
            phoneNumber = registerLogin.PhoneNumberOrEmail

        });


    }
}
