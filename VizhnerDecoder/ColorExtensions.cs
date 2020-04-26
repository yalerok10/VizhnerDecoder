using System;
using CoreGraphics;
using UIKit;

namespace VizhnerDecoder
{
    public static class ColorExtensions
    {
        public static UIImage ToImage(this UIColor color)
        {
            var rect = new CGRect(0, 0, 1, 1);
            UIGraphics.BeginImageContext(rect.Size);
            var context = UIGraphics.GetCurrentContext();
            context.SetFillColor(color.CGColor);
            context.FillRect(rect);
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return img;
        }
    }
}
