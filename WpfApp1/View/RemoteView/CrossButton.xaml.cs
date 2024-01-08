using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MediTrack.Model.RemoteModel;

namespace MediTrack.View.RemoteView
{

    public partial class CrossButton : ResourceDictionary
    {
        MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;

        public CrossButton()
        {
            InitializeComponent();
        }




        private void ShowBlockOptions(object sender, RoutedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control != null)
            {
                var popup = control.FindName("CrossButtonOptionsPopUp") as Popup;
                if (popup != null)
                {
                    popup.IsOpen = true;
                }
            }
        }




        private void CrossButtonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                var selectedValue = listBox.SelectedItem.ToString();

                if (int.TryParse(selectedValue, out int intValue))
                {
                    ActiveMonitorIDManager.InsertActiveMonitor(intValue);
                }
                else
                {
                    Console.Write("Test");
                }
            }
        }

    }
}
