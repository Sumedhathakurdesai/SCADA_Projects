using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
namespace IndiSCADA_ST
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        // 
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk0NzYyQDMxMzgyZTMxMmUzMFA2VzBrdG5GTkV3NmZUMWdkS2J6STNKTnFDNjhpU3JvQ2RKakRrUnk0ZWM9"); 
        }

        // 
        protected override void OnStartup(StartupEventArgs e)
        {
            #region "Code  for avoid to run same instance of exe" commented
            //const string appName = "IndiSCADA-ST";
            //bool createdNew;
            //_mutex = new Mutex(true, appName, out createdNew);
            //if (!createdNew)
            //{
            //    //app is already running! Exiting the application  
            //    Application.Current.Shutdown();
            //}
            //base.OnStartup(e);
            #endregion

            #region "Code to clean the IndiSCADA Folder from Appdata"
               CleanTemporaryFolders();
            #endregion
        }

        // for folder delete
        private void CleanTemporaryFolders()
        {
            String tempFolder = Environment.ExpandEnvironmentVariables("%USERPROFILE%");
            string partialName = "IndiSCADA-ST";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(tempFolder + "\\" + "AppData" + "\\" + "Local\\Microsoft" + "\\");
            FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + partialName + "*");

            foreach (FileSystemInfo foundFile in filesAndDirs)
            {
                string fullName = foundFile.FullName;
                try
                {
                    Directory.Delete(fullName, true);
                }
                catch (Exception excep)
                {
                    System.Diagnostics.Debug.WriteLine(excep);
                }
            }
        }

        //internal class Current
        //{
        //}
    }
}
