using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using jcMATH.Objects;
using jcWPLIBRARY.XML;

namespace jcMATH.ViewModel {
    public class SettingsModel : INotifyPropertyChanged {
        private readonly jcWPLIBRARY.XML.XMLHandler<Objects.SettingsXML> _settingsHandler = new XMLHandler<Objects.SettingsXML>(Common.Constants.APP_FILENAME_SETTINGS);

        public Objects.SettingsXML Settings { get; set; }

        public SettingsModel() {
            var fileResult = _settingsHandler.LoadFile();

            if (!fileResult.HasError && fileResult.Result.Any()) {
                Settings = fileResult.SingleOrDefault;
            } else {
                Settings = new SettingsXML();

                Settings.EnableAudio = true;
                Settings.MaximumValue = 9;
                Settings.MinimumValue = 0;

                _settingsHandler.WriteFile(Settings);
            }
        }

        public void SaveOptions() {
            _settingsHandler.WriteFile(Settings);
        }

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
