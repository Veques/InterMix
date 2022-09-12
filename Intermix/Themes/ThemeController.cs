using System;
using System.Windows;

namespace Intermix.Themes
{
    public class ThemeController
    {
        public static ThemesTypes CurrentTheme { get; set; }

        public enum ThemesTypes
        {
            Light, Dark
        }

        public static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[6]; }
            set { Application.Current.Resources.MergedDictionaries[6] = value; }
        }

        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }

        public static void SetTheme(ThemesTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemesTypes.Light: themeName = "LightTheme"; break;
                case ThemesTypes.Dark: themeName = "DarkTheme"; break;
            }
            try
            {
                if (!string.IsNullOrEmpty(themeName))
                {
                    ChangeTheme(new Uri($"Themes/{themeName}.xaml", UriKind.Relative));
                }
            }
            catch { }
        }
    }
}


