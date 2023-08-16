using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class FilterYearCommand : ICommand
    {
        public EventVM VM { get; set; }

        public FilterYearCommand(EventVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            
            if (VM.SelectedDate == null)
                return false;
            return true;
            
        }

        public void Execute(object parameter)
        {
            VM.FilterYear();
        }
    }
    
}
