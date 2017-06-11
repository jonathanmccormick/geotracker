using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace geotracker
{
    public class TrackingSession
    {
        public List<Position> positions = new List<Position>();

        public void StartTracking()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                LogNewPosition();
                return true;
            });
        }

        private async void LogNewPosition()
        {
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync(10000);
             
            positions.Add(position);

            Debug.WriteLine(position.Latitude + ", " + position.Longitude);
        }
    }
}
