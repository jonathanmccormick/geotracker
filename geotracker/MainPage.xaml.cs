using System.Diagnostics;
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
            var trip = new TrackingSession();
            trip.StartTracking();
        }
    }
}
