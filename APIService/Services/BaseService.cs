using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIService.Services
{
    /// <summary>
    /// Базовый сервис для работы с БД
    /// </summary>
    /// <typeparam name="T">Тип сервиса</typeparam>
    public abstract class BaseService<T> : DbContext where T : class 
    {
        public BaseService(DbContextOptions options) : base(options)
        {
            

        }

        /// <summary>
        /// Список всех элементов
        /// </summary>
        /// <returns>Возвращает список всех элементов</returns>
        public abstract IList<T> all();

        /// <summary>
        /// Добавляет в БД элемент
        /// </summary>
        /// <param name="item">Элемент для добавления</param>
        public abstract void add(T item);

        /// <summary>
        /// Возвращает элемент по Id
        /// </summary>
        /// <param name="id">Иденткфикатор элемента</param>
        /// <returns> Возвращает элемент по Id</returns>
        public abstract T get(int id);

        /// <summary>
        /// Возвращает элемент по условию
        /// </summary>
        /// <param name="item">Условия выборки</param>
        /// <returns>Возвращает элементы по условию</returns>
        public abstract IList<T> get(T item);

        /// <summary>
        /// Обновляет элемент
        /// </summary>
        /// <param name="id">Иденткфикатор элемента</param>
        /// <param name="item">Новый элемент</param>
        /// <returns>Возвращает обновленный элемент</returns>
        public abstract T update(int id, T item);
        /// <summary>
        /// Удаляет элемент
        /// </summary>  
        /// <param name="id">Иденткфикатор элемента</param>
        /// <returns>Возвращает удален ли элемент</returns>
        public abstract bool delete(int id);
    }
}
