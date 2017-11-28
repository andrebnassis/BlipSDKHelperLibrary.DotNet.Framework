# BlipSDKHelperLibrary


Nuget Package:

https://www.nuget.org/packages/BlipSDKHelperLibrary/

To install the package:

PM> Install-Package BlipSDKHelperLibrary

---------------

This library is implemented for the following Messaging Channels:

 - Facebook Messenger

# Using Examples:

First of all, you need to instantiate the service.

Code:  
```C#
        private readonly IMessagingHubSender _sender;
        private readonly IFacebookBlipSDKHelper _documentService;
        public PlainTextMessageReceiver(IMessagingHubSender sender)
        {
            _sender = sender;
            _documentService = new FacebookBlipSDKHelper();
        }
```

PS: All these examples accepts an extra/optional argument that sets the Order of the element. But if you want to use, you have to set for all the elements of the specific object.

## 1. Sending Text

### Requirements:  
Text: Obligatory

### Example:  

Code:  
```C#
	 //FACEBOOK
     var document = _documentService.CreateTextDocument("... Inspiração, e um pouco de café! E isso me basta!");
```

Result:  

![alt text](https://image.ibb.co/io6qJ6/Text.png)

## 2. Sending Image

### Requirements:  
UrlImage: Obligatory  
PreviewUrlImage: Obligatory  
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	var document = _documentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalText");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/hRUXWR/Image.png)

## 3. Sending Video

### Requirements:  
UrlVideo: Obligatory  
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	var document = _documentService.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalText");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/bvNxy6/Video.png)


## 4. Sending Web Link

### Requirements:  
Url: Obligatory  
PreviewUrl: Optional
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	var document = _documentService.CreateWebLinkDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/fyG1Qm/Image_With_Link.png)`

## 5. Sending Menu

### Requirements:  
Text: Obligatory  
\#Min Buttons: 1  
\#Max Buttons: unlimited  
PS: Buttons are grouped by 3 when sent.

### Example:

Code:  
 ```C#
	var menu = new MenuModel("ObligatoryText");
    menu.AddTextButton("Text2", "Value2", 2);
    menu.AddTextButton("Text1", "Value1", 1);
    menu.AddTextButton("Text0", "Value0");
    
    var document = _documentService.CreateMenuDocument(menu);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/eWLHy6/Menu.png)`


## 6. Sending QuickReply

### Requirements:  
Text: Obligatory  
\#Min Buttons: 1  
\#Max Buttons: 11

### Example:

Code:  
 ```C#
	var quickreply = new QuickReplyModel("ObligatoryText");
    quickreply.AddLocationButton(2);
    quickreply.AddTextButton("Text1", "Value1", "https://www.iconexperience.com/_img/v_collection_png/256x256/shadow/bullet_ball_red.png", 1);
    quickreply.AddTextButton("😀 Text3", "Value3", null, 2);
    quickreply.AddTextButton("Text0", "Value0");

    var document = _documentService.CreateQuickReplyDocument(quickreply);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/ksbwrR/Quick_Reply.gif)


## 7. Sending Multiple Documents at the same time

### Requirements:  

It follows the dependency of each document that will be sent.

### Example:

