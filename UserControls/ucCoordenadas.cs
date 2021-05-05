using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Device.Location;
using System.Text.RegularExpressions;
using CoordinateSharp;

namespace DLN
{
    public partial class ucCoordenadas : UserControl
    {
        public ucCoordenadas()
        {
            InitializeComponent();
        }

        public double ConvertDegreeAngleToDouble(string point)
        {
            //Example: 17.21.18S

            var multiplier = (point.Contains("S") || point.Contains("W")) ? -1 : 1; //handle south and west

            point = Regex.Replace(point, "[^0-9.]", ""); //remove the characters

            var pointArray = point.Split('.'); //split the string.

            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600

            var degrees = Double.Parse(pointArray[0]);
            var minutes = Double.Parse(pointArray[1]) / 60;
            var seconds = Double.Parse(pointArray[2]) / 3600;
            return (degrees + minutes + seconds) * multiplier;
        }

        public double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
        }


        private void Teste()
        {
            GeoCoordinate geo = new GeoCoordinate();
            Coordinate c = new Coordinate(40.465, -75.089);

            Coordinate.TryParse("23° 8' 50.5464'' S", out c);


            //Display DMS Format
            c.FormatOptions.Format = CoordinateFormatType.Degree_Minutes_Seconds;
            c.ToString();//N 40º 27' 54" W 75º 5' 20.4"
            c.Latitude.ToString();//N 40º 27' 54"
            c.Latitude.ToDouble();//40.465


        }

    }
}
