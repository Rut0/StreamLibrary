using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StreamServerSample
{
    public partial class PerformanceChart : UserControl
    {
        // Keep only a maximum MAX_VALUE_COUNT amount of values; This will allow
        internal const int MAX_VALUE_COUNT = 512;
        // Draw a background grid with a fixed line spacing
        internal const int GRID_SPACING = 16;

        public List<ChartLine> ChartLines;
        private Border3DStyle b3dstyle = Border3DStyle.Sunken;
        private int gridScrollOffset = 0;
        private PerfChartStyle perfChartStyle;
        private int MaxChartHeight = 0;


        public PerformanceChart()
        {
            InitializeComponent();
            this.MaxChartHeight = this.Height;
            perfChartStyle = new PerfChartStyle();

            // Set Optimized Double Buffer to reduce flickering
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Redraw when resized
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Font = SystemInformation.MenuFont;
            this.ChartLines = new List<ChartLine>();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), Description("Appearance and Style")]
        public PerfChartStyle PerfChartStyle
        {
            get { return perfChartStyle; }
            set { perfChartStyle = value; }
        }

        /// <summary>
        /// Clears the whole chart
        /// </summary>
        public void Clear()
        {
            foreach (ChartLine line in ChartLines)
                line.DrawValues.Clear();
            Invalidate();
        }
        public int CalcChartHeight()
        {
            this.MaxChartHeight = this.Height - (ChartLines.Count * 20);
            return this.MaxChartHeight;
        }

        /// <summary> Adds a value to the Chart Line </summary>
        /// <param name="value">progress value</param>
        public void AddValue(ChartLine line, double value)
        {
            if (double.IsNaN(value))
            {
                line.DrawValues.Insert(0, 0);
            }
            else
            {
                // Insert at first position; Negative values are flatten to 0 (zero)
                line.DrawValues.Insert(0, Math.Max(value, 0F));
            }

            // Remove last item if maximum value count is reached
            if (line.DrawValues.Count > MAX_VALUE_COUNT)
                line.DrawValues.RemoveAt(MAX_VALUE_COUNT);

            // Calculate horizontal grid offset for "scrolling" effect
            gridScrollOffset += line.ValueSpacing;
            if (gridScrollOffset > GRID_SPACING)
                gridScrollOffset = gridScrollOffset % GRID_SPACING;

            Invalidate();
        }

        /// <summary>
        /// Calculates the vertical Position of a value in relation the chart size,
        /// Scale Mode and, if ScaleMode is Relative, to the current maximum value
        /// </summary>
        /// <param name="value">performance value</param>
        /// <returns>vertical Point position in Pixels</returns>
        private int CalcVerticalPosition(ChartLine line, decimal value)
        {
            decimal result = Decimal.Zero;
            result = (line.CurrentMaxValue > 0) ? (value * this.MaxChartHeight / (decimal)line.CurrentMaxValue) : 0;
            result = this.Height - result;
            return Convert.ToInt32(Math.Round(result));
        }

        /// <summary>
        /// Returns the currently highest (displayed) value, for Relative ScaleMode
        /// </summary>
        /// <returns></returns>
        private double GetHighestValueForRelativeMode(ChartLine line)
        {
            double maxValue = 0;

            for (int i = 0; i < line.VisibleValues; i++)
            {
                // Set if higher then previous max value
                if (line.DrawValues[i] > maxValue)
                    maxValue = line.DrawValues[i];
            }
            return maxValue;
        }

        /// <summary>
        /// Draws the chart (w/o background or grid, but with border) to the Graphics canvas
        /// </summary>
        /// <param name="g">Graphics</param>
        private void DrawChart(Graphics g)
        {
            float offset = 0;
            foreach (ChartLine line in ChartLines)
            {
                line.VisibleValues = Math.Min(this.Width / line.ValueSpacing, line.DrawValues.Count);
                line.CurrentMaxValue = GetHighestValueForRelativeMode(line);

                // Dirty little "trick": initialize the first previous Point outside the bounds
                Point previousPoint = new Point(Width + line.ValueSpacing, this.MaxChartHeight);
                Point currentPoint = new Point();

                // Only draw average line when possible (visibleValues) and needed (style setting)
                if (line.VisibleValues > 0 && line.ShowAverageLine)
                {
                    line.AverageValue = 0;
                    if (line.DrawLines)
                    {
                        DrawAverageLine(g, line);
                    }
                }

                // Connect all visible values with lines
                for (int i = 0; i < line.VisibleValues; i++)
                {
                    currentPoint.X = previousPoint.X - line.ValueSpacing;
                    currentPoint.Y = CalcVerticalPosition(line, (decimal)line.DrawValues[i]);

                    // Actually draw the line
                    if (line.DrawLines && previousPoint.X >= 0)
                    {
                        if (line.Fill)
                        {
                            GraphicsPath path = new GraphicsPath();
                            path.AddPolygon(new Point[]
                            {
                                new Point(currentPoint.X, currentPoint.Y - 1),
                                new Point(previousPoint.X, previousPoint.Y - 1),
                                new Point(previousPoint.X, this.Height),
                                new Point(currentPoint.X, this.Height),
                            });
                            g.FillPath(new SolidBrush(Color.FromArgb(100, line.ChartLinePen.Color.R, line.ChartLinePen.Color.G, line.ChartLinePen.Color.B)), path);
                        }
                        g.DrawLine(line.ChartLinePen.Pen, previousPoint, currentPoint);
                    }
                    previousPoint = currentPoint;
                }

                // Draw current relative maximum value stirng
                SolidBrush sb = new SolidBrush(line.ChartLinePen.Color);
                g.DrawString("Peak: " + line.CurrentMaxValue.ToString() + " " + line.PeakComment, this.Font, sb, 4.0f, 2.0f + offset);
                offset += 15.0f;
            }

            // Draw Border on top
            ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, b3dstyle);
        }

        private void DrawAverageLine(Graphics g, ChartLine line)
        {
            for (int i = 0; i < line.VisibleValues; i++)
            {
                line.AverageValue += line.DrawValues[i];
            }

            if (!double.IsNaN(line.AverageValue / line.VisibleValues))
                line.AverageValue = line.AverageValue / line.VisibleValues;

            int verticalPosition = CalcVerticalPosition(line, (decimal)line.AverageValue);
            SolidBrush sb = new SolidBrush(line.ChartLinePen.Color);
            g.DrawString("Avg: " + (int)line.AverageValue + " " + line.AverageComment, this.Font, sb, 2.0f, verticalPosition - 25);
            g.DrawLine(line.ChartLinePen.Pen, 0, verticalPosition, Width, verticalPosition);
        }

        /// <summary>
        /// Draws the background gradient and the grid into Graphics <paramref name="g"/>
        /// </summary>
        /// <param name="g">Graphic</param>
        private void DrawBackgroundAndGrid(Graphics g)
        {
            // Draw the background Gradient rectangle
            Rectangle baseRectangle = new Rectangle(0, 0, this.Width, this.Height);
            using (Brush gradientBrush = new LinearGradientBrush(baseRectangle, perfChartStyle.BackgroundColorTop, perfChartStyle.BackgroundColorBottom, LinearGradientMode.Vertical))
            {
                g.FillRectangle(gradientBrush, baseRectangle);
            }

            // Draw all visible, vertical gridlines (if wanted)
            if (perfChartStyle.ShowVerticalGridLines)
            {
                for (int i = Width - gridScrollOffset; i >= 0; i -= GRID_SPACING)
                {
                    g.DrawLine(perfChartStyle.VerticalGridPen.Pen, i, 0, i, this.Height);
                }
            }

            // Draw all visible, horizontal gridlines (if wanted)
            if (perfChartStyle.ShowHorizontalGridLines)
            {
                for (int i = 0; i < Height; i += GRID_SPACING)
                {
                    g.DrawLine(perfChartStyle.HorizontalGridPen.Pen, 0, i, Width, i);
                }
            }
        }

        /// Override OnPaint method
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                CalcChartHeight();
                base.OnPaint(e);

                // Enable AntiAliasing, if needed
                if (perfChartStyle.AntiAliasing)
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                DrawBackgroundAndGrid(e.Graphics);
                DrawChart(e.Graphics);
            }
            catch
            {

            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        private void colorSet_ColorSetChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
