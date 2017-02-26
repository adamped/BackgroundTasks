using Xamarin.Forms;

namespace BackgroundTasks
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object, string>(this, "UpdateLabel", (s, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    BackgroundServiceLabel.Text = e;
                });
            });
        }
    }
}
