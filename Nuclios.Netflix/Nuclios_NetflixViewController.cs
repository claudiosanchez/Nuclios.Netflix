//
// Nuclios_NetflixViewController.cs
//
// Author:
//       Claudio Sanchez <claudio@megsoftconsulting.com>
//
// Copyright (c) 2013 Megsoft Consulting Inc
//
//
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Infragistics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;
using System.Collections.Generic;

namespace Nuclios.Netflix
{
		public partial class Nuclios_NetflixViewController : UIViewController
	{
		IGGridView _gridView;
		IGGridViewDataSourceHelper _ds;

		public Nuclios_NetflixViewController () : base ("Nuclios_NetflixViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

	
		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var rect = new RectangleF (0, 0, View.Frame.Size.Width, View.Frame.Size.Height);

			_gridView = new IGGridView (rect, IGGridViewStyle.IGGridViewStyleDefault);

			_gridView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;

			View.AddSubview (_gridView);

			_gridView.RowHeight = 200;
			_gridView.HeaderHeight = 0;
			_gridView.BackgroundColor = UIColor.Black;

			_ds = new IGGridViewDataSourceHelper ();
			_ds.AutoGenerateColumns = true;

			//var column = new IGGridViewImageColumnDefinition("ImageUrl", IGGridViewImageColumnDefinitionPropertyType.IGGridViewImageColumnDefinitionPropertyTypeStringUrl);

			//_ds.ColumnDefinitions.Add (column);

			//_ds.GroupingKey = new NSString("Title");

			var media = await GetData ();

			var dummyMovie = new NetflixMedia () {
				Title="Dummy", 
				ImageUrl ="http://a1.mzstatic.com/us/r1000/120/Video/v4/26/aa/f9/26aaf987-387d-4284-5c53-cf5ffa700b07/GDM1000651.100x100-75.jpg"
			};
		//var one = media [0] as NetflixMedia;

			_ds.Data = media;

	        _gridView.WeakDataSource = _ds;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return true;
		}

		async Task<NSObject[]> GetData ()
		{
			var movies = new List<NSObject> ();
			var url = "https://itunes.apple.com/search?term={0}&media=movie&entity=movie&limit=600&attribute=releaseYearTerm";
			var client = new HttpClient ();

			var response = await client.GetAsync (string.Format (url, "2013"));
			var stringData = await response.Content.ReadAsStringAsync ();
			var json = JsonObject.Parse (stringData);

			Console.WriteLine (stringData);

			var resultCount = json ["resultCount"];
			var results = json["results"];


			foreach(JsonValue movie in results)
			{
				movies.Add (new NetflixMedia(){
					Title = movie["trackName"],
					ImageUrl = movie["artworkUrl100"],
				});

			}
			return movies.ToArray();
		}

	}
}

