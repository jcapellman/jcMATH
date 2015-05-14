using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcMATH.Objects {
    public class SettingsXML : jcWPLIBRARY.XML.XMLReaderItem {
        public int VersionID { get; set; }

        public int MinimumValue { get; set; }

        public int MaximumValue { get; set; }

        public bool EnableAudio { get; set; }
    }
}
