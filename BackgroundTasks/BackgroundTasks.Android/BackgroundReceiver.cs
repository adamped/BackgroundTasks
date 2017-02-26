using Android.Content;
using Android.OS;
using Xamarin.Forms;

namespace BackgroundTasks.Droid
{
    [BroadcastReceiver]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakeLock.Acquire();

            MessagingCenter.Send<object, string>(this, "UpdateLabel", "Hello from Android");

            wakeLock.Release();
        }     
    }
}