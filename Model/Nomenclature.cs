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
    public class Nomenclature : INotifyPropertyChanged, ICloneable
    {
        private int _idNomenclature;
        private string _name;
        private decimal _price = 0;
        private DateTime _fromDate;
        private DateTime _toDate;

        [Key]
        public int id_nomenclature
        {
            get => _idNomenclature;
            set
            {
                _idNomenclature = value;
                NotifyPropertyChanged("id_nomenclature");

            }
        }

        public string name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged("name");

            }
        }

        public decimal price
        {
            get => _price;
            set
            {
                _price = value;
                NotifyPropertyChanged("price");

            }
        }

        public DateTime fromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                NotifyPropertyChanged("fromDate");

            }
        }

        public DateTime toDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                NotifyPropertyChanged("toDate");
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
            return new Nomenclature() {id_nomenclature = this.id_nomenclature, price = this.price, name = this.name, fromDate = this.fromDate, toDate = this.toDate};
        }
    }
}
