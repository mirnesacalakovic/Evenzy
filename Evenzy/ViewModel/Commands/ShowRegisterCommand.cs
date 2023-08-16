using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public LoginVM LoginVM { get; set; }
        public ShowRegisterCommand(LoginVM loginVM)
        {
            LoginVM = loginVM;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            LoginVM.SwitchViews();
        }
    }
}
