using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Users : INotifyPropertyChanged, ICloneable
    {
        private string _pass;
        private string _login;
        private int _idUser;

        [Key]
        public int id_user
        {
            get => _idUser;
            set
            {
                _idUser = value;
                NotifyPropertyChanged("id_user");
            } 
        }

        public string login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged("login");
            } 
        }

        public string pass
        {
            get => _pass;
            set
            {
                _pass = value;
                NotifyPropertyChanged("pass");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return new Users(){ id_user = this.id_user, login = this.login, pass = this.pass};
        }
    }
}
