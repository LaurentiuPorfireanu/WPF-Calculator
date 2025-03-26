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

namespace Calculator
{

    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void StartStandar_Click(object sender, RoutedEventArgs e)
        {
            var seccondWindow = new Standard();
            seccondWindow.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StartProgrammer_Click(object sender, RoutedEventArgs e)
        {
            var programmerWindow = new Programmer();
            programmerWindow.Show();
        }


        private void DigitGroupingRadio_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.EnableDigitGrouping = true;
        }

        private void DigitGroupingRadio_Unchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.EnableDigitGrouping = false;
        }


    }

  
}
