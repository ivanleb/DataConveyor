using DataConveyor.Views.WPF.Converters;
using Microsoft.Extensions.Configuration;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using WritableJsonConfiguration;

namespace DataConveyor.WPF.Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.LoadWithPartialName("DataConveyor.Views.WPF"));
            Locator.CurrentMutable.RegisterConstant(new ConverterBoolAndVisibility(), typeof(IBindingTypeConverter));

            IConfigurationRoot configuration = WritableJsonConfigurationFabric.Create("Settings.json");
            configuration.GetSection("Appearance:DarkThemePath").Set("Themes/Dark.xaml");
            configuration.GetSection("Appearance:LightThemePath").Set("Themes/Light.xaml");
            Locator.CurrentMutable.RegisterConstant(configuration, typeof(IConfiguration));
        }
    }
}
