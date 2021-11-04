using SEMS_APP.Global;
using SEMS_APP.Interface;
using SEMS_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMS_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        SettingViewModel viewModel;
        public SettingPage()
        {
            InitializeComponent();            
            viewModel = new SettingViewModel();
            this.BindingContext = viewModel;
        }

        private void LogOut_Tap(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
        }

        private void sfSwitch_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            viewModel.SetVanTay();
        }
    }
}