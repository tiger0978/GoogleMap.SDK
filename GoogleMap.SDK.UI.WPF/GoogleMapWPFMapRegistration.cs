using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.UI.WPF.Components.GoogleMap;
using GoogleMap.SDK.UI.WPF.MapOverlays;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Core.Components.AutoComplete.Presenters;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GoogleMap.SDK.UI.WPF
{
    public static class GoogleMapWPFMapRegistration
    {
        public static void AddGoogleMapWPFMapRegistration(this IServiceCollection services)
        {
            services.AddTransient<IAutoCompleteView, PlaceAutoCompleteView>();
            services.AddTransient<IAutoCompleteView, EmployeeAutoCompleteView>();
            services.AddTransient<IGMap, GoogleMapControl>();
            services.AddTransient<IOverlay, MapOverlay>();
        }
    }
}
