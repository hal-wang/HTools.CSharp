﻿using System;

#if UAP10_0_18362
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using HTools.Uwp.Helpers;
#endif

#if NET452
using System.Windows.Media;
using System.Windows;
#endif

namespace HTools
{
#if UAP10_0_18362||NET452
    /// <summary>
    /// 
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Color GetColor(string str)
        {
            if (str[0] == '#')
            {
                return str.Length switch
                {
                    4 => Color.FromArgb(0xff, Convert.ToByte("0x" + str[1] + str[1], 16), Convert.ToByte("0x" + str[2] + str[2], 16), Convert.ToByte("0x" + str[3].ToString() + str[3], 16)),
                    5 => Color.FromArgb(Convert.ToByte("0x" + str[1] + str[1], 16), Convert.ToByte("0x" + str[2] + str[2], 16), Convert.ToByte("0x" + str[3] + str[3], 16), Convert.ToByte("0x" + str[4] + str[4], 16)),
                    7 => Color.FromArgb(0xff, Convert.ToByte("0x" + str[1] + str[2], 16), Convert.ToByte("0x" + str[3] + str[4], 16), Convert.ToByte("0x" + str[5] + str[6], 16)),
                    9 => Color.FromArgb(Convert.ToByte("0x" + str[1] + str[2], 16), Convert.ToByte("0x" + str[3] + str[4], 16), Convert.ToByte("0x" + str[5] + str[6], 16), Convert.ToByte("0x" + str[7] + str[8], 16)),
                    _ => throw new Exception(),
                };
            }
            else
            {
                var resource = Application.Current.Resources[str];
                if (resource is Color color)
                {
                    return color;
                }
                else if (resource is SolidColorBrush brush)
                {
                    return brush.Color;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="sensory"></param>
        /// <param name="backColor"></param>
        /// <returns></returns>
        public static bool IsDarkColor(Color color, int sensory = 192, Color? backColor = null)
        {
            if (color.A < 255)
            {
                color = MergeAlpha(color, backColor);
            }

            if (color.R * 0.299 + color.G * 0.578 + color.B * 0.114 >= sensory)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Brush GetBrush(string str)
        {
            if (str[0] == '#')
            {
                return new SolidColorBrush(ColorHelper.GetColor(str));
            }
            else
            {
#if UAP10_0_18362
                var resource = Application.Current.Resources[str];
#endif
#if NET452
                var resource = Application.Current.MainWindow.FindResource(str);
#endif
                if (resource is Color color)
                {
                    return new SolidColorBrush(color);
                }
                else if (resource is Brush brush)
                {
                    return brush;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alphaColor"></param>
        /// <param name="backColor"></param>
        /// <returns></returns>
        public static Color MergeAlpha(Color alphaColor, Color? backColor = null)
        {
            if (alphaColor.A == 255) // 半透明
            {
                return alphaColor;
            }

            if (backColor == null)
            {
                backColor = GetBackColor();
            }

            double percent = (double)alphaColor.A / 255;

            alphaColor.R = (byte)(alphaColor.R * percent + backColor.Value.R * (1 - percent));
            alphaColor.G = (byte)(alphaColor.G * percent + backColor.Value.G * (1 - percent));
            alphaColor.B = (byte)(alphaColor.B * percent + backColor.Value.B * (1 - percent));
            alphaColor.A = 0xff;

            return alphaColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Color GetBackColor()
        {
#if UAP10_0_18362
            return ThemeHelper.ElementTheme == ElementTheme.Dark ? Colors.Black : Colors.White;
#elif NET452
            return Colors.White;
#endif
        }
    }

#endif
}