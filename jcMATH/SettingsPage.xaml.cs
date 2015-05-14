using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace jcMATH {
    public partial class SettingsPage : PhoneApplicationPage {
        public SettingsPage() {
            InitializeComponent();

            DataContext = new ViewModel.SettingsModel();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
            ((ViewModel.SettingsModel)DataContext).SaveOptions();
        }
    }
}