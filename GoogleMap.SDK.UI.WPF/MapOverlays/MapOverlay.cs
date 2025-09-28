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
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GoogleMap.SDK.UI.WPF.MapOverlays
{
    public class MapOverlay : IOverlay
    {
        public ObservableCollection<GMapMarker> markers = new ObservableCollection<GMapMarker>();
        public ObservableCollection<GMapMarker> routes = new ObservableCollection<GMapMarker>();
        private string overLayId;

        public string Id 
        {
            get => overLayId;
            set => overLayId = value;
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
        public void DeleteRouteElement(object element)
        {
            if (element is IEnumerable<List<Latlng>> routes)
            {
                foreach (var route in routes)
                {
                    var routeName = PolylineEncoder.EncodeCoordinates(route);
                    var existedRoute = this.routes.FirstOrDefault(x => x.Tag.ToString() == routeName);
                    this.routes.Remove(existedRoute);
                }
            }
        }
        public void DeleteMarkerElement(object element)
        {
            if (element is List<Location> locations)
            {
                foreach (var location in locations)
                {
                    var marker = markers.FirstOrDefault(x => x.Position == new PointLatLng(location.latLng.latitude, location.latLng.longitude));
                    if (marker != null)
                    {
                        markers.Remove(marker);
                    }
                }
            }
        }
        public void ClearMarkers()
        {
            markers.Clear();
        }
        public void ClearRoutes()
        {
            routes.Clear();
        }
        public void ClearAll()
        {
            this.markers.Clear();
            this.routes.Clear();
        }

        private Image InitialToolTip(ToolTip tooltip, GMapMarker marker, GMarkerGoogleType markerType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"GoogleMap.SDK.UI.WPF.Resources.{markerType}.png";
            //Stream stream = assembly.GetManifestResourceStream(resourceName);
            ////var uri = new Uri($"pack://application:,,,/GoogleMap.SDK.UI.WPF;component/Resources/{markerType}.png", UriKind.Absolute);
            //var img = new BitmapImage();
            //img.BeginInit();
            //img.CacheOption = BitmapCacheOption.OnLoad;
            //img.StreamSource = stream;
            //img.EndInit();

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"找不到內嵌資源: {resourceName}");

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;   // ← 很重要：確保在 EndInit 後可以關閉 stream
            bitmap.StreamSource = stream;                    // ← 必須在 EndInit 之前設定
            bitmap.EndInit();
            bitmap.Freeze();



            //string imgPath = System.IO.Path.Combine("Resources", markerType.ToString() + ".png");
            //byte[] bytes = File.ReadAllBytes(imgPath);

            var image = new Image
            {
                Source = bitmap,
                Width = 32,
                Height = 32,
                Cursor = Cursors.Hand,
                ToolTip = tooltip,
                Tag = marker
            };
            image.MouseLeftButtonDown += Marker_Click;
            return image;
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
