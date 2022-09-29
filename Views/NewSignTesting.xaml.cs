using System;
using System.Reflection;
using System.Security.Cryptography;

namespace FireSignage.Views;

public partial class NewSignTesting : ContentPage
{
    

    readonly IReadOnlyDictionary<string, Color> colors = typeof(Colors)
        .GetFields(BindingFlags.Static | BindingFlags.Public)
        .ToDictionary(c => c.Name, c => (Color)(c.GetValue(null) ?? throw new InvalidOperationException()));

    public NewSignTesting()  

    {
		InitializeComponent();
        
        

        
         
    }

    protected override void OnAppearing()
    {
        Picker.ItemsSource = colors.Keys.ToList();
        Picker.SelectedIndex = RandomNumberGenerator.GetInt32(Picker.ItemsSource.Count);
    }

    void HandleSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (colors.TryGetValue((string)Picker.SelectedItem, out var color))
        {
           Picker.BackgroundColor = color;
            MainLabel.TextColor = color;
        }
    }

    void Entry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        MainLabel.Text = e.NewTextValue;
    }
}

