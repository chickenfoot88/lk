namespace DependencyResolver
{
    using System;
    using System.Collections.Generic;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    /// <summary>
    /// Резолвер зависимостей
    /// </summary>
    public static class DependencyProvider
    {
        /// <summary>
        /// Список разрезовленных объектов
        /// </summary>
        private static readonly List<Type> RegisteredObjects;

        /// <summary>
        /// Контейнер
        /// </summary>
        private static readonly IWindsorContainer Container;


        /// <summary>
        /// Конструктор
        /// </summary>
        static DependencyProvider()
        {
            RegisteredObjects = new List<Type>();
            Container = new WindsorContainer();
        }
      
        /// <summary>
        /// Резолв объекта
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T : class, new() 
        {
            //если объект был зарегистрирован ранее, то резолвим
            if (RegisteredObjects.Contains(typeof (T))) return Container.Resolve<T>();

            //регистрируем и резолвим
            Container.Register(Component.For<T>().LifestyleSingleton());
            RegisteredObjects.Add(typeof(T));
            return Container.Resolve<T>();
        }
    }
}
