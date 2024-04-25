using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class AlarmTimeEntity : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "W1")
                {
                    if (this.W1 != null)
                    {
                        if (this.W1.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W1AlarmTime = Convert.ToInt32(W1);
                        if (W1AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "W2")
                {
                    if (this.W2 != null)
                    {
                        if (this.W2.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W2AlarmTime = Convert.ToInt32(W2);
                        if (W2AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "W2")
                {
                    if (this.W2 != null)
                    {
                        if (this.W2.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W2AlarmTime = Convert.ToInt32(W2);
                        if (W2AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "W3")
                {
                    if (this.W3 != null)
                    {
                        if (this.W3.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W3AlarmTime = Convert.ToInt32(W3);
                        if (W3AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "W4")
                {
                    if (this.W4 != null)
                    {
                        if (this.W4.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W4AlarmTime = Convert.ToInt32(W4);
                        if (W4AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "W5")
                {
                    if (this.W5 != null)
                    {
                        if (this.W5.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W5AlarmTime = Convert.ToInt32(W5);
                        if (W5AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "W6")
                {
                    if (this.W6 != null)
                    {
                        if (this.W6.Length == 0)
                        {
                            return result = "Please enter alarm time value.";
                        }
                        int W6AlarmTime = Convert.ToInt32(W6);
                        if (W6AlarmTime > 300)
                        {
                            return result = "Alarm time should be less than 300.";
                        }
                    }
                }
                if (name == "Value")
                {
                    if (this.Value != null)
                    {
                        if (this.Value.Length == 0)
                        {
                            return result = "Please enter value.";
                        }
                        int OtherAlarmTime = Convert.ToInt32(Value);
                        if (OtherAlarmTime > 20)
                        {
                            return result = "Value should be less than 20";
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
