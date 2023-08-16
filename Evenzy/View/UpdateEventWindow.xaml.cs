using Evenzy.Model;
using Evenzy.ViewModel;
using Evenzy.ViewModel.Helpers;
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
    /// Interaction logic for UpdateEventWindow.xaml
    /// </summary>
    public partial class UpdateEventWindow : Window
    {
        EventVM eventVM;
        public UpdateEventWindow(Event _selectedEvent)
        {

            InitializeComponent();

            eventVM = Resources["EventVM"] as EventVM;

            eventVM.EventDeleted += EventVM_EventDeleted;
            eventVM.EventUpdated += EventVM_EventUpdated;

            eventVM.SelectedEvent = _selectedEvent;

            
            Name.Text = _selectedEvent.Name;
            Description.Text = _selectedEvent.Description;
            Start.Value =_selectedEvent.StartDate;
            End.Value =  _selectedEvent.EndDate;
            Category.SelectedValue = _selectedEvent.Category;
            Priority.SelectedValue = _selectedEvent.Priority;


        }

        private void EventVM_EventUpdated(object? sender, EventArgs e)
        {
            Close();
        }

        private void EventVM_EventDeleted(object? sender, EventArgs e)
        {
            Close();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            
        }

        
    }
}
