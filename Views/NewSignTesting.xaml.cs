using System;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.ComponentModel;
using Microsoft.Maui.Graphics.Converters;

namespace FireSignage.Views;

public partial class NewSignTesting : ContentPage
{
    private ObservableCollection<String> colors = new ObservableCollection<string>();
    private Color selectedColor;
    private Color selectedBackColor;
    private Color headerTextColor;
    private string selectedItem;
    private string selectedbackItem;
    public event PropertyChangedEventHandler PropertyChanged;
    private SKColor skColorget;
    private SKColor skTextColor;


    public NewSignTesting()
	{
		InitializeComponent();

        skColorget = SKColors.Black;
        skTextColor = SKColors.White;

        foreach (var color in typeof(Color).GetFields())
        {
            Colors.Add(color.Name);
        }
         // this.BindingContext = this;
    }

    //void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
    //{
    //    SKImageInfo info = args.Info;
    //    SKSurface surface = args.Surface;
    //    SKCanvas canvas = surface.Canvas;



    //    canvas.Clear(skColorget);

    //    string str = "UBER";


    //    SKPaint textPaint = new SKPaint
    //    {

    //        Color = skTextColor,
    //        IsAntialias = true

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


    public ObservableCollection<String> Colors
    {
        get
        {
            return colors;
        }
        set
        {
            colors = value;
            OnPropertyChanged("Colors");
        }
    }

    public Color SelectedColor
    {
        get
        {
            if (SelectedItem != null)
                return (Color)typeof(Color).GetField(SelectedItem).GetValue(this);
            return SelectedColor;
        }
        internal set
        {
            selectedColor = value;
        }
    }

    public Color HeaderTextColor
    {
        get
        {
            headerTextColor = Color.FromRgb(-(SelectedColor.Red - 1), -(SelectedColor.Green - 1), -(SelectedColor.Blue - 1));
            return headerTextColor;
        }
        internal set
        {
            headerTextColor = value;
        }
    }

    public string SelectedItem
    {
        get
        {
            return selectedItem;
        }
        set
        {
            selectedItem = value;
            OnPropertyChanged("SelectedItem");
            OnPropertyChanged("SelectedColor");
            OnPropertyChanged("HeaderTextColor");

          //  ChangeTextSurface();

        }

    }

    public string SelectedBackItem
    {
        get
        {
            return selectedbackItem;
        }
        set
        {
            selectedbackItem = value;
            OnPropertyChanged("SelectedBackItem");
            OnPropertyChanged("SelectedBackColor");
            OnPropertyChanged("HeaderTextColor");

           // ChangePaintSurface();

        }

    }

    public Color SelectedBackColor
    {
        get
        {
            if (SelectedBackItem != null)
                return (Color)typeof(Color).GetField(SelectedBackItem).GetValue(this);
            return SelectedBackColor;
        }
        internal set
        {
            selectedColor = value;
        }
    }

    //public void ChangeTextSurface()
    //{
    //    skTextColor = SelectedColor.ToSKColor();
    //    if (skTextColor == null)
    //    {
    //        skTextColor = SKColors.White;
    //    }
    //    Canvas.InvalidateSurface();

    //}

    //public void ChangePaintSurface()
    //{
    //    skColorget = SelectedBackColor.ToSKColor();
    //    if (skColorget == null)
    //    {
    //        skColorget = SKColors.Black;
    //    }

    //    Canvas.InvalidateSurface();
    //}


    public void OnPropertyChanged(string property)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


        Console.WriteLine("Selected Item");
        Console.WriteLine(selectedItem);



        Console.WriteLine("Selected Background Item");
        Console.WriteLine(selectedbackItem);

    }

    
 }

