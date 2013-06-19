//
// NetflixData.cs
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
    [Register("NetflixData")]
	public class NetflixData: NSObject
	{
	    [Export("Category")]
		public string Category { get; set; }

		[Export("Media")]
		public NetflixMedia[] Media { get; set;}

		public float ScrollPosition;
	}
}
