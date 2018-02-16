using BlipSDKHelperLibrary.FacebookNativeModel;
using BlipSDKHelperLibrary.Models;
using Lime.Messaging.Contents;
using Lime.Protocol;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Takenet.MessagingHub.Client.Sender;
using static BlipSDKHelperLibrary.Models.QuickReplyModel;

namespace BlipSDKHelperLibrary
{

    internal static class BlipSDKHelperCore
    {

        //Generic implementations.
        #region Generic
        public static PlainText GENERIC_CreateTextDocument(string text)
        {
            var document = new PlainText();
            document.Text = text;
            return document;
        }

        public static DocumentCollection GENERIC_CreateCarouselDocument(CarouselModel carouselModel)
        {

            var carousel = new DocumentCollection();
            //contents.
            carousel.Items = new DocumentSelect[carouselModel.Cards.Count];
            carousel.ItemType = DocumentSelect.MediaType;

            var cards = carouselModel.Cards.OrderBy(c => c.Order).ToList();

            //Para cada content existente.
            for (int i = 0; i < cards.Count; i++)
            {

                var tmp = new DocumentSelect();
                tmp.Header = new DocumentContainer();
                tmp.Header.Value = new MediaLink();
                (tmp.Header.Value as MediaLink).Title = cards[i].Title;
                (tmp.Header.Value as MediaLink).Text = cards[i].Subtitle;

                try
                {
                    (tmp.Header.Value as MediaLink).PreviewUri = new Uri(cards[i].UrlImage);
                    (tmp.Header.Value as MediaLink).Uri = new Uri(cards[i].UrlImage);
                }
                catch { }

                (tmp.Header.Value as MediaLink).Type = MediaType.Parse("image/*");

                var buttons = cards[i].Buttons.OrderBy(c => c.Order).ToList();
                buttons.RemoveAll(c => c.Type.Equals(ButtonType.Share));

                DocumentSelectOption[] tmp_buttons = new DocumentSelectOption[buttons.Count()];
                if (buttons.Any())
                {
                    for (int j = 0; j < buttons.Count(); j++)
                    {

                        if (buttons[j].Type == Models.ButtonType.Text)
                        {
                            DocumentSelectOption button = new DocumentSelectOption();
                            button.Order = j;
                            button.Label = new DocumentContainer();
                            button.Label.Value = GENERIC_CreateTextDocument(buttons[j].Text);
                            button.Value = new DocumentContainer();
                            button.Value.Value = GENERIC_CreateTextDocument(buttons[j].Value);
                            tmp_buttons[j] = button;
                        }
                        else if (buttons[j].Type == Models.ButtonType.Link)
                        {
                            DocumentSelectOption button = new DocumentSelectOption();
                            button.Order = j;
                            button.Label = new DocumentContainer();
                            button.Label.Value = GENERIC_CreateWebLinkDocument(buttons[j].Value, null, buttons[j].Text);
                            tmp_buttons[j] = button;
                        }

                    }

                }
                tmp.Options = tmp_buttons;

                carousel.Items[i] = tmp;
            }
            return carousel;
        }

        public static MediaLink GENERIC_CreateImageDocument(string urlImage, string previewUrlImage = null, string title = null, string subtitle = null)
        {
            var imageUri = new Uri(Uri.EscapeUriString(urlImage));
            var previewImageUri = previewUrlImage.IsNullOrWhiteSpace() ? null : new Uri(Uri.EscapeUriString(previewUrlImage));

            var document = new MediaLink();
            document.Title = title.IsNullOrWhiteSpace() ? null : title;
            document.Text = subtitle.IsNullOrWhiteSpace() ? null : subtitle;
            document.Type = MediaType.Parse("image/*");
            document.PreviewUri = previewImageUri;
            document.Uri = imageUri;

            return document;
        }


        public static MediaLink GENERIC_CreateVideoDocument(string urlVideo, string title = null, string subtitle = null)
        {
            var videoUri = new Uri(Uri.EscapeUriString(urlVideo));

            var document = new MediaLink();
            document.Title = title.IsNullOrWhiteSpace() ? null : title;
            document.Text = subtitle.IsNullOrWhiteSpace() ? null : subtitle;
            document.Type = MediaType.Parse("video/*");
            document.PreviewUri = videoUri;
            document.Uri = videoUri;

            return document;
        }

