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
using Vitaldatensimulator;


namespace MediTrack.View.RemoteView
{
    /// <summary>
    /// Interaktionslogik für AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        public AddPatientWindow()
        {
            InitializeComponent();
            Database.Patient p = new Database.Patient(prename:"Franz", surname:"Zufall", birthday: new DateTime(1993,12,23));
        }
    }
}
