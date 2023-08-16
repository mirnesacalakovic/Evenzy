using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class GenerateReportCommand : ICommand
    {
        public EventVM EventVM { get; set; }

        public GenerateReportCommand(EventVM vM)
        {
            EventVM = vM;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            EventVM.GenerateReport();
        }
    }
    
}
