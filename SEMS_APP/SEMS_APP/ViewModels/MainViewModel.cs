using Newtonsoft.Json;
using SEMS_APP.Global;
using SEMS_APP.Interface;
using SEMS_APP.Models;
using SEMS_APP.Views;
using Syncfusion.SfChart.XForms;
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
    class MainViewModel : BaseViewModel
    {
        Range range = new Range();
        Range range1 = new Range();
        Range range2 = new Range();
        NeedlePointer pointer = new NeedlePointer();

        ObservableCollection<DonVi> _lstDonVi;
        ObservableCollection<RealDataChart> _dtRealDataChart;

        string _headerActive;
        float _endValueActive;
        string _todayEnergy;
        string _yearEnergy;
        string _totalEnergy;
        public ObservableCollection<DonVi> ListDonVi
        {
            get
            {
                return _lstDonVi;
            }
            set
            {
                SetProperty(ref _lstDonVi, value);
            }
        }

        public ObservableCollection<Range> RangeActive
        {
            get;
            set;
        }
        public ObservableCollection<Pointer> PointerActive
        {
            get;
            set;
        }

        public ObservableCollection<RealDataChart> RealDataChart
        {
            get { return _dtRealDataChart; }
            set
            {
                SetProperty(ref _dtRealDataChart, value);
            }
        }

        public ObservableCollection<Inverter> dtData
        {
            get
            {
                return clsVaribles._dtInverter;
            }
            set
            {
                SetProperty(ref clsVaribles._dtInverter, value);
            }
        }

        public string HeaderActive { get => _headerActive; set => SetProperty(ref _headerActive, value); }
        public float EndValueActive { get => _endValueActive; set => SetProperty(ref _endValueActive, value); }
        public string TodayEnergy { get => _todayEnergy; set => SetProperty(ref _todayEnergy, value); }
        public string YearEnergy { get => _yearEnergy; set => SetProperty(ref _yearEnergy, value); }
        public string TotalEnergy { get => _totalEnergy; set => SetProperty(ref _totalEnergy, value); }

        public MainViewModel()
        {
            ListDonVi = new ObservableCollection<DonVi>();
            RangeActive = new ObservableCollection<Range>();
            PointerActive = new ObservableCollection<Pointer>();
            RealDataChart = new ObservableCollection<RealDataChart>();
            //LoadDsDonVi();
            LoadData(Config.ma_dvql, Config.ma_khang);
            LoadChrPower(Config.ma_dvql, Config.ma_khang);
            LoadChrPVString(Config.ma_dvql, Config.ma_khang);

            MessagingCenter.Subscribe<SubscribeCallback>(this, "MQTTRev", (message) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Data2Gauge();
                });
            });
        }

        public void LoadDsDonVi()
        {
            if (!CheckInternet())
            {
                return;
            }
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };
                Config.client = new HttpClient(httpClientHandler);

                string str = Config.Url + "api/SEMS/GetDSDonVi";
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("error") == false && _json.Contains("[]") == false)
                {
                    Int32 from = _json.IndexOf("[");
                    Int32 to = _json.IndexOf("]");
                    string result = _json.Substring(from, to - from + 1);
                    ListDonVi = JsonConvert.DeserializeObject<ObservableCollection<DonVi>>(result);
                }
            }
            catch (Exception ex)
            {
                ShowLoading(ex.ToString());
            }

        }

        public async void LoadData(string donvi, string ma_kh)
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
                    TOKEN = Preferences.Get(Config.Token, "")
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/GetGeneral", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Inverter_Response contents = JsonConvert.DeserializeObject<Inverter_Response>(responseContent);
                if (contents.code == HttpStatusCode.OK)
                {
                    clsVaribles._dtInverter = contents.data;
                    SetGauge();
                }
                else
                {
                    return;
                }
                /*
                string str = Config.Url + "api/SEMS/GetGeneral?ma_dvql=" + donvi + "&TOKEN=" + Preferences.Get(Config.Token, "");
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("Không Tìm Thấy Dữ Liệu") == false && _json.Contains("[]") == false)
                {
                    Int32 from = _json.IndexOf("[");
                    Int32 to = _json.IndexOf("]");
                    string result = _json.Substring(from, to - from + 1);
                    clsVaribles._dtInverter = JsonConvert.DeserializeObject<ObservableCollection<Inverter>>(result);
                    SetGauge();
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

        public void SetGauge()
        {
            try
            {
                PointerActive.Clear();
                RangeActive.Clear();
                clsVaribles._energy.power_total_design = float.Parse(clsVaribles._dtInverter.Sum(a => a.POWER_TOTAL_DESIGN).ToString());
                clsVaribles._energy.power_total = float.Parse(clsVaribles._dtInverter.Sum(a => a.POWER_TOTAL).ToString()) / 1000;

                range.StartValue = 0;
                range.EndValue = clsVaribles._energy.power_total_design * clsVaribles._performance.Level1 / 100; ;
                range.Offset = 0.7;
                range.Thickness = 30;
                range.Color = Color.FromHex("#F03E3E");

                range1.StartValue = clsVaribles._energy.power_total_design * clsVaribles._performance.Level1 / 100;
                range1.EndValue = clsVaribles._energy.power_total_design * clsVaribles._performance.Level2 / 100;
                range1.Offset = 0.7;
                range1.Thickness = 30;
                range1.Color = Color.FromHex("#FFDD00");

                range2.StartValue = clsVaribles._energy.power_total_design * clsVaribles._performance.Level2 / 100;
                range2.EndValue = clsVaribles._energy.power_total_design;
                range2.Offset = 0.7;
                range2.Thickness = 30;
                range2.Color = Color.FromHex("#27beb7");

                RangeActive.Add(range);
                RangeActive.Add(range1);
                RangeActive.Add(range2);

                pointer.Value = clsVaribles._energy.power_total;
                pointer.EnableAnimation = true;
                pointer.AnimationDuration = 1;
                PointerActive.Add(pointer);
                EndValueActive = clsVaribles._energy.power_total_design;
                HeaderActive = string.Format("{0:#,##0.##} kW  ({1:##0.00} %)", clsVaribles._energy.power_total, clsVaribles._energy.power_total * 100 / clsVaribles._energy.power_total_design);

                clsVaribles._energy.energy_day = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_DAY).ToString());
                clsVaribles._energy.energy_year = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_YEAR).ToString());
                clsVaribles._energy.energy_total = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_TOTAL).ToString());
                TodayEnergy = string.Format("{0:#,##0.##} kWh", clsVaribles._energy.energy_day);
                YearEnergy = string.Format("{0:#,##0.##} MWh", clsVaribles._energy.energy_year);
                TotalEnergy = string.Format("{0:#,##0.##} MWh", clsVaribles._energy.energy_total);
            }
            catch (Exception ex)
            {
                ShowLoading(ex.ToString());
            }
        }

        public void Data2Gauge()
        {
            try
            {
                PointerActive.Clear();
                clsVaribles._energy.power_total = float.Parse(clsVaribles._dtInverter.Sum(a => a.POWER_TOTAL).ToString()) / 1000;
                pointer.Value = clsVaribles._energy.power_total;
                pointer.EnableAnimation = false;
                PointerActive.Add(pointer);
                HeaderActive = string.Format("{0:#,##0.##} kW  ({1:##0.00} %)", clsVaribles._energy.power_total, clsVaribles._energy.power_total * 100 / clsVaribles._energy.power_total_design);
                clsVaribles._energy.energy_day = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_DAY).ToString());
                clsVaribles._energy.energy_year = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_YEAR).ToString());
                clsVaribles._energy.energy_total = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_TOTAL).ToString());
                TodayEnergy = string.Format("{0:#,##0.##} kWh", clsVaribles._energy.energy_day);
                YearEnergy = string.Format("{0:#,##0.##} MWh", clsVaribles._energy.energy_year);
                TotalEnergy = string.Format("{0:#,##0.##} MWh", clsVaribles._energy.energy_total);
            }
            catch (Exception ex)
            {
                ShowLoading(ex.ToString());
            }
        }

        public async void LoadChrPower(string donvi, string ma_kh)
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
                    TOKEN = Preferences.Get(Config.Token, "")
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/SPLoadRealDataChart", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                RealDataChart_Response contents = JsonConvert.DeserializeObject<RealDataChart_Response>(responseContent);
                RealDataChart = contents.data;
                /*
                string str = Config.Url + "api/SEMS/SPLoadRealDataChart?TOKEN=" + Preferences.Get(Config.Token, "");
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("Không Tìm Thấy Dữ Liệu") == false && _json.Contains("[]") == false)
                {
                    Int32 from = _json.IndexOf("[");
                    Int32 to = _json.IndexOf("]");
                    string result = _json.Substring(from, to - from + 1);
                    RealDataChart = JsonConvert.DeserializeObject<ObservableCollection<RealDataChart>>(result);
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

        public async void LoadChrPVString(string donvi, string ma_kh)
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
                    TOKEN = Preferences.Get(Config.Token, "")
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await Config.client.PostAsync(Config.Url + "api/SEMS/PVString", httpContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                PVString_Response contents = JsonConvert.DeserializeObject<PVString_Response>(responseContent);
                if (contents.code == HttpStatusCode.OK)
                {
                    clsVaribles._dtPVString = contents.data;
                    clsGen.analyze_pvstring(clsVaribles._dtPVString, clsVaribles._dtPVString); //lấy ratio
                }
                else
                {
                    return;
                }
                /*
                string str = Config.Url + "api/SEMS/get_pv_string?ma_dvql=" + donvi + "&TOKEN=" + Preferences.Get(Config.Token, "");
                var _json = Config.client.GetStringAsync(str).Result;
                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                if (_json.Contains("Không Tìm Thấy Dữ Liệu") == false && _json.Contains("[]") == false)
                {
                    Int32 from = _json.IndexOf("[");
                    Int32 to = _json.IndexOf("]");
                    string result = _json.Substring(from, to - from + 1);
                    clsVaribles._dtPVString = JsonConvert.DeserializeObject<ObservableCollection<PVString>>(result); 
                    clsGen.analyze_pvstring(clsVaribles._dtPVString, clsVaribles._dtPVString); //lấy ratio
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
