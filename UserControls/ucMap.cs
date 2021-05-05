using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Security.Permissions;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace DLN
{
    public delegate void dlPontoMapa(double latitude, double longitude);

    /// <summary>
    /// Maps grom <see href="https://github.com/judero01col/GMap.NET/wiki"/>
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class ucMap : UserControl
    {
  
        public event dlPontoMapa PontoMapa = null;

        protected CultureInfo _formatUS = new CultureInfo("en-US");

        GMap.NET.WindowsForms.GMapControl gMapsControl = new GMap.NET.WindowsForms.GMapControl();
        GMapMarker currentMarker= null;
        readonly GMapOverlay top = new GMapOverlay();

        public ucMap()
        {
            InitializeComponent();
            if (!GMapControl.IsDesignerHosted)
            {
                gMapsControl.Dock = DockStyle.Fill;
                this.Controls.Add(gMapsControl);
                gMapsControl.OnPositionChanged += GMapsControl_OnPositionChanged;

                gMapsControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance; // .BingMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
                //gMapsControl. .SetCurrentPositionByKeywords("Maputo, Mozambique");
                //gMapsControl.

                //PointLatLng point = new PointLatLng(-25.966688, 32.580528);

                //get tiles from server only
                gMapsControl.Manager.Mode = AccessMode.ServerOnly;
                //not use proxy
                //GMapProvider.WebProxy = null;
                //center map on moscow
               //gMapsControl.SetPositionByKeywords("Paris, France");

                //zoom min/max; default both = 2
                gMapsControl.MinZoom = 1;
                gMapsControl.MaxZoom = 20;
                //set zoom
                gMapsControl.Zoom = 10;

                gMapsControl.CanDragMap = true;
                gMapsControl.MarkersEnabled = true;
                gMapsControl.ShowCenter = false;
                gMapsControl.DragButton = MouseButtons.Left;


 
                //GMapOverlay markersOverlay = new GMapOverlay("markers");
                //GMarkerGoogle marker = new GMarkerGoogle(point, GMarkerGoogleType.green);
                //markersOverlay.Markers.Add(marker);
                //gMapsControl.Overlays.Add(markersOverlay);


            }
        }

        /// <summary>
        /// Set the current map position
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public void SetPosition(double lat, double lng)
        {
            PointLatLng point = new PointLatLng(lat, lng);
            gMapsControl.Position = point;
            // set current marker
            if (currentMarker == null)
            currentMarker = new GMarkerGoogle(point, GMarkerGoogleType.arrow);
            currentMarker.IsHitTestVisible = false;
            top.Markers.Add(currentMarker);
          }


        private void GMapsControl_OnPositionChanged(PointLatLng point)
        {
            //PontoMapa?.Invoke(point.Lat, point.Lng);
            gMapsControl.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(point, GMarkerGoogleType.green);
            markersOverlay.Markers.Add(marker);
            gMapsControl.Overlays.Add(markersOverlay);

        }

        private string CodeToShowGoogleMaps()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("  <head>");
            sb.AppendLine("    <title>Simple click event</title>");
            sb.AppendLine("    <meta name=\"viewport\" content=\"initial-scale=1.0, user-scalable=no\">");
            sb.AppendLine("    <meta charset=\"utf-8\">");
            sb.AppendLine("    <style>");
            sb.AppendLine("      html, body, #map-canvas {");
            sb.AppendLine("        height: 100%;");
            sb.AppendLine("        margin: 0px;");
            sb.AppendLine("        padding: 0px");
            sb.AppendLine("      }");
            sb.AppendLine("    </style>");
            sb.AppendLine("    <script src=\"https://maps.googleapis.com/maps/api/js?v=3.exp\"></script>");

            sb.AppendLine("    <script type=\"text/javascript\" src=\"https://maps.googleapis.com/maps/api/js?libraries=drawing\"></script>");

            string UrlGoogleJs = "file:///" + Path.Combine(Application.StartupPath, @"JavaScript\drawingManager.js");
            sb.AppendLine("    <script src=\"" + UrlGoogleJs + "\"></script>");

            string Jquery = "file:///" + Path.Combine(Application.StartupPath, @"JavaScript\jquery-1.11.1.min.js");
            sb.AppendLine("    <script src=\"" + Jquery + "\"></script>");

            // Used for labels
            string UrlInfoBox = "file:///" + Path.Combine(Application.StartupPath, @"JavaScript\infobox.js");
            sb.AppendLine("    <script src=\"" + UrlInfoBox + "\"></script>");


            sb.AppendLine("  </head>");
            sb.AppendLine("  <body>");
            sb.AppendLine("    <div id=\"map-canvas\"></div>");
            sb.AppendLine("  </body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

        public string GetLatitude()
        {
            PontoMapa?.Invoke(Program.objParametros.LatitudeCenterMap, Program.objParametros.LongitudeCenterMap);
            return Program.objParametros.LatitudeCenterMap.ToString(_formatUS);
        }
        public string GetLongitude()
        {
            return Program.objParametros.LongitudeCenterMap.ToString(_formatUS);
        }
        public string GetZoom()
        {
            return Program.objParametros.ZoomMap;
        }


        public void ObtemCentroMapa()
        {
            //string mapCenter = (string)webBrowserGoogle.Document.InvokeScript("getMapCenter", new String[] { "" });
            //Geometry mapCenterPT = new Geometry(mapCenter);
            //Program.objParametros.LatitudeCenterMap = mapCenterPT.Latitude;
            //Program.objParametros.LongitudeCenterMap = mapCenterPT.Longitude;
            //Program.objParametros.ZoomMap = (string)webBrowserGoogle.Document.InvokeScript("getMapZoom", new String[] { "" });
        }


        public void PontoCriado(string lat, string lng)
        {
            try
            {
                double latitude = Convert.ToDouble(lat, _formatUS);
                double longitude = Convert.ToDouble(lng, _formatUS);
                PontoMapa?.Invoke(latitude, longitude);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Inicia()
        {
            //webBrowserGoogle.AllowWebBrowserDrop = false;
            //webBrowserGoogle.IsWebBrowserContextMenuEnabled = false;
            //webBrowserGoogle.ScriptErrorsSuppressed = true;
            //webBrowserGoogle.WebBrowserShortcutsEnabled = false;
            //webBrowserGoogle.ObjectForScripting = this;

            //// Inicializa o map da Google
            //webBrowserGoogle.DocumentText = CodeToShowGoogleMaps();
        }

        private void UcGoogleMap_Load(object sender, EventArgs e)
        {
 


        }
    }
}
