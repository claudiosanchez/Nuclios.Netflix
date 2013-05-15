//
// MediaCell.cs
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

namespace Nuclios.Netflix
{

	public class MediaCell:IGGridViewCell
	{
		IGGridView _gridView;
		IGGridViewSingleRowSingleFieldDataSourceHelper _ds;

		public NetflixData Data { get; set; }

		public MediaCell ()
		{

			//SetValueForKey (new NSString("MEDIA_CELL"), new NSString ("ReuseIdentifier")); 
			//SetValueForKey (new NSString("MEDIA_CELL"), "ReuseIdentifier");
			SetValueForUndefinedKey (new NSString("MEDIA_CELL"), "ReuseIdentifier");

			_gridView = new IGGridView ();
			_gridView.AllowHorizontalBounce = true;
			_gridView.AlwaysBounceVertical = false; 
			_gridView.AlwaysBounceHorizontal = true;
			_gridView.HeaderHeight = 0;
			_gridView.RowSeparatorHeight = 0;
			_gridView.RowHeight = 200;
			_gridView.ColumnWidth = IGColumnWidth.CreateNumericColumnWidth (150f);
			AddSubview (_gridView);

			var col = new IGGridViewImageColumnDefinition ("ImageUrl", 
			                                               IGGridViewImageColumnDefinitionPropertyType
			                                               .IGGridViewImageColumnDefinitionPropertyTypeStringUrl);
			col.CacheImages = true;
			_ds = new IGGridViewSingleRowSingleFieldDataSourceHelper (col);

			_gridView.WeakDataSource = _ds;
		}

		public override void CellAttached ()
		{
			if (_ds == null)
				return;

			_ds.Data = Data.Media;
			_gridView.UpdateData ();

			_gridView.ContentOffset = new PointF (Data.ScrollPosition, 0);

			}

		public override void CellDetached ()
		{
			Data.ScrollPosition = _gridView.ContentOffset.X;
			_ds.Data = null;
			_ds.InvalidateData ();
			_gridView.UpdateData ();
			Data = null;
		}

		public override void SetupSize (SizeF size)
		{
			_gridView.Frame = new RectangleF (0, 0, size.Width, size.Height);
		}
	}
	
}
