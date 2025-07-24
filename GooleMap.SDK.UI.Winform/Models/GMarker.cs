using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.ToolTips;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.UI.Winform.Models
{
    public class GMarker
    {
        public object Tag { get; set; }

        public GMapToolTip ToolTip { get; set; }

        public Location Position {  get; set; }

        public Size Size { get; set; }

        public string ToolTipText { get; set; }

        public bool IsVisible { get; set; }

    }
}
