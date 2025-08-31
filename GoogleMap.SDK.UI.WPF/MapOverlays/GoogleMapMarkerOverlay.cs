using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GooleMap.SDK.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace GoogleMap.SDK.UI.WPF.MapOverlays
{
    public class GoogleMapMarkerOverlay : AMapOverlay
    {
        public GoogleMapMarkerOverlay(string overLayId) : base(overLayId)
        {
        }

        public override void SetOverLay(IEnumerable<List<Latlng>> lists, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            throw new NotImplementedException();
        }

        public override void SetOverLay(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            ToolTip tip = toolTip as ToolTip;
            var point = new PointLatLng(lat, lng);
            GMapMarker marker = new GMapMarker(point);
            var image = InitialToolTip(tip, marker, markerType);
            marker.Shape = image;
            markers.Add(marker);
        }

        public override void SetOverLay(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var points = list.Select(x => new PointLatLng()
            {
                Lat = x.latitude,
                Lng = x.longitude
            }).ToList();

            foreach (var point in points)
            {
                GMapMarker marker = new GMapMarker(point);
                markers.Add(marker);
            }
        }

        public override void SetOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
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

        public override void DeleteOverlayElement(object element) // 刪除 list 的 GMapMarker / 實際 GMarker 刪除於外層執行
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

            //var uri = new Uri("pack://application:,,,/GoogleMap.SDK.UI.WPF;component/Resources/logo.png");
            string imgPath = Path.Combine("Resources", markerType.ToString()+".png");
            byte[] bytes = File.ReadAllBytes(imgPath);
            var image = new Image
            {
                // TODO: 將圖片url改成讀取實體的檔案
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
