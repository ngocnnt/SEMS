using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace SEMS_APP.Models
{
    public class clsVaribles
    {
        public static ObservableCollection<Inverter> _dtInverter = new ObservableCollection<Inverter>();
        public static ObservableCollection<PVString> _dtPVString = new ObservableCollection<PVString>();
        public static ObservableCollection<CAP_NHAT_CONG_SUAT> _dtPhanHoiMqtt = new ObservableCollection<CAP_NHAT_CONG_SUAT>();
        public struct TOPIC
        {
            public string data;
            public string phanhoi;
            public string yeucau;
            public string pvstring;
        }
        public static TOPIC _topic = new TOPIC { data = "REALTIME/CPC/#", phanhoi = "EMEC/INVERTER/PHANHOI", yeucau = "EMEC/INVERTER/YEUCAU", pvstring = "PVSTRING/CPC/#" };

        public struct PERFORMANCE
        {
            public int Level1;
            public int Level2;
            public int Level3;
        }
        public static PERFORMANCE _performance = new PERFORMANCE { Level1 = 40, Level2 = 60, Level3 = 100 };
        public struct ENERGY
        {
            public float power_total;
            public float power_total_design;
            public float rp_t;
            public float energy_day;
            public float energy_week;
            public float energy_month;
            public float energy_year;
            public float energy_total;
        }
        public static ENERGY _energy;

        public class USER_Response
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public User data { get; set; }
        }
        public class Inverter_Response
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public ObservableCollection<Inverter> data { get; set; }
        }
        public class RealDataChart_Response
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public ObservableCollection<RealDataChart> data { get; set; }
        }
        public class PVString_Response
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public ObservableCollection<PVString> data { get; set; }
        }
        public class MonthChart_Response
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public ObservableCollection<MonthChart> data { get; set; }
        }
        public class DayChart_Response
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public ObservableCollection<DayChart> data { get; set; }
        }

        public class CAP_NHAT_CONG_SUAT
        {
            public string KEY { get; set; }
            public string MA_DIEMDO { get; set; }
            public int? ACTIVE_POWER { get; set; }
            public int? REACTIVE_POWER { get; set; }
            public int? POWER_FACTOR { get; set; }
            public bool REMOTE_VALUE { get; set; }
            public string NOI_DUNG { get; set; }
        }
    }
}
