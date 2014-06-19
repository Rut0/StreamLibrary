namespace StreamServerSample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StreamServerSample.ChartPen chartPen1 = new StreamServerSample.ChartPen();
            StreamServerSample.ChartPen chartPen2 = new StreamServerSample.ChartPen();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.performanceChart1 = new StreamServerSample.PerformanceChart();
            this.Fpslbl = new System.Windows.Forms.Label();
            this.ScreenSizelbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(10, 130);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(717, 428);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // performanceChart1
            // 
            this.performanceChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.performanceChart1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.performanceChart1.Location = new System.Drawing.Point(279, 2);
            this.performanceChart1.Name = "performanceChart1";
            this.performanceChart1.PerfChartStyle.AntiAliasing = true;
            this.performanceChart1.PerfChartStyle.BackgroundColorBottom = System.Drawing.Color.White;
            this.performanceChart1.PerfChartStyle.BackgroundColorTop = System.Drawing.Color.White;
            chartPen1.Color = System.Drawing.Color.Black;
            chartPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen1.Width = 1F;
            this.performanceChart1.PerfChartStyle.HorizontalGridPen = chartPen1;
            this.performanceChart1.PerfChartStyle.ShowHorizontalGridLines = true;
            this.performanceChart1.PerfChartStyle.ShowVerticalGridLines = false;
            chartPen2.Color = System.Drawing.Color.Black;
            chartPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen2.Width = 1F;
            this.performanceChart1.PerfChartStyle.VerticalGridPen = chartPen2;
            this.performanceChart1.Size = new System.Drawing.Size(448, 124);
            this.performanceChart1.TabIndex = 1;
            // 
            // Fpslbl
            // 
            this.Fpslbl.AutoSize = true;
            this.Fpslbl.Location = new System.Drawing.Point(17, 19);
            this.Fpslbl.Name = "Fpslbl";
            this.Fpslbl.Size = new System.Drawing.Size(33, 13);
            this.Fpslbl.TabIndex = 2;
            this.Fpslbl.Text = "FPS: ";
            // 
            // ScreenSizelbl
            // 
            this.ScreenSizelbl.AutoSize = true;
            this.ScreenSizelbl.Location = new System.Drawing.Point(17, 50);
            this.ScreenSizelbl.Name = "ScreenSizelbl";
            this.ScreenSizelbl.Size = new System.Drawing.Size(70, 13);
            this.ScreenSizelbl.TabIndex = 3;
            this.ScreenSizelbl.Text = "Screen Size: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 567);
            this.Controls.Add(this.ScreenSizelbl);
            this.Controls.Add(this.Fpslbl);
            this.Controls.Add(this.performanceChart1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private PerformanceChart performanceChart1;
        private System.Windows.Forms.Label Fpslbl;
        private System.Windows.Forms.Label ScreenSizelbl;
    }
}

