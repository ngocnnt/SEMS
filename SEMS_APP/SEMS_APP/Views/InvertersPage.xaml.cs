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
    public partial class InvertersPage : ContentPage
    {
        InvertersViewModel viewModel;
        public InvertersPage()
        {
            InitializeComponent();
            viewModel = new InvertersViewModel();
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