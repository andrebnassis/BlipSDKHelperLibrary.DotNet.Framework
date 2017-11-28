using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{
    public class ReceiptModel
    {
        internal string OrderId { get; set; }
        internal string Currency { get; set; }
        internal List<Item> ItemList { get; set; }
        internal DateTime OrderedDate { get; set; }
        internal ShipInfo AddressInfo { get; set; }
        internal string PaymentMethod { get; set; }
        internal List<Additionals> AdditionalNotes { get; set; }
        internal double TotalCost { get; set; }
        internal double TotalTaxCost { get; set; }
        internal double TotalShippingCost { get; set; }
        internal string MerchantName { get; set; }

        public ReceiptModel(string currency_code = "USD")
        {
            OrderId = Guid.NewGuid().ToString();
            Currency = currency_code;
            ItemList = new List<Item>();
            OrderedDate = DateTime.Now;
        }

        public string GetOrderId()
        {
            return OrderId;
        }

        public DateTime GetOrderedDate()
        {
            return OrderedDate;
        }

        public Item GetItem(int index)
        {
            return ItemList[index];
        }

        public void SetTotalTaxCost(double value)
        {
            TotalTaxCost = Math.Round(value);
        }

        public void SetShippingCost(double value)
        {
            TotalShippingCost = Math.Round(value);
        }

        public void SetOrderedDate(DateTime date)
        {
            OrderedDate = date;
        }

        public void SetShipInfo(string customerName, string street1, string city, int postalCode, string state, string country, string street2 = null)
        {
            AddressInfo = new ShipInfo(customerName, street1, street2, city, postalCode, state, country);
        }

        public void SetPaymentMethod(string paymentMethod)
        {
            if (!string.IsNullOrEmpty(paymentMethod) && !string.IsNullOrWhiteSpace(paymentMethod))
            {
                PaymentMethod = paymentMethod;
            }
        }

        public void SetMerchantName(string text)
        {
            MerchantName = text;
        }

        public void SetOrderId(int number)
        {
            OrderId = number.ToString();
        }

        public void SetOrderId(string number)
        {
            OrderId = number;
        }
        public void SetOrderId(Guid number)
        {
            OrderId = number.ToString();
        }

        public void AddItem(string title, string subtitle, double oneItemPrice, int quantity, string imageUrl = null, int order = 0)
        {
            ItemList.Add(new Item(title, subtitle, oneItemPrice, Currency, quantity, imageUrl, order));
        }

        public void AddAdditionalNotes(string title, double value)
        {
            if (AdditionalNotes == null)
            {
                AdditionalNotes = new List<Additionals>();
            }
            AdditionalNotes.Add(new Additionals(title, value));
        }

        public void AddAdditionalNotes(string title, double value, bool addToTotal)
        {
            if (AdditionalNotes == null)
            {
                AdditionalNotes = new List<Additionals>();
            }
            AdditionalNotes.Add(new Additionals(title, value, addToTotal));
        }


        public class Item
        {
            internal Item(string title, string subtitle, double oneItemPrice, string currency, int quantity, string imageUrl = null, int order = 0)
            {
                Title = title;
                Subtitle = subtitle;
                Quantity = quantity;
                OneItemPrice = Math.Round(oneItemPrice, 2);
                TotalPrice = Quantity * OneItemPrice;
                ItemCurrency = currency;
                AddToSubtotal = true;
                Order = order;
                ImageUrl = imageUrl;
            }
            
            public void ChangeCurrency(string new_currency)
            {
                ItemCurrency = new_currency;
            }

            public void RemoveFromSubtotalCalculus()
            {
                AddToSubtotal = false;
            }

            internal string Title { get; set; }
            internal string Subtitle { get; set; }
            internal int Quantity { get; set; }
            internal double OneItemPrice { get; set; }
            internal double TotalPrice { get; set; }
            internal string ItemCurrency { get; set; }
            internal bool AddToSubtotal { get; set; }
            public int Order { get; set; }
            internal string ImageUrl { get; set; }
        }

        internal class ShipInfo
        {
            internal ShipInfo(string customerName, string street1, string street2, string city, int postalCode, string state, string country)
            {
                CustomerName = customerName;
                Street1 = street1;
                Street2 = street2;
                City = city;
                PostalCode = postalCode;
                State = state;
                Country = country;

            }

            internal string CustomerName { get; set; }
            internal string Street1 { get; set; }
            internal string Street2 { get; set; }
            internal string City { get; set; }
            internal int PostalCode { get; set; }
            internal string State { get; set; }
            internal string Country { get; set; }
        }

        internal class Additionals
        {
            internal IDictionary<string, double> Item { get; set; }
            internal string Title { get; set; }
            internal double Value { get; set; }
            internal bool AddToTotal { get; set; }

            internal Additionals(string title, double value, bool addToTotal = true)
            {
                Title = title;
                Value = Math.Round(value);
                AddToTotal = addToTotal;
            }



        }
    }

}
