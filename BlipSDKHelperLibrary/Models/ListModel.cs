using BlipSDKHelperLibrary.FacebookNativeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{
    public class ListModel
    {
        internal List<Item> Items { get; set; }
        internal bool HighlightHeader { get; set; }
        internal ListButton BottomButton { get; set; }

        public ListModel()
        {
            BottomButton = null;
            Items = new List<Item>();
            HighlightHeader = false;
        }

        public bool GetHighlightFirstItemSettings()
        {
            return HighlightHeader;
        }

        public void HighlightFirstItem()
        {
            HighlightHeader = true;
        }
        
        public void AddItem(string title, string subtitle, string url_image, int order = 0)
        {
            Items.Add(new Item(title, subtitle, url_image, order));
        }

        public Item GetItem(int itemIndex)
        {
            return Items[itemIndex];
        }

        public void AddBottomTextButton(string text, string value)
        {
            BottomButton = new ListButton().ToTextButton(text, value);
        }

        public class Item
        {
            internal ListButton Button { get; set; }
            internal string Title { get; set; }
            internal string Subtitle { get; set; }
            internal string ImageUrl { get; set; }
            internal string Url { get; set; }
            internal WebviewSize WebviewHeightSettings { get; set; }
            internal int Order { get; set; }
            internal Item(string title, string subtitle, string url_image, int order)
            {

                Title = title;
                Subtitle = subtitle;
                ImageUrl = url_image;
                Order = order;
                Button = null;
            }

            public void AddTextButton(string text, string value)
            {
                Button = new ListButton().ToTextButton(text, value);
            }

            public void AddWebUrl(string url, WebviewSize webviewHeightSettings = WebviewSize.tall)
            {
                Url = url;
                WebviewHeightSettings = webviewHeightSettings;
            }

        }

        internal class ListButton : Button
        {
            internal ListButton()
            {

            }
            internal ListButton ToTextButton(string text, string value)
            {
                Text = text;
                Value = value;
                Type = ButtonType.Text;
                return this;
            }
        }
    }
}
