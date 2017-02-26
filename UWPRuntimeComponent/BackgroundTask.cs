using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Xamarin.Forms;

namespace UWPRuntimeComponent
{
    public sealed class BackgroundTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Background Task Running");

            _deferral = taskInstance.GetDeferral();
            try
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

                settings.Values.Add("BackgroundTask", "Hello from UWP");
            }
            catch { }
            _deferral.Complete();
        }

        
    }
}
