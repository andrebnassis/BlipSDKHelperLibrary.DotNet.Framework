# BlipSDKHelperLibrary


Nuget Package:

https://www.nuget.org/packages/BlipSDKHelperLibrary/

To install the package:

PM> Install-Package BlipSDKHelperLibrary

---------------

# Using Examples:

PS: All these examples accepts an extra/optional argument that sets the Order of the element. But if you want to use, you have to set for all the elements of the specific object.

## 1. Sending Text

### Requirements:  
Text: Obligatory

### Example:  

Code:  
```C#
	var document = BlipSDKHelper.CreateTextDocument("... Inspiração, e um pouco de café! E isso me basta!");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
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
	var document = BlipSDKHelper.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
	
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
	var document = BlipSDKHelper.CreateVideoDocument("https://dl.dropboxusercontent.com/s/jxy3sspxbl6ilan/John%20Lennon%20-%20Imagine.mp4", "OptionalTitle", "OptionalSubtitle");
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/bvNxy6/Video.png)


## 4. Sending Weblink(Image with link)

### Requirements:  
UrlImage: Obligatory  
Url: Obligatory  
Title: Optional  
Subtitle: Optional

### Example:

Code:  
 ```C#
	var document = BlipSDKHelper.CreateImageWithLinkDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle","OptionalSubtitle");
	
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
	var menu = new MenuModel("Escolha uma opção:");
	menu.AddDefaultButton("Botão1", "Value1");
	menu.AddDefaultButton("Botão2", "Value2");
	menu.AddDefaultButton("Botão3", "Value3");
	var document = BlipSDKHelper.CreateMenuDocument(menu);
	
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
	var quickreply = new QuickReplyModel("Escolha uma opção:");
	quickreply.AddDefaultButton("Botão1", "Value1");
	quickreply.AddDefaultButton("Botão2", "Value2");
	quickreply.AddDefaultButton("Botão3", "Value3");
	quickreply.AddDefaultButton("Botão4", "Value4");
	quickreply.AddDefaultButton("Botão5", "Value5");
	quickreply.AddDefaultButton("Botão6", "Value6");
	quickreply.AddDefaultButton("Botão7", "Value7");
	quickreply.AddDefaultButton("Botão8", "Value8");
	quickreply.AddDefaultButton("Botão9", "Value9");
	quickreply.AddDefaultButton("Botão10", "Value10");
	quickreply.AddLocationButton();
	var document = BlipSDKHelperLibrary.BlipSDKHelper.CreateQuickReplyDocument(quickreply);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/ksbwrR/Quick_Reply.gif)

## 7. Sending Carousel

### Requirements:  

\#Min Buttons per Card: 0  
\#Max Buttons per Card: 3  
\#Min Cards: 1  
\#Max Cards: 10

### Example:

Code:  
 ```C#
	var carousel = new CarouselModel();
	carousel.AddCard("Title, Subtitle, Image and Buttons", "Image goes up above, Title goes above, Subtitle goes here and button goes below.", "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.GetCard(0).AddDefaultButton("First button: Text", "Value of FirstButton");
    carousel.GetCard(0).AddLinkButton("Second button: Link", "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.GetCard(0).AddShareButton();

    carousel.AddCard("Title, Subtitle and Button", "Title goes above, Subtitle goes here and button goes below.", null);
    carousel.GetCard(1).AddDefaultButton("Another button: Text", "Some other value of FirstButton");

    carousel.AddCard("Title, Subtitle and Image", "Image goes up above, Title goes above and Subtitle goes here.", "http://www.w3schools.com/css/img_fjords.jpg");

    carousel.AddCard("Title, Image and Button: Image goes up above, Title goes here and button below", null, "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.GetCard(3).AddDefaultButton("Text Button", "Value goes here");

    carousel.AddCard("Title and Image: Image goes up above, Title goes here",  null, "http://www.w3schools.com/css/img_fjords.jpg");

    carousel.AddCard("Title and Subtitle", "Title goes above, Subtitle goes here", null);

    carousel.AddCard("Title and Button: Title goes here, button goes below", null, null);
    carousel.GetCard(6).AddLinkButton("Redirect to link", "http://www.w3schools.com/css/img_fjords.jpg");
    carousel.GetCard(6).AddLinkButton("Redirect to link", "http://www.facebook.com");

    var document = BlipSDKHelper.CreateCarouselDocument(carousel);

    await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/bxOsWR/Carousel_Smaller6.gif)


## 8. Sending List

### Requirements:  

\#Min Items: 2  
\#Max Items: 4  
Title of each Item: Optional  
Subtitle of each Item: Optional

### Example:

Code:  
 ```C#
	var listModel = new ListModel();
	
	listModel.AddItem("Titulo0", "Subtitulo0", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
	listModel.AddItem("Titulo1", "Subtitulo1", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
	listModel.AddItem("Titulo2", "Subtitulo2", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
	listModel.AddItem("Titulo3", "Subtitulo3", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg", "http://www.jqueryscript.net/images/Simplest-Responsive-jQuery-Image-Lightbox-Plugin-simple-lightbox.jpg");
	
	var document = BlipSDKHelperLibrary.BlipSDKHelper.CreateListDocument(listModel);
	
	await _sender.SendMessageAsync(document, message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/k5fHy6/List.png)


## 9. Sending Multiple Documents at the same time

### Requirements:  

It follows the dependency of each document that will be sent.

### Example:

Code:  
 ```C#
	var document1 = BlipSDKHelper.CreateTextDocument("... Inspiração, e um pouco de café! E isso me basta!");
	
	var document2 = BlipSDKHelper.CreateImageDocument("https://dl.dropboxusercontent.com/s/99sw7vu788suww1/imagineFloor.jpg", "https://dl.dropboxusercontent.com/s/0u34yn7pj29ak1v/imagineFloorPreview.jpg", "OptionalTitle", "OptionalSubtitle");
	
	var groupDocs = BlipSDKHelper.CreateCollectionOfDocuments(document1, document2);
	
	//Or
	//var group = new GroupDocumentsModel();
	//group.Add(document1);
	//group.Add(document2);
	//var groupDocs = BlipSDKHelperLibrary.BlipSDKHelper.CreateCollectionOfDocuments(group);
	
	await _sender.SendMessageAsync(groupDocs , message.From, cancellationToken);
```

Result:  
![alt text](https://image.ibb.co/gjs85m/Multiple_Docs.png)