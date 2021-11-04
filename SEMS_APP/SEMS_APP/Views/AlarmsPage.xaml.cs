using SEMS_APP.Interface;
using SEMS_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMS_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmsPage : ContentPage
    {
        AlarmsViewModel viewModel;
        public AlarmsPage()
        {
            InitializeComponent(); 
            viewModel = new AlarmsViewModel();
            this.BindingContext = viewModel;
            MessagingCenter.Subscribe<SubscribeCallback>(this, "MQTTRev", (message) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    gridInverters.View.BeginInit();
                    viewModel.Data2Grid();
                    gridInverters.View.EndInit();
                    gridInverters.View.Refresh();
                });
            });
        }
    }
}