using GMap.NET;
using GMap.NET.WindowsForms;
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

        public override void SetOverLay(IEnumerable<List<Latlng>> routePoints, GMapToolTip toolTip = null)
        {
            int index = 0;
            foreach (var routePoint in routePoints)
            {
                var convertedRoutePoint = routePoint.Select(x=> new PointLatLng(x.latitude, x.longitude));
                GMapRoute polygon = new GMapRoute(convertedRoutePoint, typeof(GMapRoute).Name + index.ToString());
                Random random = new Random();
                int colorIndex = random.Next(0, colors.Length);
                polygon.Stroke = new Pen(colors[colorIndex], 3);
                this.Routes.Add(polygon);
                index++;
            }
        }

        public override void SetOverLay(double lat, double lng, GMapToolTip toolTip = null)
        {
            throw new NotImplementedException("This Method is not Supported");
        }

        public override void SetOverLay(IEnumerable<Latlng> points, GMapToolTip toolTip = null)
        {
            var convertedPoints = points.Select(x=> new PointLatLng(x.latitude, x.longitude));  
            GMapRoute polygon = new GMapRoute(convertedPoints, typeof(GMapRoute).Name);
            Random random = new Random();
            int colorIndex = random.Next(0, colors.Length);
            polygon.Stroke = new Pen(colors[colorIndex], 3);
            this.Routes.Add(polygon);
        }

        public override void SetOverLay(IEnumerable<Location> locations, GMapToolTip toolTip = null)
        {
            var convertedPoints = locations.Select(x => new PointLatLng(x.latLng.latitude, x.latLng.longitude));
            GMapRoute polygon = new GMapRoute(convertedPoints, typeof(GMapRoute).Name);
            Random random = new Random();
            int colorIndex = random.Next(0, colors.Length);
            polygon.Stroke = new Pen(colors[colorIndex], 3);
            this.Routes.Add(polygon);
        }
    }
}
