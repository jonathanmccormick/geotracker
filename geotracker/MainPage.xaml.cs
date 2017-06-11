using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace geotracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void StartTracking_Clicked(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Starting tracking...");
            GetGeoData();
        }

        async void GetGeoData()
        {
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync(10000);

			Debug.WriteLine("Position Status: {0}", position.Timestamp);
			Debug.WriteLine("Position Latitude: {0}", position.Latitude);
			Debug.WriteLine("Position Longitude: {0}", position.Longitude);
        }

    }
}
