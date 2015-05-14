using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Windows.Phone.Speech.Synthesis;

using jcMATH.ViewModel;

namespace jcMATH {
    public partial class MainPage : PhoneApplicationPage {
        private ViewModel.MainModel _mainModel;

        private bool _isLoading;

        public MainPage() {
            InitializeComponent();

            Microsoft.Phone.Shell.PhoneApplicationService.Current.ApplicationIdleDetectionMode = Microsoft.Phone.Shell.IdleDetectionMode.Disabled;

            Init();
        }

        private void Init() {
            _isLoading = true;

            _mainModel = new MainModel();
            _mainModel.PropertyChanged += _mainModel_PropertyChanged;

            DataContext = _mainModel;

            LayoutRoot.Tap += LayoutRoot_Tap;
            _mainModel.setNewNumbers();
        }

        void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            if (!_mainModel.CheckSelection()) {
                return;
            }

            _isLoading = true;
                     
            _mainModel.setNewNumbers();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            Init();
        }

        void _mainModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "ResetView":                    
                    addDots(_mainModel.Denominator, spDenominator);
                    addDots(_mainModel.Numerator, spNumerator);

                    _isLoading = false;

                    break;
            }
        }

        private void addDots(int number, StackPanel stackPanelImages) {            
            stackPanelImages.Children.Clear();

            var tmpSP = new StackPanel();
            tmpSP.Orientation = System.Windows.Controls.Orientation.Horizontal;
            tmpSP.Margin = new Thickness(0,0,0,0);

            var count = 0;

            for (var x = 0; x < Math.Abs(number); x++) {
                if (count == 6) {
                    stackPanelImages.Children.Add(tmpSP);
                    count = 1;

                    tmpSP = new StackPanel();
                    tmpSP.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    tmpSP.Margin = new Thickness(0, 10, 0, 0);
                } else {
                    count++;
                }

                var imgDot = new Image();
                imgDot.Source = new BitmapImage(new Uri("/Images/EmptyDot.png", UriKind.Relative));
                imgDot.Margin = new Thickness(0, 0, 10, 0);
                imgDot.Width = 64;
                imgDot.Height = 64;

                var gl = GestureService.GetGestureListener(imgDot);
                gl.Tap += gl_Tap;
                tmpSP.Children.Add(imgDot);
            }

            if (count != 0) {
                stackPanelImages.Children.Add(tmpSP);
            }
        }

        async void gl_Tap(object sender, Microsoft.Phone.Controls.GestureEventArgs e) {
            if (_isLoading) {
                return;
            }

            var img = (Image) sender;

            if (img.Tag != null && img.Tag.ToString() == "selected") {
                return;
            }

            _mainModel.Selected++;
            
            img.Source = new BitmapImage(new Uri("/Images/Dot.png", UriKind.Relative));
            img.Tag = "selected";

            if (!_mainModel.CheckSelection()) {
                return;
            }

            if (!_mainModel.EnableAudio) {
                return;
            }

            var _synthesizer = new SpeechSynthesizer();

            await _synthesizer.SpeakTextAsync(_mainModel.Answer.ToString(CultureInfo.InvariantCulture));
        }

        private void BtnAbout_OnClick(object sender, EventArgs e) {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void BtnSettings_OnClick(object sender, EventArgs e) {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
    }
}
