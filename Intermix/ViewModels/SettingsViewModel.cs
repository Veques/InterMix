using Intermix.Commands;
using Intermix.Themes;
using Intermix.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Intermix.ViewModels
{
    #region Models 
    public class LeadingColors
    {
        public SolidColorBrush color { get; set; }
    }
    public class SecondaryColors
    {
        public SolidColorBrush color { get; set; }
    }

    #endregion


    public class SettingsViewModel : BaseViewModel
    {
        public ICommand SaveTheme { get; set; }
        
        private static bool firstTime = true;

        #region Constructor
        public SettingsViewModel()
        {
            SaveTheme = new RelayCommand(o => Save(), o => true);

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


            LeadingColorCollection = new ObservableCollection<LeadingColors>()
            {
                
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)191, (byte)255, (byte)0))},
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)255, (byte)0, (byte)175))},
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)224, (byte)255, (byte)0))}, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)58, (byte)0, (byte)255))}, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)255, (byte)0, (byte)0))}, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)3, (byte)250, (byte)224))}, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)141, (byte)0, (byte)255))}
            
            };

            SecondaryColorCollection = new ObservableCollection<SecondaryColors>()
            {

                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)127, (byte)80))},
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)154, (byte)39, (byte)169))},
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)56, (byte)58, (byte)135)) } ,
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)22, (byte)118, (byte)115)) },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)24, (byte)180, (byte)129)) },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)221, (byte)133, (byte)197)) },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)90, (byte)189, (byte)246)) }

            };

            LeadingColorSelection = new();
            SecondaryColorSelection = new();

            for (int i = 0; i < 7; i++)
            {
                LeadingColorSelection.Add(false);
            }

            for (int i = 0; i < 7; i++)
            {
                SecondaryColorSelection.Add(false);
            }

            LeadingColorSelection[Config.Default.Leading] = true;
            SecondaryColorSelection[Config.Default.Secondary] = true;

            LeadingColorChosen = (LeadingColorCollection.ElementAt(Config.Default.Leading).color as SolidColorBrush).Color;
            SecondaryColorChosen = (SecondaryColorCollection.ElementAt(Config.Default.Secondary).color as SolidColorBrush).Color;

            ApplyColors(LeadingColorChosen, SecondaryColorChosen);
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

                    break;

                case false:
                    ThemeController.SetTheme(ThemeController.ThemesTypes.Dark);

                    DarkThemeIsChecked = true;
                    LightThemeIsChecked = false;

                    Config.Default.Theme = 1;

                    break;
            }

            var LeadingIndex = (LeadingColorSelection.ToList().FindIndex(x => x == true));
            var SecondaryIndex = (SecondaryColorSelection.ToList().FindIndex(x => x == true));

            Config.Default.Leading = LeadingIndex;
            Config.Default.Secondary = SecondaryIndex;

            LeadingColorChosen = (LeadingColorCollection.ElementAt(LeadingIndex).color as SolidColorBrush).Color;
            SecondaryColorChosen = (SecondaryColorCollection.ElementAt(SecondaryIndex).color as SolidColorBrush).Color;

            Config.Default.Save();
            ApplyColors(LeadingColorChosen, SecondaryColorChosen);


            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

        }


        private void ApplyColors(Color leading, Color secondary)
        {
            Application.Current.Resources["LeadingColor"] = leading;
            Application.Current.Resources["SecondaryLeadingColor"] = secondary;
        }


        private ObservableCollection<LeadingColors> _leadingColorCollection;
        public ObservableCollection<LeadingColors> LeadingColorCollection
        {
            get { return _leadingColorCollection; }
            set {
                _leadingColorCollection = value;
                OnPropertyChanged("LeadingColorCollection");
            }
        }
        private ObservableCollection<SecondaryColors> _secondaryColorCollection;
        public ObservableCollection<SecondaryColors> SecondaryColorCollection
        {
            get { return _secondaryColorCollection; }
            set
            {
                _secondaryColorCollection = value;
                OnPropertyChanged("SecondaryColorCollection");
            }
        }

        private ObservableCollection<bool> _leadingColorSelection;
        public ObservableCollection<bool> LeadingColorSelection
        {
            get { return _leadingColorSelection; }
            set
            {
                _leadingColorSelection = value;
                OnPropertyChanged("LeadingColorSelection");
            }
        }

        private ObservableCollection<bool> _secondaryColorSelection;
        public ObservableCollection<bool> SecondaryColorSelection
        {
            get { return _secondaryColorSelection; }
            set
            {
                _secondaryColorSelection = value;
                OnPropertyChanged("SecondaryColorSelection");
            }
        }

        private Color _leadingColorChosen ;

        public Color LeadingColorChosen
        {
            get { return _leadingColorChosen; }
            set {
                _leadingColorChosen = value;
                OnPropertyChanged("LeadingColorChosen");
            }
        }

        private Color _secondaryColorChosen;

        public Color SecondaryColorChosen
        {
            get { return _secondaryColorChosen; }
            set
            {
                _secondaryColorChosen = value;
                OnPropertyChanged("SecondaryColorChosen");
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
