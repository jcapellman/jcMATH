using System.Windows;
using Microsoft.Phone.Controls;

namespace jcMATH {
    public partial class AboutPage : PhoneApplicationPage {
        public AboutPage() {
            InitializeComponent();
            txtBlockVersion.Text = "Version " + Common.Global.APP_VERSION;
        }

        private void BtnChangeLog_OnClick(object sender, RoutedEventArgs e) {
            rwChangeLog.IsOpen = true;
        }
    }
}