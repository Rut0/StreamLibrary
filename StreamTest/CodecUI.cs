using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using StreamLibrary;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using StreamLibrary.UnsafeCodecs;

namespace StreamTest
{
    public partial class CodecUI : UserControl
    {
        public int FrameCount = 0;
        public int _videoFPS = 0;
        public int VideoFPS = 0;
        public Stopwatch VideoSW = Stopwatch.StartNew();
        public ulong StreamedSize = 0;
        public Stopwatch PerSecSW = Stopwatch.StartNew();
        public ulong _speedPerSec = 0;
        public ulong SpeedPerSec = 0;
        public int MaxEncodeProcessTime = 0;
        public int MinEncodeProcessTime = int.MaxValue;
        public int MaxDecodeProcessTime = 0;
        public int MinDecodeProcessTime = int.MaxValue;

        public IUnsafeCodec UnsafeCodec { get; set; }
        public IVideoCodec VideoCodec { get; set; }

        public bool IsUnsafe
        {
            get { return UnsafeCodec != null; }
        }

        public CodecUI()
        {
            InitializeComponent();
        }

        public unsafe void RenderBitmap(IntPtr scan0, Bitmap bmp, Rectangle scanArea, Size size, PixelFormat format)
        {
            if (UnsafeCodec == null && VideoCodec == null)
                return;

            Stopwatch CaptureSW = Stopwatch.StartNew();
            CaptureSW.Stop();

            using (MemoryStream stream = new MemoryStream(1000000))
            {
                Stopwatch CodecSW = Stopwatch.StartNew();
                if (IsUnsafe)
                    UnsafeCodec.CodeImage(scan0, scanArea, size, format, stream);
                else
                    VideoCodec.CodeImage(bmp, stream);
                CodecSW.Stop();

                stream.Position = 0;
                Stopwatch DecodecSW = Stopwatch.StartNew();
                Bitmap DecodedImage = null;

                while (stream.Length > 0)
                {
                    stream.Position = 0;
                    if (IsUnsafe)
                    {
                        DecodedImage = UnsafeCodec.DecodeData(stream);
                    }
                    else
                    {
                        DecodedImage = VideoCodec.DecodeData(stream);
                    }
                }

                //DecodedImage = videoCodec.DecodeData(new IntPtr(temp), (uint)stream.Length);
                DecodecSW.Stop();

                if (UnsafeCodec as UnsafeQuickStream != null && DecodedImage != null)
                {
                    UnsafeQuickStream quickStream = UnsafeCodec as UnsafeQuickStream;
                    Graphics g = Graphics.FromImage(DecodedImage);
                    if (quickStream.VerifyPoints != null && quickStream.VerifyPoints.Count > 0)
                        g.DrawRectangles(new Pen(Color.Red, 1), quickStream.VerifyPoints.ToArray());
                    DecodedImage = bmp;
                }

                FrameCount++;
                _videoFPS++;
                StreamedSize += (ulong)stream.Length;
                _speedPerSec += (ulong)stream.Length;

                this.Invoke(new Invoky(() =>
                {
                    if (DecodedImage != null)
                        pictureBox1.Image = (Bitmap)DecodedImage.Clone();

                    if (MaxEncodeProcessTime < CodecSW.ElapsedMilliseconds)
                        MaxEncodeProcessTime = (int)CodecSW.ElapsedMilliseconds;
                    if (MinEncodeProcessTime > CodecSW.ElapsedMilliseconds)
                        MinEncodeProcessTime = (int)CodecSW.ElapsedMilliseconds;

                    if (MaxDecodeProcessTime < DecodecSW.ElapsedMilliseconds)
                        MaxDecodeProcessTime = (int)DecodecSW.ElapsedMilliseconds;
                    if (MinDecodeProcessTime > DecodecSW.ElapsedMilliseconds)
                        MinDecodeProcessTime = (int)DecodecSW.ElapsedMilliseconds;

                    label1.Text = "Capture time: " + CaptureSW.Elapsed.Seconds + ", " + CaptureSW.Elapsed.Milliseconds;
                    label2.Text = "Frames Processed: " + FrameCount;

                    if (IsUnsafe)
                    {
                        label3.Text = "Buffers in codec: " + UnsafeCodec.BufferCount;
                        label4.Text = "Codec: " + UnsafeCodec.GetType().Name;
                        label12.Text = "Cached size: " + Math.Round(((double)UnsafeCodec.CachedSize / 1000F) / 1000F, 2) + "MB";
                        label14.Text = "Image Quality: " + UnsafeCodec.ImageQuality + "%";
                    }
                    else
                    {
                        label3.Text = "Buffers in codec: " + VideoCodec.BufferCount;
                        label4.Text = "Codec: " + VideoCodec.GetType().Name;
                        label12.Text = "Cached size: " + Math.Round(((double)VideoCodec.CachedSize / 1000F) / 1000F, 2) + "MB";
                        label14.Text = "Image Quality: " + VideoCodec.ImageQuality + "%";
                    }

                    label6.Text = "Stream size: " + Math.Round((float)stream.Length / 1000F, 2) + "KB";
                    label7.Text = "Streamed size: " + Math.Round(((double)StreamedSize / 1000F) / 1000F, 2) + "MB";
                    label8.Text = "Codec process time: " + CodecSW.Elapsed.Seconds + ", " + CodecSW.Elapsed.Milliseconds + " (FPS: " + Math.Round(1000F / CodecSW.Elapsed.Milliseconds, 0) + ")";
                    label9.Text = "Video size: " + size.Width + " x " + size.Height;
                    label10.Text = "Decoding process time: " + DecodecSW.Elapsed.Seconds + ", " + DecodecSW.Elapsed.Milliseconds + " (FPS: " + Math.Round(1000F / DecodecSW.Elapsed.Milliseconds, 0) + ")";
                    label15.Text = "Max Encoding process time: " + MaxEncodeProcessTime;
                    label16.Text = "Min Encoding process time: " + MinEncodeProcessTime;
                    label17.Text = "Max Decoding process time: " + MaxDecodeProcessTime;
                    label18.Text = "Min Decoding process time: " + MinDecodeProcessTime;

                    if (VideoSW.ElapsedMilliseconds >= 1000)
                    {
                        VideoFPS = _videoFPS;
                        SpeedPerSec = _speedPerSec;
                        label5.Text = "Video FPS: " + VideoFPS;
                        _videoFPS = 0;
                        VideoSW = Stopwatch.StartNew();
                        label13.Text = "Usage per second: " + Math.Round(((double)SpeedPerSec / 1000F) / 1000F, 2) + "MB";
                        _speedPerSec = 0;
                    }
                }));

                stream.Close();
                stream.Dispose();
            }
        }

