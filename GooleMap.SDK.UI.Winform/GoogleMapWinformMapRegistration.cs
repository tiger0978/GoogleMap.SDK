using GooleMap.SDK.Core.Components.AutoComplete.Presenters;
using GooleMap.SDK.UI.Winform.Components.AutoComplete.Views;
using IoC_Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.UI.Winform
{
    public static class GoogleMapWinformMapRegistration
    {
        public static void AddGoogleMapWinformMapRegistration(this IServiceCollection services)
        {
            services.AddTransient<IAutoCompleteView, PlaceAutoCompleteView>();
            services.AddTransient<IAutoCompleteView, EmployeeAutoCompleteView>();
        }
    }
}
