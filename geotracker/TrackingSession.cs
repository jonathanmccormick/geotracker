using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using System.Linq;
using System.Xml.Linq;
using PCLStorage;
using System.Threading.Tasks;

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

        public async Task StopTracking()
        {
            _isTracking = false;
            var xml = SerializeToXml();
            string filename = "trip-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            await SaveXmlFile(filename, xml.ToString());
            }

        private XElement SerializeToXml()
        {
			var xml = new XElement("Positions", positions.Select(x => new XElement("position",
																				   new XAttribute("latitude", x.Latitude),
																				   new XAttribute("logitude", x.Longitude),
																				   new XAttribute("heading", x.Heading),
																				   new XAttribute("altitude", x.Altitude))));

            return xml;
        }

        private async Task SaveXmlFile(string fileName, string xml)
        {
			IFolder folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("GeotrackingFiles", CreationCollisionOption.OpenIfExists);
			IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
			await file.WriteAllTextAsync(xml);
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
