using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace APIService.Controllers
{
    public abstract class BaseController : ControllerBase
    {

        /// <summary>
        /// Получение элемента 
        /// </summary>
        /// <param name="id">Идентефикатор элемента</param>
        /// <returns>Взовращает элемент по его id</returns>
        [HttpGet("{id}")]
        public abstract ActionResult<string> Get(int id);

        /// <summary>
        /// Получает все элементы
        /// </summary>
        /// <returns>Взовращает все элементы</returns>
        [HttpGet]
        public abstract ActionResult<string> Get();

        /// <summary>
        /// Добавляет элемент в БД
        /// </summary>
        /// <returns>Возвращает элемент в случае успеха</returns>
        [HttpPost("add")]
        public abstract ActionResult<string> Add();


        /// <summary>
        /// Удаляет элемент из БД
        /// </summary>
        /// <returns>Возвращает значение в случае успеха</returns>
        [HttpPost("delete")]
        public abstract ActionResult<string> Delete();


        /// <summary>
        /// Обновляет элемент в БД
        /// </summary>
        /// <param name="id">Идентефикатор элемента</param>
        /// <returns>Возвращает элемент в случае успеха</returns>
        [HttpPost("update")]
        public abstract ActionResult<string> Update();


        /// <summary>
        /// Ошибка
        /// </summary>
        /// <param name="reason">Причина ошибки</param>
        /// <returns>Возвращает ошибку</returns>
        public ActionResult<string> error(string reason = null)
        {
           return new ActionResult<string>(JsonConvert.SerializeObject(new Message<Task>() { status = "error", reason = reason }));
        }


        /// <summary>
        /// Успех
        /// </summary>
        /// <typeparam name="T">Тип элемента</typeparam>
        /// <param name="value">Мвссив элементов</param>
        /// <returns>Возвращает статус успеха с элементами</returns>
        public ActionResult<string> result<T>(IList<T> value = null) 
        {

            return new ActionResult<string>(JsonConvert.SerializeObject(new Message<T>() { status = "success", data = value }));
        }
    }
}
