using GMap.NET;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Utility;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.UI.Winform
{
    public class GoogleMapRouteOverlay : AMapOverlay
    {
        Color[] colors = { Color.Blue, Color.Green, Color.Black, Color.Yellow, Color.Red };

        public GoogleMapRouteOverlay(string overlayId) : base(overlayId)
        {
        }

        public override void SetOverLay(IEnumerable<List<Latlng>> routePoints, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
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

        public override void SetOverLay(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            throw new NotImplementedException();
        }

        public override void SetOverLay(IEnumerable<Latlng> points, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            //var convertedPoints = points.Select(x => new PointLatLng(x.latitude, x.longitude));

            var routeName = PolylineEncoder.EncodeCoordinates(points);
            var convertedPoints = points.Select(x => new PointLatLng(x.latitude, x.longitude));
            GMapRoute polygon = new GMapRoute(convertedPoints, routeName);
            Random random = new Random();
            int colorIndex = random.Next(0, colors.Length);
            polygon.Stroke = new Pen(colors[colorIndex], 3);
            this.Routes.Add(polygon);
        }

        public override void SetOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var points = locations.Select(x => new Latlng()
            {
                latitude = x.latLng.latitude,
                longitude = x.latLng.longitude
            });
            var routeName = PolylineEncoder.EncodeCoordinates(points);
            var convertedPoints = points.Select(x=>new PointLatLng(x.latitude, x.longitude));
            GMapRoute polygon = new GMapRoute(convertedPoints, routeName);

            Random random = new Random();
            int colorIndex = random.Next(0, colors.Length);
            polygon.Stroke = new Pen(colors[colorIndex], 3);
            this.Routes.Add(polygon);
        }

        public override void DeleteOverlayElement(object element)
        {
            if (element is IEnumerable<List<Latlng>> routes)
            {
                foreach (var route in routes)
                {
                    var routeName = PolylineEncoder.EncodeCoordinates(route);
                    var existedRoute = Routes.FirstOrDefault(x=>x.Name == routeName);
                    Routes.Remove(existedRoute);    
                }
            }
        }

    }
}
