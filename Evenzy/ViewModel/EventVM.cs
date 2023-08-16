using Evenzy.Model;
using Evenzy.View;
using Evenzy.ViewModel.Commands;
using Evenzy.ViewModel.Helpers;
using Evenzy.ViewModel.ValueConverters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Evenzy.ViewModel
{
    public class EventVM : INotifyPropertyChanged
    {
        public ObservableCollection<Event> Events { get; set; } //ovaj prop ce biti vezan za list view i prikazivace eventove
        //public ObservableCollection<Category> Categories { get; set; } //ovaj prop ce biti vezan za combobox i prikazivace kategorije

        public event EventHandler EventAdded;
        public event EventHandler EventDeleted;
        public event EventHandler EventUpdated;
        public event EventHandler ReportWindow;

        //imacemo selected event
        

        public UpdateEventWindow UpdateEventWindow { get; set; }
        public AddEventWindow OpenedAddEventWindow { get; set; }
        public GenerateReportWindow GenerateReportWindow { get; set; }

        public GenerateReportCommand GenerateReportCommand { get; set; }
        public GenRepWinCommand GenRepWinCommand { get; set; }
        public AddNewEventCommand AddNewEventCommand { get; set; }
        public AddEventCommand AddEventCommand { get; set; }
        public DeleteCommand DeleteCommand { get; set; }
        public UpdateCommand UpdateCommand { get; set; }
        public CheckedCommand CheckedCommand { get; set; }
        public FilterYearCommand FilterYearCommand { get; set; }
        public FilterMonthCommand FilterMonthCommand { get; set; }
        public FilterTodayCommand FilterTodayCommand { get; set; }
        public AllEventsCommand AllEventsCommand { get; set; }
        public FilterCategoryCommand FilterCategoryCommand { get; set; }
        public FilterPriorityCommand FilterPriorityCommand { get; set; }
        public FilterOnGoingCommand FilterOnGoingCommand { get; set; }
        public EventVM()
        {
            Events = new ObservableCollection<Event>();
            AddNewEventCommand = new AddNewEventCommand(this);
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow;
            SelectedDate = DateTime.UtcNow;
            AddEventCommand = new AddEventCommand(this);
            CheckedCommand = new CheckedCommand(this);
            DeleteCommand = new DeleteCommand(this);
            UpdateCommand = new UpdateCommand(this);
            FilterYearCommand = new FilterYearCommand(this);
            FilterMonthCommand = new FilterMonthCommand(this);
            FilterTodayCommand = new FilterTodayCommand(this);
            AllEventsCommand = new AllEventsCommand(this);
            FilterCategoryCommand = new FilterCategoryCommand(this);
            FilterPriorityCommand = new FilterPriorityCommand(this);
            GenerateReportCommand = new GenerateReportCommand(this);
            GenRepWinCommand = new GenRepWinCommand(this);
            FilterOnGoingCommand = new FilterOnGoingCommand(this);


            Events.CollectionChanged += Events_CollectionChanged;
        }

        

        private void Events_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Events");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set 
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }
        

        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set 
            { 
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        private string selectedPriority;

        public string SelectedPriority
        {
            get { return selectedPriority; }
            set 
            { 
                selectedPriority = value;
                OnPropertyChanged("SelectedPriority");
            }
        }


        private Event selectedEvent;

        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set { selectedEvent = value;
                //OnPropertyChanged("SelectedEvent");
            }
        }

        public void ShowWindow()
        {
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow;
            OpenedAddEventWindow = new AddEventWindow();
            OpenedAddEventWindow.ShowDialog();
            GetEvents();
        }
        public void ShowWindow2()
        {
            FromDate = DateTime.UtcNow;
            ToDate = DateTime.UtcNow;
            GenerateReportWindow = new GenerateReportWindow();
            GenerateReportWindow.ShowDialog();
            
        }



        //property za eventove

        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                OnPropertyChanged("Name");
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; 
                OnPropertyChanged("Description");
            }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set 
            { 
                category = value;
                OnPropertyChanged("Category");
            }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set 
            { 
                startDate = DateTime.Parse(value.ToString());
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { 
                endDate = DateTime.Parse(value.ToString());
                OnPropertyChanged("EndDate");
            }
        }

           

        private bool done;

        public bool Done
        {
            get { return done; }
            set 
            { 
                done = value; 
                OnPropertyChanged("Done");
            }
        }

        private readonly KeyValuePair<string, string>[] priorities = {
            new KeyValuePair<string, string>("High", "High"),
            new KeyValuePair<string, string>("Medium", "Medium"),
            new KeyValuePair<string, string>("Low", "Low")
        };

        public KeyValuePair<string, string>[] Priorities
        {
            get { return priorities; }
        }

        private readonly KeyValuePair<string, string>[] categories = {
            new KeyValuePair<string, string>("Personal", "Personal"),
            new KeyValuePair<string, string>("Work", "Work"),
            new KeyValuePair<string, string>("Family", "Family"),
            new KeyValuePair<string, string>("Social", "Social"),
            new KeyValuePair<string, string>("Education", "Education"),
            new KeyValuePair<string, string>("Health", "Health"),
            new KeyValuePair<string, string>("Meetings", "Meetings"),
            new KeyValuePair<string, string>("Others", "Others")
        };

        public KeyValuePair<string, string>[] Categories
        {
            get { return categories; }
        }


        private string priority;

        public string Priority
        {
            get { return priority; }
            set 
            {
                priority = value;
                OnPropertyChanged("Priority");
            }
        }

        private DateTime fromDate;

        public DateTime FromDate
        {
            get { return fromDate; }
            set 
            { 
                fromDate = value;
                OnPropertyChanged("FromDate");
            }
        }
        private DateTime toDate;

        public DateTime ToDate
        {
            get { return toDate; }
            set 
            {
                toDate = value;
                OnPropertyChanged("ToDate");
            }
        }

        public void AddEvent()
        {   
            var userid = int.Parse(App.UserId);
            Event newEvent = new Event()
            {
                Name = Name,
                Description = Description,
                Category = Category,
                StartDate = StartDate,
                EndDate = EndDate,
                Priority = Priority,
                UserId = userid
            };
   
            var result = DatabaseHelper.Insert(newEvent);

            if (result == false)
            {
                MessageBox.Show("Faild to add new event", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Event added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            EventAdded?.Invoke(this, new EventArgs());

            GetEvents();
        }

        public void GetEvents()
        {
            if (App.UserId == null || App.UserId == "")
            {
                return;
            }
            var userid = int.Parse(App.UserId);
            var events = DatabaseHelper.Read<Event>().Where(e => e.UserId.Equals(userid)).ToList();

            Events.Clear();

            foreach (var e in events)
            {
                Events.Add(e);
            };
        }

        public void Checked(Event parameter)
        {
            bool isChecked = parameter.Done;
            if(isChecked == true)
            {
                parameter.Done = false;
            }
            else
            {
                parameter.Done = true;
            }

            DatabaseHelper.Update(parameter);
            GetEvents();
        }
        
        public void DeleteEvent()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                DatabaseHelper.Delete(SelectedEvent);
                EventDeleted?.Invoke(this, new EventArgs());
                GetEvents();
            }
            else
            {
                GetEvents();
                return;
            }
        }

        public void UpdateEvent()
        {
            //var categoryId = DatabaseHelper.Read<Category>().SingleOrDefault(c => c.Id == SelectedEvent.CategoryId).Id;

            //if (categoryId == null) return;

            SelectedEvent.Name = Name;
            SelectedEvent.Description = Description;
            SelectedEvent.StartDate = StartDate;
            SelectedEvent.EndDate = EndDate;
            SelectedEvent.Category = Category;
            SelectedEvent.Priority = Priority;

            DatabaseHelper.Update(SelectedEvent);
            EventUpdated?.Invoke(this, new EventArgs());
            GetEvents();

        }
        public void FilterYear()
        {
            var userId = int.Parse(App.UserId);
            var selectedYear = SelectedDate.Year;
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.StartDate.Year == selectedYear && e.UserId == userId).ToList();

            if(SelectedCategory != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Category == SelectedCategory).ToList();
            }
            if(SelectedPriority != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Priority == SelectedPriority).ToList();
            }


            Events.Clear();

            foreach (var e in filteredEvents)
            {
                Events.Add(e);
            };
        }

        public void FilterMonth()
        {
            var userId = int.Parse(App.UserId);
            var selected = SelectedDate;
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.StartDate.Month == selected.Month && e.StartDate.Year == selected.Year && e.UserId == userId).ToList();

            if (SelectedCategory != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Category == SelectedCategory).ToList();
            }
            if (SelectedPriority != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Priority == SelectedPriority).ToList();
            }


            Events.Clear();

            foreach (var e in filteredEvents)
            {
                Events.Add(e);
            };
        }

        public void FilterToday()
        {
            var userId = int.Parse(App.UserId);
            var selectedDay = DateTime.Today;
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.StartDate.Day == selectedDay.Day && e.UserId == userId).ToList();

            Events.Clear();

            foreach (var e in filteredEvents)
            {
                Events.Add(e);
            };
        }
     
        public void FilterOnGoing()
        {   
            var userId = int.Parse(App.UserId);
            SelectedDate = DateTime.Today;
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.StartDate <= SelectedDate && e.EndDate >= SelectedDate && e.UserId == userId).ToList();
            if (SelectedCategory != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Category == SelectedCategory && e.UserId == userId).ToList();
            }
            if (SelectedDate != null)
            {
                filteredEvents = filteredEvents.Where(e => e.StartDate.Year == SelectedDate.Year && e.EndDate.Year >= SelectedDate.Year || e.StartDate.Month == SelectedDate.Month && e.EndDate.Month >= SelectedDate.Month || e.StartDate.Day == SelectedDate.Day && e.EndDate.Day >= SelectedDate.Day && e.UserId == userId).ToList();
            }
            if (SelectedPriority != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Priority == SelectedPriority).ToList();
            }

            Events.Clear();

            foreach (var e in filteredEvents)
            {
                Events.Add(e);
            };

        }

        public void ShowAllEvents()
        {
           
            SelectedPriority = null;
            SelectedCategory = null;
            GetEvents();
        }

        public void FilterCategory()
        {
            var userId = int.Parse(App.UserId);
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.Category == SelectedCategory && e.UserId == userId).ToList();
      
            /*if (SelectedPriority != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Priority == SelectedPriority).ToList();
            }
            if (SelectedDate != null)
            {
                filteredEvents = filteredEvents.Where(e => e.StartDate.Year == SelectedDate.Year && e.EndDate.Year >= SelectedDate.Year || e.StartDate.Month == SelectedDate.Month && e.EndDate.Month >= SelectedDate.Month || e.StartDate.Day == SelectedDate.Day && e.EndDate.Day >= SelectedDate.Day && e.UserId == userId).ToList();
            }*/
            Events.Clear();

            foreach (var e in filteredEvents)
            {
                Events.Add(e);
            };

        }

        public void FilterPriority()
        {
            var userId = int.Parse(App.UserId);
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.Priority == SelectedPriority && e.UserId == userId).ToList();
            if (SelectedCategory != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Category == SelectedCategory && e.UserId == userId).ToList();
            }
            /*if(SelectedDate != null)
            {
                filteredEvents = filteredEvents.Where(e => e.StartDate.Year == SelectedDate.Year && e.EndDate.Year >= SelectedDate.Year || e.StartDate.Month == SelectedDate.Month && e.EndDate.Month >= SelectedDate.Month || e.StartDate.Day == SelectedDate.Day && e.EndDate.Day >= SelectedDate.Day && e.UserId == userId).ToList();
            }*/
            Events.Clear();

            foreach (var e in filteredEvents)
            {
                Events.Add(e);
            };
        }


        public void GenerateReport()
        {
            var userId = int.Parse(App.UserId);
            var startDate = FromDate;
            var endDate = ToDate;
            var filteredEvents = DatabaseHelper.Read<Event>().Where(e => e.StartDate >= startDate && e.EndDate <= endDate && e.UserId == userId).ToList();
        
            string txtFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Report.txt");
        
            FileStream fileStream = new FileStream(txtFile, FileMode.Create);
           
            var contents = filteredEvents;

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.WriteLine("Report from " + startDate.ToLongDateString() + " to " + endDate.ToLongDateString());
                writer.WriteLine(" ");
                writer.WriteLine("Name" + " " + " " + "Description" + " " + " " + "Category" + " " + " " + "Start Date" + " " + " " + "End Date" + " " + " " + "Priority" + " " + " " + "Done");
                writer.WriteLine(" ");
                foreach (var e in contents)
                {
                    writer.WriteLine(e.Name + " " + " " + e.Description + " " + " " + e.Category + " " + " " + e.StartDate + " " + " " + e.EndDate + " " + " " + e.Priority + " " + " " + (e.Done ? "Completed" : "Not Completed"));
                    writer.WriteLine(" ");
                }
            }

            MessageBox.Show("Reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            ReportWindow?.Invoke(this, new EventArgs());
        }
    }
}
