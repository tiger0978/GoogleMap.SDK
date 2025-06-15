using IoC_Container;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using GoogleMap.SDK.API;
namespace GoogleMap.SDK.UI.Winform.Test
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(IConfiguration), configuration));
            services.AddGoogleMapAPIRegistration();
            var provides = services.BuildServiceProvider();

            services.AddTransient<Form, Form>();

            var form = (Form)provides.GetService(typeof(Form));

            ApplicationConfiguration.Initialize();
            Application.Run(form);
        }
    }
}