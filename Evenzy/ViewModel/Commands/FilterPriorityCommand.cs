using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class FilterPriorityCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public EventVM EventVM { get; set; }

        public FilterPriorityCommand(EventVM vM)
        {
            EventVM = vM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            EventVM.FilterPriority();
        }
    }
}
