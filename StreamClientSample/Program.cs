using StreamLibrary;
using StreamLibrary.UnsafeCodecs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace StreamClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) //keep connecting
            {
                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect("localhost", 4432);
                    IUnsafeCodec unsafeCodec = new UnsafeStreamCodec(80);

                    Console.WriteLine("connected");

                    while (true)
                    {
                        Bitmap bmp = CaptureScreen();
                        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        Size size = new System.Drawing.Size(bmp.Width, bmp.Height);
                        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                        using (MemoryStream stream = new MemoryStream(10000000)) //allocate already enough memory to make it fast
                        {
                            unsafeCodec.CodeImage(bmpData.Scan0, rect, size, bmp.PixelFormat, stream);

                            if (stream.Length > 0)
                            {
                                //send the stream over to the server
                                //to make it more stable we also send how big the stream of data is
                                socket.Send(BitConverter.GetBytes((int)stream.Length)); //we convert it to INT, safes us 4 bytes
                                socket.Send(stream.GetBuffer(), (int)stream.Length, SocketFlags.None);
                            }
                        }
                        bmp.UnlockBits(bmpData);
                        bmp.Dispose();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }



        private static Bitmap CaptureScreen()
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