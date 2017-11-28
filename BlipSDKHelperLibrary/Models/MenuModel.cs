using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{

    public class MenuModel
    {
        internal string Text { get; set; }
        internal List<MenuButton> Options { get; set; }

        public MenuModel(string text)
        {
            Text = text;
            Options = new List<MenuButton>();
        }

        public void AddTextButton(string text, string value, int order = 0)
        {
            Options.Add( new MenuButton(text,value,order));
        }
      
        internal class MenuButton : Button
        {
            internal MenuButton()
            {

            }
            internal MenuButton(string text, string value, int order = 0)
            {
                Text = text;
                Value = value;
                Order = order;
                Type = ButtonType.Text;
            }
        }
    }
}
