using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace jxGameFramework.Media
{
    class LibVLC
    {
        private enum Property
        {
            VideoWidth,
            VideoHeight,
            VideoFPS,
            MediaLength
        };

        [DllImport("LibVLCWrapper")]
        private static extern int Init();

        [DllImport("LibVLCWrapper")]
        private static extern void Destroy();

        [DllImport("LibVLCWrapper")]
        private static extern IntPtr Open(IntPtr URi);

        [DllImport("LibVLCWrapper")]
        private static extern void Close(IntPtr Instance);

        [DllImport("LibVLCWrapper")]
        private static extern IntPtr GetInfo(IntPtr Instance, Property p);

        [DllImport("LibVLCWrapper")]
        private static extern void Seek(IntPtr Instance, long time);

        [DllImport("LibVLCWrapper")]
        private static extern void NextFrame(IntPtr Instance);

        [DllImport("LibVLCWrapper")]
        private static extern IntPtr GetFrame(IntPtr Instance);

        private static bool Initialized = false;

        // The non-static part.

        IntPtr VlcWrapperData;

        public LibVLC(Uri URi)
        {
            if (!Initialized)
                if (Init() == 0)
                    throw new Exception("Initialization of LibVLC failed.");

            VlcWrapperData = Marshal.StringToHGlobalUni(URi.AbsoluteUri);
            if (VlcWrapperData == IntPtr.Zero)
                throw new Exception("Open media file failed.");

            VideoWidth = (UInt32)Marshal.PtrToStructure(GetInfo(VlcWrapperData, Property.VideoWidth), typeof(UInt32));
            VideoWidth = (UInt32)Marshal.PtrToStructure(GetInfo(VlcWrapperData, Property.VideoHeight), typeof(UInt32));
            VideoFPS = (Single)Marshal.PtrToStructure(GetInfo(VlcWrapperData, Property.VideoFPS), typeof(Single));
            MediaLength = (Int64)Marshal.PtrToStructure(GetInfo(VlcWrapperData, Property.MediaLength), typeof(Int64));
        }

        public UInt32 VideoWidth { get; private set; }
        public UInt32 VideoHeight { get; private set; }
        public Single VideoFPS { get; private set; }
        public Int64 MediaLength { get; private set; }
    }
}
