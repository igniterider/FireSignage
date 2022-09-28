using System;
using System.Reflection;
using Microsoft.Maui.Graphics.Converters;


namespace FireSignage
{
    public class SolidColorBrushColor : ObservableObject, IEquatable<SolidColorBrushColor>, IComparable<SolidColorBrushColor>
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public string Hex { get; private set; }

        public static List<SolidColorBrushColor> All { get; private set; }

        public bool Equals(SolidColorBrushColor other)
        {
            return Name.Equals(other.Name);
        }

        public int CompareTo(SolidColorBrushColor other)
        {
            return Name.CompareTo(other.Name);
        }

        static SolidColorBrushColor()
        {
            List<SolidColorBrushColor> all = new List<SolidColorBrushColor>();

            // Loop through the properties of the Brush class
            foreach (PropertyInfo propertyInfo in typeof(Brush).GetRuntimeProperties())
            {
                if (propertyInfo.PropertyType == typeof(SolidColorBrush))
                {
                    // Instantiate a SolidColorBrushColor object
                    SolidColorBrush brush = (SolidColorBrush)propertyInfo.GetValue(null);

                    SolidColorBrushColor brushColor = new SolidColorBrushColor
                    {
                        Name = propertyInfo.Name,
                        Color = brush.Color,
                        Hex = brush.Color.ToHex()
                    };

                    all.Add(brushColor);
                }
            }

            all.TrimExcess();
            all.Sort();
            All = all;
        }


        

        ColorTypeConverter colorTypeConverter = new ColorTypeConverter();

        string selectedColorName;
        public string SelectedColorName
        {
            get { return selectedColorName; }
            set
            {
                if (selectedColorName != value)
                {
                    selectedColorName = value;
                    OnPropertyChanged();
                    OnPropertyChanged("SelectedColor");
                }
            }
        }

        public SolidColorBrush SelectedColor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(selectedColorName))
                {
                    return startcolor;
                }
                return (SolidColorBrush)colorTypeConverter.ConvertFromInvariantString(selectedColorName);
            }
        }


        SolidColorBrush startcolor = SolidColorBrush.White;

    }
}