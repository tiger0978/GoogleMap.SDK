using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.Commons.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleMap.SDK.Core
{
    public class MapOverlayService : IMapOverlayService
    {
        private static Dictionary<string, IOverlay> _overlays = new Dictionary<string, IOverlay>();
        public IOverlay this[string overlayId] => _overlays[overlayId];
        IServiceProvider provider;
        public MapOverlayService(IServiceProvider provider) 
        {
            this.provider = provider;
        }

        public List<string> GetOverLays()
        {
            return _overlays.Keys.ToList();
        }

        public IOverlay CreateOverlay(string overlayId = "MapOverlay")
        {
            if (!_overlays.TryGetValue(overlayId, out IOverlay mapOverlay))
            {
                IOverlay overlayNew = provider.GetService<IOverlay>();
                overlayNew.Id = overlayId;
                mapOverlay = overlayNew;
                _overlays[overlayId] = mapOverlay;
            }
            return mapOverlay;

        }

        public IOverlay AddMarkers(IEnumerable<Location> locations, object data, EventHandler<MarkerInfo> clickEvent, string overlayId = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay iOverLay = CreateOverlay(overlayId);
            iOverLay.SetMarkerOverLay(locations,data,clickEvent, markerType, toolTip);
            return iOverLay;
        }


        public IOverlay AddRoutes(IEnumerable<List<Latlng>> routes, string overlayId)
        {
            IOverlay iOverlay = CreateOverlay(overlayId);
            iOverlay.SetRouteOverLay(routes);
            return iOverlay;
        }

        public void DeleteMarkers(string overlayId)
        {
            if (_overlays.TryGetValue(overlayId, out IOverlay overlay))
            {
                overlay.ClearMarkers();
            }
        }

        public void DeleteRoutes(string overlayId)
        {
            if(_overlays.TryGetValue(overlayId, out IOverlay overlay))
            {
                overlay.ClearRoutes();
            }
        }

        public void DeleteOverlay() 
        {
            foreach(var overlay in _overlays.Values) 
            {
                overlay.ClearAll();
            }
            _overlays.Clear();
        }


        public IOverlay DeleteOverlay(string overlayId = "MapOverlay")
        {
            if (_overlays.TryGetValue(overlayId, out IOverlay overlay))
            {
                overlay.ClearAll();
            }
            return overlay;
        }

        public void DeleteMarkerElement(object element, string overlayId = "MapOverlay")
        {
            if (_overlays.TryGetValue(overlayId, out IOverlay overlay))
            {
                overlay.DeleteMarkerElement(element);
            }
        }

        public void DeleteRouteElement(object element, string overlayId = "MapOverlay")
        {
            if (_overlays.TryGetValue(overlayId, out IOverlay overlay))
            {
                overlay.DeleteRouteElement(element);
            }
        }

        public IOverlay GetOverlay(string overlayId = "MapOverlay")
        {
            _overlays.TryGetValue(overlayId, out IOverlay overlay);
            return overlay;
        }


    }
}
