using Evenzy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class AddEventCommand : ICommand
    {
        public AddEventCommand(EventVM eventVM)
        {
            EventVM = eventVM;
        }

        public EventVM EventVM { get; set; }
        public AddEventWindow AddEventWindow { get; set; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            /*if(EventVM.Name == null)
            {
                MessageBox.Show("Please enter event name");
                return false;
            }
            if (EventVM.Description == null)
            {
                MessageBox.Show("Please enter event description");
                return false;
            }
            if (EventVM.StartDate == null)
            {
                MessageBox.Show("Please enter event start date");
                return false;
            }
            if (EventVM.EndDate == null)
            {
                MessageBox.Show("Please enter event end date");
                return false;
            }
            if (EventVM.Category == null)
            {
                MessageBox.Show("Please enter event category");
                return false;
            }
            if (EventVM.Priority == null)
            {
                MessageBox.Show("Please enter event priority");
                return false;
            }*/
            return true;
        }

        public void Execute(object? parameter)
        {
            //metoda iz eventvm za cuvanje u bazu
            EventVM.AddEvent();
        }
    }
}
