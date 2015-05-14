using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace jcMATH.Common {
    public class Global {
        /// <summary>
        /// Returns the Assembly Version and Unknown if there is problems extracting the version
        /// </summary>
        public static string APP_VERSION {
            get {
                var assembly = Assembly.GetExecutingAssembly().FullName;

                if (assembly.Length < 2) {
                    return "Unknown";
                }

                var fullVersionNumber = assembly.Split('=')[1].Split(',')[0];

                return new Version(fullVersionNumber).ToString();
            }
        }

        public static bool IsDarkTheme() {
            var backgroundBrush = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;

            return backgroundBrush.Color == Color.FromArgb(255, 0, 0, 0);
        }
    }
}
