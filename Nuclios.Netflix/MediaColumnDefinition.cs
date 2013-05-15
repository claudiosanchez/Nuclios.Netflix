//
// MediaColumnDefinition.cs
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

	public class MediaColumnDefinition: IGGridViewColumnDefinition
	{
		public MediaColumnDefinition (string key):base(key)
		{
			
		}

		public override IGGridViewCell CreateCell (IGGridView gridView, 
		                                           IGCellPath path, 
		                                           IGGridViewDataSourceHelper dataSource)
		{
			var data = dataSource.ResolveDataObjectForRow (path);
			MediaCell cell = (gridView.DequeueReusableCell ("MEDIA_CELL")as MediaCell);

			if (cell == null)
				cell = new MediaCell ("MEDIA_CELL");

			cell.Data = data as NetflixData;

			return cell;
		}
	}
	
}
