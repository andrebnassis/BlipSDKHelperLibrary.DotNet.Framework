using BlipSDKHelperLibrary.Models;
using Lime.Messaging.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Interfaces
{
    public interface IMenuCreation
    {
        Select CreateMenuDocument(MenuModel menuModel);
    }
}
