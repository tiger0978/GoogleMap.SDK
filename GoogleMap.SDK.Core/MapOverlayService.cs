using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
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
        public IOverlay AddMarkers(IEnumerable<Location> locations, string overlayId, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null)
        {
            IOverlay iOverLay = CreateOverlay(overlayId);
            iOverLay.SetMarkerOverLay(locations, markerType, toolTip);
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
            IOverlay overlay = _overlays[overlayId];
            overlay.ClearMarkers();
        }

        public void DeleteRoutes(string overlayId)
        {
            IOverlay overlay = _overlays[overlayId];
            overlay.ClearRoutes();
        }

        public IOverlay DeleteOverlay(string overlayId = "MapOverlay")
        {
            IOverlay overlay = _overlays[overlayId];
            overlay.ClearAll();
            return overlay;
        }

        public void DeleteMarkerElement(object element, string overlayId = "MapOverlay")
        {
            IOverlay overlay = _overlays[overlayId];
            overlay.DeleteMarkerElement(element);
        }

        public void DeleteRouteElement(object element, string overlayId = "MapOverlay")
        {
            IOverlay overlay = _overlays[overlayId];
            overlay.DeleteRouteElement(element);
        }

        public IOverlay GetOverlay(string overlayId = "MapOverlay")
        {
            IOverlay overlay = _overlays[overlayId];
            return overlay;
        }
    }
}
