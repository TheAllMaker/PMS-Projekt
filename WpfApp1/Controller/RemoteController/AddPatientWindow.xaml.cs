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

        private void TextBoxMonitorId_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextMonitorID.Visibility = Visibility.Collapsed; // Platzhaltertext ausblenden
        }

        private void TextBoxMonitorId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxMonitorId.Text))
            {
                PlaceholderTextMonitorID.Visibility = Visibility.Visible; // Platzhaltertext anzeigen, wenn die TextBox leer ist
            }
        }

        private void TextBoxFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextFirstName.Visibility = Visibility.Collapsed; // Platzhaltertext ausblenden
        }

        private void TextBoxFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxFirstName.Text))
            {
                PlaceholderTextFirstName.Visibility = Visibility.Visible; // Platzhaltertext anzeigen, wenn die TextBox leer ist
            }
        }

        private void TextBoxLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextLastName.Visibility = Visibility.Collapsed; // Platzhaltertext ausblenden
        }

        private void TextBoxLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxLastName.Text))
            {
                PlaceholderTextLastName.Visibility = Visibility.Visible; // Platzhaltertext anzeigen, wenn die TextBox leer ist
            }
        }

        private void TextBoxAVRP_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextAVRP.Visibility = Visibility.Collapsed; // Platzhaltertext ausblenden
        }

        private void TextBoxAVRP_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxAVRP.Text))
            {
                PlaceholderTextAVRP.Visibility = Visibility.Visible; // Platzhaltertext anzeigen, wenn die TextBox leer ist
            }

        }

        private void TextBoxEWS_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextEWS.Visibility = Visibility.Collapsed; // Platzhaltertext ausblenden
        }

        private void TextBoxEWS_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxEWS.Text))
            {
                PlaceholderTextEWS.Visibility = Visibility.Visible; // Platzhaltertext anzeigen, wenn die TextBox leer ist
            }

        }
    }
}
