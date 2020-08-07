using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Helpers
{
    class ApiHelper 
    {
        /// <summary>
        /// Отправка API запросов 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="callbackAction">Действие по окончании запроса</param>
        /// <param name="action">Контроллер</param>
        /// <param name="param">Параметры</param>
        public static async void APIRequest(string Url, string controller, string action = null, Action<string> callbackAction = null,  Dictionary<string, string> param = null, string type = "get")
        {
            HttpClient client = new HttpClient();
            try
            {
                string url = $"{Url}/{controller}";
                HttpResponseMessage response = null;
                if (action != null)
                {
                    url += $"/{action}";
                }

                if (type == "get")
                {
                    if (param != null)
                    {
                        url += $"/?{QueryString(param)}";
                    }
                    response = await client.GetAsync(url);
                }
                else
                {
                    if(param != null)
                    {
                        var content = new FormUrlEncodedContent(param);
                        response = await client.PostAsync(url, content);
                    }
                }

                var responseString = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseString))
                {
                    Console.WriteLine(responseString);
                    callbackAction?.Invoke(responseString);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        /// <summary>
        /// Преобразует словарь в пару ключ=значение
        /// </summary>
        /// <param name="dict">Исходный словарь</param>
        /// <returns>Возвращает словарь в виде строки</returns>
        private static string QueryString(IDictionary<string, string> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(item.Key + "=" + item.Value);
            }
            return string.Join("&", list);
        }

    }
}
