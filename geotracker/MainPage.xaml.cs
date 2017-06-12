using System.Diagnostics;
using Xamarin.Forms;

namespace geotracker
{
    public partial class MainPage : ContentPage
    {
        TrackingSession trip;

        public MainPage()
        {
            InitializeComponent();
        }

        void StartStopTracking_Clicked(object sender, System.EventArgs e)
        {
            if (StartStopTracking.Text == "Start tracking")
            {
                StartStopTracking.Text = "Stop tracking";

				Debug.WriteLine("Starting tracking...");
				trip = new TrackingSession();
				trip.StartTracking();
            }
            else if (StartStopTracking.Text == "Stop tracking")
            {
                trip.StopTracking();
                StartStopTracking.Text = "Start tracking";
                Debug.WriteLine("Stopped");
            }
        }
    }
}
