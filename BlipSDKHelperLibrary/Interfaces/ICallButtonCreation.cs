using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Interfaces
{
    public interface ICallButtonCreation
    {
        JsonDocument CreateCallButtonDocument(string title, string buttonText, string phoneNumber);
    }
}
