﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HTools.Uwp.Controls.Setting
{
    /// <summary>
    /// Why not Template：UWP doesn't support x:TypeArguments
    /// </summary>
    public class BaseSettingCell : ContentControl
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseSettingCell()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(BaseSettingCell), new PropertyMetadata(string.Empty));



        /// <summary>
        /// 
        /// </summary>
        public Visibility NextVisible
        {
            get { return (Visibility)GetValue(NextVisibleProperty); }
            set { SetValue(NextVisibleProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty NextVisibleProperty =
            DependencyProperty.Register("NextVisible", typeof(Visibility), typeof(BaseSettingCell), new PropertyMetadata(Visibility.Visible));


    }
}
