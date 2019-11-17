﻿using DSVAlpin2Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSVAlpin2
{
  /// <summary>
  /// Interaction logic for ChartTestsUC.xaml
  /// </summary>
  public partial class ChartTestsUC : UserControl
  {
    private AppDataModel _dm;
    private Race _race;


    public ChartTestsUC()
    {
      InitializeComponent();
    }


    public void Init(AppDataModel dm, Race race)
    {
      _dm = dm;
      _race = race;


      InitializeComponent();
    }


    public void Display(RaceResultViewProvider results)
    {
      SetupChart(msChart.GetChart(), results);
    }


    public void configureAxis(ChartArea area, RaceResultViewProvider results)
    {
      area.AxisX.Minimum = 0.5; // Double.NaN;
      area.AxisX.Maximum = Double.NaN;

      area.AxisX.CustomLabels.Clear();
      var lr = results.GetView() as System.Windows.Data.ListCollectionView;
      if (lr.Groups != null)
      {
        int x1 = 1;
        int x2Last = 1;
        int x2 = 1;
        string name2=null, name2Last=null;
        foreach (var group in lr.Groups)
        {
          if (group is System.Windows.Data.CollectionViewGroup cvGroup)
          {
            var lblGroup = area.AxisX.CustomLabels.Add( x1 - 0.5, x1 + 0.5, cvGroup.Name.ToString());
            lblGroup.GridTicks = GridTickTypes.None;

            // Second Level if possible
            if (cvGroup.Name is ParticipantClass @class)
            {
              name2 = @class.Group.Name;
              if (name2Last == null) // init set to same name
                name2Last = name2;

              if (!string.Equals(name2, name2Last))
              {
                var lblName2 = area.AxisX.CustomLabels.Add( x2Last - 0.5, (x2-1) + 0.5, name2Last, 1, LabelMarkStyle.LineSideMark);
                name2Last = name2;
                x2Last = x2;
              }
            }
          }

          x1++;
          x2++;
        }

        // Final 2nd group
        if (name2 != null && name2Last != null)
        {
          var lblName2 = area.AxisX.CustomLabels.Add(x2Last - 0.5, (x2-1)+ 0.5, name2Last, 1, LabelMarkStyle.LineSideMark);
        }
      }

      // Enable X axis labels automatic fitting
      area.AxisX.IsLabelAutoFit = true;
      area.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.WordWrap;
      area.AxisX.MajorGrid.Enabled = false;
      area.AxisX.MinorGrid.Enabled = false;

      area.AxisY.Minimum = Double.NaN;
      area.AxisY.Maximum = Double.NaN;
      area.AxisY.IsMarginVisible = false;
      area.AxisY.IsStartedFromZero = false;
      area.AxisY.Title = "Zeit";

      area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
      area.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
      area.AxisY.MajorGrid.Interval = 2.0;

      area.AxisY.MinorGrid.Enabled = true;
      area.AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
      area.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
      area.AxisY.MinorGrid.Interval = 0.5;


      // Enable scale breaks
      area.AxisY.ScaleBreakStyle.Enabled = true;
      area.AxisY.ScaleBreakStyle.BreakLineStyle = BreakLineStyle.Wave;
      area.AxisY.ScaleBreakStyle.Spacing = 2;
      area.AxisY.ScaleBreakStyle.LineWidth = 2;
      area.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Red;
      area.AxisY.ScaleBreakStyle.CollapsibleSpaceThreshold = 25;
      // If all data points are significantly far from zero, the Chart will calculate the scale minimum value
      area.AxisY.ScaleBreakStyle.StartFromZero = StartFromZero.Yes;
    }


    public void addResult(System.Windows.Forms.DataVisualization.Charting.Series ds, object result, int x)
    {
      if (!(result is RaceResultItem item))
        return;

      double timeValue = 0.0;
      if (item.TotalTime != null)
        timeValue = ((TimeSpan)item.TotalTime).TotalMilliseconds / 1000.0;

      if (timeValue > 0.0)
      {
        DataPoint p = new DataPoint(x, timeValue);
        //DataPoint p = new DataPoint(timeValue, x);
        //p.Label = item.Participant.Fullname;
        p.Label = string.Format("{0} {1}.", item.Participant.Name, item.Participant.Firstname.Substring(0,1));

        ds.Points.Add(p);
      }
    }


    protected void createAndAddChartArea(Chart chart, RaceResultViewProvider results)
    {
      if (chart.ChartAreas.Count() == 0)
      {
        ChartArea area = new ChartArea();
        chart.ChartAreas.Add(area);
      }

      configureAxis(chart.ChartAreas[0], results);
    }


    protected void fillDataSeries(System.Windows.Forms.DataVisualization.Charting.Series ds, RaceResultViewProvider results)
    {
      // Populate series data with random data
      var lr = results.GetView() as System.Windows.Data.ListCollectionView;
      if (lr.Groups != null)
      {
        int x = 1;
        foreach (var group in lr.Groups)
        {
          if (group is System.Windows.Data.CollectionViewGroup cvGroup)
          {
            foreach (var result in cvGroup.Items)
              addResult(ds, result, x);

            x++;
          }
        }
      }
    }


    protected void formatDataSeries(System.Windows.Forms.DataVisualization.Charting.Series ds, RaceResultViewProvider results)
    {
      // Set point chart type
      ds.ChartType = SeriesChartType.Point;

      // Enable data points labels
      ds.IsValueShownAsLabel = true;
      ds["LabelStyle"] = "Bottom";

      ds.SmartLabelStyle.Enabled = true;
      ds.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Partial;
      ds.SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Arrow;
      ds.SmartLabelStyle.CalloutLineColor = System.Drawing.Color.Red;
      ds.SmartLabelStyle.CalloutLineWidth = 1;
      ds.SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Underlined;

      // Set marker size
      ds.MarkerSize = 15;

      // Set marker shape
      ds.MarkerStyle = MarkerStyle.Circle;
    }


    public void SetupChart(Chart chart, RaceResultViewProvider results)
    {
      createAndAddChartArea(msChart.GetChart(), results);

      // Setup Data Series
      chart.Series.Clear();
      var ds = new System.Windows.Forms.DataVisualization.Charting.Series();
      fillDataSeries(ds, results);
      formatDataSeries(ds, results);
      msChart.GetChart().Series.Add(ds);

      //msChart.GetChart().SaveImage("", ChartImageFormat.)
    }


  }
}
