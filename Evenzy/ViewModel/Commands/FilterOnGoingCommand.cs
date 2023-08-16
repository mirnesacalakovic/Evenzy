using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class FilterOnGoingCommand : ICommand
    {
        public EventVM EventVM { get; set; }

        public FilterOnGoingCommand(EventVM eventVM)
        {
            EventVM = eventVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        // This method is called when the command is executed.
        public void Execute(object parameter)
        {
            EventVM.FilterOnGoing();
        }
    }
}
