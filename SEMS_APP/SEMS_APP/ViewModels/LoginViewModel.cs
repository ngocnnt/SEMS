using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SEMS_APP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _isShowHidePassword;
        string _phonenumber;
        string _fullname;
        string _password;
        string _thongbao;
        public bool IsPasswordShow { get => _isShowHidePassword; set { SetProperty(ref _isShowHidePassword, value); } }
        public string PhoneNumber
        {
            get => _phonenumber;
            set
            {
                SetProperty(ref _phonenumber, value);
                if (_phonenumber.Length >= 10)
                {
                    string old = _phonenumber.Substring(3, 5);
                    _phonenumber = _phonenumber.Replace(old, "*****");
                }

            }
        }
        public string Password { get => _password; set { SetProperty(ref _password, value); } }
        public string FullName { get => _fullname; set { SetProperty(ref _fullname, value); } }
        public string THONG_BAO { get => _thongbao; set { SetProperty(ref _thongbao, value); } }

        public Command LoginCommand { get; }
        public Command FingerComand { get; }
        public Command FaceComand { get; }
        public Command ShowHideTapCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            FingerComand = new Command(OnFingerCLicked);
            FaceComand = new Command(OnFaceCLicked);
            ShowHideTapCommand = new Command(OnShowHidePassClicked);
        }

        private bool ValidateLogin(object arg)
        {
            return !String.IsNullOrWhiteSpace(_password);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                ShowLoading("Đang kiểm tra vui lòng đợi");
                await Task.Delay(2000);
            }
            catch (Exception ex)
            {
                HideLoading();
            }
        }

        private async void OnFaceCLicked(object obj)
        {
            throw new NotImplementedException();
        }

        private async void OnFingerCLicked(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnShowHidePassClicked(object obj)
        {
            IsPasswordShow = !IsPasswordShow;
        }
    }
}
