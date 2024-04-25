using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class ChemicalNameMasterEntity : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string result = null;
                if (name == "ChemicalPercentage")
                {
                    if (this.ChemicalPercentage != null)
                    {
                        if (this.ChemicalPercentage.Length == 0)
                        {
                            return result = "Please enter value.";
                        }
                        Single i;
                        if (!Single.TryParse(this.ChemicalPercentage.ToString(), out i))
                        {
                            return result = "Please enter a valid number";
                        }
                        Single ChemicalPercent = Convert.ToSingle(ChemicalPercentage);
                        if (ChemicalPercent > 100)
                        {
                            return result = "Chemical Percentage should be between 0-100";
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
