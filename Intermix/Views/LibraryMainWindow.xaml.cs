using Intermix.Stores;
using Intermix.ViewModels;
using Intermix.ViewModels.LibrarySystem;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Intermix.Views
{
    /// <summary>
    /// Interaction logic for LibraryMainWindow.xaml
    /// </summary>
    public partial class LibraryMainWindow : Window
    {
        public LibraryMainWindow()
        {
            InitializeComponent();

            NavigationStore navigationStore = new();
            navigationStore.CurrentViewModel = new MainLibraryPageViewModel(navigationStore);
            DataContext = new LibraryMainWindowViewModel(navigationStore);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;
        private Duration _duration = new(TimeSpan.FromSeconds(0.2));
        private float _opacity = 0;
        private void Frame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;

                _navArgs = e;
                _opacity = 1;

                DoubleAnimation animation0 = new();
                animation0.From = 1;
                animation0.To = 0;
                animation0.Duration = _duration;
                animation0.Completed += SlideCompleted;
                frame.BeginAnimation(OpacityProperty, animation0);
            }
            _allowDirectNavigation = false;
        }

        private void SlideCompleted(object sender, EventArgs e)
        {
            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                        frame.Navigate(_navArgs.Content);
                    else
                        frame.Navigate(_navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    frame.GoBack();
                    break;
                case NavigationMode.Forward:
                    frame.GoForward();
                    break;
                case NavigationMode.Refresh:
                    frame.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new();
                    animation0.From = 0;
                    animation0.To = _opacity;
                    animation0.Duration = _duration;
                    frame.BeginAnimation(OpacityProperty, animation0);
                });
        }


    }
}
