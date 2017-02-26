
using Android.App;
using Android.Content;
using Android.OS;
using Xamarin.Forms;

namespace BackgroundTasks.Droid
{
    [Service]
    public class PeriodicService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // From shared code or in your PCL
            MessagingCenter.Send<object, string>(this, "UpdateLabel", "Hello from Android");

            return StartCommandResult.NotSticky;
        }

    }
}