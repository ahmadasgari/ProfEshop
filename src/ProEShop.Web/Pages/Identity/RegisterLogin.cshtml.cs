using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using ProEShop.Services.Services.Identity;
using ProEShop.ViewModels.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.Web.Pages.Identity;

public class RegisterLoginModel : PageBase
{
    #region Constructor
    private readonly IApplicationUserManager _userManager;
    private readonly ILogger<RegisterLoginModel> _logger;
    private readonly SiteSettings _siteSettings;
    private readonly ISendSms _sendSms;

    public RegisterLoginModel(
        IApplicationUserManager userManager,
        ILogger<RegisterLoginModel> logger,
        IOptionsMonitor<SiteSettings> siteSettings,
        ISendSms sendSms)
    {
        _logger = logger;
        _userManager = userManager;
        _siteSettings = siteSettings.CurrentValue;
        _sendSms = sendSms;
            
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
                var usernew = new User
                {
                    UserName = registerLogin.PhoneNumberOrEmail,
                    PhoneNumber = registerLogin.PhoneNumberOrEmail,
                    Avatar = _siteSettings.UserDefaultAvatar,
                    Email = $"{StringHelpers.GenerateGuid()}@test.com",
                
                };
                var result = await _userManager.CreateAsync(usernew);
                if (result.Succeeded)
                {
                    _logger.LogInformation(LogCodes.RegisterCode,
                    $"{usernew.UserName} created a new account with phone number");
                    var phoneNumberToken1 = await _userManager.GenerateChangePhoneNumberTokenAsync(usernew, registerLogin.PhoneNumberOrEmail);
                    //var sendText = $"کد فعال سازی : {phoneNumberToken1}";
                    //var SmsSend = _sendSms.sendsms(, usernew.PhoneNumber, "", sendText, false);
                    usernew.SendSmsLastTime = DateTime.Now;
                    await _userManager.UpdateAsync(usernew);
                    return RedirectToPage("./LoginWithPhoneNumber", new
                    {
                        phoneNumber = registerLogin.PhoneNumberOrEmail

                    });


                }
                else
                {
                    ModelState.AddErrorsFromResult(result);
                    return Page();
                }

            }
            

            if (DateTime.Now > user.SendSmsLastTime.AddMinutes(3))
            {
                var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, registerLogin.PhoneNumberOrEmail);
                //var sendText = $"کد فعال سازی : {phoneNumberToken}";
                //var SmsSend = _sendSms.sendsms("", "", user.PhoneNumber, "", sendText, false);
                user.SendSmsLastTime = DateTime.Now;
                await _userManager.UpdateAsync(user);

            }


        }
        return RedirectToPage("./LoginWithPhoneNumber", new
        {
            phoneNumber = registerLogin.PhoneNumberOrEmail

        });


    }
}
