using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SEMS_APP.Global;
using SEMS_APP.Models;
using SEMS_APP.Views;
using Syncfusion.SfGauge.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SEMS_APP.Models.clsVaribles;

namespace SEMS_APP.ViewModels
{
    class StatisticsViewModel : BaseViewModel
    {
        ObservableCollection<MonthChart> _dtMonthChart;
        public ObservableCollection<MonthChart> MonthChart
        {
            get { return _dtMonthChart; }
            set
            {
                SetProperty(ref _dtMonthChart, value);
            }
        }
        ObservableCollection<MonthChart> _dtYearChart;
        public ObservableCollection<MonthChart> YearChart
        {
            get { return _dtYearChart; }
            set
            {
                SetProperty(ref _dtYearChart, value);
            }
        }
        ObservableCollection<DayChart> _dtDayChart;
        public ObservableCollection<DayChart> DayChart
        {
            get { return _dtDayChart; }
            set
            {
                SetProperty(ref _dtDayChart, value);
            }
        }
        public StatisticsViewModel()
        {
            DayChart = new ObservableCollection<DayChart>();
            MonthChart = new ObservableCollection<MonthChart>();
            YearChart = new ObservableCollection<MonthChart>();
            LoadDataDay(Config.ma_dvql, Config.ma_khang);
            LoadDataMonth(Config.ma_dvql, Config.ma_khang);
            LoadDataYear(Config.ma_dvql, Config.ma_khang);
        }
        public async void LoadDataDay(string donvi, string ma_kh)
        {
            if (!CheckInternet())
            {
                return;
            }
            try
            {
                //if (IsBusy == true) return;
                //IsBusy = true;
                await Task.Delay(1000);
                var data = new
                {
                    ma_dvql = donvi,
                    ma_khang = ma_kh,
                    ngay_gio = new DateTime(StatisticsPage.year, StatisticsPage.month, StatisticsPage.day),
                    TOKEN = Preferences.Get(Config.Token, "")
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/SPCHARTDAYBYDATE", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result; 
                DayChart_Response contents = JsonConvert.DeserializeObject<DayChart_Response>(responseContent, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy"});
                DayChart = contents.data;                
            }
            catch (Exception ex)
            {
                ShowLoading(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async void LoadDataMonth(string donvi, string ma_kh)
        {
            if (!CheckInternet())
            {
                return;
            }
            try
            {
                //if (IsBusy == true) return;
                //IsBusy = true;
                await Task.Delay(1000);
                var data = new
                {
                    ma_dvql = donvi,
                    ma_khang = ma_kh,
                    ngay_gio = new DateTime(StatisticsPage.year, StatisticsPage.month, StatisticsPage.day),
                    TOKEN = Preferences.Get(Config.Token, "")
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/SPCHARTMONTHBYDATE", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                MonthChart_Response contents = JsonConvert.DeserializeObject<MonthChart_Response>(responseContent);
                MonthChart = contents.data;                
                /*
                string str = Config.Url + "api/SEMS/SPCHARTMONTH?ma_dvql=" + donvi + "&TOKEN=" + Preferences.Get(Config.Token, "");
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("Không Tìm Thấy Dữ Liệu") == false && _json.Contains("[]") == false)
                {
                    Int32 from = _json.IndexOf("[");
                    Int32 to = _json.IndexOf("]");
                    string result = _json.Substring(from, to - from + 1);
                    MonthChart = JsonConvert.DeserializeObject<ObservableCollection<MonthChart>>(result);
                }
                else
                {
                    return;
                }
                */

            }
            catch (Exception ex)
            {
                ShowLoading(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadDataYear(string donvi, string ma_kh)
        {
            if (!CheckInternet())
            {
                return;
            }
            try
            {
                //if (IsBusy == true) return;
                //IsBusy = true;
                await Task.Delay(1000);
                var data = new
                {
                    ma_dvql = donvi,
                    ma_khang = ma_kh,
                    ngay_gio = new DateTime(StatisticsPage.year, StatisticsPage.month, StatisticsPage.day),
                    TOKEN = Preferences.Get(Config.Token, "")
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/SPCHARTYEARBYDATE", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                MonthChart_Response contents = JsonConvert.DeserializeObject<MonthChart_Response>(responseContent);
                YearChart = contents.data;
                /*
                string str = Config.Url + "api/SEMS/SPCHARTYEAR?ma_dvql=" + donvi + "&TOKEN=" + Preferences.Get(Config.Token, "");
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("Không Tìm Thấy Dữ Liệu") == false && _json.Contains("[]") == false)
                {
                    Int32 from = _json.IndexOf("[");
                    Int32 to = _json.IndexOf("]");
                    string result = _json.Substring(from, to - from + 1);
                    YearChart = JsonConvert.DeserializeObject<ObservableCollection<MonthChart>>(result);
                }
                else
                {
                    return;
                }
                */

            }
            catch (Exception ex)
            {
                ShowLoading(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
