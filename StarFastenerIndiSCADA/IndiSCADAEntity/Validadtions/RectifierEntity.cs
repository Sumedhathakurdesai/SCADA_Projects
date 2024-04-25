using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class RectifierEntity : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "ManualCurrent")
                {
                    if (this.ManualCurrent != null)
                    {
                        if (this.ManualCurrent.Length == 0)
                        {
                            return result = "Please enter Manual current value.";
                        }
                        int ManualCurrentValue = Convert.ToInt32(ManualCurrent);
                        if (ManualCurrentValue > 900)
                        {
                            return result = "Current should be less than 900.";
                        }
                    }
                    string rowID = RectifierNo;
                    if (rowID != null )
                    {
                       
                        //indiSCADAB
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
