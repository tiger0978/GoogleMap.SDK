using AutoMapper;
using GMap.NET;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using GooleMap.SDK.Contract;
using GooleMap.SDK.Core;
using GooleMap.SDK.UI.Winform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GooleMap.SDK.UI.Winform.Components.AutoComplete.GoogleMap
{
    public class GoogleMapControl : UserControl, IGMap
    {
        private GMapControl gMapControl;
        protected static Dictionary<string, AMapOverlay> OverLays = new Dictionary<string, AMapOverlay>();

        public Location Position
        {
            get => new Location(gMapControl.Position.Lat, gMapControl.Position.Lng);
            set => gMapControl.Position = new PointLatLng(value.latLng.latitude, value.latLng.longitude);
        }
        public double Zoom
        {
            get => gMapControl.Zoom;
            set => gMapControl.Zoom = value;
        }

        public delegate void GMarkerClick(GMarker item, MouseEventArgs e);
        public event GMarkerClick MarkerEvent;

        public GoogleMapControl()
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

        public void CreateRoute(IEnumerable<Latlng> points, object toolTip = null)
        {
            IOverlay routeOverlay = OverlayFactory.Create(MapOverlayType.Route);
            TryAddOverlayInGmapControlOverlays(routeOverlay);
            routeOverlay.SetOverLay(points, GMarkerGoogleType.none, toolTip);
        }
        public void CreateRoute(IEnumerable<List<Latlng>> routePoints, object toolTip = null)
        {
            IOverlay routeOverlay = OverlayFactory.Create(MapOverlayType.Route);
            TryAddOverlayInGmapControlOverlays(routeOverlay);
            routeOverlay.SetOverLay(routePoints, GMarkerGoogleType.none, toolTip);
        }
        public void CreateRoute(IEnumerable<Location> locations, object toolTip = null)
        {
            IOverlay routeOverlay = OverlayFactory.Create(MapOverlayType.Route);
            TryAddOverlayInGmapControlOverlays(routeOverlay);
            routeOverlay.SetOverLay(locations, GMarkerGoogleType.none, toolTip);
        }
        public void CreateRoute(string overlayName, IEnumerable<Latlng> points, object toolTip = null)
        {
            IOverlay routeOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            TryAddOverlayInGmapControlOverlays(routeOverlay);
            routeOverlay.SetOverLay(points, GMarkerGoogleType.none, toolTip);
        }
        public void CreateRoute(string overlayName, IEnumerable<List<Latlng>> routePoints, object toolTip = null)
        {
            IOverlay routeOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            TryAddOverlayInGmapControlOverlays(routeOverlay);
            routeOverlay.SetOverLay(routePoints, GMarkerGoogleType.none, toolTip);
        }
        public void CreateRoute(string overlayName, IEnumerable<Location> locations, object toolTip = null)
        {
            IOverlay routeOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            TryAddOverlayInGmapControlOverlays(routeOverlay);
            routeOverlay.SetOverLay(locations, GMarkerGoogleType.none, toolTip);
        }

        public void CreateMarker(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay markerOverLay = OverlayFactory.Create(MapOverlayType.Marker);
            TryAddOverlayInGmapControlOverlays(markerOverLay);
            markerOverLay.SetOverLay(lat, lng, markerType, toolTip);
            SwitchGMapView(lat, lng);
        }
        public void CreateMarker(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay markerOverLay = OverlayFactory.Create(MapOverlayType.Marker);
            TryAddOverlayInGmapControlOverlays(markerOverLay);
            markerOverLay.SetOverLay(list, markerType, toolTip);
            var move = list.FirstOrDefault();
            SwitchGMapView(move.latitude, move.longitude);
        }
        public void CreateMarker(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay markerOverLay = OverlayFactory.Create(MapOverlayType.Marker);
            TryAddOverlayInGmapControlOverlays(markerOverLay);
            markerOverLay.SetOverLay(locations, markerType, toolTip);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }
        public void CreateMarker(string overlayName, double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay markerOverLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            TryAddOverlayInGmapControlOverlays(markerOverLay);
            markerOverLay.SetOverLay(lat, lng, markerType, toolTip);
            SwitchGMapView(lat, lng);
        }
        public void CreateMarker(string overlayName, IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay markerOverLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            TryAddOverlayInGmapControlOverlays(markerOverLay);
            markerOverLay.SetOverLay(list, markerType, toolTip);
            var move = list.FirstOrDefault();
            SwitchGMapView(move.latitude, move.longitude);
        }
        public void CreateMarker(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay markerOverLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            TryAddOverlayInGmapControlOverlays(markerOverLay);
            markerOverLay.SetOverLay(locations, markerType, toolTip);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }

        public void ClearMarkerOverlay()
        {
            IOverlay markerOverLay = OverlayFactory.Create(MapOverlayType.Marker);
            gMapControl.Overlays.Remove((AMapOverlay)markerOverLay);
        }
        public void ClearMarkerOverlay(string overlayName)
        {
            IOverlay markerOverLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            gMapControl.Overlays.Remove((AMapOverlay)markerOverLay);
        }
        public void ClearRouteOverlay()
        {
            IOverlay markerOverLay = OverlayFactory.Create(MapOverlayType.Route);
            OverLays.TryGetValue(typeof(GoogleMapRouteOverlay).Name, out AMapOverlay routeOverlay);
            if (routeOverlay != null)
            {
                gMapControl.Overlays.Remove(routeOverlay);
                OverLays.Remove(typeof(GoogleMapRouteOverlay).Name);
            }
        }
        public void ClearRouteOverlay(string overlayName)
        {
            IOverlay markerOverLay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            OverLays.TryGetValue(typeof(GoogleMapRouteOverlay).Name, out AMapOverlay routeOverlay);
            if (routeOverlay != null)
            {
                gMapControl.Overlays.Remove(routeOverlay);
                OverLays.Remove(typeof(GoogleMapRouteOverlay).Name);
            }
        }
        public void DeleteMarkers(List<Location> locations)
        {
            IOverlay markerOverLay = OverlayFactory.Create(MapOverlayType.Marker);
            markerOverLay.DeleteOverlayElement(locations);
        }
        public void DeleteMarkers(string overlayName, List<Location> locations)
        {
            IOverlay markerOverLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            markerOverLay.DeleteOverlayElement(locations);
        }
        public void DeleteRoutes(IEnumerable<List<Latlng>> routes)
        {
            IOverlay routeOverlay = OverlayFactory.Create(MapOverlayType.Route);
            routeOverlay.DeleteOverlayElement(routes);
        }
        public void DeleteRoutes(string overlayName, IEnumerable<List<Latlng>> routes)
        {
            IOverlay routeOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            routeOverlay.DeleteOverlayElement(routes);
        }
        public void ClearAll()
        {
            ClearMarkerOverlay();
            ClearRouteOverlay();
        }
        public void ClearAll(string overlayName)
        {
            ClearMarkerOverlay(overlayName);
            ClearRouteOverlay(overlayName);
        }

        private void SwitchGMapView(double lat, double lng)
        {
            gMapControl.Zoom = 12;
            gMapControl.Position = new PointLatLng(lat, lng);
            gMapControl.Zoom = 13;
            gMapControl.ShowCenter = true;
        }
        private void TryAddOverlayInGmapControlOverlays(IOverlay overlay)
        {
            if (!gMapControl.Overlays.Any(x => x == overlay))
            {
                this.gMapControl.Overlays.Add((AMapOverlay)overlay);
            }
        }
    }
}
