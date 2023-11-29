using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MediTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //[STAThread]
        //public static void Main()
        //{
        //    App app = new App();
        //    MainWindow mainWindow = new MainWindow();
        //    app.Run(mainWindow);
        //}
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create an instance of the MainWindow
            MainWindow mainWindow = new MainWindow();

            // Show the MainWindow
            mainWindow.Show();
        }
    }


    
}
