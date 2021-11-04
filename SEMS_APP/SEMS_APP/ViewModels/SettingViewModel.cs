using SEMS_APP.Dialog;
using SEMS_APP.Global;
using SEMS_APP.Interface;
using SEMS_APP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SEMS_APP.ViewModels
{
    class SettingViewModel : BaseViewModel
    {
        int _cnt = 0;
        bool _toggledVanTay;
        public bool ToggledVanTay 
        { 
            get { return _toggledVanTay; }
            set
            {
                //if (ToggledVanTay != value)
                {
                    _toggledVanTay = value;
                }
                OnPropertyChanged("ToggledVanTay");
            }
        }

        public SettingViewModel()
        {
            _toggledVanTay = Preferences.Get(Config.AprroveFinger, false);      
            if (!_toggledVanTay)
                _cnt++;
        }
        public async void SetVanTay()
        {
            if (_cnt == 0 && _toggledVanTay == true) return;
            _cnt++;
            var ok = await new MessageXacThucMatKhau().Show();
            if (ok == Global.DialogReturn.OK)
            {
                Preferences.Set(Config.AprroveFinger, _toggledVanTay);
                //await new MessageBox("Cài đặt thành công.").Show();
                DependencyService.Get<IToast>().Show("Cài đặt thành công.");
            }
            else ToggledVanTay = !_toggledVanTay;
        }
    }
}
