using BlipSDKHelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lime.Messaging.Contents;
using BlipSDKHelperLibrary.Interfaces;
using Lime.Protocol;
using BlipSDKHelperLibrary.FacebookNativeModel;
using RestSharp;
using Newtonsoft.Json;
using Takenet.MessagingHub.Client.Sender;

namespace BlipSDKHelperLibrary
{
    public class FacebookBlipSDKHelper : IFacebookBlipSDKHelper
    {
       
        public FacebookBlipSDKHelper()
        {

        }
        
        public JsonDocument CreateCallButtonDocument(string title, string buttonText, string phoneNumber)
        {
            return BlipSDKHelperCore.MESSENGER_CreateCallButtonDocument(title, buttonText, phoneNumber);
        }

        public DocumentCollection CreateCarouselDocument(CarouselModel carouselModel)
        {
            return BlipSDKHelperCore.GENERIC_CreateCarouselDocument(carouselModel);
        }

        public DocumentCollection CreateCollectionOfDocuments(GroupDocumentsModel content)
        {
            return BlipSDKHelperCore.GENERIC_CreateCollectionOfDocuments(content);
        }

        public DocumentCollection CreateCollectionOfDocuments(params Document[] content)
        {
            return BlipSDKHelperCore.GENERIC_CreateCollectionOfDocuments(content);
        }

        public MediaLink CreateImageDocument(string urlImage, string previewUrlImage = null, string title = null, string subtitle = null)
        {
            return BlipSDKHelperCore.GENERIC_CreateImageDocument(urlImage, previewUrlImage, title, subtitle);
        }

        public Document CreateListDocument(ListModel itemsList)
        {
            return BlipSDKHelperCore.MESSENGER_CreateListDocument(itemsList);
        }

        public Select CreateMenuDocument(MenuModel menuModel)
        {
            return BlipSDKHelperCore.GENERIC_CreateMenuDocument(menuModel);
        }

        public Document CreateQuickReplyDocument(QuickReplyModel quickReplyModel)
        {
            return BlipSDKHelperCore.MESSENGER_CreateQuickReplyDocument(quickReplyModel);
        }

        public JsonDocument CreateReceiptDocument(ReceiptModel receipt)
        {
            return BlipSDKHelperCore.MESSENGER_CreateReceiptDocument(receipt);
        }

        public PlainText CreateTextDocument(string text)
        {
            return BlipSDKHelperCore.GENERIC_CreateTextDocument(text);
        }

        public MediaLink CreateVideoDocument(string urlVideo, string title = null, string subtitle = null)
        {
            return BlipSDKHelperCore.GENERIC_CreateVideoDocument(urlVideo, title, subtitle);
        }

        public WebLink CreateWebLinkDocument(string url, string previewUrl = null, string title = null, string subtitle = null)
        {
            return BlipSDKHelperCore.GENERIC_CreateWebLinkDocument(url, previewUrl, title, subtitle);
        }

        private async Task<string> GetPageAccessToken(IMessagingHubSender sender)
        {
            object pageAccessToken = "";

            try
            {
                var commandResponse = await sender.SendCommandAsync(new Command() { Id = EnvelopeId.NewId(), Method = CommandMethod.Get, Uri = new LimeUri("/configuration/caller") });
                foreach (var item in (commandResponse.Resource as DocumentCollection).Items)
                {
                    object key = "";
                    var foundKey = (item as JsonDocument).TryGetValue("name", out key);
                    if (key.ToString().Equals("PageAccessToken"))
                    {
                        var foundValue = (item as JsonDocument).TryGetValue("value", out pageAccessToken);
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error trying to get PageAccessToken", e);
            }

            return pageAccessToken.ToString();
        }

        public async Task<IRestResponse> RegisterDomainToWhitelist(IMessagingHubSender sender, params string[] urls)
        {
            var pageAccessToken = await GetPageAccessToken(sender);

            if (pageAccessToken.Trim().IsNullOrEmpty())
            {
                throw (new Exception("Could not get PageAccessToken"));
            }

            var client = new RestClient("https://graph.facebook.com/v2.6/me");
            var request = new RestRequest("thread_settings?access_token={PageAccessToken}", Method.POST);
            request.AddUrlSegment("PageAccessToken", pageAccessToken);

            var UrlList = new List<string>();
            foreach (var url in urls)
            {
                UrlList.Add(url);
            }
            var UrlListJson = JsonConvert.SerializeObject(UrlList);

            request.AddParameter("setting_type", "domain_whitelisting");
            request.AddParameter("whitelisted_domains", UrlListJson);
            request.AddParameter("domain_action_type", "add");
            request.AddHeader("Content-Type", "application/json");

            var result = await client.ExecuteTaskAsync(request);

            return result;
        }

        public async Task<IRestResponse> RegisterDomainToWhitelist(IMessagingHubSender sender, List<string> urls)
        {
            var pageAccessToken = await GetPageAccessToken(sender);

            if (pageAccessToken.Trim().IsNullOrEmpty())
            {
                throw (new Exception("Could not get PageAccessToken"));
            }

            var client = new RestClient("https://graph.facebook.com/v2.6/me");
            var request = new RestRequest("thread_settings?access_token={PageAccessToken}", Method.POST);
            request.AddUrlSegment("PageAccessToken", pageAccessToken);

            var UrlList = new List<string>();
            foreach (var url in urls)
            {
                UrlList.Add(url);
            }
            var UrlListJson = JsonConvert.SerializeObject(UrlList);

            request.AddParameter("setting_type", "domain_whitelisting");
            request.AddParameter("whitelisted_domains", UrlListJson);
            request.AddParameter("domain_action_type", "add");
            request.AddHeader("Content-Type", "application/json");

            var result = await client.ExecuteTaskAsync(request);

            return result;
        }
    }
}
