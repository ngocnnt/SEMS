using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SEMS_APP.Models
{
    /*
    public class PVString
    {
        public string MA_DIEMDO { get; set; }
        public string SERIAL { get; set; }
        //[JsonProperty("SERIALID")]
        public string SERIALID { set { SERIAL = value; } }
        public DateTime NGAYGIO { get; set; }
        public string STRING { get; set; }
        public double IA { get; set; }
        //[JsonProperty("I")]
        public double I { set { IA = value; } }
        public double VA { get; set; }
        //[JsonProperty("V")]
        public double V { set { VA = value; } }
        public double WA { get; set; }
        //[JsonProperty("W")]
        public double W { set { WA = value; } }
        public double RATIO { get; set; }
    }
    */
    public class PVString : INotifyPropertyChanged
    {
        public string ID_DONVI { get; set; }
        public string TEN_DVIQLY { get; set; }
        public string MA_TRAM { get; set; }
        public string MA_DIEMDO { get; set; }
        public string TEN_DIEMDO { get; set; }
        public string MA_CAPDA { get; set; }
        public string SERIAL { get; set; }
        public string SERIALID { get; set; }
        public string METER_TYPE { get; set; }
        public DateTime NGAYGIO { get; set; }
        //public string STRING { get; set; }
        private string _STRING;
        public string STRING
        {
            get { return _STRING; }
            set
            {
                _STRING = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("STRING"));
            }
        }
        //public decimal IA { get; set; }
        private decimal _IA;
        public decimal IA
        {
            get { return _IA; }
            set
            {
                _IA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IA"));
            }
        }
        public decimal I { get; set; }
        //public decimal VA { get; set; }
        private decimal _VA;
        public decimal VA
        {
            get { return _VA; }
            set
            {
                _VA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VA"));
            }
        }
        public decimal V { get; set; }
        //public decimal WA { get; set; }
        private decimal _WA;
        public decimal WA
        {
            get { return _WA; }
            set
            {
                _WA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WA"));
            }
        }
        public decimal W { get; set; }
        //public decimal RATIO { get; set; }
        private decimal _RATIO;

        public decimal RATIO
        {
            get { return _RATIO; }
            set
            {
                //if (RATIO != value)
                {
                    _RATIO = value;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RATIO"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
