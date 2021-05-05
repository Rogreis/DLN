// -----------------------------------------------------------------------
// <copyright file="Wgs84.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DLN
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Device.Location;


    // http://www.findlatitudeandlongitude.com/


    /// <summary>
    /// World Geodetic System (WGS  latest revision WGS 84 )
    /// </summary>
    public class Wgs84
    {
        public int Graus { get; set; }
        public int Minutos { get; set; }
        public int Segundos { get; set; }

        public double Valor { get; set; }

        //private string latitudePattern = @"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$";
        //private string longitudePattern = @"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$";

        public Wgs84(int graus, int minutos, int segundos)
        {
            Graus = graus;
            Minutos = minutos;
            Segundos = segundos;
            Valor = ConvertDegreeAngleToDouble(graus, minutos, segundos);
            
            GeoCoordinate c = new GeoCoordinate();

        }

        public Wgs84(string ponto)
        {

            Graus = 0;
            Minutos = 0;
            Segundos = 0;


            try
            {
                // Primeiro tenta ver se valor veio em número double
                double val = -999;
                double.TryParse(ponto, out val);
                if (val == -999)
                {
                    FromDouble(val);
                    return;
                }
                

                ponto = ponto.Replace("º", ".").Replace("'", ".").Replace("''", ".").Replace("\"", ".");
                //Exemplo: 17.21.18S

                var multiplier = (ponto.Contains("S") || ponto.Contains("W")) ? -1 : 1; //handle south and west

                ponto = Regex.Replace(ponto, "[^0-9.]", ""); //remove the characters

                var pointArray = ponto.Split('.'); //split the string.

                //Decimal degrees = 
                //   whole number of degrees, 
                //   plus minutes divided by 60, 
                //   plus seconds divided by 3600

                if (pointArray.Length > 0)
                    Graus = int.Parse(pointArray[0]) * multiplier;
                if (Graus > 90)
                    Graus = 0;
                if (pointArray.Length > 1)
                    Minutos = int.Parse(pointArray[1]);
                if (Minutos > 90)
                    Minutos = 0;
                if (pointArray.Length > 2)
                    Segundos = int.Parse(pointArray[2]);
                if (Segundos > 90)
                    Segundos = 0;
                Valor = ConvertDegreeAngleToDouble(Graus, Minutos, Segundos) * multiplier;
            }
            catch
            {
                Graus = 0;
                Minutos = 0;
                Segundos = 0;
                Valor = 0;
            }
        }

        private void FromDouble(double valGraus)
        {
            //ensure the value will fall within the primary range [-180.0..+180.0]
            while (valGraus < -180.0)
                valGraus += 360.0;

            while (valGraus > 180.0)
                valGraus -= 360.0;

            //switch the value to positive
            bool IsNegative = valGraus < 0;
            valGraus = Math.Abs(valGraus);

            //gets the degree
            Graus = (int)Math.Floor(valGraus);
            var delta = valGraus - Graus;

            //gets minutes and seconds
            var seconds = (int)Math.Floor(3600.0 * delta);
            Segundos = seconds % 60;
            Minutos = (int)Math.Floor(seconds / 60.0);
            delta = delta * 3600.0 - seconds;
            Valor = ConvertDegreeAngleToDouble(Graus, Minutos, Segundos) * (IsNegative? -1 : 1);

        }



        private double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600

            return degrees + (minutes / 60) + (seconds / 3600);
        }

        public override string ToString()
        {
            return Graus.ToString("00") + "º " + Minutos.ToString("00") + "' " + Segundos.ToString("00") + "''  " + Valor.ToString();
        }


    }
}
