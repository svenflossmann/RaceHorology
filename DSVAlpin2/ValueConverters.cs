﻿using DSVAlpin2Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DSVAlpin2
{
  class RunResultRunTimeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      RunResult rr = (RunResult)value;

      string result="";

      if (rr.ResultCode == RunResult.EResultCode.Normal)
      {
        if (rr.Runtime != null)
          result = rr.Runtime?.ToString(@"mm\:ss\,ff");
        else
          result = "---";
      }
      else
        result = rr.ResultCode.ToString();

      return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}