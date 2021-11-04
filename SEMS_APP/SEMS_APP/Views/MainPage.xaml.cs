using SEMS_APP.Global;
using SEMS_APP.Interface;
using SEMS_APP.Models;
using SEMS_APP.ViewModels;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMS_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        MqttClientRepository repository = new MqttClientRepository();
        MainViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            //đăng ki kênh mqtt       
            if (MqttClientRepository.client == null)
            {
                if (Config.ma_khang != "")
                {
                    clsVaribles._topic.data = "REALTIME/CPC/PP/" + Config.ma_dvql + "/" + Config.ma_khang;
                    clsVaribles._topic.pvstring = "PVSTRING/CPC/PP/" + Config.ma_dvql + "/" + Config.ma_khang;
                }
                MqttClientRepository.client = repository.Create("222.255.138.213", 1883, "lucnv", "lucnv", new List<string> { clsVaribles._topic.data, clsVaribles._topic.phanhoi, clsVaribles._topic.pvstring }, Guid.NewGuid().ToString());//
            }
            viewModel = new MainViewModel();
            this.BindingContext = viewModel;
        }

        private void cbDonVi_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
        //    try
        //    {
        //        viewModel.LoadData("CPC");
        //    }
        //    catch (Exception)
        //    {
        //    }
        }        
    }
}