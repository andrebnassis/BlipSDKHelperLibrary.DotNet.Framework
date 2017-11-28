using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.FacebookNativeModel
{
    internal class FacebookNativeList
    {
        internal FacebookNativeList()
        {
            type = FacebookNativeElementType.template;
            payload = new Payload();
        }

        public FacebookNativeElementType type { get; set; }
        public Payload payload { get; set; }

        internal void HighlightHeader()
        {
            payload.top_element_style = TopElementStyle.large;
        }

        internal ItemList GetItem(int index)
        {
            return payload.elements[index];
        }

        internal void AddBottomButton(string text, string value)
        {
            payload.buttons.Add(new ItemListButton().ToTextButton(text, value));
        }

        internal void AddItem(string title, string subtitle, string image_url)
        {
            payload.elements.Add(new ItemList(title, subtitle, image_url));
        }

        internal class Payload
        {
            internal Payload()
            {
                template_type = FacebookNativeTemplateType.list;
                top_element_style = TopElementStyle.compact;
                elements = new List<ItemList>();
                buttons = new List<ItemListButton>();
            }
            
            public FacebookNativeTemplateType template_type { get; set; }
            public TopElementStyle top_element_style { get; set; }
            public List<ItemList> elements { get; set; }
            public List<ItemListButton> buttons { get; set; }
        }

        internal enum TopElementStyle
        {
            compact,
            large
        }

        internal class ItemList
        {
            internal ItemList(string title, string subtitle, string image_url)
            {
                this.title = title;
                this.subtitle = subtitle;
                this.image_url = image_url;
                buttons = new List<ItemListButton>();
                default_action = null;
            }

            internal void AddButton(string text, string value)
            {
                buttons.Add(new ItemListButton().ToTextButton(text, value));
            }

            internal ItemList SetItemListAsWebLink(string url, string webviewRatioRatio)
            {
                default_action = new DefaultAction();
                default_action.ToWebLink(url, webviewRatioRatio);
                return this;
            }

            public string title { get; set; }
            public string subtitle { get; set; }
            public string image_url { get; set; }
            public DefaultAction default_action { get; set; }
            public List<ItemListButton> buttons { get; set; }
        }

        internal class DefaultAction
        {
            public FacebookNativeButtonType type { get; set; }
            public string url { get; set; }
            public bool messenger_extensions { get; set; }
            public string webview_height_ratio { get; set; }
            public string fallback_url { get; set; }

            internal DefaultAction()
            {

            }

            internal DefaultAction ToWebLink(string url, string webviewHeightRatio = "tall")
            {
                type = FacebookNativeButtonType.web_url;
                this.url = url;
                messenger_extensions = true;
                webview_height_ratio = webviewHeightRatio;
                fallback_url = url;
                      
                return this;
            }

            
        }

        internal class ItemListButton
        {
            internal ItemListButton()
            {

            }

            internal ItemListButton ToTextButton(string text, string value)
            {
                title = text;
                type = FacebookNativeButtonType.postback;
                payload = value;
                return this;
            }
            public string title { get; set; }
            public FacebookNativeButtonType type { get; set; }
            public string payload { get; set; }
        }

    }

   
}
