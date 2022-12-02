using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics;
using Realms;
using Realms.Sync;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireSignage.Viewmodels
{
    public partial class DeviceSignViewModel : BaseViewModel
    {
        private Realm signrealm;
        string NameofPage;
       
        public DisplaySign getsign { get; }

        private string backcolor;
        private string backcolor2;
        private string displaytext;
        private string displaytext2;
        private string ridername;
        private string textcolor;
        private string textcolor2;

        private string devicename;

        public DeviceSignViewModel()
        {
           
            
        }


        public string DisplayText
        {
            set { SetProperty(ref displaytext, value); }
            get { return displaytext; }


        }

        [RelayCommand]
        async Task Watching()
        {
            var part = App.realmApp.CurrentUser.Id;

            if (signrealm == null)
            {


                var syncConfig = new FlexibleSyncConfiguration(App.realmApp.CurrentUser);

                signrealm = await Realm.GetInstanceAsync(syncConfig);
                var newpage = signrealm.All<DisplaySign>().FirstOrDefault(t => t.OwnerId == App.realmApp.CurrentUser.Id);
                if (newpage == null)
                {
                    Console.WriteLine("realm showing null");
                    var transaction = signrealm.BeginWrite();
                    try
                    {
                        var newsign = new DisplaySign
                        {
                            Displaytext = "UBER",
                            Displaytext2 = "",
                            Id = App.realmApp.CurrentUser.Id,
                            OwnerId = App.realmApp.CurrentUser.Id
                        };
                        DisplayText = newsign.Displaytext;

                        // Perform a write op...
                        signrealm.Add(newsign);
                        // Do other work that needs to be included in
                        // this transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Something went wrong; roll back the transaction
                        transaction.Dispose();
                    }




                }

                newpage.PropertyChanged += async (sender, eventArgs) =>  //changed from no async
                {
                    // pulls current value

                    Debug.WriteLine("Pull value =" + newpage.Displaytext);
                    NameofPage = newpage.Pagename;
                    backcolor = newpage.Backgroundcolor;
                    backcolor2 = newpage.Backgroundcolor2;
                    displaytext = newpage.Displaytext;
                    displaytext2 = newpage.Displaytext2;
                    ridername = newpage.Ridername;
                    textcolor = newpage.Textcolor;
                    textcolor2 = newpage.Textcolor2;
                    devicename = newpage.Devicename2;

                    DisplayText = newpage.Displaytext;
                };
                DisplayText = newpage.Displaytext;
                Console.WriteLine("Display Text = " + DisplayText);
                Console.WriteLine("displaytext = " + displaytext);
                // Later, when you no longer wish to receive notifications
                //watch.Dispose();
                

            }

        }

        

        private async Task ChangePage(string page)
        {

            await Shell.Current.GoToAsync("//" + page);
        }

        //public void Draw(ICanvas canvas, RectF dirtyRect)
        //{
        //    string str = "UBER";
        //    float textWidth = canvas.GetStringSize();
           



        //    canvas.FontColor = Colors.Black;
        //    canvas.FontSize = 30;
        //    canvas.DrawString("UBER",0,100, HorizontalAlignment.Center, VerticalAlignment.Top);


        //}

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            string str = "UBER";


            SKPaint textPaint = new SKPaint
            {

                Color = SKColors.White
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
            canvas.DrawText(str, xText, yText1 + 50, textPaint);

        }
    }
}
