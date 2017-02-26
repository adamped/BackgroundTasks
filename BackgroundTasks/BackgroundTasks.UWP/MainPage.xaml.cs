using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;

namespace BackgroundTasks.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new BackgroundTasks.App());

            if (IsRegistered())
                Deregister();

            this.Loaded += MainPage_Loaded;
          
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await BackgroundTask();
        }

        private void Deregister()
        {
            var taskName = "BackgroundTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
                if (task.Value.Name == taskName)
                    task.Value.Unregister(true);
        }

        private bool IsRegistered()
        {
            var taskName = "BackgroundTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
                if (task.Value.Name == taskName)
                    return true;

            return false;
        }

        private async Task BackgroundTask()
        {

            BackgroundExecutionManager.RemoveAccess();

            await BackgroundExecutionManager.RequestAccessAsync();

            var builder = new BackgroundTaskBuilder();

            builder.Name = "BackgroundTask";
            builder.TaskEntryPoint = "UWPRuntimeComponent.BackgroundTask";
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));

            BackgroundTaskRegistration task = builder.Register();

            task.Completed += Task_Completed;
        }



        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var key = "BackgroundTask";
            var message = settings.Values[key].ToString();

            // Run your background task code here
            MessagingCenter.Send<object, string>(this, "UpdateLabel", message);
        }
    }
}
