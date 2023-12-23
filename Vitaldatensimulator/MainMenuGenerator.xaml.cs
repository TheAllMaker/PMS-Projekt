using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vitaldatensimulator
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private MainCreatePatientWindow Simulator;
        private int zaehler;

        public MainMenu()
        {
            InitializeComponent();
            //this.Closing += MainWindow_Closing;
        }

        private void StartNewGenerator_Click(object sender, RoutedEventArgs e)
        {
            Simulator = new MainCreatePatientWindow();
            Simulator.Closing -= Simulator.MainWindow_Closing;
            Simulator.Show();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            zaehler += 1;
            ConfirmClose();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            zaehler += 1;
            ConfirmClose();
        }

        private void ConfirmClose()
        {
            if (zaehler == 1)
            {
                MessageBoxResult result = MessageBox.Show("Möchten Sie wirklich alle Generatoren und das Hauptmenü schließen?", "Schließen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Simulator.UpdateAliveStatus();
                    // Schließe das Programm
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
