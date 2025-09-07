using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.Core.Utility;
using MarkerType = GoogleMap.SDK.Contract.Commons.Enums.GMarkerGoogleType;
using GoogleMarkerType = GMap.NET.WindowsForms.Markers.GMarkerGoogleType;
using GoogleMap.SDK.Contract.Utility;
using System.Drawing;


namespace GooleMap.SDK.UI.Winform.MapOverlays
{
    public class MapOverlay : GMapOverlay, IOverlayNew
    {
        string IOverlayNew.Id 
        {
            get => this.Id;
            set => this.Id = value;
        }

        public void ClearAll()
        {
            this.Routes.Clear();
            this.Markers.Clear();
        }

        public void ClearMarkers()
        {
            this.Markers.Clear();
        }

        public void ClearRoutes()
        {
            this.Routes.Clear();
        }
        public void DeleteMarkerElement(object element)
        {
            if (element is List<Location> locations)
            {
                foreach (var location in locations)
                {
                    var marker = Markers.FirstOrDefault(x => x.Position == new PointLatLng(location.latLng.latitude, location.latLng.longitude));
                    if (marker != null)
                    {
                        Markers.Remove(marker);
                    }
                }
            }
        }
        public void DeleteRouteElement(object element)
        {
            if (element is IEnumerable<List<Latlng>> routes)
            {
                foreach (var route in routes)
                {
                    var routeName = PolylineEncoder.EncodeCoordinates(route);
                    var existedRoute = Routes.FirstOrDefault(x => x.Name == routeName);
                    Routes.Remove(existedRoute);
                }
            }
        }
        public void SetMarkerOverLay(IEnumerable<Location> locations, MarkerType markerType = MarkerType.red_dot, object toolTip = null)
        {
            GMapToolTip gMapToolTip = toolTip as GMapToolTip;
            int markerIndex = 0;
            foreach (var location in locations)
            {
                GMapMarker marker = AddMarker(location.latLng.latitude, location.latLng.longitude, markerType, gMapToolTip);

                this.Markers.Add(marker);
                marker.Tag = markerIndex++;
            }
        }
        public void SetRouteOverLay(IEnumerable<List<Latlng>> routePoints)
        {
            Color[] colors = { Color.Blue, Color.Green, Color.Black, Color.Yellow, Color.Red };
            foreach (var routePoint in routePoints)
            {
                //var convertedRoutePoint = routePoint.Select(x => new PointLatLng(x.latitude, x.longitude));
                var routeName = PolylineEncoder.EncodeCoordinates(routePoint);
                var convertedPoints = routePoint.Select(x => new PointLatLng(x.latitude, x.longitude));
                GMapRoute polygon = new GMapRoute(convertedPoints, routeName);
                Random random = new Random();
                int colorIndex = random.Next(0, colors.Length);
                polygon.Stroke = new Pen(colors[colorIndex], 3);
                this.Routes.Add(polygon);
            }
        }
        private GMapMarker AddMarker(double Lat, double Lng, MarkerType googleType, GMapToolTip toolTip = null)
        {
            GoogleMarkerType markerType = Mapper.Map<MarkerType, GoogleMarkerType>(googleType);
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Lat, Lng), markerType);
            if (toolTip != null)
            {
                marker.ToolTip = toolTip;
            }
            return marker;
        }
    }
}
