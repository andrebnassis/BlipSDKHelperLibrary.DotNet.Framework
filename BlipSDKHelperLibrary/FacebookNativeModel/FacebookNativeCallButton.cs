using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.FacebookNativeModel
{
    internal class FacebookNativeCallButton
    {
        public string type { get; set; }
        public Payload payload { get; set; }

        internal FacebookNativeCallButton(string type)
        {
            this.type = type;
            payload = new Payload();
        }

        internal class Payload
        {
            internal Payload()
            {
                buttons = new List<Button>();
            }
            public string template_type { get; set; }
            public string text { get; set; }
            public List<Button> buttons { get; set; }
        }

        internal class Button
        {
            internal Button(string type, string title, string payload)
            {
                this.type = type;
                this.title = title;
                this.payload = payload;
            }
            public string type { get; set; }
            public string title { get; set; }
            public string payload { get; set; }
        }
    }

  

   
}
