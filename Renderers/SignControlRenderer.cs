using System;
using SkiaSharp;

namespace FireSignage.Renderers
{


    class SignControlRenderer : IRenderer
    {
        private SKCanvas privcanvas;
        private SKColor _fillColor;
        private SKColor _textColor;
        private string _getstring;

        public void PaintSurface(SKSurface surface, SKImageInfo info)
        {


            privcanvas = surface.Canvas;

            privcanvas.Clear();

            string str = "UBER 4";

            
            SKPaint textPaint = new SKPaint
            {

                Color = SKColors.Red                     /*TextColor*/


            };

            // Adjust TextSize property str so text is 95% of screen width
            float textWidth = textPaint.MeasureText(str);
            textPaint.TextSize = 0.95f * info.Width * textPaint.TextSize / textWidth;

            // Find the text bounds
            SKRect textBounds = new SKRect();
            textPaint.MeasureText(str, ref textBounds);

            float xText = info.Width / 2 - textBounds.MidX;

            float yText1 = info.Height / 3 - textBounds.MidY;
            // And draw the text for text 1
            privcanvas.DrawText(str, xText, yText1 + 50, textPaint);



        }



        public SKColor FillColor
        {
            get => _fillColor;
            set
            {
                if (_fillColor != value)
                {
                    _fillColor = value;
                    RefreshRequested?.Invoke(this, EventArgs.Empty);

                }

            }
        }

        public string GetString
        {
            get => _getstring;
            set
            {
                if (_getstring != value)
                {
                    _getstring = value;
                    RefreshRequested?.Invoke(this, EventArgs.Empty);
                }

            }
        }

        public SKColor TextColor
        {
            get => _textColor;
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    RefreshRequested?.Invoke(this, EventArgs.Empty);
                }

            }
        }


        public event EventHandler RefreshRequested;
    }
}

