using Lime.Messaging.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Interfaces
{
    public interface IPlainTextCreation
    {
        PlainText CreateTextDocument(string text);

    }
}
