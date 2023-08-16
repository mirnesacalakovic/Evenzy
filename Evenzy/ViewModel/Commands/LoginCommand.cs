using Evenzy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evenzy.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginVM VM { get; set; }
        public LoginCommand(LoginVM vM)
        {
            VM = vM;
        }

        
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public bool CanExecute(object? parameter)
        {
            User user = parameter as User;
            
            if (user == null)
                return false;
            if(string.IsNullOrEmpty(user.Email))
                return false;
            if (string.IsNullOrEmpty(user.Password))
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.Login();
        }
    }
    
    
}
