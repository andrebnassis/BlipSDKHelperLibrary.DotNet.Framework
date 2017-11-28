using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{
    public class QuickReplyModel
    {
        internal string Text { get; set; }
        internal List<QuickReplyButton> Options { get; set; }

        public QuickReplyModel(string text)
        {
            Text = text;
            Options = new List<QuickReplyButton>();
        }

        public void AddTextButton(string text, string value, string imageUrl = null, int order = 0)
        {
            Options.Add(new QuickReplyButton().ToTextButton(text, value, imageUrl, order));
        }

        public void AddLocationButton(int order = 0)
        {
            Options.Add(new QuickReplyButton().ToLocationButton(order));
        }
        
        internal class QuickReplyButton : Button
        {
            internal string ImageUrl { get; set; }
            internal QuickReplyButton()
            {
               
            }

            internal QuickReplyButton ToTextButton(string text, string value, string imageUrl = null, int order = 0)
            {
                Text = text;
                Value = value;
                Order = order;
                Type = ButtonType.Text;
                ImageUrl = imageUrl;
                return this;
            }

            internal QuickReplyButton ToLocationButton(int order = 0)
            {
                Order = order;
                Type = ButtonType.Location;
                return this;

            }
        }
    }
}
