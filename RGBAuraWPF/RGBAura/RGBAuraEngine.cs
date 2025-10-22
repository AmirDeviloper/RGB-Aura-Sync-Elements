using Options.RGBAura;
using RGBAuraWPF.Options;
using RGBAuraWPF.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RGBAuraWPF
{
    public class RGBAuraEngine
    {
        //public static bool Lock { get; set; }
        internal static void GradientEngine()
        {

        }
        internal static void RGBRecursiveEngine(
            Control control,
            ref int Counter,
            ref int ColorSpectrum,
            Speed speed,
            ColorSection colorSection)
        {
            // TODO => Work Just In One Control
            if (GlobalVariables.PauseLock)
            {
                if (ColorSpectrum > 6)
                {
                    --ColorSpectrum;
                    GlobalVariables.RecersiveLock = true;
                }

                if (Counter > GlobalVariables.Min && ColorSpectrum % 2 == 1)
                {
                    Counter -= GlobalVariables.Min;
                    RGBAuraSync.ColorSectionPicker(control, colorSection, ColorSpectrum, Counter);

                    if (Counter == GlobalVariables.Min)
                        ColorSpectrum--;
                }
                else if (Counter < GlobalVariables.MAX && ColorSpectrum % 2 == 0)
                {
                    Counter += GlobalVariables.Min;
                    RGBAuraSync.ColorSectionPicker(control, colorSection, ColorSpectrum, Counter);

                    if (Counter == GlobalVariables.MAX)
                        ColorSpectrum--;
                }
                if (ColorSpectrum == 0)
                {
                    GlobalVariables.RecersiveLock = false;
                    RGBEngine(control, ref Counter, ref ColorSpectrum, speed, colorSection, TransportType.Normal);
                }
            }
        }
        internal static void RGBEngine(
            Control control,
            ref int Counter,
            ref int ColorSpectrum,
            Speed speed,
            ColorSection colorSection,
            TransportType transportType)
        {
            if (GlobalVariables.PauseLock)
            {
                if (ColorSpectrum > 6 || ColorSpectrum == 0)
                    RGBAuraSync.ResetValue(out Counter, out ColorSpectrum);

                if (Counter < GlobalVariables.MAX && ColorSpectrum % 2 == 1 && !GlobalVariables.RecersiveLock)
                {
                    Counter += GlobalVariables.Min;
                    RGBAuraSync.ColorSectionPicker(control, colorSection, ColorSpectrum, Counter);

                    if (Counter == GlobalVariables.MAX)
                        ColorSpectrum++;
                }
                else if (Counter > GlobalVariables.Min && ColorSpectrum % 2 == 0 && !GlobalVariables.RecersiveLock)
                {
                    Counter -= GlobalVariables.Min;
                    RGBAuraSync.ColorSectionPicker(control, colorSection, ColorSpectrum, Counter);

                    if (Counter == GlobalVariables.Min)
                        ColorSpectrum++;
                }
                if ((ColorSpectrum == 7 && transportType == TransportType.Recursive) || GlobalVariables.RecersiveLock)
                    RGBRecursiveEngine(control, ref Counter, ref ColorSpectrum, speed, colorSection);

                //Delay(speed);
                Task.Delay((int)speed);
                Thread.Sleep((int)speed);
            }
        }

        internal static void BreathEngine(
            Control control,
            ref int Counter,
            ref int ColorSpectrum,
            Speed speed,
            ColorSection colorSection,
            TransportType transportType,
            ref bool Lock)
        {
            if (GlobalVariables.PauseLock)
            {
                if (Counter < GlobalVariables.MAX && !Lock && !GlobalVariables.RecersiveLock)
                {
                    Counter += GlobalVariables.Min;
                    RGBAuraSync.ColorSectionPicker(control, colorSection, ColorSpectrum, Counter);

                    if (Counter == GlobalVariables.MAX)
                        if (transportType == TransportType.Recursive)
                            Lock = true;
                        else if (transportType == TransportType.Normal)
                            Counter = 1;
                }
                else if (Counter > GlobalVariables.Min && Lock && transportType == TransportType.Recursive && !GlobalVariables.RecersiveLock)
                {
                    Counter -= GlobalVariables.Min;
                    RGBAuraSync.ColorSectionPicker(control, colorSection, ColorSpectrum, Counter);

                    if (Counter == GlobalVariables.Min)
                        Lock = false;
                }
                //Delay(speed);
                //Task.Delay((int)speed);
                Thread.Sleep((int)speed);
            }
        }

    }
}
