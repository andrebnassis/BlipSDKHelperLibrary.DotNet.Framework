using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlipSDKHelperLibrary.FacebookNativeModel.FacebookNativeReceipt.Payload;

namespace BlipSDKHelperLibrary.FacebookNativeModel
{
    internal class FacebookNativeReceipt
    {
        public FacebookNativeElementType type { get; set; }
        public Payload payload { get; set; }

        internal FacebookNativeReceipt()
        {
            type = FacebookNativeElementType.template;
            payload = new Payload();
        }

        internal void AddItem(string title, string subtitle, int quantity, double price, string image_url, string currency)
        {
            payload.elements.Add(new Element(title, subtitle, price, quantity, currency, image_url));
        }

        internal void AddOrderedDate(string date)
        {
            
            payload.timestamp = date;
        }

        internal void AddShipInfo(string customerName, string street1, string city, string postalcode, string state, string country, string street2 = "")
        {
            payload.address = new Address(street1, city, postalcode, state, country, street2 == null ? "": street2);
            payload.recipient_name = customerName;
        }

        internal void SetPaymentMethod(string paymentMethod)
        {
            payload.payment_method = paymentMethod;
        }

        internal void AddSummary(string title, double value)
        {
            payload.adjustments.Add(new Adjustment(title, value));
        }

        internal void AddToTotalCost(double value)
        {
            payload.summary.total_cost += value;
        }

        
        internal void SetTotalCost(double value)
        {
            payload.summary.total_cost = value;
        }

        internal void SetShippingCost(double value)
        {
            payload.summary.shipping_cost = value;
        }

        internal void SetTotalTax(double value)
        {
            payload.summary.total_tax = value;
        }

        internal void SetOrderedNumber(string number)
        {
            payload.order_number = number;
        }

        internal void SetMerchantName(string text)
        {
            payload.merchant_name = text;
        }


        public class Payload
        {
            internal Payload(string currency = "USD")
            {
                template_type = FacebookNativeTemplateType.receipt;
                sharable = true.ToString().ToLower();
                this.currency = currency;
                address = new Address();
                summary = new Summary();
                adjustments = new List<Adjustment>();
                elements = new List<Element>();
            }

            public FacebookNativeTemplateType template_type { get; set; }
            public string sharable { get; set; }
            public string recipient_name { get; set; }
            public string merchant_name { get; set; }
            public string order_number { get; set; }
            public string currency { get; set; }
            public string payment_method { get; set; }
            public string order_url { get; set; }
            public string timestamp { get; set; }
            public Address address { get; set; }
            public Summary summary { get; set; }
            public List<Adjustment> adjustments { get; set; }
            public List<Element> elements { get; set; }

          

            internal class Address
            {
                internal Address()
                {

                }
                internal Address(string street1, string city, string postalcode, string state, string country, string street2 = null)
                {
                    street_1 = street1;
                    street_2 = street2;
                    this.city = city;
                    postal_code = postalcode;
                    this.state = state;
                    this.country = country;
                    
                }
                public string street_1 { get; set; }
                public string street_2 { get; set; }
                public string city { get; set; }
                public string postal_code { get; set; }
                public string state { get; set; }
                public string country { get; set; }
            }



            internal class Summary
            {
                internal Summary()
                {

                }
                public double subtotal { get; set; }
                public double shipping_cost { get; set; }
                public double total_tax { get; set; }
                public double total_cost { get; set; }
            }

            internal class Adjustment
            {
                internal Adjustment(string title, double value)
                {
                    name = title;
                    amount = value;
                }
                public string name { get; set; }
                public double amount { get; set; }
            }

            internal class Element
            {
                internal Element(string title, string subtitle, double price, int quantity, string currency, string image_url)
                {
                    this.title = title;
                    this.subtitle = subtitle;
                    this.quantity = quantity;
                    this.price = price;
                    this.currency = currency;
                    this.image_url = image_url;

                }
                public string title { get; set; }
                public string subtitle { get; set; }
                public int quantity { get; set; }
                public double price { get; set; }
                public string currency { get; set; }
                public string image_url { get; set; }
            }
        }
    }

   
}
