using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity;
using System.Reflection.Metadata;

namespace ProEShop.Web.Pages.Identity;

public class LoginWithPhoneNumberModel : PageModel
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;
    private readonly IApplicationSignInManager _signInManager;
    private readonly ISendSms _sendSms;

    public LoginWithPhoneNumberModel(
        IApplicationUserManager userManager,
        IApplicationSignInManager signInManager,
        ISendSms sendSms)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _sendSms = sendSms;
    }

    #endregion

    public LoginWithPhoneNumberViewModel LoginWithPhoneNumber { get; set; }
    = new LoginWithPhoneNumberViewModel();
    [ViewData]
    public string ActivationCode { get; set; }
    public async Task<IActionResult> OnGetAsync(string phoneNumber)
    {
        var userSendSmsLastTime = await _userManager.GetSendSmsLastTimeAsync(phoneNumber);
        if (userSendSmsLastTime is null)
        {
            return RedirectToPage("/Error");
        }

        #region Development
        var user = await _userManager.FindByNameAsync(phoneNumber);
        var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        ActivationCode = phoneNumberToken;
        #endregion

        var (min, sec) = userSendSmsLastTime.Value.GetMinuteAndSecondForLoginWithPhoneNumberPage();
        LoginWithPhoneNumber.SendSmsLastTimeMinute = min;
        LoginWithPhoneNumber.SendSmsLastTimeSecond = sec;
        LoginWithPhoneNumber.PhoneNumber = phoneNumber;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(LoginWithPhoneNumberViewModel loginWithPhoneNumber)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByNameAsync(loginWithPhoneNumber.PhoneNumber);
        if(user is null)
        {
            return Page();
        }
        var result = await _userManager.VerifyChangePhoneNumberTokenAsync(
            user, loginWithPhoneNumber.ActivationCode, loginWithPhoneNumber.PhoneNumber);
        if (!result)
        {
            return Page();
        }
        await _signInManager.SignInAsync(user, true);
        return RedirectToPage("/Test");
    }

    public async Task<IActionResult> OnPostSendUserSmsActivationAsync(string phoneNumber)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user is null)
            return new JsonResult(new JsonResultOperation(false));
        if (user.SendSmsLastTime.AddMinutes(3) > DateTime.Now)
            return new JsonResult(new JsonResultOperation(false));
        var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        var sendText = $"کد فعال سازی : {phoneNumberToken}";
        //var SmsSend = _sendSms.sendsms("", "", user.PhoneNumber, "", sendText, false);
        user.SendSmsLastTime = DateTime.Now;
        await _userManager.UpdateAsync(user);
        return new JsonResult(new JsonResultOperation(true, "کد فعالسازی مجددا ارسال شد")
        {
            Data = new
            {
                activationCode = phoneNumberToken
            }
        });


    }
}