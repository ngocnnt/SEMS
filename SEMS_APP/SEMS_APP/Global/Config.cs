using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace SEMS_APP.Global
{
    public class Config
    {
        public static string FullName = "FullName";
        public static string Password = "Password";
        public static string Token = "Token";
        public static string AprroveFinger = "AprroveFinger";
        public static string AprrovePassword = "AprrovePassword";
        public static string Url = "http://113.160.225.75/SEMS/";
        //"http://smart.cpc.vn/SEMSApi/";
        public static string device_token = "";
        public static HttpClient client;
        public static string ma_dvql = "";
        public static string ma_khang = "";
    }
    public enum DialogReturn
    {
        OK = 0,
        Cancel = 1,
        Repeat = 2,
        Stop = 3
    }
}
