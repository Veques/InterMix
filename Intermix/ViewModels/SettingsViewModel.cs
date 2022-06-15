using Intermix.Commands;
using Intermix.Themes;
using Intermix.ViewModels.Base;
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
        public bool isSelected { get; set; }
    }
    public class SecondaryColors
    {
        public SolidColorBrush color { get; set; }
        public bool isSelected { get; set; }
    }

    #endregion
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

            LeadingColorCollection = new ObservableCollection<LeadingColors>()
            {
                
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)191, (byte)255, (byte)0)), isSelected = false },
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)255, (byte)0, (byte)175)), isSelected = false },
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)224, (byte)255, (byte)0)), isSelected = false }, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)58, (byte)0, (byte)255)), isSelected = false }, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)255, (byte)0, (byte)0)), isSelected = false }, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)3, (byte)250, (byte)224)), isSelected = false }, 
                new LeadingColors { color =new SolidColorBrush (Color.FromArgb(255, (byte)141, (byte)0, (byte)255)), isSelected = false }
            
            };

            SecondaryColorCollection = new ObservableCollection<SecondaryColors>()
            {

                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)127, (byte)80)), isSelected = false },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)154, (byte)39, (byte)169)), isSelected = false },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)56, (byte)58, (byte)135)), isSelected = false },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)22, (byte)118, (byte)115)), isSelected = false },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)24, (byte)180, (byte)129)), isSelected = false },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)221, (byte)133, (byte)197)), isSelected = false },
                new SecondaryColors { color = new SolidColorBrush(Color.FromArgb(255, (byte)90, (byte)189, (byte)246)), isSelected = false }

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

            LeadingColorChosen = (LeadingColorCollection.Where(x => x.isSelected == true).FirstOrDefault().color as SolidColorBrush).Color;
            SecondaryColorChosen = (SecondaryColorCollection.Where(x => x.isSelected == true).FirstOrDefault().color as SolidColorBrush).Color;

            Application.Current.Resources["LeadingColor"] = SecondaryColorChosen;
            Application.Current.Resources["SecondaryLeadingColor"] = LeadingColorChosen;


            //reset aplikacji tutaj jeszcze
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
