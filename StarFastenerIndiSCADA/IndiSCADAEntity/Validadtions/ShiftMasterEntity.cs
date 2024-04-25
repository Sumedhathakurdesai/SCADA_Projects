using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class ShiftMasterEntity : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;
                if (name == "ShiftNumber")
                {
                    if (this.ShiftNumber != null)
                    {
                        if (this.ShiftNumber.Length == 0)
                        {
                            return result = "Please enter value.";
                        }
                        int i;
                        if (!int.TryParse(this.ShiftNumber.ToString(), out i))
                        {
                            return result = "Please enter a valid number";

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
