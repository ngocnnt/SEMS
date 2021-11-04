using SEMS_APP.Interface;
using SEMS_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMS_APP
{
    public partial class AppShell : Shell
    {
        MqttClientRepository repository = new MqttClientRepository();
        public AppShell()
        {
            InitializeComponent();
            ////đăng ki kênh mqtt       
            //if (MqttClientRepository.client == null)
            //{
            //    MqttClientRepository.client = repository.Create("222.255.138.213", 1883, "lucnv", "lucnv", new List<string> { clsVaribles._topic.data, clsVaribles._topic.phanhoi, clsVaribles._topic.pvstring }, Guid.NewGuid().ToString());//
            //}
            //if (Preferences.Get(Config.FullName, "") != "")
            //    CurrentItem = Login;
            //else
            CurrentItem = Register;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}