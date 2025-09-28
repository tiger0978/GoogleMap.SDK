using IoC_Container;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete.Views;
using Microsoft.Extensions.DependencyInjection;
using GoogleMap.SDK.Core;
using GoogleMap.SDK.Contracts.GoogleAPI;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;
using GoogleMap.SDK.API;
using GoogleMap.SDK.Core.Components.AutoComplete.Presenters;
using GoogleMap.SDK.UI.Winform;


namespace GoogleMap.SDK.UI.Winform.Test_.net_framework_
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new IoC_Container.ServiceCollection();
            services.AddGoogleMapCoreRegistration();
            services.AddGoogleMapWinformMapRegistration();
            services.AddTransient<Form, Form1>();
           
            var provides = services.BuildServiceProvider();
            var form = (Form)provides.GetService(typeof(Form));
            Application.Run(form);
        }
    }
}
