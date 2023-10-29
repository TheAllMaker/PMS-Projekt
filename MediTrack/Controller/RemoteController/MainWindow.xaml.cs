﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void API_Button_Clicked(object sender, RoutedEventArgs e)
        {
            // Handle Button 1 click event
        }

        private void Select_Button_Clicked(object sender, RoutedEventArgs e)
        {
            // Instanziert ein neues Fenster
            Window SelectWindow = new Window
            {
                Title = "Select Patient", // Name des neuen Fenster's
                Width = SystemParameters.PrimaryScreenWidth * 0.75, 
                Height = SystemParameters.PrimaryScreenHeight * 0.75,
                WindowStartupLocation = WindowStartupLocation.CenterScreen 
            };
            SelectWindow.Show();    
            SelectWindow.Owner = this;
            //SelectWindow.ShowDialog();


        }

        private void Add_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Window AddNewPatient = new Window
            {
                Title = "Add a new Patient",
                Width = SystemParameters.PrimaryScreenWidth * 0.75,
                Height = SystemParameters.PrimaryScreenHeight * 0.75,
                WindowStartupLocation= WindowStartupLocation.CenterScreen 
            };
            AddNewPatient.Show();   
            AddNewPatient.Owner = this; 

        }

        private void POWER_Button_Clicked(object sender, RoutedEventArgs e)
        {

        }


    }
}