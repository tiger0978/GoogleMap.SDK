using AutoMapper;
using GMap.NET;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contracts.Commons.Models;
using GooleMap.SDK.UI.Winform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GooleMap.SDK.UI.Winform
{
    public class GMapsUtility : UserControl, IGMap
    {
        private GMapControl gMapControl;
        protected static Dictionary<string, AMapOverlay> OverLays = new Dictionary<string, AMapOverlay>();
        public delegate void GMarkerClick(GMarker item, MouseEventArgs e);
        public event GMarkerClick MarkerEvent;

        public GMapsUtility()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.gMapControl = new GMapControl();
            this.gMapControl.OnMarkerClick += GMapControl_OnMarkerClick;
            this.SuspendLayout();
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 2;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomEnabled = true;
            this.gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl1";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(298, 261);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 0D;
            this.gMapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance; // 設置地圖源
            GMaps.Instance.Mode = AccessMode.ServerAndCache; // GMap工作模式
            this.gMapControl.Position = new PointLatLng(25.22073181939088, 121.56732774228881);
            this.gMapControl.MaxZoom = 20;
            this.gMapControl.Zoom = 14;
            this.gMapControl.ShowCenter = false;

            // 
            // GMap
            // 
            this.Controls.Add(this.gMapControl);
            this.Name = "GMap";
            this.Size = new System.Drawing.Size(298, 261);
            this.ResumeLayout(false);
        }


        private void GMapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<GMapMarker, GMarker>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<GMarker>(item);
            MarkerEvent.Invoke(result, e);
        }

        public void CreateRoute(IEnumerable<Latlng> points, GMapToolTip toolTip = null)
        {
            ClearRoutes();
            AMapOverlay routeOverlay = GetMapOverlay<GoogleMapRouteOverlay>();
            routeOverlay.SetOverLay(points, toolTip);
        }

        public void CreateRoute(IEnumerable<List<Latlng>> routePoints, GMapToolTip toolTip = null)
        {
            ClearRoutes();
            AMapOverlay routeOverlay = GetMapOverlay<GoogleMapRouteOverlay>();
            routeOverlay.SetOverLay(routePoints, toolTip);

        }

        public void CreateRoute(IEnumerable<Location> locations, GMapToolTip toolTip = null)
        {
            ClearRoutes();
            AMapOverlay routeOverlay = GetMapOverlay<GoogleMapRouteOverlay>();
            routeOverlay.SetOverLay(locations, toolTip);
        }

        public void CreateMarker(double lat, double lng, GMapToolTip toolTip = null)
        {
            AMapOverlay markerOverLay = GetMapOverlay<GoogleMapMarkerOverlay>();
            markerOverLay.SetOverLay(lat, lng, toolTip);
            SwitchGMapView(lat, lng);

        }

        public void CreateMarker(IEnumerable<Latlng> list, GMapToolTip toolTip = null)
        {
            AMapOverlay markerOverLay = GetMapOverlay<GoogleMapMarkerOverlay>();
            markerOverLay.SetOverLay(list, toolTip);
            var move = list.FirstOrDefault();
            SwitchGMapView(move.latitude, move.longitude);

        }

        public void CreateMarker(IEnumerable<Location> locations, GMapToolTip toolTip = null)
        {
            AMapOverlay markerOverLay = GetMapOverlay<GoogleMapMarkerOverlay>();
            markerOverLay.SetOverLay(locations, toolTip);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }

        public void ClearMarkers()
        {
            OverLays.TryGetValue(typeof(GoogleMapMarkerOverlay).Name, out AMapOverlay markerOverLay);
            if (markerOverLay != null)
            {
                gMapControl.Overlays.Remove(markerOverLay);
                OverLays.Remove(typeof(GoogleMapMarkerOverlay).Name);
            }
        }

        public void ClearRoutes()
        {
            OverLays.TryGetValue(typeof(GoogleMapRouteOverlay).Name, out AMapOverlay routeOverlay);
            if (routeOverlay != null)
            {
                gMapControl.Overlays.Remove(routeOverlay);
                OverLays.Remove(typeof(GoogleMapRouteOverlay).Name);
            }
        }

        public void ClearAll()
        {
            ClearMarkers();
            ClearRoutes();
        }

        public void DeleteMarker(Location location)
        {
            AMapOverlay markerOverLay = GetMapOverlay<GoogleMapMarkerOverlay>();
            var marker = markerOverLay.Markers.FirstOrDefault(x => x.Position == new PointLatLng(location.latLng.latitude, location.latLng.longitude));
            if (marker != null)
            {
                markerOverLay.Markers.Remove(marker);
            }
        }

        private AMapOverlay GetMapOverlay<T>() where T : AMapOverlay, new()
        {
            AMapOverlay routeOverlay;
            if (!OverLays.TryGetValue(typeof(T).Name, out routeOverlay))
            {
                routeOverlay = new T();
                OverLays.Add(typeof(T).Name, routeOverlay);
                gMapControl.Overlays.Add(routeOverlay);
            }
            return routeOverlay;
        }

        private void SwitchGMapView(double lat, double lng)
        {
            gMapControl.Zoom = 12;
            gMapControl.Position = new PointLatLng(lat, lng);
            gMapControl.Zoom = 13;
            gMapControl.ShowCenter = true;
        }
    }
}
