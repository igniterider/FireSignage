using Microsoft.Maui.Controls;
using Syncfusion.Maui.ListView;
using System;
using System.Reflection;

namespace FireSignage;

public partial class MainPage : ContentPage
{
    
	public MainPage()
	{
		InitializeComponent();
       
        

        //listView.GroupHeaderTemplate = new DataTemplate(() =>
        //{
        //    var grid = new Grid { BackgroundColor = Color.FromHex("#E4E4E4") };
        //    var label = new Label
        //    {
        //        VerticalOptions = LayoutOptions.Center,
        //        HorizontalOptions = LayoutOptions.Center,
        //        TextColor = Colors.Black,
        //        TextTransform = TextTransform.Uppercase, 
        //    };
        //    label.SetBinding(Label.TextProperty, new Binding("Key"));
        //    grid.Children.Add(label);
        //    return grid;
        //});



        //var group = listView.DataSource.Groups[1];
        //listView.ExpandGroup(group);
        //listView.CollapseGroup(group);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var url = "https://www.igniterider.com";
        await Launcher.OpenAsync(url);
    }
}


