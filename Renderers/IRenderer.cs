using System;
using SkiaSharp; 


namespace FireSignage.Renderers
{
    interface IRenderer
    {
        void PaintSurface(SKSurface surface, SKImageInfo info);
        event EventHandler RefreshRequested;
    }
}

