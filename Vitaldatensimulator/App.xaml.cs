using System;
using System.Windows;

namespace Vitaldatensimulator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Erzeuge die Startfensterinstanz
            MainCreatePatientWindow startWindow = new MainCreatePatientWindow();

            // Zeige das Startfenster an
            startWindow.Show();
        }
    }
}
