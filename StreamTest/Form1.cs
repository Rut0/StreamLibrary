using StreamLibrary;
using StreamLibrary.Codecs;
using StreamLibrary.src;
using StreamLibrary.UnsafeCodecs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StreamTest
{
    public delegate void Invoky();
    public partial class Form1 : Form
    {
        private Stopwatch RefreshSW = Stopwatch.StartNew();

        public Form1()
        {
            InitializeComponent();
            codecUI1.UnsafeCodec = new UnsafeStreamCodec(80);
            codecUI2.UnsafeCodec = new UnsafeOptimizedCodec(80);
            codecUI3.UnsafeCodec = new UnsafeCachedStreamCodec(80);
            //codecUI4.UnsafeCodec = new UnsafeCacheCodec(80);

            AddCodecList(codecUI1);
            AddCodecList(codecUI2);
            AddCodecList(codecUI3);
            //AddCodecList(codecUI4);
        }

        public void AddCodecList(CodecUI UI)
        {
            if (UI.UnsafeCodec == null && UI.VideoCodec == null)
                return;

            listView1.Items.Add(new ListViewItem(new string[]
            {
                UI.IsUnsafe ? UI.UnsafeCodec.GetType().Name : UI.VideoCodec.GetType().Name,
                "0",
                "0",
                "0",
                "0",
            }) { Tag = UI });
        }
        public void UpdateCodecList(CodecUI UI, bool ForceUpdate = false)
        {
            if (RefreshSW.ElapsedMilliseconds <= 1000 && !ForceUpdate)
            {
                return;
            }

            this.Invoke(new Invoky(() =>
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    ListViewItem item = listView1.Items[i];
                    if ((UI.IsUnsafe ? UI.UnsafeCodec.GetType().Name : UI.VideoCodec.GetType().Name) == item.SubItems[0].Text)
                    {
                        item.SubItems[1].Text = UI.VideoFPS.ToString();
                        item.SubItems[2].Text = UI.FrameCount.ToString();
                        item.SubItems[3].Text = Math.Round((((float)UI.SpeedPerSec / 1000F) / 1000F), 2) + "MB";
                        item.SubItems[4].Text = Math.Round((((float)UI.StreamedSize / 1000F) / 1000F), 2) + "MB";
                    }
                }
            }));
            RefreshSW = Stopwatch.StartNew();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(CaptureThread);
        }

        private void CaptureThread(object o)
        {
            while (true)
            {
                Bitmap bmp = CaptureScreen();
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                Size size = new System.Drawing.Size(bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                TestCodec(codecUI1, bmp, bmpData, rect, size);
                TestCodec(codecUI2, bmp, bmpData, rect, size);
                //TestCodec(codecUI3, bmp, bmpData, rect, size);
                //TestCodec(codecUI4);
                //TestCodec(codecUI5);

                bmp.UnlockBits(bmpData);
                bmp.Dispose();
            }

            /*foreach (string file in Directory.GetFiles(@"C:\images\"))
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(file);
                //Bitmap bmp = CaptureScreen();

                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                codecUI1.RenderBitmap(bmpData.Scan0, null, new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                codecUI2.RenderBitmap(bmpData.Scan0, null, new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                bmp.UnlockBits(bmpData);
                codecUI3.RenderBitmap(IntPtr.Zero, bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                codecUI4.RenderBitmap(IntPtr.Zero, (Bitmap)bmp.Clone(), new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                codecUI5.RenderBitmap(IntPtr.Zero, (Bitmap)bmp.Clone(), new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                codecUI6.RenderBitmap(IntPtr.Zero, (Bitmap)bmp.Clone(), new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                bmp.Dispose();

                UpdateCodecList(codecUI1);
                UpdateCodecList(codecUI2);
                UpdateCodecList(codecUI3);
                UpdateCodecList(codecUI4);
                UpdateCodecList(codecUI5);
            }*/
        }

        List<Bitmap> images = new List<Bitmap>();

        private void TestCodec(CodecUI UI, Bitmap bitmap, BitmapData bmpData, Rectangle scanArea, Size ImgSize)
        {
            int i = 0;
            /*Bitmap bmp = (Bitmap)Bitmap.FromFile(@"D:\DragonBox\wallpapers\Space-HD-Wallpaper_Pack2-12.jpg");
            Bitmap bmp2 = (Bitmap)Bitmap.FromFile(@"D:\DragonBox\wallpapers\Penguin FlyingCat.png");
            Bitmap bmpClone = (Bitmap)bmp.Clone();
            Bitmap bmp2Clone = (Bitmap)bmp2.Clone();
            

            bool switcher = false;

            //byte[] temp = new FastBitmap(bmp).ToByteArray();

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            BitmapData bmpData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadWrite, bmp2.PixelFormat);

            while (true)
            {
                Bitmap bmp = CaptureScreen();
                bmp.Save(@"C:\images\" + i + ".png");
                bmp.Dispose();
                i++;
            }*/


            if (UI.UnsafeCodec != null)
            {
                UI.RenderBitmap(bmpData.Scan0, bitmap, scanArea, ImgSize, bitmap.PixelFormat);
            }
            else
            {
                UI.RenderBitmap(IntPtr.Zero, bitmap, scanArea, ImgSize, bitmap.PixelFormat);
            }
            UpdateCodecList(UI);

            /*foreach (string file in Directory.GetFiles(@"C:\images\310.to.Yuma.2007.1080p.BrRip.x264.BOKUTOX (6-4-2014 3-43-10 PM)\", "*.*", SearchOption.TopDirectoryOnly))
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(file);
                //Bitmap bmpClone = (Bitmap)bmp.Clone();

                //Bitmap bmp = CaptureScreen();
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                if (UI.UnsafeCodec != null)
                {
                    Bitmap bmpClone = (Bitmap)bmp.Clone();
                    UI.RenderBitmap(bmpData.Scan0, bmpClone, new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                    bmpClone.Dispose();
                }
                else
                {
                    UI.RenderBitmap(IntPtr.Zero, bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Size(bmp.Width, bmp.Height), bmp.PixelFormat);
                }
                UpdateCodecList(UI);
            }*/
        }

        private Bitmap CaptureScreen()
        {
            Rectangle rect = Screen.AllScreens[0].WorkingArea;

            try
            {
                Bitmap bmpScreenshot = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(0, 0, 0, 0, new Size(bmpScreenshot.Width, bmpScreenshot.Height), CopyPixelOperation.SourceCopy);
                gfxScreenshot.Dispose();
                return bmpScreenshot;
            }
            catch { return new Bitmap(rect.Width, rect.Height); }
        }
    }
}