        public static WebLink GENERIC_CreateWebLinkDocument(string url, string previewUrl = null, string title = null, string subtitle = null)
        {
            Uri uri = null;
            if (!string.IsNullOrEmpty(url))
            {
                uri = new Uri(Uri.EscapeUriString(url));
            }

            var previewUri = previewUrl.IsNullOrWhiteSpace() ? null : new Uri(Uri.EscapeUriString(previewUrl));

            var document = new WebLink();
            document.Title = title.IsNullOrWhiteSpace() ? null : title;
            document.Text = subtitle.IsNullOrWhiteSpace() ? null : subtitle;
            document.Uri = uri;
            document.PreviewUri = previewUri;

            return document;
        }

        public static Select GENERIC_CreateMenuDocument(MenuModel menuModel)
        {
            var document = new Select();
            document.Text = menuModel.Text;

            var options = menuModel.Options.OrderBy(c => c.Order).ToList();


            document.Options = new SelectOption[options.Count];

            for (int i = 0; i < options.Count; i++)
            {
                document.Options[i] = new SelectOption();
                var button = options.ElementAt(i);
                document.Options[i].Text = button.Text;
                document.Options[i].Value = GENERIC_CreateTextDocument(button.Value);
                document.Options[i].Order = i;

            }

            return document;
        }

        public static Select GENERIC_CreateMenuDocument(MenuModel menuModel, SelectScope scope)
        {
            var document = new Select();
            document.Text = menuModel.Text;
            document.Scope = scope;

            var options = menuModel.Options.OrderBy(c => c.Order).ToList();


            document.Options = new SelectOption[options.Count];

            for (int i = 0; i < options.Count; i++)
            {
                document.Options[i] = new SelectOption();
                var button = options.ElementAt(i);
                document.Options[i].Text = button.Text;
                document.Options[i].Value = GENERIC_CreateTextDocument(button.Value);
                document.Options[i].Order = i;

            }

            return document;
        }

        public static DocumentCollection GENERIC_CreateCollectionOfDocuments(params Document[] content)
        {
            var document = new DocumentCollection();
            document.ItemType = DocumentContainer.MediaType;
            document.Items = new Document[content.Count()];
            document.Total = content.Count();
            for (int i = 0; i < content.Count(); i++)
            {
                document.Items[i] = new DocumentContainer();
                (document.Items[i] as DocumentContainer).Value = content[i];
            }
            return document;
        }

        public static DocumentCollection GENERIC_CreateCollectionOfDocuments(GroupDocumentsModel content)
        {
            var docsListOrdered = content.Docs.OrderBy(c => c.Order).ToList();

            var document = new DocumentCollection();
            document.ItemType = DocumentContainer.MediaType;
            document.Items = new Document[docsListOrdered.Count()];
            document.Total = docsListOrdered.Count();
            for (int i = 0; i < docsListOrdered.Count(); i++)
            {
                document.Items[i] = new DocumentContainer();
                (document.Items[i] as DocumentContainer).Value = docsListOrdered[i].Doc;
            }

            return document;
        }


        public static DocumentCollection GENERIC_CreateQuickReplyDocument(QuickReplyModel quickReplyModel)
        {

            var options = new List<SelectOption>();
            var buttons = quickReplyModel.Options.OrderBy(c => c.Order).ToList();

            var documentList = new GroupDocumentsModel();

            if (buttons.Exists(c => ButtonType.Location.Equals(c.Type)))
            {
                documentList.Add(CreateSendLocationDocument(quickReplyModel.Text));
            }
            else
            {
                for (int i = 0; i < buttons.Count(); i++)
                {
                    if (ButtonType.Text.Equals(buttons[i].Type))
                    {
                        options.Add(CreateQuickReplyTextButton(buttons[i].Text, buttons[i].Value, i));
                    }
                }
            }

            if (options.Any())
            {
                var quickreplydoc = new Select();
                quickreplydoc.Scope = SelectScope.Immediate;
                quickreplydoc.Options = new SelectOption[options.Count];
                quickreplydoc.Text = quickReplyModel.Text;

                var selectOption = new SelectOption[options.Count()];

                for (int i = 0; i < options.Count(); i++)
                {
                    selectOption[i] = options[i];
                }

                quickreplydoc.Options = selectOption;

                documentList.Add(quickreplydoc);
            }

            var document = GENERIC_CreateCollectionOfDocuments(documentList);

            return document;
        }

        private static SelectOption CreateQuickReplyTextButton(string text, string key, int order)
        {
            var button = new SelectOption();
            button.Text = text;
            button.Value = GENERIC_CreateTextDocument(key);
            button.Order = order;

            return button;
        }

