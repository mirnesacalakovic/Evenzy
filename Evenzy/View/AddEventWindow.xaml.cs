﻿using Evenzy.ViewModel;
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
    /// Interaction logic for AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        private EventVM eventVM;
        public AddEventWindow()
        {
            InitializeComponent();

            eventVM = Resources["EventVM"] as EventVM;
            eventVM.EventAdded += EventVM_EventAdded;
        }

        private void EventVM_EventAdded(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
