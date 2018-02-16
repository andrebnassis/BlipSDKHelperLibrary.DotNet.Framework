# BlipSDKHelperLibrary


Nuget Package:

https://www.nuget.org/packages/BlipSDKHelperLibrary/

To install the package:

PM> Install-Package BlipSDKHelperLibrary

---------------

This library is implemented for the following Messaging Channels:

 - Facebook Messenger  
 - BLiPChat  

# Using Examples:

First of all, you need to instantiate the service.

Code:  
```C#
        private readonly IMessagingHubSender _sender;
        private readonly IFacebookBlipSDKHelper _facebookDocumentService;
		private readonly IBlipChatBlipSDKHelper _blipchatDocumentService;
        public PlainTextMessageReceiver(IMessagingHubSender sender)
        {
            _sender = sender;
            _facebookDocumentService = new FacebookBlipSDKHelper();
			_blipchatDocumentService = new BlipChatBlipSDKHelper();
        }
```

PS: Almost all these examples accepts an extra/optional argument that sets the Order of the element (it does not work for ListModel). But if you want to use, you have to set for all the elements of the specific object.

## 1. Sending Text

### Channels:  
Facebook Messenger  
BLiPChat  

### Requirements:  
Text: Obligatory

### Example:  

Code:  
 ```C#
	//FACEBOOK
	var document = _facebookDocumentService.CreateTextDocument("Sending a simple text");
	//BLIPCHAT
    var document = _blipchatDocumentService.CreateTextDocument("Sending a simple text");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  

![alt text](https://image.ibb.co/hjUJzR/01_Plain_Text.png)

## 2. Sending Image

### Channels:  
Facebook Messenger  
BLiPChat  

### Requirements:  
UrlImage: Obligatory  
PreviewUrlImage: Obligatory  
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	//FACEBOOK
    var document = _facebookDocumentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
	//BLIPCHAT
    var document = _blipchatDocumentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/hRUXWR/Image.png)

## 3. Sending Video

### Channels:  
Facebook Messenger  

### Requirements:  
UrlVideo: Obligatory  
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	//FACEBOOK
	var document = _facebookDocumentService.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalSubtitle");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/hrn7R6/02_Video.png)


## 4. Sending Web Link

### Channels:  
Facebook Messenger  
BLiPChat  

### Requirements:  
Url: Obligatory  
PreviewUrl: Optional  
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	//FACEBOOK
    var document = _facebookDocumentService.CreateWebLinkDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
    //BLIPCHAT
    var document = _blipchatDocumentService.CreateWebLinkDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/fyG1Qm/Image_With_Link.png)`

## 5. Sending Menu

### Channels:  
Facebook Messenger  
BLiPChat  

### Requirements:  
Text: Obligatory  
\#Min Buttons: 1  
\#Max Buttons: unlimited  
PS: On Facebook Messenger Channel, buttons are grouped by 3 when sent.

### Example:

Code:  
 ```C#
	var menu = new MenuModel("Choose an option:");
    menu.AddTextButton("Button2", "Value2", 2);
    menu.AddTextButton("Button1", "Value1", 1);
    menu.AddTextButton("Button0", "Value0");
    
    //FACEBOOK
    var document = _facebookDocumentService.CreateMenuDocument(menu);
    //BLIPCHAT
    var document = _blipchatDocumentService.CreateMenuDocument(menu);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/ns2G3m/09_Menu.png)`


## 6. Sending QuickReply

### Channels:  
Facebook Messenger  
BLiPChat  

### Requirements:  
Text: Obligatory  
\#Min Buttons: 1  
\#Max Buttons: 11  
PS: On BLiPChat, if you add a LocationButton on QuickReply, it will ignore the others text buttons and will show only the LocationButton.  

### Example:

