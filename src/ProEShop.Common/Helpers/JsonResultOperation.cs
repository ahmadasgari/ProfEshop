using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Common.Helpers;
public class JsonResultOperation
{
    public bool IsSuccessful { get; }
    public string Message { get; }
    public object Data { get; set; }
    public JsonResultOperation(bool isSuccessful, string message = "خطایی به وجود آمد")
    {
        IsSuccessful = isSuccessful;
        Message = message;
    }
}
