using Evenzy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class AddNewEventCommand : ICommand
    {
        public EventVM EventVM { get; set; }
        public AddNewEventCommand(EventVM vM)
        {
            EventVM = vM; 
        }

        public AddEventWindow AddEventWindow { get; set; }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //metoda iz eventvm
            EventVM.ShowWindow();   
        }
    }
}
