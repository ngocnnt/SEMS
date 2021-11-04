using System;
using System.Collections.Generic;
using System.Text;

namespace SEMS_APP.Models
{
    public class User
    {
        public string MA_DVIQLY { get; set; }
        public string TEN_DNHAP { get; set; }
        public string MAT_KHAU { get; set; }
        public int SO_LSAI { get; set; }
        public bool HOAT_DONG { get; set; }
        public string TEN { get; set; }
        public string CHUC_DANH { get; set; }
        public string DIA_CHI { get; set; }
        public string SO_DTHOAI { get; set; }
        public string EMAIL { get; set; }
        public bool NHAN_SMS { get; set; }
        public bool DNHAP_AD { get; set; }
        public bool ISDELETED { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string NGUOI_TAO { get; set; }
        public DateTime NGAY_SUA { get; set; }
        public string NGUOI_SUA { get; set; }
        public string MA_KHANG { get; set; }
        public string DEVICE_TYPE { get; set; }
        public string FIREBASE_ID { get; set; }
        public string TOKEN { get; set; }
    }
}
