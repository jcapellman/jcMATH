using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using jcMATH.Objects;
using jcWPLIBRARY.XML;

namespace jcMATH.ViewModel {
    public class MainModel : INotifyPropertyChanged {
        private readonly Random _rnd;

        public int Denominator { get; set; }
        public int Numerator { get; set; }
        public int Selected { get; set; }

        public int Answer { get; set; }

        public Visibility AnswerTextBlock { get; set; }

        private readonly jcWPLIBRARY.XML.XMLHandler<Objects.SettingsXML> _settingsHandler = new XMLHandler<Objects.SettingsXML>(Common.Constants.APP_FILENAME_SETTINGS);

        private readonly Objects.SettingsXML _settings;

        public bool EnableAudio {
            get {
                return _settings.EnableAudio;
            }
        }

        public MainModel() {
            var fileResult = _settingsHandler.LoadFile();

            if (!fileResult.HasError && fileResult.Result.Any()) {
                _settings = fileResult.SingleOrDefault;
            } else {
                _settings = new SettingsXML();

                _settings.EnableAudio = true;
                _settings.MaximumValue = 9;
                _settings.MinimumValue = 0;

                _settingsHandler.WriteFile(_settings);
            }

            _rnd = new Random((int)DateTime.Now.Ticks);
        }

        private int genRandomNumber(int min, int max) {
            return _rnd.Next(min, max);
        }

        public bool CheckSelection() {
            if (Selected == Answer) {
                AnswerTextBlock = Visibility.Visible;
                OnPropertyChanged("AnswerTextBlock");
                return true;
            }

            return false;
        }

        public void setNewNumbers() {
            Selected = 0;

            Numerator = genRandomNumber(_settings.MinimumValue, _settings.MaximumValue);
            Denominator = genRandomNumber(_settings.MinimumValue, _settings.MaximumValue);

            Answer = Numerator + Denominator;
            AnswerTextBlock = Visibility.Collapsed;

            OnPropertyChanged("Answer");
            OnPropertyChanged("Numerator");
            OnPropertyChanged("Denominator");
            OnPropertyChanged("AnswerTextBlock");
            OnPropertyChanged("ResetView");
        }

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
