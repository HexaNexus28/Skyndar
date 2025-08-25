using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var culture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(culture.IetfLanguageTag)));

            // On injecte le converter global dans les ressources
            Resources.Add("TimeSpanConverter", new TimeSpanToHourMinuteConverter());
        }
    }

    // Le converter directement dans ce fichier
    public class TimeSpanToHourMinuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TimeSpan ts)
                return Binding.DoNothing;

            return ts.ToString(@"hh\:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.TryParse(value?.ToString(), out var ts) ? ts : TimeSpan.Zero;
        }

    }
}
