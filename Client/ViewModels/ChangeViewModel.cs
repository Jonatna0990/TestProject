using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Command;
using Client.Views;
using GalaSoft.MvvmLight;
using Model;

namespace Client.ViewModels
{
    public class ChangeViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; set; }

        private Action<Nomenclature> action;

        public Nomenclature nomenclature
        {
            get => _nomenclature;
            set
            {
                _nomenclature = value;
                RaisePropertyChanged("nomenclature");
            } 
        }

        private Mode mode;
        private Nomenclature _nomenclature;

        public ChangeViewModel()
        {
           
        }
        public void ShowChangeItem(Action<Nomenclature> action, Mode mode, Nomenclature nomenclature = null)
        {

            this.action = action;
            this.mode = mode;
          
            if (nomenclature == null) this.nomenclature = new Nomenclature();
            else this.nomenclature = nomenclature;


            AddCommand = new RelayCommand(t =>
            {
                if(this.nomenclature != null)
                    action?.Invoke(this.nomenclature);
            });

            ChangeItem change = new ChangeItem(mode);
            change.DataContext = this;
            change.ShowDialog();

        }
    }
}
