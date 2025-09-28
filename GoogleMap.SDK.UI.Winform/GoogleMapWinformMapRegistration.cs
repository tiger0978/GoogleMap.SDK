using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Core.Components.AutoComplete.Presenters;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete.GoogleMap;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete.Views;
using GoogleMap.SDK.UI.Winform.MapOverlays;
using IoC_Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GoogleMap.SDK.UI.Winform
{
    public static class GoogleMapWinformMapRegistration
    {
        public static void AddGoogleMapWinformMapRegistration(this IServiceCollection services)
        {
            services.AddTransient<IAutoCompleteView, PlaceAutoCompleteView>();
            services.AddTransient<IAutoCompleteView, EmployeeAutoCompleteView>();
            services.AddTransient<IGMap, GoogleMapControl>();
            services.AddTransient<IOverlay,MapOverlay>();
        }
    }
}
