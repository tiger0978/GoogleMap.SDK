using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using GoogleMap.SDK.Contract.Utility;
using System.Windows.Shapes;
using System.IO;
using Path = System.Windows.Shapes.Path;

namespace GoogleMap.SDK.UI.WPF.MapOverlays
{
    public class MapOverlay : GMapMarker, IOverlayNew
    {
        public List<GMapMarker> markers = new List<GMapMarker>();
        public List<GMapMarker> routes = new List<GMapMarker>();
        public string overLayId;
        public MapOverlay(string overLayId) : base(default)
        {
            this.overLayId = overLayId;
        }
        public void ClearAll()
        {
            this.markers.Clear();
            this.routes.Clear();
        }

        public void SetMarkerOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            ToolTip tip = toolTip as ToolTip;
            var points = locations.Select(x => new PointLatLng()
            {
                Lat = x.latLng.latitude,
                Lng = x.latLng.longitude
            });
            foreach (var point in points)
            {
                GMapMarker marker = new GMapMarker(point);
                var image = InitialToolTip(tip, marker, markerType);
                marker.Shape = image;
                markers.Add(marker);
            }
        }

        public void SetRouteOverLay(IEnumerable<List<Latlng>> routes)
        {
            foreach (var routePoint in routes)
            {
                var routeName = PolylineEncoder.EncodeCoordinates(routePoint);
                var convertedPoints = routePoint.Select(x => new PointLatLng(x.latitude, x.longitude));
                GMapRoute polygon = new GMapRoute(convertedPoints);
                polygon.Tag = routeName;
                Random random = new Random();

                Color[] colors = { Colors.Blue, Colors.Green, Colors.Black, Colors.Yellow, Colors.Red };
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

        private ImageSource ToImageSource(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (var stream = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(
                    stream,
                    BitmapCreateOptions.None,
                    BitmapCacheOption.OnLoad
                );
            }
        }
        private Image InitialToolTip(ToolTip tooltip, GMapMarker marker, GMarkerGoogleType markerType)
        {
            string imgPath = System.IO.Path.Combine("Resources", markerType.ToString() + ".png");
            byte[] bytes = File.ReadAllBytes(imgPath);
            var image = new Image
            {
                Source = ToImageSource(bytes),
                Width = 32,
                Height = 32,
                Cursor = Cursors.Hand,
                ToolTip = tooltip,
                Tag = marker
            };
            
            image.MouseLeftButtonDown += Marker_Click;
            return image;
        }
        private void Marker_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element?.Tag is GMapMarker marker)
            {
                // 你可以用 marker.Position, marker.Tag 等等
                var info = marker.Tag?.ToString() ?? "無標題";
                MessageBox.Show($"你點了 marker：{info} ({marker.Position.Lat}, {marker.Position.Lng})");
            }
        }
    }
}
