using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ChangeViewModel changeViewModel;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public ObservableCollection<Nomenclature> Nomenclatures { get; set; }
        public Nomenclature SelectedNomenclature { get; set; }
        public MainViewModel()
        {
            changeViewModel = new ChangeViewModel();
            Nomenclatures = new ObservableCollection<Nomenclature>();
            GetNomenctlatures();

            AddCommand = new RelayCommand(t =>
            {
                changeViewModel.ShowChangeItem(nomenclature =>
                {
                    AddOrUpdateNomenclature(nomenclature, Mode.Add);
                }, Mode.Add);
               
            });
            UpdateCommand = new RelayCommand(t =>
            {
                if (SelectedNomenclature != null)
                {
                  
                    changeViewModel.ShowChangeItem(nomenclature =>
                    {
                        AddOrUpdateNomenclature(nomenclature, Mode.Update);

                    }, Mode.Update, (Nomenclature)SelectedNomenclature.Clone());
                }

            });
            RemoveCommand = new RelayCommand(t =>
            {
                if (SelectedNomenclature != null)
                {
                    var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DeleteNomenclature(SelectedNomenclature);
                    }
                }
                else MessageBox.Show("Не выбран элемент", "Ошибка");

            });
        }

        public void GetNomenctlatures()
        {
            ApiHelper.APIRequest(ValuesHelper.API_URL, ValuesHelper.API_NOMENCLATURE, null, t =>
            {
                var d = JsonConvert.DeserializeObject<Message<Nomenclature>>(t);
                if (d.status.Contains("success"))
                {
                    if (d.data != null)
                    {
                        foreach (var nomenclature in d.data)
                        {
                            Nomenclatures.Add((Nomenclature)nomenclature.Clone());
                        }
                    }
                }
                else
                {
                    MessageBox.Show(d.reason.ToString(), "Ошибка");
                }
            });
        }

        public void DeleteNomenclature(Nomenclature nomenclature)
        {
            Dictionary<string,string> param = new Dictionary<string, string>();
            param.Add("id", nomenclature.id_nomenclature.ToString());
            ApiHelper.APIRequest(ValuesHelper.API_URL, ValuesHelper.API_NOMENCLATURE, ValuesHelper.API_DELETE, t =>
            {
                var d = JsonConvert.DeserializeObject<Message<Nomenclature>>(t);
                if (d.status.Contains("success"))
                {
                    Nomenclatures.Remove(nomenclature);
                }
                else
                {
                    MessageBox.Show(d.reason.ToString(), "Ошибка");
                }
            }, param, "post");
        }
        public void AddOrUpdateNomenclature(Nomenclature nomenclature, Mode mode)
        {

            Dictionary<string, string> param = new Dictionary<string, string>();
            int pos = 0;
            if (mode == Mode.Update)
            {
                pos = Nomenclatures.IndexOf(SelectedNomenclature);
                param.Add("id", nomenclature.id_nomenclature.ToString());

            }

            param.Add("name", nomenclature.name);
            param.Add("price", nomenclature.price.ToString("0.##"));
            param.Add("from_date", nomenclature.fromDate.ToString("yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture));
            param.Add("to_date", nomenclature.toDate.ToString("yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture));
            ApiHelper.APIRequest(ValuesHelper.API_URL, ValuesHelper.API_NOMENCLATURE,  mode == Mode.Add ? ValuesHelper.API_ADD : ValuesHelper.API_UPDATE, t =>
            {
                var d = JsonConvert.DeserializeObject<Message<Nomenclature>>(t);
                if (d.status.Contains("success"))
                {
                    if (mode == Mode.Add)
                    {
                        Nomenclatures.Add((Nomenclature)d.data.FirstOrDefault()?.Clone());
                    }
                    else
                    {
                        Nomenclatures.RemoveAt(pos);
                        Nomenclatures.Insert(pos, (Nomenclature)d.data.FirstOrDefault()?.Clone());
                    }
                   
                }
                else
                {
                    MessageBox.Show(d.reason.ToString(), "Ошибка");
                }
            }, param, "post");
        }
     

    }

}
