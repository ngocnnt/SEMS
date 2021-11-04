using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SEMS_APP.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMS_APP.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageXacThucMatKhau : PopupPage, INotifyPropertyChanged
    {
        TaskCompletionSource<DialogReturn> _tsk = null;
        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        private bool _isShowHidePassword;
        public bool IsPasswordShow
        {
            get => _isShowHidePassword;
            set
            {
                _isShowHidePassword = value;
                OnPropertyChanged("IsPasswordShow");
            }
        }
        public Command ShowHideTapCommand { get; }
        public Command XacThucCommand { get; }
        public Command CloseCommand { get; }
        public MessageXacThucMatKhau()
        {
            InitializeComponent();
            _isShowHidePassword = true;
            XacThucCommand = new Command(OnXacThucClicked, ValidateLogin);
            this.PropertyChanged +=
                                  (_, __) => XacThucCommand.ChangeCanExecute();
            CloseCommand = new Command(OnCloseClicked);
            BindingContext = this;
        }

        private async void OnCloseClicked(object obj)
        {
            await Navigation.PopPopupAsync();
            _tsk.SetResult(DialogReturn.Cancel);
        }

        private async void OnXacThucClicked()
        {
            try
            {
                await Task.Delay(2000);
                string str = Config.Url + "api/SEMS/XacThuc?user=" + Preferences.Get(Config.FullName, "") + "&password=" + _password;
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("Không Tìm Thấy Dữ Liệu") == false && _json.Contains("[]") == false)
                {
                    Preferences.Set(Config.Password, _password);
                    await Navigation.PopPopupAsync();
                    _tsk.SetResult(DialogReturn.OK);
                }
                else
                {
                    await new MessageBox("Sai mật khẩu. Vui lòng thử lại").Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        private bool ValidateLogin()
        {
            return !String.IsNullOrWhiteSpace(_password);
        }



        public async Task<DialogReturn> Show()
        {
            _tsk = new TaskCompletionSource<DialogReturn>();
            await Navigation.PushPopupAsync(this);
            return await _tsk.Task;
        }

        private void imgShowPassword_Tapped(object sender, EventArgs e)
        {
            IsPasswordShow = !IsPasswordShow;
        }
    }
}