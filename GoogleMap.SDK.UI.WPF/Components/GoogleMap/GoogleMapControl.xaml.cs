using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.UI.WPF.MapOverlays;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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

        public event EventHandler<MarkerInfo> OnMarkerClicked;

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

        public void CreateMarker(double lat, double lng, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null)
        {
            var location = new Location(lat, lng);
            var locations = new List<Location>();
            locations.Add(location);
            BuildMarkers(overlayName, locations, markerType, data, toolTip);
        }
        public void CreateMarker(IEnumerable<Latlng> list, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null)
        {
            var locations = list.Select(x=> new Location(x.latitude, x.longitude)).ToList();
            BuildMarkers(overlayName, locations, markerType, data, toolTip);
        }
        public void CreateMarker(IEnumerable<Location> locations, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null)
        {
            BuildMarkers(overlayName, locations, markerType, data, toolTip);
        }
        public void CreateMarker(Location location, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null)
        {
            List<Location> locations = new List<Location>();
            locations.Add(location);
            BuildMarkers(overlayName, locations, markerType, data, toolTip);
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

        public List<string> GetOverLays()
        {
            return _mapOverlayService.GetOverLays();
        }
        public void ClearOverlay()
        {
            _mapOverlayService.DeleteOverlay();
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
            Console.WriteLine("test");
            Console.WriteLine(e.Action);
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(GMapMarker element in e.NewItems)
                {
                    gmap.Markers.Add(element);
                }   
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems == null) return;
                foreach (GMapMarker element in e.OldItems)
                {
                    gmap.Markers.Remove(element);
                }
            }

        }
        private void BuildMarkers(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType,object data, object toolTip)
        {
            var iOverlay = _mapOverlayService.CreateOverlay(overlayName);
            TryAddOverlayInGmapControlOverlays(iOverlay);
            _mapOverlayService.AddMarkers(locations, data, OnMarkerClicked, overlayName, markerType, toolTip);
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

        public void ActivateRoute(int index, string overlayName = "MapOverlay")
        {
            var iOverlay = _mapOverlayService.GetOverlay(overlayName);
            TryAddOverlayInGmapControlOverlays(iOverlay);
            iOverlay.ActivateRoute(index);
        }


    }
}
