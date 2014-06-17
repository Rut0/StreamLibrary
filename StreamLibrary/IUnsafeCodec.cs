using StreamLibrary.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace StreamLibrary
{
    public abstract class IUnsafeCodec
    {
        protected JpgCompression jpgCompression;
        public abstract ulong CachedSize { get; internal set; }

        private int _imageQuality;
        public int ImageQuality
        {
            get { return _imageQuality; }
            set
            {
                _imageQuality = value;
                jpgCompression = new JpgCompression(value);
            }
        }


        public abstract event IVideoCodec.VideoDebugScanningDelegate onCodeDebugScan;
        public abstract event IVideoCodec.VideoDebugScanningDelegate onDecodeDebugScan;

        public IUnsafeCodec(int ImageQuality = 100)
        {
            this.jpgCompression = new JpgCompression(ImageQuality);
            this.ImageQuality = ImageQuality;
        }

        public abstract int BufferCount { get; }
        public abstract CodecOption CodecOptions { get; }
        public abstract unsafe void CodeImage(IntPtr Scan0, Rectangle ScanArea, Size ImageSize, PixelFormat Format, Stream outStream);
        public abstract unsafe Bitmap DecodeData(Stream inStream);
        public abstract unsafe Bitmap DecodeData(IntPtr CodecBuffer, uint Length);
    }
}