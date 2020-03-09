﻿/*
 *  Copyright (C) 2019 - 2020 by Sven Flossmann
 *  
 *  This file is part of Race Horology.
 *
 *  Race Horology is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  any later version.
 * 
 *  Race Horology is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with Race Horology.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  Diese Datei ist Teil von Race Horology.
 *
 *  Race Horology ist Freie Software: Sie können es unter den Bedingungen
 *  der GNU Affero General Public License, wie von der Free Software Foundation,
 *  Version 3 der Lizenz oder (nach Ihrer Wahl) jeder neueren
 *  veröffentlichten Version, weiter verteilen und/oder modifizieren.
 *
 *  Race Horology wird in der Hoffnung, dass es nützlich sein wird, aber
 *  OHNE JEDE GEWÄHRLEISTUNG, bereitgestellt; sogar ohne die implizite
 *  Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
 *  Siehe die GNU Affero General Public License für weitere Details.
 *
 *  Sie sollten eine Kopie der GNU Affero General Public License zusammen mit diesem
 *  Programm erhalten haben. Wenn nicht, siehe <https://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Globalization;
using System.Windows.Data;

namespace RaceHorologyLib
{
  /// <summary>
  /// Converts a position number into a string. Position 0 is transferred into "./."
  /// </summary>
  public class PositionConverter : IValueConverter
  {
    private bool _inParantheses;
    public PositionConverter()
    {
      _inParantheses = false;
    }


    public PositionConverter(bool inParantheses)
    {
      _inParantheses = inParantheses;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      uint position = (uint)value;

      if (_inParantheses)
      {
        if (position == 0)
          return "(-)";

        return string.Format("({0})", position);
      }
      else
      {
        if (position == 0)
          return "---";

        return position.ToString();
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }


  public class ResultCodeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      try
      {
        RunResult.EResultCode rc = (RunResult.EResultCode)value;

        if (rc == RunResult.EResultCode.Normal)
          return "";

        return rc.ToString();
      }
      catch (Exception)
      {
        return "";
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }


  public class ResultCodeWithCommentConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Length != 2)
        throw new Exception("invalid number of argumnets");

      try
      {
        RunResult.EResultCode rc = (RunResult.EResultCode)values[0];
        string comment = (string)values[1];

        if (rc == RunResult.EResultCode.Normal || rc == RunResult.EResultCode.NotSet)
          return "";

        if (string.IsNullOrEmpty(comment))
          return rc.ToString();
        else
          return string.Format("{0} ({1})", rc.ToString(), comment);
      }
      catch(Exception)
      {
        return "";
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }


  public class ResultTimeAndCodeConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Length != 2)
        throw new Exception("invalid number of argumnets");

      try
      {
        TimeSpan? t = (TimeSpan?)values[0];
        RunResult.EResultCode rc = (RunResult.EResultCode)values[1];

        // Return time
        if (rc == RunResult.EResultCode.Normal)
          return t.ToRaceTimeString();

        if (rc == RunResult.EResultCode.NotSet)
          return "";

        // Return result code
        return rc.ToString();
      }
      catch (Exception)
      {
        return "";
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }


}
