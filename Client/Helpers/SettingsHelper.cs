using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class SettingsHelper
    {
        /// <summary>
        /// Получает логин пользователя
        /// </summary>
        /// <returns>Возвращает логин</returns>
        public static string GetLogin()
        {
            string type = "";
            try
            {
                return Properties.Settings.Default.login;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return type;
        }

        /// <summary>
        /// Устанавливает логин пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        public static void SetLogin(string login)
        {
            try
            {
                Properties.Settings.Default.login = login;
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        /// <summary>
        /// Получает пароль пользователя
        /// </summary>
        /// <returns>Возвращает пароль</returns>
        public static string GetPassword()
        {
            string type = "";
            try
            {
                return Properties.Settings.Default.password;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return type;
        }

        /// <summary>
        /// Устанавливает пароль пользователя
        /// </summary>
        /// <param name="password">Пароль пользователя</param>
        public static void SetPassword(string password)
        {
            try
            {
                Properties.Settings.Default.password = password;
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
