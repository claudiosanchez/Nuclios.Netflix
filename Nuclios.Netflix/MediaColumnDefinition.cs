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

    [Register("MediaColumnDefinition")]
    public class MediaColumnDefinition: IGGridViewColumnDefinition
	{

		private readonly string CellIdentifier = "MediaCell";
		
        public MediaColumnDefinition (string key):base(key)
		{
			
		}

		public override IGGridViewCell CreateCell (IGGridView gridView, 
		                                           IGCellPath path, 
		                                           IGGridViewDataSourceHelper dataSource)
		{
			var data = dataSource.ResolveDataObjectForRow (path);

		    var cell = (MediaCell) gridView.DequeueReusableCell (CellIdentifier) ?? new MediaCell (CellIdentifier);

            cell.Data = data as NetflixData;

			return cell;
		}
	}
	
}
