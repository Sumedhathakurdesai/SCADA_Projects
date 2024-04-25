using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class OutOfRangeSettingsDipTime : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;


                //if (name == "Value")
                //{
                //    if (this.Value != null)
                //    {
                //        if (this.Value.Length == 0)
                //        {
                //            return result = "Please enter alarm time value.";
                //        }
                //        int OtherAlarmTime = Convert.ToInt32(Value);
                //        if (OtherAlarmTime > 200)
                //        {
                //            return result = "Alarm time should be less than 200.";
                //        }
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
