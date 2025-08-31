using GoogleMap.SDK.UI.WPF.Components.GoogleMap;
using GooleMap.SDK.Contract;
using GooleMap.SDK.Core.Components.AutoComplete.Presenters;
using GooleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.UI.WPF
{
    public static class GoogleMapWPFMapRegistration
    {
        public static void AddGoogleMapWPFMapRegistration(this IServiceCollection services)
        {
            services.AddTransient<IAutoCompleteView, PlaceAutoCompleteView>();
            services.AddTransient<IAutoCompleteView, EmployeeAutoCompleteView>();
            services.AddTransient<IGMap, GoogleMapControl>();
        }
    }
}
