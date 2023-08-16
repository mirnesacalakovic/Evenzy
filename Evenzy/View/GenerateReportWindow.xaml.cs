using Evenzy.ViewModel;
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

namespace Evenzy.View
{
    /// <summary>
    /// Interaction logic for GenerateReportWindow.xaml
    /// </summary>
    public partial class GenerateReportWindow : Window
    {
        private EventVM eventVM;
        public GenerateReportWindow()
        {
            InitializeComponent();
            eventVM = Resources["EventVM"] as EventVM;
            eventVM.FromDate = DateTime.Now;
            eventVM.ToDate = DateTime.Now;
            eventVM.ReportWindow += ReportWindowEventHandler;
        }

        
        

    private void ReportWindowEventHandler(object? sender, EventArgs e)
    {
        Close();
    }
}
}