Code:  
 ```C#
	var quickreply = new QuickReplyModel("Choose an option:");
    quickreply.AddLocationButton(10);
    quickreply.AddTextButton("Button1", "Value1", "https://www.iconexperience.com/_img/v_collection_png/256x256/shadow/bullet_ball_red.png", 1);
    quickreply.AddTextButton("😀 Button2", "Value2", null, 2);
    quickreply.AddTextButton("Button0", "Value0");
	quickreply.AddTextButton("Button3", "Value3", null, 3);
	quickreply.AddTextButton("Button4", "Value4", null, 4);
	quickreply.AddTextButton("Button5", "Value5", null, 5);
	quickreply.AddTextButton("Button6", "Value6", null, 6);
	quickreply.AddTextButton("Button7", "Value7", null, 7);
	quickreply.AddTextButton("Button8", "Value8", null, 8);
	quickreply.AddTextButton("Button9", "Value9", null, 9);

    //FACEBOOK
    var document = _facebookDocumentService.CreateQuickReplyDocument(quickreply);
    //BLIPCHAT
    var document = _blipchatDocumentService.CreateQuickReplyDocument(quickreply);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/bXvBm6/03_Quick_Reply.gif)


## 7. Sending Multiple Documents at the same time

### Channels:  
Facebook Messenger  
BLiPChat  

### Requirements:  
It follows the dependency of each document that will be sent.

### Example:

Code:  
 ```C#
	//FACEBOOK
    var document0 = _facebookDocumentService.CreateTextDocument("Sending a simple text");
    var document1 = _facebookDocumentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
    var document2 = _facebookDocumentService.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalSubtitle");
    
	var document = _facebookDocumentService.CreateCollectionOfDocuments(document1, document2, document0);

    //BLIPCHAT
    var document0 = _blipchatDocumentService.CreateTextDocument("Sending a simple text");
    var document1 = _blipchatDocumentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
    
	var document = _blipchatDocumentService.CreateCollectionOfDocuments(document1, document0);

    await _sender.SendMessageAsync(document, message.From, cancellationToken);
	

    ////OR if you want to order the values explicity.
    //FACEBOOK
    var document0 = _facebookDocumentService.CreateTextDocument("Sending a simple text");
    var document1 = _facebookDocumentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
    var document2 = _facebookDocumentService.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalSubtitle");
    var collection = new GroupDocumentsModel();
    collection.Add(document0, 2);
    collection.Add(document1);
    collection.Add(document2, 1);
	
	var document = _facebookDocumentService.CreateCollectionOfDocuments(collection);

    //BLIPCHAT
    var document0 = _blipchatDocumentService.CreateTextDocument("Sending a simple text");
    var document1 = _blipchatDocumentService.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
    var collection = new GroupDocumentsModel();
    collection.Add(document0, 2);
    collection.Add(document1);
	
	var document = _blipchatDocumentService.CreateCollectionOfDocuments(collection);
	
    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/bFpqDm/04_Multiple_Documents.png)


## 8. Sending Carousel

### Channels:  
Facebook Messenger  
BLiPChat  

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

    //FACEBOOK
    var document = _facebookDocumentService.CreateCarouselDocument(carousel);
    //BLIPCHAT
    var document = _blipchatDocumentService.CreateCarouselDocument(carousel);
	
    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/gDiNR6/05_Carousel.gif)


## 9.0 Prepare to Send List With WebUrl on Item

### Channels:  
Facebook Messenger  

### Requirements:  
Useful if it is first time that you sets an URL when you use GetItem.AddWebUrl CreateListDocument's function (item 9.1).

### Example:

Code:  
 ```C#
	//FACEBOOK
    var result = await _facebookDocumentService.RegisterDomainToWhitelist(_sender, "https://url1.com","https://url2.com");
    //OR
    var urlList = new List<string>() { "https://url1.com", "https://url2.com" };
    var result = await _facebookDocumentService.RegisterDomainToWhitelist(_sender, urlList)
```

## 9.1 Sending List

### Channels:  
Facebook Messenger  

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
    list.AddItem("Title1", "Subtitle1", "https://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", 1);
    list.AddItem("Title3", "Subtitle3", null, 3);
    list.AddItem("Title0", "Subtitle0", "https://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
    list.GetItem(2).AddTextButton("ButtonText0", "ButtonValue0");
    list.AddItem("Title2", "Subtitle2", "", 2);
    list.GetItem(3).AddTextButton("ButtonText2", "ButtonValue2");
    list.GetItem(3).AddWebUrl("https://www.youtube.com/");

    //IMPORTANT(only for List Creation case): 
    //If it is first time that you are using GetItem.AddWebUrl method, dont forget to call RegisterDomainToWhitelist function, passing the Urls as parameters.

	//FACEBOOK
    var document = _facebookDocumentService.CreateListDocument(list);
    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/hGLYzR/06_List.png)


## 10. Sending Receipt

### Channels:  
Facebook Messenger  

### Requirements:  
Currency: Obligatory  
Payment Method: Obligatory  
Ship Info: Obligatory

### Example:

Code:
```C#
    var receipt = new ReceiptModel("BRL");

    receipt.AddItem("Classic White T-Shirt0", "Single Item Price: 25", 25, 2, "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
    receipt.GetItem(0).ChangeCurrency("USD"); //Optional
    receipt.GetItem(0).RemoveFromSubtotalCalculus(); //Optional
    receipt.AddItem("Classic White T-Shirt 2 ", "Single Item Price: 1", 1, 10, null);
    receipt.AddItem("Classic White T-Shirt1", "Single Item Price: 25", 25, 2, "https://img.michaels.com/L6/3/IOGLO/873480063/212543238/10093626_r.jpg");

    receipt.SetOrderedDate(DateTime.Now); //Optional: Default is DateTime.Now
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

	//FACEBOOK
    var document = _facebookDocumentService.CreateReceiptDocument(receipt);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:   
![alt text](https://image.ibb.co/ivBdYm/07_Receipt.gif)


## 11. Sending CallButton

### Channels:  
Facebook Messenger  

### Requirements:  
It is not working properly yet. Needs to investigate.

### Example:

Code:
```C#
	//FACEBOOK
    var document = _facebookDocumentService.CreateCallButtonDocument("Initial Text", "Call", "+5531999999999");

    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:   
![alt text](https://image.ibb.co/m7ovw6/08_Call_Button.png)