using mpNuget;
using Org.BouncyCastle.Asn1.Cmp;
using ProEShop.Services.Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Services.Services.Identity;
public class SendSms : ISendSms
{
    public RestResponse sendsms(string username, string password, string to, string from, string text, bool isFlash)
    {
        RestClient restclient = new RestClient(username, password);
        var result = restclient.Send(to, from, text, false);
        return result;


    }
}
