using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contract.Utility;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.Windows.Shapes.Path;

namespace GoogleMap.SDK.UI.WPF.MapOverlays
{
    public class MapOverlay : IOverlay
    {
        public ObservableCollection<GMapMarker> markers = new ObservableCollection<GMapMarker>();
        public ObservableCollection<GMapMarker> routes = new ObservableCollection<GMapMarker>();
        private string overLayId;

        private EventHandler<MarkerInfo> OnMarkerClicked;

        public string Id 
        {
            get => overLayId;
            set => overLayId = value;
        }

        public void SetMarkerOverLay(IEnumerable<Location> locations, object data, EventHandler<MarkerInfo> clickEvent, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            ToolTip tip = toolTip as ToolTip;
            OnMarkerClicked = clickEvent;
            var points = locations.Select(x => new PointLatLng()
            {
                Lat = x.latLng.latitude,
                Lng = x.latLng.longitude
            });
            foreach (var point in points)
            {
                GMapMarker marker = new GMapMarker(point);
                var image = InitialToolTip(tip, marker, markerType);
                MarkerInfo markerInfo = new MarkerInfo(new Location(point.Lat, point.Lng), marker, data);

                marker.Shape = image;
                image.Tag = markerInfo;
                image.MouseLeftButtonUp += Image_MouseLeftButtonUp;
                marker.ZIndex = 200;
                markers.Add(marker);
            }
        }

        public void SetRouteOverLay(IEnumerable<List<Latlng>> routes)
        {
            int index = 0;
            foreach (var routePoint in routes)
            {
                var routeName = PolylineEncoder.EncodeCoordinates(routePoint);
                var convertedPoints = routePoint.Select(x => new PointLatLng(x.latitude, x.longitude));
                GMapRoute polygon = new GMapRoute(convertedPoints);
                polygon.Tag = routeName;

                Path routePath = new Path
                {
                    StrokeLineJoin = PenLineJoin.Round,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round,
                    Cursor = Cursors.Hand,
                    Tag = polygon // 將 polygon 存入 Path 的 Tag，方便事件中識別
                };

                // 初始樣式判定：第一筆為最佳路線
                if (index == 0) 
                {
                    ApplyPrimaryStyle(routePath);
                    polygon.ZIndex = 100;
                }
                else 
                {
                    ApplyAlternativeStyle(routePath);
                    polygon.ZIndex = 10;
                }

                routePath.MouseLeftButtonDown += (s, e) =>
                {
                    e.Handled = true; // 阻止事件穿透到地圖
                    UpdateRouteFocus(s as Path);
                };

                polygon.Shape = routePath;
                this.routes.Add(polygon);
                index++;
            }
        }

        public void ActivateRoute(int index)
        {
            for (int i = 0; i < routes.Count; i++) 
            {
                if (i == index) 
                {
                    ApplyPrimaryStyle((Path)routes[i].Shape);
                    routes[i].ZIndex = 100;
                    continue;
                }
                ApplyAlternativeStyle((Path)routes[i].Shape);
                routes[i].ZIndex = 10;
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
            foreach (var marker in markers.ToList())
            {
                markers.Remove(marker);
            }
        }
        public void ClearRoutes()
        {
            foreach(var route in routes.ToList())
            {
                routes.Remove(route);
            }
        }
        public void ClearAll()
        {
            foreach (var marker in markers.ToList())
            {
                markers.Remove(marker);
            }
            foreach (var route in routes.ToList())
            {
                routes.Remove(route);
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            MarkerInfo marker = image.Tag as MarkerInfo;
            OnMarkerClicked?.Invoke(marker.Marker, marker);
        }

        private Image InitialToolTip(ToolTip tooltip, GMapMarker marker, GMarkerGoogleType markerType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"GoogleMap.SDK.UI.WPF.Resources.{markerType}.png";
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"找不到內嵌資源: {resourceName}");

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;   // ← 很重要：確保在 EndInit 後可以關閉 stream
            bitmap.StreamSource = stream;                    // ← 必須在 EndInit 之前設定
            bitmap.EndInit();
            bitmap.Freeze();
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

        // 主路線樣式：深藍、不透明、較粗
        private void ApplyPrimaryStyle(Path path)
        {
            path.Stroke = Brushes.DarkBlue;
            path.StrokeThickness = 6;
            path.Opacity = 1.0;
        }

        // 替代路線樣式：淺灰或淡藍、半透明、較細
        private void ApplyAlternativeStyle(Path path)
        {
            path.Stroke = new SolidColorBrush(Color.FromRgb(66, 133, 244));
            path.StrokeThickness = 4;
            path.Opacity = 1.0;
        }

        private void UpdateRouteFocus(Path clickedPath)
        {
            if (clickedPath == null) return;
            foreach (var routeMarker in this.routes)
            {
                if (routeMarker.Shape is Path path)
                {
                    if (path == clickedPath)
                    {
                        ApplyPrimaryStyle(path);
                        routeMarker.ZIndex = 100;
                    }
                    else
                    {
                        ApplyAlternativeStyle(path);
                        routeMarker.ZIndex = 10;
                    }
                }
            }
        }

    }
}
