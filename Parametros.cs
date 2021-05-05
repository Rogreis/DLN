// -----------------------------------------------------------------------
// <copyright file="Parametros.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DLN
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Parametros
    {
        public DateTime UltimaDataInicial= DateTime.MinValue;
        public DateTime UltimaDataFinal = DateTime.MinValue;
        public string LastLocal = "";

        public int StartTime = 8;
        public int EndTime = 18;
        public double LatitudeCenterMap = 51.5073509;
        public double LongitudeCenterMap = -0.1277583;
        public double Azimuth = 0;
        public double PointPAngle = 0;
        public double PointPAzimuth = 0;


        public string ZoomMap = "12";

    }
}
