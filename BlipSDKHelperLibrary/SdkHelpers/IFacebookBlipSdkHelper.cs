using System.Collections.Generic;
using System.Threading.Tasks;
using BlipSDKHelperLibrary.Models;
using Lime.Messaging.Contents;
using Lime.Protocol;
using RestSharp;
using Takenet.MessagingHub.Client.Sender;
using BlipSDKHelperLibrary.Interfaces;

namespace BlipSDKHelperLibrary
{
    public interface IFacebookBlipSDKHelper :
    ICallButtonCreation,
    ICarouselCreation,
    ICollectionCreation,
    IImageCreation,
    IListCreation,
    IMenuCreation,
    IPlainTextCreation,
    IQuickreplyCreation,
    IReceiptCreation,
    IVideoCreation,
    IWebLinkCreation
    {
        Task<IRestResponse> RegisterDomainToWhitelist(IMessagingHubSender sender, params string[] urls);
        Task<IRestResponse> RegisterDomainToWhitelist(IMessagingHubSender sender, List<string> urls);
    }
}