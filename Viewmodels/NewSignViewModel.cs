using System;
using System.Web;
using FireSignage.Models;
using Microsoft.Maui.Graphics.Converters;
using SkiaSharp.Views.Maui;


namespace FireSignage.Viewmodels
{
    class NewSignViewModel : BaseViewModel


    {
        public ObservableCollection<PreMadeSigns> _getSigns = new ObservableCollection<PreMadeSigns>();

        private ObservableCollection<String> colors = new ObservableCollection<string>();
        private Color selectedColor;
        private Color selectedBackColor;

        private string selectedItem;
        private string selectedbackItem;

        ColorTypeConverter converter = new ColorTypeConverter();

        public Command TextColorCommand { get; set; }

        public Command BackColorCommand { get; set; }

        public Renderers.IRenderer Renderer { get; set; }



        public NewSignViewModel()
        {

            BackColorCommand = new Command(BackColorChange);
            TextColorCommand = new Command(TextColorChange);
            Renderer = new Renderers.SignControlRenderer();


            foreach (var color in typeof(Color).GetFields())
            {
                MyColors.Add(color.Name);
            }



        }


        void StartingColors(string text, string tcolor, string backcolor)
        {

            
            

            Color ctext = (Color)(converter.ConvertFromInvariantString(tcolor));
            var textcolor = ctext.ToSKColor();

            Color bcolor = (Color)(converter.ConvertFromInvariantString(backcolor));
            var backgroundcolor = bcolor.ToSKColor();

            ((Renderers.SignControlRenderer)Renderer).GetString = text;

            ((Renderers.SignControlRenderer)Renderer).TextColor = textcolor;


            ((Renderers.SignControlRenderer)Renderer).FillColor = backgroundcolor;

        }

        void TextColorChange()
        {
            var skcolor = selectedColor.ToSKColor();
            var xamcolor = SelectedColor.ToSKColor();

            ((Renderers.SignControlRenderer)Renderer).TextColor = skcolor;
            ((Renderers.SignControlRenderer)Renderer).TextColor = xamcolor;

        }

        void BackColorChange()
        {
            var skcolor = selectedBackColor.ToSKColor();
            var xamcolor = SelectedBackColor.ToSKColor();

            ((Renderers.SignControlRenderer)Renderer).FillColor = skcolor;
            ((Renderers.SignControlRenderer)Renderer).FillColor = xamcolor;

        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {

            string displaytext = HttpUtility.UrlDecode(query["displaytext"]); ;
            string textcolor = HttpUtility.UrlDecode(query["textcolor"]); ;
            string backgroundcolor = HttpUtility.UrlDecode(query["backgroundcolor"]); ;
            StartingColors(displaytext, textcolor, backgroundcolor);

        }

        public ObservableCollection<String> MyColors
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

                //took out colorchange method


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
                TextColorChange();



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
                BackColorChange();



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




    }
}