        private static Document CreateSendLocationDocument(string text)
        {
            var document = new Input();
            document.Label = new DocumentContainer();
            document.Label.Value = text;
            (document as Input).Validation = new InputValidation();
            (document as Input).Validation.Rule = InputValidationRule.Type;
            (document as Input).Validation.Type = Location.MediaType;

            return document;
        }

        #endregion

        //Messenger Special Implementations: Receipt, CallButton, List, QuickReply;
        #region Messenger
        public static DocumentCollection MESSENGER_CreateCarouselDocument(CarouselModel carouselModel)
        {

            var carousel = new DocumentCollection();
            //contents.
            carousel.Items = new DocumentSelect[carouselModel.Cards.Count];
            carousel.ItemType = DocumentSelect.MediaType;

            var cards = carouselModel.Cards.OrderBy(c => c.Order).ToList();

            //Para cada content existente.
            for (int i = 0; i < cards.Count; i++)
            {

                var tmp = new DocumentSelect();
                tmp.Header = new DocumentContainer();
                tmp.Header.Value = new MediaLink();
                (tmp.Header.Value as MediaLink).Title = cards[i].Title;
                (tmp.Header.Value as MediaLink).Text = cards[i].Subtitle;

                try
                {
                    (tmp.Header.Value as MediaLink).PreviewUri = new Uri(cards[i].UrlImage);
                    (tmp.Header.Value as MediaLink).Uri = new Uri(cards[i].UrlImage);
                }
                catch { }

                (tmp.Header.Value as MediaLink).Type = MediaType.Parse("image/*");

                var buttons = cards[i].Buttons.OrderBy(c => c.Order).ToList();

                DocumentSelectOption[] tmp_buttons = new DocumentSelectOption[buttons.Count()];
                if (buttons.Any())
                {
                    for (int j = 0; j < buttons.Count(); j++)
                    {

                        if (buttons[j].Type == Models.ButtonType.Text)
                        {
                            DocumentSelectOption button = new DocumentSelectOption();
                            button.Order = j;
                            button.Label = new DocumentContainer();
                            button.Label.Value = GENERIC_CreateTextDocument(buttons[j].Text);
                            button.Value = new DocumentContainer();
                            button.Value.Value = GENERIC_CreateTextDocument(buttons[j].Value);
                            tmp_buttons[j] = button;
                        }
                        else if (buttons[j].Type == Models.ButtonType.Link)
                        {
                            DocumentSelectOption button = new DocumentSelectOption();
                            button.Order = j;
                            button.Label = new DocumentContainer();
                            button.Label.Value = GENERIC_CreateWebLinkDocument(buttons[j].Value, null, buttons[j].Text);
                            button.Value = new DocumentContainer();
                            button.Value.Value = GENERIC_CreateTextDocument(buttons[j].Value);
                            tmp_buttons[j] = button;
                        }
                        else if (buttons[j].Type == Models.ButtonType.Share)
                        {
                            DocumentSelectOption button = new DocumentSelectOption();
                            button.Order = button.Order;
                            button.Label = new DocumentContainer();
                            button.Label.Value = new WebLink() { Uri = new Uri("share:") };
                            tmp_buttons[j] = button;
                        }

                    }

                }
                tmp.Options = tmp_buttons;

                carousel.Items[i] = tmp;
            }
            return carousel;
        }

        public static JsonDocument MESSENGER_CreateQuickReplyDocument(QuickReplyModel quickReplyModel)
        {

            var quickReplyJsonDoc = new FacebookNativeQuickReply(quickReplyModel.Text);

            var buttons = quickReplyModel.Options.OrderBy(c => c.Order).ToList();

            for (int i = 0; i < buttons.Count(); i++)
            {
                if (Models.ButtonType.Text.Equals(buttons[i].Type))
                {
                    quickReplyJsonDoc.AddTextButton(buttons[i].Text, buttons[i].Value, buttons[i].ImageUrl);
                }
                else if (Models.ButtonType.Location.Equals(buttons[i].Type))
                {
                    quickReplyJsonDoc.AddLocationButton();
                }
            }

            var jsonDict = new Dictionary<string, object>();
            jsonDict.Add("text", quickReplyJsonDoc.text);
            jsonDict.Add("quick_replies", JsonConvert.SerializeObject(quickReplyJsonDoc.quick_replies));

            var document = new JsonDocument(jsonDict, MediaType.ApplicationJson);

            return document;

        }

