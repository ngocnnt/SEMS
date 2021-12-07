using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace SEMS_APP.Models
{
    public class PhanHoiMqtt
    {
        public string KEY { get; set; }
        public string MA_DIEMDO { get; set; }
        public decimal ACTIVE_POWER { get; set; }
        public decimal REACTIVE_POWER { get; set; }
        public decimal POWER_FACTOR { get; set; }
        public bool REMOTE_VALUE { get; set; }
        public string NOI_DUNG { get; set; }
    }
}
