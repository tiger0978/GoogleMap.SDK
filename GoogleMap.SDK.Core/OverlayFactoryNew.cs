using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Core
{
    public class OverlayFactoryNew
    {
        private static Dictionary<string, IOverlayNew> _overlays = new Dictionary<string, IOverlayNew>();
        public static IOverlayNew Create()
        {
            return Create("MapOverlay");
        }
        public static IOverlayNew Create(string overlayId)
        {
            if (_overlays.TryGetValue(overlayId, out IOverlayNew mapOverlay))
            {
                return mapOverlay;
            }
            var types = Assembly.GetEntryAssembly().GetTypes();
            var assembyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());
            var overlayType = assembyTypes.FirstOrDefault(x => x.Name == "MapOverlay");
            if (overlayType == null)
            {
                throw new InvalidOperationException($"找不到符合的型別。");
            }
            var instance = (IOverlayNew)Activator.CreateInstance(overlayType, [overlayId]);
            _overlays.Add(overlayId, instance);
            return instance;
        }
    }
}
