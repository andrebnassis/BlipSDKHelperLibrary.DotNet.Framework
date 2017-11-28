using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{
    public class CarouselModel
    {
        internal List<CarouselCard> Cards{ get; set; }

        public CarouselModel()
        {
            Cards = new List<CarouselCard>();
        }

        public void AddCard(string title, string subtitle, string urlImage = null, int order = 0)
        {
            Cards.Add(new CarouselCard(title,subtitle, urlImage, order));
        }
        
        public CarouselCard GetCard(int cardIndex)
        {
            return Cards[cardIndex];
        }

    }

    public class CarouselCard
    {
        internal List<CarouselButton> Buttons { get; set; }
        internal string Title {  get; set; }
        internal string Subtitle { get; set; }
        internal string UrlImage { get; set; }
        internal int Order { get; set; }

        internal CarouselCard(string title, string subtitle, string urlImage, int order = 0) 
        {
            Title = title;
            Subtitle = subtitle;
            UrlImage = urlImage;
            Order = order;
            Buttons = new List<CarouselButton>();

        }
        
        public void AddTextButton(string text, string value, int order = 0)
        {
            Buttons.Add(new CarouselButton().ToTextButton(text, value, order));
        }

        public void AddLinkButton(string text, string url, int order = 0)
        {
            Buttons.Add(new CarouselButton().ToLinkButton(text, url, order));
        }
        
        public void AddShareButton(int order = 0)
        {
            Buttons.Add(new CarouselButton().ToShareButton(order));
        }

    }

    internal class CarouselButton : Button
    {
        internal CarouselButton()
        {

        }

        internal CarouselButton ToTextButton(string text, string value, int order = 0)
        {
            Text = text;
            Value = value;
            Order = order;
            Type = ButtonType.Text;
            return this;
        }

        internal CarouselButton ToLinkButton(string text, string value, int order = 0)
        {
            Text = text;
            Value = value;
            Order = order;
            Type = ButtonType.Link;
            return this;
        }
        
        internal CarouselButton ToShareButton(int order = 0)
        {
            Order = order;
            Type = ButtonType.Share;
            return this;
        }


    }
}
