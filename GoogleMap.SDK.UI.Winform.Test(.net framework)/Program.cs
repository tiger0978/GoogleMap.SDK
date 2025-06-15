using IoC_Container;
using GoogleMap.SDK.API.Commons.Models;
using GoogleMap.SDK.API.Enums;
using GoogleMap.SDK.API;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(IConfiguration), configuration));
            services.AddGoogleMapAPIRegistration();
            var provides = services.BuildServiceProvider();

            services.AddTransient<Form, Form1>();




            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = (Form)provides.GetService(typeof(Form));



            Application.Run(form);
        }
    }
}
