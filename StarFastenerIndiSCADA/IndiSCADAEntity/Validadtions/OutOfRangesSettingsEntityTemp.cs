using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class OutOfRangesSettingsEntityTemp : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;


                if (name == "HighSPTemp")
                {
                    if (this.HighSPTemp != null)
                    {
                        if (this.HighSPTemp.Length == 0)
                        {
                            return result = "Please enter high temp SP value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(HighSPTemp);
                        if (OtherAlarmTime > 75)
                        {
                            return result = "High Temp should be less than 75.";
                        }
                    }
                }
                if (name == "LowSPTemp")
                {
                    if (this.LowSPTemp != null)
                    {
                        if (this.LowSPTemp.Length == 0)
                        {
                            return result = "Please enter Low temp SP value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(LowSPTemp);
                        if (OtherAlarmTime > 75)
                        {
                            return result = "Low Temp should be less than 75.";
                        }
                    }
                }
                if (name == "SetPointTemp")
                {
                    if (this.SetPointTemp != null)
                    {
                        if (this.SetPointTemp.Length == 0)
                        {
                            return result = "Please enter set point temp SP value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(SetPointTemp);
                        if (OtherAlarmTime > 75)
                        {
                            return result = "set point Temp should be less than 75.";
                        }
                    }
                }
                if (name == "DelayTemp")
                {
                    if (this.DelayTemp != null)
                    {
                        if (this.DelayTemp.Length == 0)
                        {
                            return result = "Please enter timer SP value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(DelayTemp);
                        if (OtherAlarmTime <= 10000 && OtherAlarmTime > 0)
                        {
                           
                        }
                        else
                        {
                            return result = "Timer value should be in range 0-10000.";
                        }
                    }
                }
                if (name == "AvgTemp")
                {
                    if (this.AvgTemp != null)
                    {
                        if (this.AvgTemp.Length == 0)
                        {
                            return result = "Please enter average temp value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(AvgTemp);
                        if (OtherAlarmTime > 75)
                        {
                            return result = "Average temp should be less than 75.";
                        }
                    }
                }
                return result;
            }
        }
        public string Error
        {
            get { return string.Empty; }
        }
    }
}
