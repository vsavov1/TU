﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PS_Calc.Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type t, object parameter, CultureInfo culture)
        {
            int i = 0;

            string s = value as string;
            if (s != null)
                int.TryParse(s, out i);

            return i;
        }

        public object ConvertBack(object value, Type t, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
