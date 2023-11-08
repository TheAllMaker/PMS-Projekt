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
            //string FirstNameString = txtFirstName.Text;
            //string LastNameString = txtLastName.Text;
            //string PatientNumberIDString =  txtPatientNumber.Text;
            //int PatientNumberID = int.Parse(PatientNumberIDString);
            //Database.Patient p = new Database.Patient(FirstName:FirstNameString, LastName:LastNameString,PatientNumber:PatientNumberID );
            //Database.SQLqueries.addPatient(p);
        }

        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AbortButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void TextBoxPatientId_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextPatientID.Visibility = Visibility.Collapsed; // Platzhaltertext ausblenden
        }

        private void TextBoxPatientId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxPatientId.Text))
            {
                PlaceholderTextPatientID.Visibility = Visibility.Visible; // Platzhaltertext anzeigen, wenn die TextBox leer ist
            }
        }
    }
}
