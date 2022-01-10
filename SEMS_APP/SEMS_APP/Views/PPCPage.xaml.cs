using SEMS_APP.Interface;
using SEMS_APP.ViewModels;
using Syncfusion.SfNumericUpDown.XForms;
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
    public partial class PPCPage : ContentPage
    {
        PPCViewModel viewModel;
        public PPCPage()
        {
            InitializeComponent();
            viewModel = new PPCViewModel();
            this.BindingContext = viewModel;
            MessagingCenter.Subscribe<SubscribeCallback>(this, "MQTTRev", (message) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //gridInverters.View.BeginInit();
                    viewModel.Data2Grid();
                    //gridInverters.View.EndInit();
                    //gridInverters.View.Refresh();
                });
            });
            MessagingCenter.Subscribe<SubscribeCallback>(this, "MQTTPhanhoi", (message) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.ThongBao();
                });
            });
        }

        private void TurnOff_Tap(object sender, EventArgs e)
        {
            //await frameOff.ScaleTo(1.1, 100);
            viewModel.TurnOff();
            //await frameOff.ScaleTo(1, 100);
        }

        private void TurnOn_Tap(object sender, EventArgs e)
        {
            viewModel.TurnOn();
        }

        private void SetPoint_Tap(object sender, EventArgs e)
        {
            viewModel.SetPoints();
        }

        private void SetPoint_ValueChanged(object sender, ValueEventArgs e)
        {
            gridInverters.View.BeginInit();
            viewModel.ChangeSetPoint();
            gridInverters.View.EndInit();
            gridInverters.View.Refresh();
        }

        private void CHECK_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            gridInverters.View.BeginInit();
            viewModel.CHECKStateChanged();
            gridInverters.View.EndInit();
            gridInverters.View.Refresh();
        }
    }
    public class CustomNumericUpDown : SfNumericUpDown
    {
    }
}