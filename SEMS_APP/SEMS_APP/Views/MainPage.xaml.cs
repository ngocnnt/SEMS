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
                switch (Config.ma_dvql)
                {
                    case "CPC":
                        clsVaribles._topic.data = "REALTIME/CPC/#";
                        clsVaribles._topic.pvstring = "PVSTRING/CPC/#";
                        break;
                    case "PQ":
                    case "PP":
                        clsVaribles._topic.data = "REALTIME/CPC/" + Config.ma_dvql + "/#";
                        clsVaribles._topic.pvstring = "PVSTRING/CPC/" + Config.ma_dvql + "/#";
                        break;
                    default:
                        if (Config.ma_dvql.Length == 4) //cty đl
                        {
                            clsVaribles._topic.data = "REALTIME/CPC/" + Config.ma_dvql + "/#";
                            clsVaribles._topic.pvstring = "PVSTRING/CPC/" + Config.ma_dvql + "/#";
                        }
                        else //đvi đl
                        {
                            string dvcha = "";
                            if ((Config.ma_dvql.Substring(0, 2) == "PP") || (Config.ma_dvql.Substring(0, 2) == "PQ"))
                                dvcha = Config.ma_dvql.Substring(0, 2);
                            else dvcha = Config.ma_dvql.Substring(0, 4);
                            if (Config.ma_khang != "")
                            {
                                clsVaribles._topic.data = "REALTIME/CPC/" + dvcha + "/" + Config.ma_dvql + "/" + Config.ma_khang;
                                clsVaribles._topic.pvstring = "PVSTRING/CPC/" + dvcha + "/" + Config.ma_dvql + "/" + Config.ma_khang;
                            }
                            else
                            {
                                clsVaribles._topic.data = "REALTIME/CPC/" + dvcha + "/" + Config.ma_dvql + "/#";
                                clsVaribles._topic.pvstring = "PVSTRING/CPC/" + dvcha + "/" + Config.ma_dvql + "/#";
                            }
                        }
                        break;
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