using Model.RGBAura;
using Options.RGBAura;
using RGBAuraWPF;
using RGBAuraWPF.Options;
using RGBAuraWPF.Variable;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace RGBAuraWPF
{

    public static class RGBAuraSync // <TControl> where TControl : Control
    {
        

        /// <summary>
        /// End RGB Cycle And Can't Be Reset Or Start.
        /// </summary>
        public static void StopAll()
        {
            if (GlobalVariables.Start)
            {
                GlobalVariables.Start = GlobalVariables.PauseLock = false;
            }
        }
        /// <summary>
        /// Start The Color Cycle Of Methods.
        /// </summary>
        public static void StartAll()
        {
            GlobalVariables.Start = GlobalVariables.PauseLock = true;
        }
        /// <summary>
        /// For Resume RGBAura Sync Methods From Pause Mode.
        /// </summary>
        public static void ResumeAll()
        {
            if (!GlobalVariables.PauseLock)
            {
                GlobalVariables.PauseLock = true;
            }
        }
        /// <summary>
        /// Pause Methods And Take Rest To Them.
        /// </summary>
        public static void PauseAll()
        {
            if (GlobalVariables.PauseLock)
            {
                GlobalVariables.PauseLock = false;
            }
        }

      
        /// <summary>
        /// 'TypeControl' Should One Of The WinForm Controls Like TextBox.
        /// </summary>
        /// <typeparam name = "TypeControl" ></ typeparam >
        /// < param name= "control" ></ param >
        public static Task RGBSpectrumAsync<TypeControl>(
            this TypeControl control,
            Speed speed = Speed.Normal,
            ColorSection colorSection = ColorSection.ForeColor,
            TransportType transportType = TransportType.Normal)
            where TypeControl : Control
        {
            //Initilizer(control);
            int Counter, ColorSpectrum;
            ResetValue(out Counter, out ColorSpectrum);
            //StartAll();
            return Task.Run(() =>
            {
                while (GlobalVariables.Start)
                {
                    RGBAuraEngine.RGBEngine(control, ref Counter, ref ColorSpectrum, speed, colorSection, transportType);
                }
            });
        }

        /// <summary>
        /// This Method , Take All Of Form Controls And Make Theme RGB Async .
        /// </summary>
        /// <param name="window">Use This As Form You Want To Have ColorFull Controls.(use extenstion like this :'this.RGBAllFormControlsAsync(...)')</param>
        /// <param name="speed">Speed Of The RGB Cycle , Select What You Want.</param>
        /// <param name="colorSection">BackColor Or ForeColor Of Your Controls.</param>
        /// <returns></returns>
        public static Task RGBFormSpectrumAsync(
            this Window window,
            Speed speed = Speed.Normal,
            ColorSection colorSection = ColorSection.ForeColor,
            TransportType transportType = TransportType.Normal)
        {
            int Counter, ColorSpectrum;
            ResetValue(out Counter, out ColorSpectrum);
            StartAll();
            return Task.Run(() =>
            {
                while (GlobalVariables.Start)
                {
                    foreach (Control control in FindVisualChildren<Control>(window))
                    {
                        RGBAuraEngine.RGBEngine(control, ref Counter, ref ColorSpectrum, speed, colorSection, transportType);
                    }

                }
            });
        }
        public static Task OneColorSpectrumAsync()
        {
            return Task.Run(() =>
            {

            });

        }
        public static Task RGBOneColorAsync(
            this Control control,
            OneColor oneColor,
            Speed speed = Speed.Normal,
            ColorSection colorSection = ColorSection.ForeColor,
            TransportType transportType = TransportType.Recursive)
        {
            int Counter, ColorSpectrum ;
            bool Lock = false;
            ResetValue(out Counter, out ColorSpectrum,oneColor);
            
            return Task.Run(() =>
            {
                while (GlobalVariables.Start)
                {
                    RGBAuraEngine.BreathEngine(control,ref Counter, ref ColorSpectrum,speed,colorSection,transportType,ref Lock);
                }
            });
        }
        public static Task RGBBackGroundFormAsync(
            this Window form,
            Speed speed = Speed.Normal,
            TransportType transportType = TransportType.Normal)
        {
            int Counter, ColorSpectrum;
            ResetValue(out Counter, out ColorSpectrum);
            StartAll();
            return Task.Run(() =>
            {
                while (GlobalVariables.Start)
                {
                    RGBAuraEngine.RGBEngine(
                        form,
                        ref Counter,
                        ref ColorSpectrum,
                        speed,
                        ColorSection.BackColor, transportType);
                    //ItterateControls(form,ref Counter, ref ColorSpectrum, speed);
                }
            });
        }
        //public static Task RGBBreathing

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
                throw new ArgumentNullException("Argument Can't Be Null.");

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child != null && child is T)
                    yield return (T)child;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        private static void ItterateControls(Window window, ref int Counter, ref int ColorSpectrum, Speed speed)
        {
            foreach (Control control in FindVisualChildren<Control>(window))
                control.Background = Brushes.Transparent;
        }

        private static SolidColorBrush FromArgb(int r, int g, int b)
        {
            return new SolidColorBrush(Color.FromArgb(255, (byte)r, (byte)g, (byte)b));
        }
        //private static GradientBrush FromArgbGradient(int r, int g, int b)
        //{
            


        //    return new gradien(Color.FromArgb(255, (byte)r, (byte)g, (byte)b));
        //}
        private static SolidColorBrush ColorPicker(int colorSpectrum, int counter)
        {
            switch (colorSpectrum)
            {
                
                case 1: return FromArgb(GlobalVariables.MAX, GlobalVariables.Min, counter);
                case 2: return FromArgb(counter, GlobalVariables.Min, GlobalVariables.MAX);
                case 3: return FromArgb(GlobalVariables.Min, counter, GlobalVariables.MAX);
                case 4: return FromArgb(GlobalVariables.Min, GlobalVariables.MAX, counter);
                case 5: return FromArgb(counter, GlobalVariables.MAX, GlobalVariables.Min);
                case 6: return FromArgb(GlobalVariables.MAX, counter, GlobalVariables.Min);
                default: return Brushes.White;
            }
        }
        internal static void ColorSectionPicker(Control control, ColorSection colorSection, int colorSpectrum, int counter)
        {

            switch (colorSection)
            {
                case ColorSection.BackColor:
                    control.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        control.Background = ColorPicker(colorSpectrum, counter);
                    }));
                    break;
                case ColorSection.ForeColor:
                    control.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        control.Foreground = ColorPicker(colorSpectrum, counter);
                    }));
                    break;
                default:
                    break;
            }
        }
        internal static void ResetValue(out int Counter, out int ColorSpectrum , OneColor oneColor = OneColor.None)
        {
            Counter = 0;
            ColorSpectrum = oneColor == OneColor.None?1:(int)oneColor;
        }
    }
}
