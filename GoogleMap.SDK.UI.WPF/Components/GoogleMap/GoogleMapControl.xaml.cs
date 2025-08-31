using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Core;
using GoogleMap.SDK.UI.WPF.MapOverlays;
using GooleMap.SDK.Contract;
using GooleMap.SDK.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public GoogleMapControl()
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
        }

        public void CreateMarker(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            gmap.Markers.Clear();
            var location = new Location(lat, lng);
            var locations = new List<Location>();
            locations.Add(location);
            AddMarkers(locations, markerType, toolTip);
            SwitchGMapView(lat, lng);
        }

        public void CreateMarker(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            gmap.Markers.Clear();
            var locations = list.Select(x=> new Location(x.latitude, x.longitude)).ToList();
            AddMarkers(locations, markerType, toolTip);
            var move = list.FirstOrDefault();
            SwitchGMapView(move.latitude, move.longitude);
        }

        public void CreateMarker(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            gmap.Markers.Clear();
            AddMarkers(locations, markerType, toolTip);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }

        public void CreateMarker(string overlayName, double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            gmap.Markers.Clear();
            var location = new Location(lat, lng);
            var locations = new List<Location>();
            locations.Add(location);
            AddMarkers(locations, markerType, toolTip, overlayName);
            SwitchGMapView(lat, lng);
        }

        public void CreateMarker(string overlayName, IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            gmap.Markers.Clear();
            var locations = list.Select(x => new Location(x.latitude, x.longitude)).ToList();
            AddMarkers(locations, markerType, toolTip, overlayName);
            var move = list.FirstOrDefault();
            SwitchGMapView(move.latitude, move.longitude);
        }

        public void CreateMarker(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            gmap.Markers.Clear();
            AddMarkers(locations, markerType, toolTip, overlayName);
            var move = locations.FirstOrDefault();
            SwitchGMapView(move.latLng.latitude, move.latLng.longitude);
        }

        public void CreateRoute(IEnumerable<Latlng> routePoints, object toolTip = null)
        {
            //gmap.Markers.Clear();
            //IOverlay iRouteOverlay = OverlayFactory.Create(MapOverlayType.Route);
            //AMapOverlay routeOverlay = (AMapOverlay)iRouteOverlay;
            //routeOverlay.SetOverLay(points, GMarkerGoogleType.none, toolTip);
            //foreach (var route in routeOverlay.routes)
            //{
            //    gmap.Markers.Add(route);
            //}
            gmap.Markers.Clear();
            var routes = new List<List<Latlng>>();
            routes.Add(routePoints.ToList());
            IOverlayNew mapOverlay = OverlayFactoryNew.Create("MapOverlay");
            MapOverlay routeOverlay = (MapOverlay)mapOverlay;
            routeOverlay.SetRouteOverLay(routes, GMarkerGoogleType.none, toolTip);
            foreach (var route in routeOverlay.markers)
            {
                gmap.Markers.Add(route);
            }
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }

        public void CreateRoute(IEnumerable<List<Latlng>> routePoints, object toolTip = null)
        {
            //gmap.Markers.Clear();
            //IOverlay iRouteOverlay = OverlayFactory.Create(MapOverlayType.Route);
            //AMapOverlay routeOverlay = (AMapOverlay)iRouteOverlay;
            //routeOverlay.SetOverLay(routePoints, GMarkerGoogleType.none, toolTip);
            //foreach (var route in routeOverlay.routes)
            //{
            //    gmap.Markers.Add(route);
            //}
            gmap.Markers.Clear();
            IOverlayNew mapOverlay = OverlayFactoryNew.Create("MapOverlay");
            MapOverlay routeOverlay = (MapOverlay)mapOverlay;
            routeOverlay.SetRouteOverLay(routePoints, GMarkerGoogleType.none, toolTip);
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }

        public void CreateRoute(IEnumerable<Location> locations, object toolTip = null)
        {
            gmap.Markers.Clear();
            IOverlay iRouteOverlay = OverlayFactory.Create(MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)iRouteOverlay;
            routeOverlay.SetOverLay(locations, GMarkerGoogleType.none, toolTip);
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }

        public void CreateRoute(string overlayName, IEnumerable<Latlng> points, object toolTip = null)
        {
            gmap.Markers.Clear();
            IOverlay iRouteOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)iRouteOverlay;
            routeOverlay.SetOverLay(points, GMarkerGoogleType.none, toolTip);
            foreach (var route in routeOverlay.markers)
            {
                gmap.Markers.Add(route);
            }
        }

        public void CreateRoute(string overlayName, IEnumerable<List<Latlng>> routePoints, object toolTip = null)
        {
            gmap.Markers.Clear();
            IOverlay iRouteOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)iRouteOverlay;
            routeOverlay.SetOverLay(routePoints, GMarkerGoogleType.none, toolTip);
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }

        public void CreateRoute(string overlayName, IEnumerable<Location> locations, object toolTip = null)
        {
            gmap.Markers.Clear();
            IOverlay iRouteOverlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)iRouteOverlay;
            routeOverlay.SetOverLay(locations, GMarkerGoogleType.none, toolTip);
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }


        public void ClearMarkerOverlay()
        {
            gmap.Markers.Clear();
            IOverlay overLay = OverlayFactory.Create(MapOverlayType.Marker);
            AMapOverlay markerOverlay = (AMapOverlay)overLay;
            markerOverlay.ClearAll();
        }

        public void ClearMarkerOverlay(string overlayName)
        {
            gmap.Markers.Clear();
            IOverlay overLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            AMapOverlay markerOverlay = (AMapOverlay)overLay;
            markerOverlay.ClearAll();
        }

        public void ClearRouteOverlay()
        {
            gmap.Markers.Clear();
            IOverlay overlay = OverlayFactory.Create(MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)overlay;
            routeOverlay.ClearAll();
        }

        public void ClearRouteOverlay(string overlayName)
        {
            gmap.Markers.Clear();
            IOverlay overlay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)overlay;
            routeOverlay.ClearAll();
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
        public void DeleteMarkers(List<Location> locations)
        {
            gmap.Markers.Clear();
            IOverlay overLay = OverlayFactory.Create(MapOverlayType.Marker);
            AMapOverlay markerOverlay = (AMapOverlay)overLay;
            markerOverlay.DeleteOverlayElement(locations);
            foreach (var marker in markerOverlay.markers)
            {
                gmap.Markers.Add(marker);
            }
        }

        public void DeleteMarkers(string overlayName, List<Location> locations)
        {
            gmap.Markers.Clear();
            IOverlay overLay = OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            AMapOverlay markerOverlay = (AMapOverlay)overLay;
            markerOverlay.DeleteOverlayElement(locations);
            foreach (var marker in markerOverlay.markers)
            {
                gmap.Markers.Add(marker);
            }
        }

        public void DeleteRoutes(IEnumerable<List<Latlng>> routes)
        {
            gmap.Markers.Clear();
            IOverlay overLay = OverlayFactory.Create(MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)overLay;
            routeOverlay.DeleteOverlayElement(routes);
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }
        public void DeleteRoutes(string overlayName, IEnumerable<List<Latlng>> routes)
        {
            gmap.Markers.Clear();
            IOverlay overLay = OverlayFactory.Create(overlayName, MapOverlayType.Route);
            AMapOverlay routeOverlay = (AMapOverlay)overLay;
            routeOverlay.DeleteOverlayElement(routes);
            foreach (var route in routeOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
        }
        private void AddMarkers(IEnumerable<Location> locations, GMarkerGoogleType markerType, object toolTip, string overlayName = "")
        {
            //IOverlay overlay = string.IsNullOrEmpty(overlayName) ? 
            //        OverlayFactory.Create(MapOverlayType.Marker) : OverlayFactory.Create(overlayName, MapOverlayType.Marker);
            //AMapOverlay markerOverlay = (AMapOverlay)overlay;
            //markerOverlay.SetOverLay(locations, markerType, toolTip);
            //foreach (var marker in markerOverlay.markers)
            //{
            //    gmap.Markers.Add(marker);
            //}
            IOverlayNew mapOverlay = string.IsNullOrEmpty(overlayName) ?
                    OverlayFactoryNew.Create("MapOverlay") : OverlayFactoryNew.Create(overlayName);
            MapOverlay markerOverlay = (MapOverlay)mapOverlay;

            markerOverlay.SetMarkerOverLay(locations, markerType, toolTip);
            foreach (var marker in markerOverlay.markers)
            {
                gmap.Markers.Add(marker);
            }
            foreach (var route in markerOverlay.routes)
            {
                gmap.Markers.Add(route);
            }
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
