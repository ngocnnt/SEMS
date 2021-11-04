using Newtonsoft.Json;
using SEMS_APP.Interface;
using SEMS_APP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static SEMS_APP.Models.clsVaribles;

namespace SEMS_APP.ViewModels
{
    public class PPCViewModel : BaseViewModel
    {
        public ObservableCollection<Inverter> _listInverters;
        public ObservableCollection<Inverter> ListInverters
        {
            get { return _listInverters; }
            set
            {
                _listInverters = value;
                OnPropertyChanged("ListInverters");

            }
        }
        public bool selectAllChecked = false;

        public bool SelectAllChecked
        {
            get { return selectAllChecked; }
            set
            {
                if (selectAllChecked != value)
                {
                    selectAllChecked = value;
                    if (value) ChangeListState(value);
                }
                OnPropertyChanged("SelectAllChecked");
            }
        }

        public bool selectDSChecked = true;

        public bool SelectDSChecked
        {
            get { return selectDSChecked; }
            set
            {
                if (selectDSChecked != value)
                {
                    selectDSChecked = value;
                }
                OnPropertyChanged("SelectDSChecked");
            }
        }

        public int setPointValue = 50;

        public int SetPointValue
        {
            get { return setPointValue; }
            set
            {
                setPointValue = value;
                OnPropertyChanged("SetPointValue");
            }
        }

        public PPCViewModel()
        {
            ListInverters = new ObservableCollection<Inverter>();
            foreach (Inverter temp in clsVaribles._dtInverter)
            {
                ListInverters.Add(temp);
            }
        }
        public void Data2Grid()
        {
            ListInverters.Clear();
            foreach (Inverter tempAdd in clsVaribles._dtInverter)
            {
                ListInverters.Add(tempAdd);
            }
        }

        public void ChangeSetPoint()
        {
            foreach (Inverter temp in ListInverters)
            {
                if (temp.CHECK)
                {
                    temp.SETPOINT_P_HT = SetPointValue;
                }
            }
        }

        public void CHECKStateChanged()
        {
            bool selectAll = true;
            foreach (Inverter temp in ListInverters)
            {
                if (!temp.CHECK)
                {
                    selectAll = false;
                }
                else temp.SETPOINT_P_HT = SetPointValue;
            }
            if (selectAll)
            {
                SelectAllChecked = true;
                SelectDSChecked = false;
            }
            else
            {
                SelectAllChecked = false;
                SelectDSChecked = true;
            } 
                
        }

        private void ChangeListState(bool value)
        {
            if (ListInverters != null)
            {
                foreach (var data in ListInverters)
                {
                    data.CHECK = true;
                }
            }
        }

        public void TurnOff()
        {
            SetOnOff(false);
        }
        public void TurnOn()
        {
            SetOnOff(true);
        }
        public void SetPoint()
        {
            try
            {
                string str = "";
                ObservableCollection<CAP_NHAT_CONG_SUAT> listcapnhat = new ObservableCollection<CAP_NHAT_CONG_SUAT>();
                foreach (Inverter row in clsVaribles._dtInverter)
                {
                    if (row.CHECK)
                    {
                        str += ";" + row.MA_DIEMDO.ToString() + ":" + row.SETPOINT_P_HT.ToString();
                        //CAP_NHAT_CONG_SUAT capnhat = new CAP_NHAT_CONG_SUAT { KEY = "ACTIVE_POWER", CONG_SUAT = Convert.ToInt32(row.SETPOINT_P_HT), MA_DIEMDO = row.MA_DIEMDO.ToString(), NOI_DUNG = "CAI DAT CONG SUAT" };
                        CAP_NHAT_CONG_SUAT capnhat = new CAP_NHAT_CONG_SUAT
                        {
                            KEY = "REDUCE",
                            MA_DIEMDO = row.MA_DIEMDO.ToString(),
                            ACTIVE_POWER = Convert.ToInt32(row.SETPOINT_P_HT),
                            REACTIVE_POWER = 0,
                            POWER_FACTOR = 100,
                            REMOTE_VALUE = true,
                            NOI_DUNG = "CÀI ĐẶT ACTIVE_POWER"
                        };

                        listcapnhat.Add(capnhat);
                    }
                }
                if (listcapnhat.Count > 0)
                {
                    MqttClientRepository.PublibMessage(clsVaribles._topic.yeucau, JsonConvert.SerializeObject(listcapnhat));
                    DependencyService.Get<IToast>().Show(string.Format("Đã yêu cầu cài đặt công suất inverters: {0}", str));
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void SetOnOff(bool on_off)
        {
            try
            {
                string str = "";
                ObservableCollection<CAP_NHAT_CONG_SUAT> listcapnhat = new ObservableCollection<CAP_NHAT_CONG_SUAT>();
                foreach (Inverter row in clsVaribles._dtInverter)
                {
                    if (row.CHECK)
                    {
                        str += ";" + row.MA_DIEMDO.ToString();
                        CAP_NHAT_CONG_SUAT capnhat = new CAP_NHAT_CONG_SUAT
                        {
                            KEY = "REMOTE",
                            MA_DIEMDO = row.MA_DIEMDO.ToString(),
                            ACTIVE_POWER = 100,
                            REACTIVE_POWER = 0,
                            POWER_FACTOR = 100,
                            REMOTE_VALUE = on_off,
                            NOI_DUNG = "CÀI ĐẶT REMOTE_VALUE"
                        };
                        listcapnhat.Add(capnhat);
                    }
                }
                if (listcapnhat.Count > 0)
                {
                    MqttClientRepository.PublibMessage(clsVaribles._topic.yeucau, JsonConvert.SerializeObject(listcapnhat));
                    DependencyService.Get<IToast>().Show(string.Format("Đã yêu cầu {0} inverters: {1}", on_off == true ? "MỞ" : "ĐÓNG", str));
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
