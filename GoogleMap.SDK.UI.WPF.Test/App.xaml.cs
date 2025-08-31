using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GoogleMap.SDK.Core;
using GoogleMap.SDK.UI.WPF;
using GooleMap.SDK.UI.WPF;



namespace GoogleMap.SDK.UI.WPF.Test
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new IoC_Container.ServiceCollection();
            services.AddGoogleMapCoreRegistration();
            services.AddGoogleMapWPFMapRegistration();
            services.AddTransient<Window, MainWindow>();
            var provides = services.BuildServiceProvider();
            var wpf = (Window)provides.GetService(typeof(Window));
            wpf.Show();
        }
    }
}
