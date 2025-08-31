using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Utility;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GoogleMap.SDK.UI.WPF.MapOverlays
{
    public class GoogleMapRouteOverlay : AMapOverlay
    {
        Color[] colors = { Colors.Blue, Colors.Green, Colors.Black, Colors.Yellow, Colors.Red };
        public GoogleMapRouteOverlay(string overLayId) : base(overLayId)
        {

        }

        public override void SetOverLay(IEnumerable<List<Latlng>> routePoints, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            foreach (var routePoint in routePoints)
            {
                var routeName = PolylineEncoder.EncodeCoordinates(routePoint);
                var convertedPoints = routePoint.Select(x => new PointLatLng(x.latitude, x.longitude));
                GMapRoute polygon = new GMapRoute(convertedPoints);
                polygon.Tag = routeName;
                Random random = new Random();
                int colorIndex = random.Next(0, colors.Length);
                polygon.Shape = new Path
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 3,
                    Opacity = 0.7
                };
                this.routes.Add(polygon);
            }
        }

        public override void SetOverLay(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            throw new NotImplementedException();
        }

        public override void SetOverLay(IEnumerable<Latlng> points, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var routeName = PolylineEncoder.EncodeCoordinates(points);
            var convertedPoints = points.Select(x => new PointLatLng(x.latitude, x.longitude));
            GMapRoute polygon = new GMapRoute(convertedPoints);
            polygon.Tag = routeName;
            Random random = new Random();
            int colorIndex = random.Next(0, colors.Length);
            polygon.Shape = new Path
            {
                Stroke = Brushes.Red,
                StrokeThickness = 3,
                Opacity = 0.7
            };
            this.routes.Add(polygon);
        }

        public override void SetOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var points = locations.Select(x => new Latlng()
            {
                latitude = x.latLng.latitude,
                longitude = x.latLng.longitude
            });
            var routeName = PolylineEncoder.EncodeCoordinates(points);
            var convertedPoints = points.Select(x => new PointLatLng(x.latitude, x.longitude));
            GMapRoute polygon = new GMapRoute(convertedPoints);
            polygon.Tag = routeName;

            Random random = new Random();
            int colorIndex = random.Next(0, colors.Length);
            polygon.Shape = new Path
            {
                Stroke = Brushes.Red,
                StrokeThickness = 3,
                Opacity = 0.7
            };
            this.routes.Add(polygon);
        }

        public override void DeleteOverlayElement(object element)
        {
            if (element is IEnumerable<List<Latlng>> routes)
            {
                foreach (var route in routes)
                {
                    var routeName = PolylineEncoder.EncodeCoordinates(route);
                    var existedRoute = base.routes.FirstOrDefault(x => x.Tag.ToString() == routeName);
                    base.routes.Remove(existedRoute);
                }
            }
        }
    }
}
