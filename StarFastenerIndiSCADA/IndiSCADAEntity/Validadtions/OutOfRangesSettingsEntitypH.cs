using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class OutOfRangesSettingsEntitypH : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;


                if (name == "HighSPpH")
                {
                    if (this.HighSPpH != null)
                    {
                        if (this.HighSPpH.Length == 0)
                        {
                            return result = "Please enter high pH SP value.";
                        }
                        float OtherAlarmTime = float.Parse(HighSPpH);
                        if (OtherAlarmTime < 2.5 && OtherAlarmTime > 1.8)
                        {
                            
                        }
                        else
                        {
                            return result = "high pH SP should be in range 1.8 to 2.5";
                        }
                    }
                }
                if (name == "LowSPpH")
                {
                    if (this.LowSPpH != null)
                    {
                        if (this.LowSPpH.Length == 0)
                        {
                            return result = "Please enter low pH SP value.";
                        }
                        float OtherAlarmTime = float.Parse(LowSPpH);
                        if (OtherAlarmTime < 2.5 && OtherAlarmTime > 1.8)
                        {
                           
                        }
                        else
                        {
                            return result = "low pH SP should be in range 1.8 to 2.5";
                        }
                    }
                }
                if (name == "AvgpH")
                {
                    if (this.AvgpH != null)
                    {
                        if (this.AvgpH.Length == 0)
                        {
                            return result = "Please enter average pH SP value.";
                        }
                        float OtherAlarmTime = float.Parse(AvgpH);
                        if (OtherAlarmTime < 2.5 && OtherAlarmTime > 1.8)
                        {
                            
                        }
                        else
                        {
                            return result = "average pH SP should be in range 1.8 to 2.5";
                        }
                    }
                }
                if (name == "DelaypH")
                {
                    if (this.DelaypH != null)
                    {
                        if (this.DelaypH.Length == 0)
                        {
                            return result = "Please enter timer value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(DelaypH);
                        if (OtherAlarmTime <= 10000 && OtherAlarmTime > 0)
                        {
                            
                        }
                        else
                        {
                            return result = "Timer value should be in range 0-10000.";
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
