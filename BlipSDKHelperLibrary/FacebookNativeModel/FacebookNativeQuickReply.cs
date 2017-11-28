using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.FacebookNativeModel
{
    internal class FacebookNativeQuickReply
    {
        public FacebookNativeQuickReply(string text = "")
        {
            this.text = text;
            quick_replies = new List<QuickReply>();
        }
        
        public string text { get; set; }
        
        public List<QuickReply> quick_replies { get; set; }

        internal void AddTextButton(string title, string subtitle, string imageUrl = null)
        {
            quick_replies.Add(new QuickReply().ToTextButton(title, subtitle, imageUrl));
        }

        internal void AddLocationButton()
        {
            quick_replies.Add(new QuickReply().ToLocationButton());
        }

        internal class QuickReply
        {
            public QuickReply()
            {

            }
            public FacebookNativeContentType content_type { get; set; }
            public string title { get; set; }
            public string payload { get; set; }
            public string image_url { get; set; }

            internal QuickReply ToTextButton(string title, string subtitle, string imageUrl = null)
            {
                content_type = FacebookNativeContentType.text;
                this.title = title;
                payload = subtitle;
                image_url = imageUrl;
                return this;
            }

            internal QuickReply ToLocationButton()
            {
                content_type = FacebookNativeContentType.location;
                return this;
            }



        }

        
    }
}
