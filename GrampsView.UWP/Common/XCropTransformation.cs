namespace GrampsView.UWP.Common
{
    using FFImageLoading.Extensions;
    using FFImageLoading.Transformations;
    using FFImageLoading.Work;

    public class XCropTransformation : TransformationBase
    {
        public XCropTransformation() : this(0d, 0d, 100d, 100d)
        {
        }

        public XCropTransformation(double xOffset, double yOffset, double xLength, double yLength)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            XLength = xLength;
            YLength = yLength;
        }

        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public double XLength { get; set; }
        public double YLength { get; set; }

        public override string Key
        {
            get
            {
                return string.Format("CropTransformation,xOffset={1},yOffset={2},xLength={3},yLength={4}",
                 XOffset, YOffset, XLength, YLength);
            }
        }

        protected override BitmapHolder Transform(BitmapHolder bitmapSource, string path, ImageSource source, bool isPlaceholder, string key)
        {
            return ToCropped(bitmapSource, XOffset, YOffset, XLength, YLength);
        }

        public static BitmapHolder ToCropped(BitmapHolder source, double xOffset, double yOffset, double xLength, double yLength)
        {
            int cropX = (int)xOffset;
            int cropY = (int)yOffset;

            int width = (int)xLength;
            int height = (int)yLength;

            // Copy the pixels line by line using fast BlockCopy
            var result = new byte[width * height * 4];

            for (var line = 0; line < height; line++)
            {
                var srcOff = (((int)cropY + line) * source.Width + (int)cropX) * ColorExtensions.SizeOfArgb;
                var dstOff = line * width * ColorExtensions.SizeOfArgb;
                Helpers.BlockCopy(source.PixelData, srcOff, result, dstOff, width * ColorExtensions.SizeOfArgb);
            }

            return new BitmapHolder(result, width, height);
        }

        public static BitmapHolder ToCropped(BitmapHolder source, int x, int y, int width, int height)
        {
            var srcWidth = source.Width;
            var srcHeight = source.Height;

            // Clamp to boundaries
            if (x < 0) x = 0;
            if (x + width > srcWidth) width = srcWidth - x;
            if (y < 0) y = 0;
            if (y + height > srcHeight) height = srcHeight - y;

            // Copy the pixels line by line using fast BlockCopy
            var result = new byte[width * height * 4];

            for (var line = 0; line < height; line++)
            {
                var srcOff = ((y + line) * srcWidth + x) * ColorExtensions.SizeOfArgb;
                var dstOff = line * width * ColorExtensions.SizeOfArgb;
                Helpers.BlockCopy(source.PixelData, srcOff, result, dstOff, width * ColorExtensions.SizeOfArgb);
            }

            return new BitmapHolder(result, width, height);
        }
    }
}