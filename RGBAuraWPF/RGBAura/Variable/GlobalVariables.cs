using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBAuraWPF.Variable
{
    public static class GlobalVariables
    {
        /// <summary>
        /// This Value Minimum Color Value For Drawing.
        /// </summary>
        internal static int Min { get { return 1; } set { value = 1; } }
        /// <summary>
        /// This Value Maximum Color Value For Drawing.
        /// </summary>
        internal static int MAX { get { return 255; } set { value = 255; } }
        /// <summary>
        /// Use For Start The Cycle Of RGBAuraSync
        /// </summary>
        internal static bool Start { get; set; }
        /// <summary>
        /// Use For Pause The Cycle Of RGBAuraSync
        /// </summary>
        internal static bool PauseLock { get; set; }
        /// <summary>
        /// Use For Pause The Cycle Of RGBAuraSync
        /// </summary>
        internal static bool RecersiveLock { get; set; }
    }
}
