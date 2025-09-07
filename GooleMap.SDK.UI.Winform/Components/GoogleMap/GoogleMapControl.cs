using AutoMapper;
using GMap.NET;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Core;
using GooleMap.SDK.Contract;
using GooleMap.SDK.Core;
using GooleMap.SDK.UI.Winform.MapOverlays;
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
        private readonly IMapOverlayService _mapOverlayService;

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

        public GoogleMapControl(IMapOverlayService mapOverlayService)
        {
            InitializeComponent();
            _mapOverlayService = mapOverlayService;
            this.ParentChanged += OnParentChanged;  
        }

        private void OnParentChanged(object sender,EventArgs e)
        {
            Panel parentPanel = this.Parent as Panel;
            this.Width = parentPanel.Width;
            this.Height = parentPanel.Height;
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

        public void CreateRoute(IEnumerable<Latlng> routePoints, string overlayName = "MapOverlay")
        {
            var routes = new List<List<Latlng>>();
            routes.Add(routePoints.ToList());
            BuildRoute(overlayName, routes);
        }
        public void CreateRoute(List<List<Latlng>> routes, string overlayName = "MapOverlay")
        {
            BuildRoute(overlayName, routes);
        }
        public void CreateRoute(IEnumerable<Location> locations, string overlayName = "MapOverlay")
        {
            var routePoint = locations.Select(x => new Latlng(x.latLng.latitude, x.latLng.longitude)).ToList();
            var routes = new List<List<Latlng>>();
            routes.Add(routePoint);
            BuildRoute(overlayName, routes);
        }

        public void CreateMarker(double lat, double lng, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var location = new Location(lat, lng);
            var locations = new List<Location>();
            locations.Add(location);
            BuildMarker(overlayName, locations, markerType, toolTip);
        }
        public void CreateMarker(IEnumerable<Latlng> list, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var locations = list.Select(x => new Location(x.latitude, x.longitude)).ToList();
            BuildMarker(overlayName, locations, markerType, toolTip);
        }
        public void CreateMarker(IEnumerable<Location> locations, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            BuildMarker(overlayName, locations, markerType, toolTip);
        }

        public void ClearOverlay(string overlayName)
        {
            var overlay = _mapOverlayService.DeleteOverlay();
            gMapControl.Overlays.Remove((MapOverlay)overlay);
        }
        public void ClearRoutes(string overlayName)
        {
            _mapOverlayService.DeleteRoutes(overlayName);
        }
        public void ClearMarkers(string overlayName)
        {
            _mapOverlayService.DeleteMarkers(overlayName);
        }
        public void RemoveRouteElement(IEnumerable<List<Latlng>> routes, string overlayName)
        {
            _mapOverlayService.DeleteRouteElement(routes, overlayName);
        }
        public void RemoveMarkerElement(List<Location> locations, string overlayName)
        {
            _mapOverlayService.DeleteMarkerElement(locations, overlayName);
        }

        public void HideOverlay(string overlayName = "MapOverlay")
        {
            var overlay = _mapOverlayService.GetOverlay(overlayName);
            gMapControl.Overlays.Remove((MapOverlay)overlay);
        }
        public void ShowOverlay(string overlayName = "MapOverlay")
        {
            var overlay = _mapOverlayService.GetOverlay(overlayName);
            gMapControl.Overlays.Add((MapOverlay)overlay);
        }

        private void BuildMarker(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var iOverlay = _mapOverlayService.AddMarkers(locations, overlayName, markerType, toolTip);
            TryAddOverlayInGmapControlOverlays(iOverlay);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }
        private void BuildRoute(string overlayName, List<List<Latlng>> routes)
        {
            IOverlay iOverlay = _mapOverlayService.AddRoutes(routes, overlayName);
            TryAddOverlayInGmapControlOverlays(iOverlay);
            var move = routes[0].FirstOrDefault();
            SwitchGMapView(move.latitude, move.longitude);
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
                this.gMapControl.Overlays.Add((MapOverlay)overlay);
            }
        }
    }
}
