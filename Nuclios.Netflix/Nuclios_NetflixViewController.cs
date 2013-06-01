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
using System.Linq;

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
			_ds.AutoGenerateColumns = false;

			var column = new MediaColumnDefinition("Media");

			_ds.ColumnDefinitions.Add (column);

			//_ds.GroupingKey = "Category";

			var media = await GetData ();

			var results = media
				.GroupBy (m=> m.Genre, 
			           (key, g) => new NetflixData { Category = key, Media = g.ToList ().ToArray() })
					.ToArray ();

			_ds.Data = results;

	        _gridView.WeakDataSource = _ds;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return true;
		}

		static NetflixMedia MapNetflixMedia (JsonValue movie)
		{
			return new NetflixMedia () {
				Title = movie ["trackName"],
				ImageUrl = movie ["artworkUrl100"],
				Genre = movie ["primaryGenreName"],
			};
		}

		async Task<IList<NetflixMedia>> GetData ()
		{
			var movies = new List<NetflixMedia> ();
			var url = "https://itunes.apple.com/search?term={0}&media=movie&entity=movie&limit=600&attribute=releaseYearTerm";

			var client = new HttpClient ();

			var response = await client.GetAsync (string.Format (url, "2013"));
			var stringData = await response.Content.ReadAsStringAsync ();
			var json = JsonObject.Parse (stringData);

			var results = json["results"];

			foreach(JsonValue movie in results)
			{
				movies.Add (MapNetflixMedia (movie));

			}
			return movies;
		}

	}
}