        public static JsonDocument MESSENGER_CreateListDocument(ListModel itemsList)
        {
            var ListJsonDoc = new FacebookNativeList();
            if (itemsList.BottomButton != null)
            {
                ListJsonDoc.AddBottomButton(itemsList.BottomButton?.Text, itemsList.BottomButton?.Value);
            }

            if (itemsList.GetHighlightFirstItemSettings())
            {
                ListJsonDoc.HighlightHeader();
            }

            var items = itemsList.Items.OrderBy(c => c.Order).ToList();

            for (int i = 0; i < items.Count(); i++)
            {
                ListJsonDoc.AddItem(items[i].Title, items[i].Subtitle, items[i].ImageUrl);

                if (!items[i].Url.IsNullOrWhiteSpace())
                {
                    ListJsonDoc.GetItem(i).SetItemListAsWebLink(items[i].Url, items[i].WebviewHeightSettings.ToString());
                }
                if (items[i].Button != null)
                {
                    ListJsonDoc.GetItem(i).AddButton(items[i].Button.Text, items[i].Button.Value);
                }

            }

            var jsonDict = new Dictionary<string, object>();
            var jsonResult = JsonConvert.SerializeObject(ListJsonDoc);
            jsonDict.Add("attachment", jsonResult);
            var document = new JsonDocument(jsonDict, MediaType.ApplicationJson);

            return document;
        }

        public static JsonDocument MESSENGER_CreateCallButtonDocument(string title, string buttonText, string phoneNumber)
        {
            var callButton = new FacebookNativeCallButton(FacebookNativeElementType.template.ToString());

            callButton.payload = new FacebookNativeCallButton.Payload();
            callButton.payload.template_type = FacebookNativeTemplateType.button.ToString();
            callButton.payload.text = title;
            callButton.payload.buttons.Add(new FacebookNativeCallButton.Button(FacebookNativeButtonType.phone_number.ToString(), buttonText, phoneNumber));

            var jsonDict = new Dictionary<string, object>();
            var jsonResult = JsonConvert.SerializeObject(callButton);
            jsonDict.Add("attachment", jsonResult);

            var document = new JsonDocument(jsonDict, MediaType.ApplicationJson);

            return document;
        }

        public static JsonDocument MESSENGER_CreateReceiptDocument(ReceiptModel receipt)
        {
            var items = receipt.ItemList.OrderByDescending(c => c.Order).ToList();

            var facebookReceipt = new FacebookNativeReceipt();
            facebookReceipt.payload.currency = receipt.Currency;

            foreach (var item in items)
            {
                item.TotalPrice = (item.Quantity) * item.OneItemPrice;
                facebookReceipt.AddItem(item.Title, item.Subtitle, item.Quantity, item.TotalPrice, item.ImageUrl, item.ItemCurrency);

                if (item.AddToSubtotal)
                {
                    facebookReceipt.payload.summary.subtotal += item.TotalPrice;
                }
            }

            var additionals = receipt.AdditionalNotes;

            if (additionals != null)
            {
                foreach (var additionalItem in additionals)
                {
                    facebookReceipt.payload.adjustments.Add(new FacebookNativeReceipt.Payload.Adjustment(additionalItem.Title, additionalItem.Value));
                    if (additionalItem.AddToTotal)
                    {
                        facebookReceipt.payload.summary.total_cost += additionalItem.Value;
                    }
                }
            }

            facebookReceipt.payload.summary.total_tax = receipt.TotalTaxCost;
            facebookReceipt.payload.summary.total_cost += facebookReceipt.payload.summary.total_tax;

            facebookReceipt.payload.summary.shipping_cost = receipt.TotalShippingCost;
            facebookReceipt.payload.summary.total_cost += facebookReceipt.payload.summary.shipping_cost;

            facebookReceipt.payload.summary.total_cost += facebookReceipt.payload.summary.subtotal;

            facebookReceipt.SetPaymentMethod(receipt.PaymentMethod);

            facebookReceipt.SetMerchantName(receipt.MerchantName);

            facebookReceipt.SetOrderedNumber(receipt.GetOrderId());

            Int32 unixTimestamp = (Int32)(receipt.GetOrderedDate().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            facebookReceipt.AddOrderedDate(unixTimestamp.ToString());

            var addressInfo = receipt.AddressInfo;
            facebookReceipt.AddShipInfo(addressInfo.CustomerName, addressInfo.Street1, addressInfo.City, addressInfo.PostalCode.ToString(), addressInfo.State, addressInfo.Country, addressInfo.Street2);


            var jsonDict = new Dictionary<string, object>();
            var jsonResult = JsonConvert.SerializeObject(facebookReceipt);
            jsonDict.Add("attachment", jsonResult);
            var document = new JsonDocument(jsonDict, MediaType.ApplicationJson);

            return document;


        }
        #endregion



    }



}
