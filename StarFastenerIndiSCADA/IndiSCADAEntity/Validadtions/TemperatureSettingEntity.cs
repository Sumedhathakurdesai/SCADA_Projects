using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TemperatureSettingEntity : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;
                if (name == "HighSP")
                {
                    if (this.HighSP != null)
                    {
                        if (this.HighSP.Length == 0)
                        {
                            return result = "Please enter value.";
                        }
                    }
                }
                if (name == "LowSP")
                {
                    if (this.LowSP != null)
                    {
                        if (this.LowSP.Length == 0)
                        {
                            return result = "Please enter value.";
                        }
                    }
                }
                if (name == "ActualSP")
                {
                    if (this.ActualSP != null)
                    {
                        if (this.ActualSP.Length == 0)
                        {
                            return result = "Please enter value.";
                        }
                    }
                }

                //if (name == "HighSP")
                //{
                //    if (this.HighSP != null)
                //    {
                //        if (this.HighSP.Length == 0)
                //        {
                //            return result = "Please enter value.";
                //        }
                //    }
                //    int HighSPValue = Convert.ToInt32(HighSP);
                //    if (HighSPValue > 100)
                //    {
                //        return result = "Current should be less than 100.";
                //    }
                //}

                //if (name == "LowSP")
                //{
                //    if (this.LowSP != null)
                //    {
                //        if (this.LowSP.Length == 0)
                //        {
                //            return result = "Please enter value.";
                //        }
                //    }
                //    int LowSPValue = Convert.ToInt32(LowSP);
                //    if (LowSPValue > 100)
                //    {
                //        return result = "Current should be less than 100.";
                //    }
                //}

                //if (name == "ActualSP")
                //{
                //    if (this.ActualSP != null)
                //    {
                //        if (this.ActualSP.Length == 0)
                //        {
                //            return result = "Please enter value.";
                //        }
                //    }
                //    int ActualSPValue = Convert.ToInt32(ActualSP);
                //    if (ActualSPValue > 100)
                //    {
                //        return result = "Current should be less than 100.";
                //    }
                //}

                return result;
            }
        }
        public string Error
        {
            get { return string.Empty; }
        }
    }
}
