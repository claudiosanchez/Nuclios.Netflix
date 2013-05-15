Nuclios.Netflix
===============
A Xamarin.iOS version of the XCode-based sample "Creating a Netflix style iOS Grid" by [Stephen Zaharuk](http://www.infragistics.com/community/blogs/stevez/archive/2012/11/13/creating-a-netflix-style-ios-grid.aspx).

While checking out my usual late afternoon reading stream (using Flipboard), I found this great article on how to put together a very sexy UI utilizing [Infragistics's iOS Controls](http://www.infragistics.com/products/ios/); it instantly occurred to me that this would be a great idea for an article. It was almost muscle memory, because I found my hands spinning a browser window open to my favorite Search engine -[ Bing.com ](http://bing.com "Shameless plug")- and typing the keywords***"Nuclios Monotouch Bindings"***.  To my surprise the product already ships with Xamarin.iOS bindings out of the box.

> Innovative Functionality Meets iOS Familiarity
> 
> Take advantage of a powerful API that is familiar to all iOS developers. You can use NucliOS controls in native iOS projects built with Objective C and Xcode. You can even build in C#; with Xamarin.ioS support, NucliOS includes final MonoTouch bindings for all of our iOS controls. 

And so the journey begins. Steve has done a great job of explaining in detail the implementation, so I won't bore you with the details. Instead I will highlight what things to keep in mind when "translating" code developed with XCode and Obj-C to the stuff dreams are made of (C# and Xamarin.iOS). 
    
    // Obj-C Implementation of the model 
    
    @interface NetflixData : NSObject
           @property (nonatomic, retain)NSString* category;
           @property (nonatomic, retain)NSMutableArray* media;
           @property (nonatomic, assign)CGFloat scrollPosition;
    @end 


   1-Remember to add the `[Export()]` Attribute to your properties in your `NSObject` derived classes. This will allow the under-laying native implementation to use the properties of your Data Model.


    public class NetflixMedia: NSObject
    	{
    		[Export("ImageUrl")]
    		public string ImageUrl {
    			get;
    			set;
    		}
        
    		[Export("Title")]
    		public string Title {
    			get;
    			set;
    		}
    
			[Export("Genre")]
    		public string Genre {
    			get;
    			set;
    		}
      	}
  
2 - Use the WeakDelagate property WeakDataSource
 
     //NucliosViewController.cs  - Obj-C
     _gridView.WeakDataSource = _ds;
	...

Instead of..

    //NucliosViewController.m  - Obj-C
    _gridView.dataSource = _ds;
    ...

3- When creating creating overloaded constructors, remember to extract any initialization code to a separate method, so you can call it (and don't try to code on an empty stomach). 
    
	public class MediaCell:IGGridViewCell
	{
		public MediaCell (string identifier): base(identifier)
		{
			Init ();
		}

4- This is not a required thing, but If you find that it just doesn't make sense to re-rewrite:



![GridView](Screenshots/6332.IMG_0250.PNG)

## Netflix UI on iPad ##

![Netflix ](Screenshots/2350.netflixSS.PNG)