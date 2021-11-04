using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using SEMS_APP.Dialog;
using SEMS_APP.Global;
using SEMS_APP.Helpers;
using SEMS_APP.Interface;
using SEMS_APP.Models;
using SEMS_APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SEMS_APP.Models.clsVaribles;

namespace SEMS_APP.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private bool _isShowHidePassword;
        private bool _isAprroveFinger = false;
        string _fullname;
        string _password;
        string _thongbao;
        string _eyeOutline;
        public bool IsPasswordShow { get => _isShowHidePassword; set { SetProperty(ref _isShowHidePassword, value); } }
        public string Password { get => _password; set { SetProperty(ref _password, value); } }
        public string FullName { get => _fullname; set { SetProperty(ref _fullname, value); } }
        public string EyeOutline { get => _eyeOutline; set { SetProperty(ref _eyeOutline, value); } }
        public string THONG_BAO { get => _thongbao; set { SetProperty(ref _thongbao, value); } }
        //ObservableCollection<User> _user;
        //public ObservableCollection<User> Users { get => _user; set { SetProperty(ref _user, value); } }
        User _user;
        public User Users { get => _user; set { SetProperty(ref _user, value); } }

        public Command LoginCommand { get; }
        public Command FingerComand { get; }
        public Command ShowHideTapCommand { get; }

        public RegisterViewModel()
        {
            _fullname = Preferences.Get(Config.FullName, "");
            _isAprroveFinger = Preferences.Get(Config.AprroveFinger, false);
            IsPasswordShow = true;
            EyeOutline = FontIconClass.EyeOffOutline;
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            FingerComand = new Command(OnFingerCLicked);
            this.PropertyChanged +=
                                  (_, __) => LoginCommand.ChangeCanExecute();
            ShowHideTapCommand = new Command(OnShowHidePassClicked);
        }

        private bool ValidateLogin(object arg)
        {
            return !String.IsNullOrWhiteSpace(_fullname)
                && !String.IsNullOrWhiteSpace(_password);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                ShowLoading("Đang kiểm tra vui lòng đợi");
                await Task.Delay(2000);
                var data = new
                {
                    username = _fullname,
                    password = _password,
                    platform = DeviceInfo.Platform.ToString(),
                    firebase = Config.device_token
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/Login", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                USER_Response contents = JsonConvert.DeserializeObject<USER_Response>(responseContent);
                if (contents.code == HttpStatusCode.OK)
                {
                    Users = contents.data;
                    Preferences.Set(Config.Token, Users.TOKEN);
                    Preferences.Set(Config.FullName, _fullname);
                    Config.ma_dvql = (Users.MA_DVIQLY == null) ? "" : Users.MA_DVIQLY;
                    Config.ma_khang = (Users.MA_KHANG == null) ? "" : Users.MA_KHANG;
                    HideLoading();
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    DependencyService.Get<IStatusBar>().ShowStatusBar();
                }
                else
                {
                    HideLoading();
                    await new MessageBox("User của bạn chưa kích hoạt / sai mật khẩu. Vui lòng thử lại").Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                HideLoading();
            }
        }

        private void OnShowHidePassClicked(object obj)
        {
            IsPasswordShow = !IsPasswordShow;
            if (!IsPasswordShow)
                EyeOutline = FontIconClass.EyeOutline;
            else EyeOutline = FontIconClass.EyeOffOutline;
        }

        private async void OnFingerCLicked(object obj)
        {
            if (!CheckInternet())
            {
                return;
            }

            if (!_isAprroveFinger)
            {
                await new MessageBox("Ứng dụng chưa được cài đặt vân tay. Vui lòng đăng nhập bằng mật khẩu và vào Setting để cài đặt vân tay.").Show();
                return;
            }    

            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("SEMS", "Vui lòng quét vân tay"));
            
            if (result.Authenticated)
            {
                try
                {
                    _password = Preferences.Get(Config.Password, "");
                    ShowLoading("Đang kiểm tra vui lòng đợi");
                    await Task.Delay(2000);
                    var data = new
                    {
                        username = _fullname,
                        password = _password,
                        platform = DeviceInfo.Platform.ToString(),
                        firebase = Config.device_token
                    };
                    var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await Config.client.PostAsync(Config.Url + "api/SEMS/Login", httpContent);
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    USER_Response contents = JsonConvert.DeserializeObject<USER_Response>(responseContent);
                    if (contents.code == HttpStatusCode.OK)
                    {
                        Users = contents.data;
                        Preferences.Set(Config.Token, Users.TOKEN);
                        Preferences.Set(Config.FullName, _fullname);
                        Config.ma_dvql = (Users.MA_DVIQLY == null) ? "" : Users.MA_DVIQLY;
                        Config.ma_khang = (Users.MA_KHANG == null) ? "" : Users.MA_KHANG;
                        HideLoading();
                        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                        DependencyService.Get<IStatusBar>().ShowStatusBar();
                    }
                    else
                    {
                        HideLoading();
                        await new MessageBox("User của bạn chưa kích hoạt / sai mật khẩu. Vui lòng thử lại").Show();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    HideLoading();
                }

            }
            else
            {
                if (result.ErrorMessage != "Cancel")
                    await new MessageBox("Vân tay không đúng").Show();
            }
        }

    }

}
