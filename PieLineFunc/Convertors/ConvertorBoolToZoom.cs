﻿using System;
using System.Globalization;
using System.Windows.Data;
using LiveCharts;

namespace PieLineFunc.Convertors
{
    public class ConvertorBoolToZoom : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? ZoomingOptions.Xy : ZoomingOptions.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}