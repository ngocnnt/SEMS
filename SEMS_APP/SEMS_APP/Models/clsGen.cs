using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SEMS_APP.Models
{
    public class clsGen
    {
        public static void analyze_inverter(Inverter data, ObservableCollection<Inverter> dt)
        {
            try
            {
                if (data != null)
                {
                    lock (dt)
                    {
                        var row = dt.FirstOrDefault(i => i.MA_DIEMDO == data.MA_DIEMDO);
                        if (row != null)
                        {
                            row.POWER_TOTAL = data.POWER_TOTAL;
                            row.ENERGY_DAY = data.ENERGY_DAY / 1000;
                            row.ENERGY_YEAR = data.ENERGY_YEAR / 1000000;
                            row.ENERGY_TOTAL = data.ENERGY_TOTAL / 1000000;
                            row.PERFORMANCE = Math.Round(data.POWER_TOTAL / ((int)row.POWER_TOTAL_DESIGN * 10), 2);
                            row.A_A = data.A_A;
                            row.A_B = data.A_B;
                            row.A_C = data.A_C;
                            row.V_A = data.V_A;
                            row.V_B = data.V_B;
                            row.V_C = data.V_C;
                            row.AP_A = data.AP_A;
                            row.AP_B = data.AP_B;
                            row.AP_C = data.AP_C;
                            row.AP_T = data.AP_T;
                            row.RP_A = data.RP_A;
                            row.RP_B = data.RP_B;
                            row.RP_C = data.RP_C;
                            row.RP_T = data.RP_T;
                            row.PF_A = data.PF_A;
                            row.PF_B = data.PF_B;
                            row.PF_C = data.PF_C;
                            row.PF_T = data.PF_T;

                            row.SETPOINT_P = data.SETPOINT_P;
                            row.SETPOINT_PF = data.SETPOINT_PF;
                            row.SETPOINT_Q = data.SETPOINT_Q;

                            row.NGAYGIO = data.NGAYGIO;
                            row.STATUS = data.STATUS;
                            row.EVENT = data.EVENT;
                            if (data.STATUS.IndexOf("I_STATUS_MPPT") >= 0 || data.STATUS.IndexOf("I_STATUS_OFF") >= 0)
                            {
                                row.REMOTE_CONTROL = true;
                                row.REMOTE_CONTROL_REV = false;
                            }
                            else
                            {
                                row.REMOTE_CONTROL = false;
                                row.REMOTE_CONTROL_REV = true;
                            }

                        }
                        float.Parse(clsVaribles._dtInverter.Sum(a => a.POWER_TOTAL_DESIGN).ToString());
                        clsVaribles._energy.energy_day = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_DAY).ToString());
                        clsVaribles._energy.energy_year = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_YEAR).ToString());
                        clsVaribles._energy.energy_total = float.Parse(clsVaribles._dtInverter.Sum(a => a.ENERGY_TOTAL).ToString());
                        clsVaribles._energy.power_total = float.Parse(clsVaribles._dtInverter.Sum(a => a.POWER_TOTAL).ToString()) / 1000;
                        clsVaribles._energy.rp_t = float.Parse(clsVaribles._dtInverter.Sum(a => a.RP_T).ToString()) / 1000;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }


        }

        public static void analyze_pvstring(ObservableCollection<PVString> temp, ObservableCollection<PVString> dt)
        {
            try
            {
                if (dt != null && temp != null) 
                {
                    lock (dt)
                    {
                        // double total = (double)temp.Sum(a => a.WA);
                        foreach (PVString row in temp)
                        {
                            decimal total = 0;
                            foreach (PVString rowfind in temp)
                            {
                                if (rowfind.MA_DIEMDO == row.MA_DIEMDO)
                                    total += rowfind.WA;
                            }
                            var rowdt = dt.FirstOrDefault(i => i.MA_DIEMDO == row.MA_DIEMDO && i.STRING == row.STRING);
                            rowdt.VA = row.VA;
                            rowdt.IA = row.IA;
                            rowdt.WA = row.WA;
                            rowdt.NGAYGIO = row.NGAYGIO;
                            if (total == 0) rowdt.RATIO = 0;
                            else rowdt.RATIO = Math.Round(rowdt.WA * 100 / total, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }


        }
    }
}