Code:  
 ```C#
	var document0 = _documentService.CreateTextDocument("... Inspiração, e um pouco de café! E isso me basta!");
    var document1 = _documentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalText");
    var document2 = _documentService.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalText");
    
    var document = _documentService.CreateCollectionOfDocuments(document1, document2, document0);
    await _sender.SendMessageAsync(document, message.From, cancellationToken);

    ////OR if you want to order the values explicity.
    var document0 = _documentService.CreateTextDocument("... Inspiração, e um pouco de café! E isso me basta!");
    var document1 = _documentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalText");
    var document2 = _documentService.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalText");

    var collection = new GroupDocumentsModel();
    collection.Add(document0, 2);
    collection.Add(document1);
    collection.Add(document2, 1);
    var document = _documentService.CreateCollectionOfDocuments(collection);
    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/gjs85m/Multiple_Docs.png)


## 8. Sending Carousel

### Requirements:  

\#Min Buttons per Card: 0  
\#Max Buttons per Card: 3  
\#Min Cards: 1  
\#Max Cards: 9

### Example:

Code:  
 ```C#
	var carousel = new CarouselModel();
    carousel.AddCard("Title, Subtitle, Image and Buttons", "Image goes up above, Title goes above, Subtitle goes here and button goes below.", "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.GetCard(0).AddLinkButton("Button1: Link", "http://www.w3schools.com/css/img_fjords.jpg", 1);
    carousel.GetCard(0).AddTextButton("Button0: Text", "Value0");
    carousel.GetCard(0).AddShareButton(2);

    carousel.AddCard("Title, Subtitle and Button", "Title goes above, Subtitle goes here and button goes below.",null);
    carousel.GetCard(1).AddTextButton("Button0: Text", "Value0");
    carousel.AddCard("Title, Subtitle and Image", "Image goes up above, Title goes above and Subtitle goes here.", "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.AddCard("Title, Image and Button: Image goes up above, Title goes here and button below", null, "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.GetCard(3).AddLinkButton("Button0: Link", "http://www.w3schools.com/css/img_fjords.jpg");

    carousel.AddCard("Title and Image: Image goes up above, Title goes here", null, "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.AddCard("Title and Subtitle", "Title goes above, Subtitle goes here",null, 10);
    carousel.AddCard("Title and Button: Title goes here, button goes below",null);
    carousel.GetCard(6).AddLinkButton("Button0: Link", "http://www.facebook.com");

    var document = _documentService.CreateCarouselDocument(carousel);
    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/bxOsWR/Carousel_Smaller6.gif)


## 9.0 Prepare to Send List With Url Button

### Requirements:

Useful if it is first time that you sets an URL when you use GetItem.AddWebUrl CreateListDocument's function (item 9.1).

### Example:

Code:  
 ```C#
    var result = await _documentService.RegisterDomainToWhitelist(_sender, "https://url1.com","https://url2.com");
    //OR
    var urlList = new List<string>() { "https://url1.com", "https://url2.com" };
    var result = await _documentService.RegisterDomainToWhitelist(_sender, urlList)
```

## 9.1 Sending List

### Requirements:  

\#Min Items: 2  
\#Max Items: 4  
Title of each Item: Optional  
Subtitle of each Item: Optional

PS: If you use function GetItem.AddWebUrl, see Item 9.0

### Example:

Code:  
 ```C#
	var list = new ListModel();
    list.HighlightFirstItem();
    list.AddBottomTextButton("BottomButton Text", "BottomButtonValue");
    list.AddItem("Text", "Subtitle", "https://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", 1);
    list.AddItem("Text", "Subtitle", null, 5);
    list.AddItem("Text", "Subtitle", "https://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
    list.GetItem(2).AddTextButton("ButtonText", "ButtonValue");
    list.AddItem("Text", "Subtitle", "", 3);
    list.GetItem(3).AddTextButton("ButtonText", "ButtonValue");
    list.GetItem(3).AddWebUrl("https://www.youtube.com/");

    //IMPORTANT:
    //If it is first time that you are using GetItem.AddWebUrl method , dont forget to call RegisterDomainToWhitelist function (Item 9.0), passing the Urls as parameters.

    var document = _documentService.CreateListDocument(list);
    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/k5fHy6/List.png)


## 10. Sending Receipt

### Requirements: 

Under construction

### Example:

Code:
```C#
    var receipt = new ReceiptModel("BRL");

    receipt.AddItem("Classic White T-Shirt", "Single Item Price: 25", 25, 2, "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", 2);
    receipt.GetItem(0).ChangeCurrency("USD"); //Optional
    receipt.GetItem(0).RemoveFromSubtotalCalculus(); //Optional
    receipt.AddItem("Classic White T-Shirt 2 ", "Single Item Price: 1", 1, 10);
    receipt.AddItem("Classic White T-Shirt1", "Single Item Price: 25", 25, 2, "https://img.michaels.com/L6/3/IOGLO/873480063/212543238/10093626_r.jpg", 3);

    receipt.SetOrderedDate(DateTime.UtcNow); //Optional: Default is DateTime.Now
    receipt.SetPaymentMethod("Obligatory"); //Obligatory
    receipt.SetShipInfo("Stephane Crozatier", "1 Hacker Way", "Menlo Park", 94025, "CA", "US", "Optional second address street"); //Obligatory

    receipt.SetOrderId(1); //Optional: default is Random Guid.
    receipt.SetMerchantName("Company Name"); //Optional: Default is Bots Name

    receipt.SetShippingCost(20); //Optional
    receipt.SetTotalTaxCost(30); //Optional


    receipt.AddAdditionalNotes("Discount1", -1, false); //Optional
    receipt.AddAdditionalNotes("Discount2", -2); //Optional
    receipt.AddAdditionalNotes("Discount3", -3, true); //Optional
    receipt.AddAdditionalNotes("Some other stuff", 180); //Optional

    var document = _documentService.CreateReceiptDocument(receipt);

    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result: 
Under construction


## 11. Sending CallButton

### Requirements: 

Under construction

### Example:

Code:
```C#
	var document = _documentService.CreateCallButtonDocument("Initial Text", "Call", "+5531999999999");

    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result: 
Under construction