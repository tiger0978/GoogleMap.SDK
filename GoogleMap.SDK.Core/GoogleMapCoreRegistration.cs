using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.API;
using GooleMap.SDK.Core.Components.AutoComplete.Presenters;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;
using Microsoft.Extensions.Configuration;

namespace GoogleMap.SDK.Core
{
    public static class GoogleMapCoreRegistration
    {

        public static void AddGoogleMapCoreRegistration(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            services.Add(new ServiceDescriptor(typeof(IConfiguration), configuration));
            services.AddGoogleMapAPIRegistration();
            services.AddTransient<IAutoCompletePresenter, EmployeeAutoCompletePresenter>();
            services.AddTransient<IAutoCompletePresenter, PlaceAutoCompletePresenter>();
        }
    }
}
