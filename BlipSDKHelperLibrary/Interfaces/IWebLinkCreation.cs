using Lime.Messaging.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Interfaces
{
    public interface IWebLinkCreation
    {
        WebLink CreateWebLinkDocument(string url, string previewUrl = null, string title = null, string subtitle = null);
    }
}
