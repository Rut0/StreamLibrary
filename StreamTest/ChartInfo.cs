using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace StreamTest
{
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class PerfChartStyle
    {
        public ChartPen VerticalGridPen { get; set; }
        public ChartPen HorizontalGridPen { get; set; }

        public Color BackgroundColorTop { get; set; }
        public Color BackgroundColorBottom { get; set; }

        public bool ShowVerticalGridLines { get; set; }
        public bool ShowHorizontalGridLines { get; set; }
        public bool AntiAliasing { get; set; }

        public PerfChartStyle()
        {
            VerticalGridPen = new ChartPen();
            HorizontalGridPen = new ChartPen();
            ShowVerticalGridLines = true;
            ShowHorizontalGridLines = true;
            AntiAliasing = true;
            BackgroundColorTop = Color.DarkGreen;
            BackgroundColorBottom = Color.DarkGreen;
        }
    }

    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class ChartPen
    {
        private Pen pen;

        public ChartPen()
        {
            pen = new Pen(Color.Black);
        }

        public Color Color
        {
            get { return pen.Color; }
            set { pen.Color = value; }
        }

        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get { return pen.DashStyle; }
            set { pen.DashStyle = value; }
        }

        public float Width
        {
            get { return pen.Width; }
            set { pen.Width = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Pen Pen
        {
            get { return pen; }
        }
    }

    public class ChartLine
    {
        public ChartPen ChartLinePen;
        // Amount of currently visible values (calculated from control width and value spacing)
        public int VisibleValues = 0;
        // Horizontal value space in Pixels
        public int ValueSpacing = 50;
        // The currently highest displayed value, required for Relative Scale Mode
        public double CurrentMaxValue { get; set; }

        // The current average value
        public double AverageValue { get; set; }
        public bool ShowAverageLine = true;
        public bool Fill = false;

        // List of stored values
        public List<double> DrawValues = new List<double>(PerformanceChart.MAX_VALUE_COUNT);
        // Value queue for Timer Modes
        public Queue<double> WaitingValues = new Queue<double>();

        public string AverageComment = "";
        public string PeakComment = "";
        public bool DrawLines = true;

        public ChartLine()
        {
            ChartLinePen = new ChartPen();
            CurrentMaxValue = 0;
            AverageValue = 0;
        }
    }
}
