using ProEShop.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProEShop.ViewModels.Identity;
public class LoginWithPhoneNumberViewModel
{
    [Display(Name ="کد فعالسازی")]
    [Required(ErrorMessage =AttributesErrorMessages.RequiredMessage)]
    [MaxLength(6, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [MinLength(6,ErrorMessage =AttributesErrorMessages.MinLengthMessage)]
    public string ActivationCode { get; set; }
    [HiddenInput]
    public string PhoneNumber { get; set; }
    public byte SendSmsLastTimeMinute { get; set; }
    public byte SendSmsLastTimeSecond { get; set; }


}
