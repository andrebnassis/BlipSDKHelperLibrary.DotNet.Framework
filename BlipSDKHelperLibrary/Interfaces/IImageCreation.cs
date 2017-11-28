using Lime.Messaging.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Interfaces
{
    public interface IImageCreation
    {
        MediaLink CreateImageDocument(string urlImage, string previewUrlImage = null, string title = null, string subtitle = null);
    }
}
