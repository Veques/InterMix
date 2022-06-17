using Intermix.Commands;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.MainWindowView.TicTacToeView;
using System;
using System.Windows.Input;
using System.Windows;
using Intermix.Themes;
using Intermix.Pages.MainWindow;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Intermix.ViewModels
{
    public class Colors
    {
        public string Name {get; set; }
        public SolidColorBrush Brush { get; set; }
    }
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand SaveTheme { get; set; }


        #region Constructor
        public SettingsViewModel()
        {

            if (Config.Default.Theme == 0)
            {
                LightThemeIsChecked = true;
                DarkThemeIsChecked = false;
            }
            else
            {
                DarkThemeIsChecked = true;
                LightThemeIsChecked = false;
            }

            SaveTheme = new RelayCommand(o => Save(), o => true);
            ObservableCollection<Colors> ListOfColors = new()
            {
                new Colors { Name = "Red", Brush = new SolidColorBrush(Color.FromArgb(255,27,161,226)),
                } 
            };
                
        }
        #endregion

        public void Save()
        {
            switch (LightThemeIsChecked)
            {
                case true:
                    ThemeController.SetTheme(ThemeController.ThemesTypes.Light);

                    DarkThemeIsChecked = false;
                    LightThemeIsChecked = true;

                    Config.Default.Theme = 0;
                    Config.Default.Save();


                    break;


                case false:
                    ThemeController.SetTheme(ThemeController.ThemesTypes.Dark);
                    
                    DarkThemeIsChecked = true;
                    LightThemeIsChecked = false;

                    Config.Default.Theme = 1;
                    Config.Default.Save();

                    break;
            }

            //reset aplikacji tutaj jeszcze

            var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source = new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute);

            Color colorr = (Color)myResourceDictionary["LeadingColor"];



        }

        private ObservableCollection<Colors> _listOfColors;
        public ObservableCollection<Colors> ListOfColors
        {
            get { return _listOfColors; }
            set { _listOfColors = value;
                OnPropertyChanged("ListOfColors");
            }
        }

        private SolidColorBrush _chosenColor = Brushes.Red;

        public SolidColorBrush ChosenColor
        {
            get { return _chosenColor; }
            set {
                _chosenColor = value;
                OnPropertyChanged("Red");
            }
        }


        private bool _lightTheme;

        public bool LightThemeIsChecked
        {
            get { return _lightTheme; }
            set { _lightTheme = value;
                OnPropertyChanged("LightThemeIsChecked");
            }
        }

        private bool _darkTheme;

        public bool DarkThemeIsChecked
        {
            get { return _darkTheme; }
            set
            {
                _darkTheme = value;
                OnPropertyChanged("DarkThemeIsChecked");
            }
        }

    }
}
