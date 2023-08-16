using Evenzy.Model;
using Evenzy.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class DeleteCommand : ICommand
    {
        public EventVM EventVM { get; set; }

        public DeleteCommand(EventVM eventVM)
        {
            EventVM = eventVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            EventVM.DeleteEvent();
        }
    }
    
}
