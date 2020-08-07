using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Command;
using Client.Helpers;
using Client.Views;
using GalaSoft.MvvmLight;
using Model;
using Newtonsoft.Json;

namespace Client.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private Users _user;

        public Users user
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged("user");
            } 
        }


        public ICommand LoginCommand { get; set; }

        public AuthViewModel()
        {
            user = new Users();
            if (!string.IsNullOrEmpty(SettingsHelper.GetLogin()) && !string.IsNullOrEmpty(SettingsHelper.GetPassword()))
            {
                var login = SettingsHelper.GetLogin();
                var password = SettingsHelper.GetPassword();
                AuthUser(login, password);
            }

            LoginCommand = new RelayCommand(t =>
            {
                string login = user.login;
                string password = user.pass;
                AuthUser(login, password);
            });
        }

        private void AuthUser(string login, string password)
        {
          

            var param = new Dictionary<string, string>();
            param.Add("login",login);
            param.Add("pass",password);

           

            ApiHelper.APIRequest(ValuesHelper.API_URL, ValuesHelper.API_USER, ValuesHelper.API_AUTH, res =>
            {
                var d = JsonConvert.DeserializeObject<Message<Nomenclature>>(res);
                if (d.status.Contains("success"))
                {
                    SettingsHelper.SetLogin(user.login);
                    SettingsHelper.SetPassword(user.pass);
                    var main = new Main();
                    Window current = Application.Current.MainWindow;
                    main.Show();
                    current?.Close();

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка");
                }
            }, param, "post");
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
