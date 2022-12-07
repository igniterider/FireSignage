using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace FireSignage.Views.SignDisplay;

public partial class DisplayTest : ContentPage
{
   
	public DisplayTest()
	{
		InitializeComponent();
        
	}

    //public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
    //{
    //    SKImageInfo info = args.Info;
    //    SKSurface surface = args.Surface;
    //    SKCanvas canvas = surface.Canvas;

    //    canvas.Clear();

    //    string str = "UBER";


    //    SKPaint textPaint = new SKPaint
    //    {

    //        Color = SKColors.White
    //    };

    //    // Adjust TextSize property str so text is 95% of screen width
    //    float textWidth = textPaint.MeasureText(str);
    //    textPaint.TextSize = 0.95f * info.Width * textPaint.TextSize / textWidth;

    //    // Find the text bounds
    //    SKRect textBounds = new SKRect();
    //    textPaint.MeasureText(str, ref textBounds);

    //    float xText = info.Width / 2 - textBounds.MidX;

    //    float yText1 = info.Height / 3 - textBounds.MidY;
    //    // And draw the text for text 1
    //    canvas.DrawText(str, xText, yText1 + 50, textPaint);

    //}
}