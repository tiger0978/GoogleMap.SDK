using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Core;
using GoogleMap.SDK.UI.WPF.MapOverlays;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Location = GoogleMap.SDK.Contracts.Commons.Models.Location;

namespace GoogleMap.SDK.UI.WPF.Components.GoogleMap
{
    /// <summary>
    /// GoogleMapControl.xaml 的互動邏輯
    /// </summary>
    public partial class GoogleMapControl : UserControl, IGMap
    {
        private readonly IMapOverlayService _mapOverlayService;
        private List<IOverlay> Overlays = new List<IOverlay>();

        public Location Position
        {
            get => new Location(gmap.Position.Lat, gmap.Position.Lng); 
            set => gmap.Position = new PointLatLng(value.latLng.latitude, value.latLng.longitude);
        }
        public double Zoom 
        {
            get => gmap.Zoom; 
            set => gmap.Zoom = value; 
        }

        public GoogleMapControl(IMapOverlayService mapOverlayService)
        {
            InitializeComponent();
            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.Position = new PointLatLng(25.033964, 121.564468);
            gmap.MinZoom = 2;
            gmap.MaxZoom = 18;
            gmap.Zoom = 14;
            gmap.ShowCenter = false;
            gmap.CanDragMap = true;
            gmap.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            gmap.DragButton = MouseButton.Left;
            _mapOverlayService = mapOverlayService;
           
        }

        public void CreateMarker(double lat, double lng, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var location = new Location(lat, lng);
            var locations = new List<Location>();
            locations.Add(location);
            BuildMarkers(overlayName, locations, markerType, toolTip);
        }
        public void CreateMarker(IEnumerable<Latlng> list, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            var locations = list.Select(x=> new Location(x.latitude, x.longitude)).ToList();
            BuildMarkers(overlayName, locations, markerType, toolTip);
        }
        public void CreateMarker(IEnumerable<Location> locations, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            BuildMarkers(overlayName, locations, markerType, toolTip);
        }
        public void CreateMarker(Location location, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            List<Location> locations = new List<Location>();
            locations.Add(location);
            BuildMarkers(overlayName, locations, markerType, toolTip);
        }

        public void CreateRoute(IEnumerable<Latlng> routePoints, string overlayName = "MapOverlay")
        {
            var routes = new List<List<Latlng>>();
            routes.Add(routePoints.ToList());
            BuildRoutes(overlayName, routes);
        }
        public void CreateRoute(List<List<Latlng>> routes, string overlayName = "MapOverlay")
        {
            BuildRoutes(overlayName, routes);
        }
        public void CreateRoute(IEnumerable<Location> locations, string overlayName = "MapOverlay")
        {
            var routePoint = locations.Select(x => new Latlng(x.latLng.latitude, x.latLng.longitude)).ToList();
            var routes = new List<List<Latlng>>();
            routes.Add(routePoint);
            BuildRoutes(overlayName, routes);
        }

        public void ClearOverlay(string overlayName)
        {
            _mapOverlayService.DeleteOverlay(overlayName);
        }
        public void ClearRoutes(string overlayName)
        {
            _mapOverlayService.DeleteRoutes(overlayName);
        }
        public void ClearMarkers(string overlayName)
        {
            _mapOverlayService.DeleteMarkers(overlayName);
        }
        public void RemoveRouteElement(IEnumerable<List<Latlng>> routes, string overlayName = "MapOverlay")
        {
            _mapOverlayService.DeleteRouteElement(routes, overlayName);
        }
        public void RemoveMarkerElement(List<Location> locations, string overlayName = "MapOverlay")
        {
            _mapOverlayService.DeleteMarkerElement(locations,overlayName);
        }

        public void HideOverlay(string overlayName)
        {
            var mapOverlay = (MapOverlay)_mapOverlayService.GetOverlay(overlayName);
            foreach (var route in mapOverlay.routes)
            {
                gmap.Markers.Remove(route);
            }
            foreach (var marker in mapOverlay.markers)
            {
                gmap.Markers.Remove(marker);
            }
        }
        public void ShowOverlay(string overlayName)
        {
            var mapOverlay = (MapOverlay)_mapOverlayService.GetOverlay(overlayName);
            foreach (var route in mapOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
            foreach (var marker in mapOverlay.markers)
            {
                gmap.Markers.Add(marker);
            }
        }

        public void TryAddOverlayInGmapControlOverlays(IOverlay overlay)
        {
            if (!Overlays.Any(x => x == overlay))
            {
                var mapOverlay = (MapOverlay)overlay;
                mapOverlay.markers.CollectionChanged += ElementChanged;
                mapOverlay.routes.CollectionChanged += ElementChanged;
                Overlays.Add(mapOverlay);
            }
        }
        private void ElementChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(GMapMarker element in e.NewItems)
                {
                    gmap.Markers.Add(element);
                }   
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (GMapMarker element in e.OldItems)
                {
                    gmap.Markers.Remove(element);
                }
            }
        }
        private void BuildMarkers(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType, object toolTip)
        {
            var iOverlay = _mapOverlayService.CreateOverlay(overlayName);
            TryAddOverlayInGmapControlOverlays(iOverlay);
            _mapOverlayService.AddMarkers(locations, overlayName, markerType, toolTip);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }
        private void BuildRoutes(string overlayName, List<List<Latlng>> routes)
        {
            var iOverlay = _mapOverlayService.CreateOverlay(overlayName);
            TryAddOverlayInGmapControlOverlays(iOverlay);
            _mapOverlayService.AddRoutes(routes, overlayName);
        }
        private void SwitchGMapView(double lat, double lng)
        {
            gmap.Zoom = 12;
            gmap.Position = new PointLatLng(lat, lng);
            gmap.Zoom = 13;
            gmap.ShowCenter = true;
        }
    }
}
