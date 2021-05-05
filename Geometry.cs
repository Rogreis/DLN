using System;
using System.Globalization;

namespace DLN
{
	public class Geometry 
	{
		protected CultureInfo _formatUS = new CultureInfo("en-US");

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public Geometry(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public Geometry(string googlePointString)
		{
			if (googlePointString != null )
			{
				char[] separators = { '(', ')', ',' };
				string[] parts = googlePointString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				Latitude = double.Parse(parts[0], _formatUS);
				Longitude = double.Parse(parts[1], _formatUS);
			}
			// (51.5073509, -0.12775829999998223)
		}




		public string FormatWithoutParentesis()
		{
			return string.Format(_formatUS, "{0},{1}", Latitude, Longitude);
		}

		public string LatLngLiteral()
		{
			// {lat: -34, lng: 151}
			return string.Format(_formatUS, "{{lat: {0}, lng:{1}}}", Latitude, Longitude);
		}


		public override string ToString()
		{
			return string.Format("( {0},{1} )", Latitude, Longitude);
		}


	}
}
