using Intermix.Themes;
using Intermix.ViewModels;
using System.Windows;


namespace Intermix
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

            if (!Config.Default.FirstTime)
            {
                Application.Current.Resources["LeadingColor"] = Config.Default.LeadingColor; 
                Application.Current.Resources["SecondaryLeadingColor"] = Config.Default.SecondaryColor;
                Config.Default.FirstTime = false;
            }

            var vm = new SettingsViewModel();

            switch(Config.Default.Theme)
            {
                case 0: 
                    ThemeController.SetTheme(ThemeController.ThemesTypes.Light);
                    break;
                case 1: 
                    ThemeController.SetTheme(ThemeController.ThemesTypes.Dark);
                    break;
            }



        }
    }
}
