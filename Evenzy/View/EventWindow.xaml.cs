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
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        private EventVM eventVM;
        public EventWindow()
        {

            InitializeComponent();

            eventVM = Resources["EventVM"] as EventVM;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (!string.IsNullOrEmpty(App.UserId))
                    eventVM.GetEvents();
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event selectedEvent = (Event)eventListView.SelectedItem;


            if (selectedEvent != null)
            {
                
                UpdateEventWindow updateEventWindow = new UpdateEventWindow(selectedEvent);
                updateEventWindow.ShowDialog();

                eventVM.GetEvents();
            }

            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var selectedEvent = checkBox.DataContext as Event;
                if (selectedEvent != null)
                {
                    DatabaseHelper.Update(selectedEvent);
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var selectedEvent = checkBox.DataContext as Event;
                if (selectedEvent != null)
                {
                    DatabaseHelper.Update(selectedEvent);
                }
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            var userId = int.Parse(App.UserId);
            var filteredEvents = DatabaseHelper.Read<Event>().Where(f => f.Done == true && f.UserId == userId).ToList();
            if (eventVM.SelectedCategory != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Category == eventVM.SelectedCategory).ToList();
            }
            
            if (eventVM.SelectedPriority != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Priority == eventVM.SelectedPriority).ToList();
            }
            eventVM.Events.Clear();

            foreach (var f in filteredEvents)
            {
                eventVM.Events.Add(f);
            };
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {

            var userId = int.Parse(App.UserId);
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.Done == false && e.UserId == userId).ToList();
            if (eventVM.SelectedCategory != null)
            {
               filteredEvents = filteredEvents.Where(e => e.Category == eventVM.SelectedCategory).ToList();
            }
            
            if (eventVM.SelectedPriority != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Priority == eventVM.SelectedPriority).ToList();
            }
            eventVM.Events.Clear();
            foreach (var f in filteredEvents)
            {
                eventVM.Events.Add(f);
            };

            
        }

        private void High_Button_Click(object sender, RoutedEventArgs e)
        {
            var userId = int.Parse(App.UserId);
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.Priority == "High" && e.UserId == userId).ToList();
           
            eventVM.Events.Clear();
            foreach (var f in filteredEvents)
            {
                eventVM.Events.Add(f);
            };
            var filteredEvents1 = DatabaseHelper.Read<Event>().Where(e => e.Priority == "Medium" && e.UserId == userId).ToList();
            foreach (var f in filteredEvents1)
            {
                eventVM.Events.Add(f);
            };
            var filteredEvents2 = DatabaseHelper.Read<Event>().Where(e => e.Priority == "Low" && e.UserId == userId).ToList();
            foreach (var f in filteredEvents2)
            {
                eventVM.Events.Add(f);
            };


        }
        private void Low_Button_Click(object sender, RoutedEventArgs e)
        {
            var userId = int.Parse(App.UserId);
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.Priority == "Low" && e.UserId == userId).ToList();
           
            eventVM.Events.Clear();
            foreach (var f in filteredEvents)
            {
                eventVM.Events.Add(f);
            };
            var filteredEvents1 = DatabaseHelper.Read<Event>().Where(e => e.Priority == "Medium" && e.UserId == userId).ToList();
            foreach (var f in filteredEvents1)
            {
                eventVM.Events.Add(f);
            };
            var filteredEvents2 = DatabaseHelper.Read<Event>().Where(e => e.Priority == "High" && e.UserId == userId).ToList();
            foreach (var f in filteredEvents2)
            {
                eventVM.Events.Add(f);
            };
        }
    }
              
}
