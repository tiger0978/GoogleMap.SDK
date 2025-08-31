using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GooleMap.SDK.Core
{
    public class OverlayFactory
    {
        private static Dictionary<string, IOverlay> _overlays = new Dictionary<string, IOverlay>();
        public static IOverlay Create(MapOverlayType type)
        {
            return Create(type.ToString(), type);
        }
        public static IOverlay Create(string overlayId, MapOverlayType type)
        {
            if (_overlays.TryGetValue(overlayId, out IOverlay mapOverlay))
            {
                return mapOverlay;
            }
            var types = Assembly.GetEntryAssembly().GetTypes();

            var assembyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());
            var overlayType = assembyTypes.FirstOrDefault(x => x.Name.Contains($"{type.ToString()}Overlay"));
            
            if (overlayType == null)
            {
                throw new InvalidOperationException($"找不到符合 {type.ToString()}Overlay 的型別。");
            }
            var instance = (IOverlay)Activator.CreateInstance(overlayType,[overlayId]);
            _overlays.Add(overlayId, instance);
            return instance;
        }
    }
}
