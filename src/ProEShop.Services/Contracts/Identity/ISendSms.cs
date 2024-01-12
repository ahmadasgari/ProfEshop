using mpNuget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Services.Contracts.Identity;
public interface ISendSms
{
    public RestResponse sendsms(string username, string password, string to, string from, string text, bool isFlash);
}
