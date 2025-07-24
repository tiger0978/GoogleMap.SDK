using GMap.NET;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.UI.Winform
{
    public abstract class AMapOverlay : GMapOverlay
    {
        public abstract void SetOverLay(IEnumerable<List<Latlng>> lists, GMapToolTip toolTip = null);
        public abstract void SetOverLay(double lat, double lng, GMapToolTip toolTip = null);
        public abstract void SetOverLay(IEnumerable<Latlng> list, GMapToolTip toolTip = null);
        public abstract void SetOverLay(IEnumerable<Location> locations, GMapToolTip toolTip = null);
    }
}
