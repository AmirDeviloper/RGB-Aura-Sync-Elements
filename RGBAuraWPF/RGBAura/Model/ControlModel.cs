using Options.RGBAura;
using RGBAuraWPF.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model.RGBAura
{
    public class ControlModel 
    {
        internal Control Control { get; set; }
        internal ColorSection ColorSection { get; set; }
        internal Speed Speed{ get; set; } 
        internal TransportType TransportType { get; set; }

        internal int Counter { get; set; }
        internal int ColorSpectrum { get; set; }

        internal bool Start { get; set; }
        internal bool PauseLock { get; set; }
        internal bool RecersiveLock { get; set; }

        

    }
}
