using BlipSDKHelperLibrary.Models;
using Lime.Messaging.Contents;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.SdkHelpers
{
    public class BlipChatBlipSDKHelper : IBlipChatBlipSDKHelper
    {
        public BlipChatBlipSDKHelper()
        {

        }
        public DocumentCollection CreateCarouselDocument(CarouselModel carouselModel)
        {
            return BlipSDKHelperCore.GENERIC_CreateCarouselDocument(carouselModel);
        }

        public DocumentCollection CreateCollectionOfDocuments(GroupDocumentsModel content)
        {
            return BlipSDKHelperCore.GENERIC_CreateCollectionOfDocuments(content);
        }

        public DocumentCollection CreateCollectionOfDocuments(params Document[] content)
        {
            return BlipSDKHelperCore.GENERIC_CreateCollectionOfDocuments(content);
        }

        public MediaLink CreateImageDocument(string urlImage, string previewUrlImage = null, string title = null, string subtitle = null)
        {
            return BlipSDKHelperCore.GENERIC_CreateImageDocument(urlImage, previewUrlImage, title, subtitle);
        }

        public Select CreateMenuDocument(MenuModel menuModel)
        {
            return BlipSDKHelperCore.GENERIC_CreateMenuDocument(menuModel);
        }

        public Document CreateQuickReplyDocument(QuickReplyModel quickReplyModel)
        {
            return BlipSDKHelperCore.GENERIC_CreateQuickReplyDocument(quickReplyModel);
        }

        public Document CreateQuickReplySendLocationDocument(QuickReplyModel quickReplyModel)
        {
            return BlipSDKHelperCore.GENERIC_CreateQuickReplySendLocationDocument(quickReplyModel);
        }

        public PlainText CreateTextDocument(string text)
        {
            return BlipSDKHelperCore.GENERIC_CreateTextDocument(text);
        }

        public WebLink CreateWebLinkDocument(string url, string previewUrl = null, string title = null, string subtitle = null)
        {
            return BlipSDKHelperCore.GENERIC_CreateWebLinkDocument(url, previewUrl, title, subtitle);
        }
    }
}
