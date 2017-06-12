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
        private bool _isTracking;

        public void StartTracking()
        {
            _isTracking = true;

            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                LogNewPosition();
                return _isTracking;
            });
        }

        public void StopTracking()
        {
            _isTracking = false;
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
