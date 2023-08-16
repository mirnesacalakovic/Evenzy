using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class FilterMonthCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public EventVM EventVM { get; set; }

        public FilterMonthCommand(EventVM viewModel)
        {
            EventVM = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (EventVM.SelectedDate == null)
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            EventVM.FilterMonth();
        }
    }
    
}
