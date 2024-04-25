using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ErrorLogger
{
    public static class LogError
    {
        public static void ErrorLog(String Title, String _ErrorDateTime, String ErrorMessage, String IsDeveloperMode, Boolean IsAdminLogEnable)
        {
            try
            {
                String FileName = DateTime.Now.ToShortDateString();
                FileName = Regex.Replace(FileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                if (!(System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\")))
                {

                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\");
                }
                {
                    FileStream _fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + FileName + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    StreamWriter _SstreamWriter = new StreamWriter(_fileStream);

                    _SstreamWriter.Close();

                    _fileStream.Close();

                    FileStream LogFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + FileName + ".txt", FileMode.Append, FileAccess.Write);

                    StreamWriter _strWriter = new StreamWriter(LogFile);
                    if (IsAdminLogEnable == true)
                    {
                        _strWriter.WriteLine(string.Format("{0,10} {1,-30} {2,-20} ", ">> " + _ErrorDateTime, Title, ErrorMessage));
                    }
                    _strWriter.Close();
                    LogFile.Close();
                }
            }
            catch(Exception ex)
            {
                //throw;
            }
        }
    }
}
