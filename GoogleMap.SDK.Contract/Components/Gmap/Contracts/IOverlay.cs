using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.Components.Gmap.Contracts
{
    public interface IOverlay
    {
        void SetOverLay(IEnumerable<List<Latlng>> lists, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void SetOverLay(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void SetOverLay(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void SetOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void DeleteOverlayElement(object element);
    }
}
