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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeehiveManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Quenn quenn;

        public MainWindow()
        {
            InitializeComponent();
            quenn = new Quenn();
            statusReport.Text = quenn.StatusReport;
        }

        private void AssignJob_Click(object sender, RoutedEventArgs e)
        {
            quenn.AssignBee(jobSelector.Text);
            statusReport.Text = quenn.StatusReport;
        }

        private void WorkShift_Click(object sender, RoutedEventArgs e)
        {
            quenn.WorkTheNextShift();
            statusReport.Text = quenn.StatusReport;
        }
    }
}