        private void onCodeDebug(Rectangle ScanArea)
        {
            using(Bitmap tempBmp = new Bitmap(1920, 1080))
            using (Graphics g = Graphics.FromImage(tempBmp))
            using (Graphics targetG = this.pictureBox1.CreateGraphics())
            {
                g.Clear(Color.Black);
                g.DrawRectangle(new Pen(Color.Red, 3), ScanArea);
                Bitmap tempBmpThumb = (Bitmap)tempBmp.GetThumbnailImage(this.pictureBox1.Width, this.pictureBox1.Height, null, IntPtr.Zero);
                targetG.Clear(Color.Black);
                targetG.DrawImage(tempBmpThumb, 0, 0);

                this.Invoke(new Invoky(() =>
                {
                    label11.Text = "Current debug pos:\r\nX: " + ScanArea.X + "\r\nY: " + ScanArea.Y + "\r\nWidth: " + ScanArea.Width + "\r\nHeight: " + ScanArea.Height;
                }));
                tempBmpThumb.Dispose();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (IsUnsafe)
                    UnsafeCodec.onCodeDebugScan += onCodeDebug;
                else
                    VideoCodec.onCodeDebugScan += onCodeDebug;
            }
            else
            {
                if (IsUnsafe)
                    UnsafeCodec.onCodeDebugScan -= onCodeDebug;
                else
                    VideoCodec.onCodeDebugScan -= onCodeDebug;
            }
        }
    }
}