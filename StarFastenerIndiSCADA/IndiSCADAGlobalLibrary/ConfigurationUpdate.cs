using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAGlobalLibrary
{
    public static class ConfigurationUpdate
    {

        #region Public/Private Property

        private static bool _StopWagonStatusDatalog;
        public static bool StopWagonStatusDatalog
        {
            get
            {
                return _StopWagonStatusDatalog;
            }
            set
            {
                _StopWagonStatusDatalog = value;
            }
        }
        #endregion
    }
}
