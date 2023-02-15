using System.Windows;
using System.Windows.Controls;

namespace SolarisUI.ViewModel
{
    public class CelestialDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item is not CelestialViewModel celestialViewModel)
            {
                return null;
            }

            var window = Application.Current.MainWindow;
            return celestialViewModel.Name switch
            {
                "Sun" => window.FindResource("SunDataTemplate") as DataTemplate,
                _ => window.FindResource("PlanetDataTemplate") as DataTemplate
            };
        }
    }
}
