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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginVM viewModel;
        EventVM eventVM;
        public LoginWindow()
        {
            InitializeComponent();

            viewModel = Resources["LoginVM"] as LoginVM;
            viewModel.Authenticated += ViewModel_Authenticated;
            eventVM = Resources["EventVM"] as EventVM;

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void ViewModel_Authenticated(object sender, EventArgs e)
        {
            eventVM.GetEvents();
            Close();
        }
    }
}
