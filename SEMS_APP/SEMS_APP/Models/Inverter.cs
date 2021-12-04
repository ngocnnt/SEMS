using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace SEMS_APP.Models
{
    public class Inverter : INotifyPropertyChanged
    {
        public string MA_DVICTREN { get; set; }
        public string TEN_DVICTREN { get; set; }
        public string MA_DVIQLY { get; set; }
        public string TEN_DVIQLY { get; set; }
        public string MA_KHANG { get; set; }
        public string TEN_KHANG { get; set; }
        public string MA_TRAM { get; set; }
        public string MA_DIEMDO { get; set; }
        //public string TEN_DIEMDO { get; set; }
        public string MA_CAPDA { get; set; }
        public string SERIALID { get; set; }
        public string METER_TYPE { get; set; }
        //public int POWER_TOTAL_DESIGN { get; set; }
        //public DateTime NGAYGIO { get; set; }
        private DateTime _NGAYGIO;
        public DateTime NGAYGIO
        {
            get { return _NGAYGIO; }
            set
            {
                _NGAYGIO = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NGAYGIO"));
            }
        }
        //public decimal? POWER_TOTAL { get; set; }
        private decimal? _POWER_TOTAL;

        public decimal? POWER_TOTAL
        {
            get { return _POWER_TOTAL; }
            set
            {
                _POWER_TOTAL = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("POWER_TOTAL"));
            }
        }
        public decimal? ENERGY_DAY { get; set; }
        public decimal? ENERGY_WEEK { get; set; }
        public decimal? ENERGY_MONTH { get; set; }
        public decimal? ENERGY_YEAR { get; set; }
        public decimal? ENERGY_TOTAL { get; set; }
        //public decimal? PERFORMANCE { get; set; }
        public decimal? AP_A { get; set; }
        public decimal? AP_B { get; set; }
        public decimal? AP_C { get; set; }
        public decimal? AP_T { get; set; }
        public decimal? APPARENT { get; set; }
        public decimal? A_A { get; set; }
        public decimal? A_B { get; set; }
        public decimal? A_C { get; set; }
        public decimal? V_A { get; set; }
        public decimal? V_B { get; set; }
        public decimal? V_C { get; set; }
        public decimal? PF_A { get; set; }
        public decimal? PF_B { get; set; }
        public decimal? PF_C { get; set; }
        public decimal? PF_T { get; set; }
        public decimal? RP_A { get; set; }
        public decimal? RP_B { get; set; }
        public decimal? RP_C { get; set; }
        public decimal? RP_T { get; set; }
        //public decimal? SETPOINT_P { get; set; }
        private decimal? _SETPOINT_P;

        public decimal? SETPOINT_P
        {
            get { return _SETPOINT_P; }
            set
            {
                _SETPOINT_P = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SETPOINT_P"));
            }
        }
        public decimal? SETPOINT_PF { get; set; }
        public decimal? SETPOINT_Q { get; set; }
        //public bool? REMOTE_CONTROL { get; set; }
        //public string STATUS { get; set; }
        private string _STATUS;
        public string STATUS
        {
            get { return _STATUS; }
            set
            {
                _STATUS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("STATUS"));
            }
        }
        //public string EVENT { get; set; }
        private string _EVENT;
        public string EVENT
        {
            get { return _EVENT; }
            set
            {
                _EVENT = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EVENT"));
            }
        }
        //public int SETPOINT_P_HT { get; set; }
        private int _SETPOINT_P_HT;

        public int SETPOINT_P_HT
        {
            get { return _SETPOINT_P_HT; }
            set
            {
                _SETPOINT_P_HT = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SETPOINT_P_HT"));
            }
        }

        //public bool CHECK { get; set; }
        private string _TEN_DIEMDO;
        public string TEN_DIEMDO
        {
            get
            {
                return _TEN_DIEMDO;
            }
            set
            {
                _TEN_DIEMDO = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TEN_DIEMDO"));
            }
        }

        private int _POWER_TOTAL_DESIGN;

        public int POWER_TOTAL_DESIGN
        {
            get { return _POWER_TOTAL_DESIGN; }
            set
            {
                _POWER_TOTAL_DESIGN = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("POWER_TOTAL_DESIGN"));
            }
        }

        private bool _CHECK;

        public bool CHECK
        {
            get { return _CHECK; }
            set
            {
                if (CHECK != value)
                {
                    _CHECK = value;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CHECK"));
            }
        }

        private decimal? _PERFORMANCE;

        public decimal? PERFORMANCE
        {
            get { return _PERFORMANCE; }
            set
            {
                //if (PERFORMANCE != value)
                {
                    _PERFORMANCE = value;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PERFORMANCE"));
            }
        }

        private bool _REMOTE_CONTROL;
        /*
        public bool REMOTE_CONTROL
        {
            get { return _REMOTE_CONTROL; }
            set
            {
                //if (REMOTE_CONTROL != value)
                {
                    _REMOTE_CONTROL = value;
                    //_REMOTE_CONTROL_RE = !_REMOTE_CONTROL;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("REMOTE_CONTROL"));
            }
        }
        */
        public bool REMOTE_CONTROL
        {
            get { return _REMOTE_CONTROL; }
            set
            {
                _REMOTE_CONTROL = value;
                REMOTE_CONTROL_REV = !value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("REMOTE_CONTROL"));
            }
        }
        private bool _REMOTE_CONTROL_REV;

        public bool REMOTE_CONTROL_REV
        {
            get { return _REMOTE_CONTROL_REV; }
            set
            {
                //if (REMOTE_CONTROL_RE != value)
                {
                    _REMOTE_CONTROL_REV = value;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("REMOTE_CONTROL_REV"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
