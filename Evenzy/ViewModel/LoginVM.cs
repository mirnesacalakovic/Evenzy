using Evenzy.Model;
using Evenzy.ViewModel.Commands;
using Evenzy.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Evenzy.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {

        private bool isRegisterVisible = false;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler Authenticated;
        private User user;
        public User User
        {
			get { return user; }
			set 
            { 
                user = value; 
                OnPropertyChanged("User");
            }
		}

        private string email;
        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User
                {
                    Email = this.Email,
                    Password = password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    Name = name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }
        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }


        private Visibility registerVis;

        public Visibility RegisterVis
        {
            get { return registerVis; }
            set 
            { 
                registerVis = value; 
                OnPropertyChanged("RegisterVis");
            }
        }
        private Visibility loginVis;
        public Visibility LoginVis
        {
            get { return loginVis; }
            set
            {
                loginVis = value;
                OnPropertyChanged("LoginVis");
            }
        }

        private bool isLoggedIn;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set 
            { 
                isLoggedIn = value; 
                OnPropertyChanged("IsLoggedIn");
            }
        }

        public void SwitchViews()
        {
            isRegisterVisible = !isRegisterVisible;
            if (isRegisterVisible)
            {
                RegisterVis = Visibility.Visible;
                LoginVis = Visibility.Collapsed;
            }
            else
            { 
                RegisterVis = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }
        public LoginVM()
        {
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            User = new User();
        }

        //LOGIN METODA
        public void Login()
        {
            var userDb = DatabaseHelper.Read<User>().Where(u => u.Email == Email).ToList().FirstOrDefault();

            if(userDb == null)
            {
                MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(userDb.Password == Password)
            {
                isLoggedIn = true;
                User = userDb;
                App.UserId = userDb.Id.ToString();
                Authenticated?.Invoke(this, new EventArgs());
                return;
            }
            else
            {
                MessageBox.Show("Password is not correct");
                return;
            }
        }

        //REGISTER METODA
        public void Register()
        {
            var userDb = new User
            {
                Email = this.Email,
                Password = this.Password,
                ConfirmPassword = this.ConfirmPassword,
                Name = this.Name,
                Lastname = this.Lastname
            };
            var result = DatabaseHelper.Insert<User>(userDb);
            if(result == false)
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            userDb = DatabaseHelper.Read<User>().Where(u => u.Email == Email).ToList().FirstOrDefault();
            if(userDb != null)
            {
                App.UserId = User.Id.ToString();
                IsLoggedIn = true;
                User = userDb;
                Authenticated?.Invoke(this, new EventArgs());
            }


        }
        
    }
}
