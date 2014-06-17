namespace StreamTest
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codecUI6 = new StreamTest.CodecUI();
            this.codecUI5 = new StreamTest.CodecUI();
            this.codecUI4 = new StreamTest.CodecUI();
            this.codecUI3 = new StreamTest.CodecUI();
            this.codecUI2 = new StreamTest.CodecUI();
            this.codecUI1 = new StreamTest.CodecUI();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(382, 2449);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Codec";
            this.columnHeader1.Width = 131;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "FPS";
            this.columnHeader2.Width = 44;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Frames";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Stream Size";
            this.columnHeader3.Width = 78;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Total Streamed";
            this.columnHeader4.Width = 102;
            // 
            // codecUI6
            // 
            this.codecUI6.Location = new System.Drawing.Point(1070, 645);
            this.codecUI6.Name = "codecUI6";
            this.codecUI6.Size = new System.Drawing.Size(630, 318);
            this.codecUI6.TabIndex = 6;
            this.codecUI6.UnsafeCodec = null;
            this.codecUI6.VideoCodec = null;
            // 
            // codecUI5
            // 
            this.codecUI5.Location = new System.Drawing.Point(434, 645);
            this.codecUI5.Name = "codecUI5";
            this.codecUI5.Size = new System.Drawing.Size(630, 318);
            this.codecUI5.TabIndex = 5;
            this.codecUI5.UnsafeCodec = null;
            this.codecUI5.VideoCodec = null;
            // 
            // codecUI4
            // 
            this.codecUI4.Location = new System.Drawing.Point(1070, 321);
            this.codecUI4.Name = "codecUI4";
            this.codecUI4.Size = new System.Drawing.Size(630, 318);
            this.codecUI4.TabIndex = 3;
            this.codecUI4.UnsafeCodec = null;
            this.codecUI4.VideoCodec = null;
            // 
            // codecUI3
            // 
            this.codecUI3.Location = new System.Drawing.Point(434, 321);
            this.codecUI3.Name = "codecUI3";
            this.codecUI3.Size = new System.Drawing.Size(630, 318);
            this.codecUI3.TabIndex = 2;
            this.codecUI3.UnsafeCodec = null;
            this.codecUI3.VideoCodec = null;
            // 
            // codecUI2
            // 
            this.codecUI2.Location = new System.Drawing.Point(1070, 2);
            this.codecUI2.Name = "codecUI2";
            this.codecUI2.Size = new System.Drawing.Size(630, 318);
            this.codecUI2.TabIndex = 1;
            this.codecUI2.UnsafeCodec = null;
            this.codecUI2.VideoCodec = null;
            // 
            // codecUI1
            // 
            this.codecUI1.Location = new System.Drawing.Point(434, 2);
            this.codecUI1.Name = "codecUI1";
            this.codecUI1.Size = new System.Drawing.Size(630, 318);
            this.codecUI1.TabIndex = 0;
            this.codecUI1.UnsafeCodec = null;
            this.codecUI1.VideoCodec = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1742, 588);
            this.Controls.Add(this.codecUI6);
            this.Controls.Add(this.codecUI5);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.codecUI4);
            this.Controls.Add(this.codecUI3);
            this.Controls.Add(this.codecUI2);
            this.Controls.Add(this.codecUI1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Streaming Library Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CodecUI codecUI1;
        private CodecUI codecUI2;
        private CodecUI codecUI3;
        private CodecUI codecUI4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private CodecUI codecUI5;
        private CodecUI codecUI6;
        private System.Windows.Forms.ColumnHeader columnHeader5;

    }
}

