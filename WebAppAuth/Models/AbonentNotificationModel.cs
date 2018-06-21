using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Main;

namespace WebAppAuth.Models
{
    /// <summary>
    /// Уведомление по дому
    /// </summary>
    public class AbonentNotificationModel : BaseEntity
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        [Display(Name = "Заголовок")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        [Display(Name = "Содержимое")]
        public virtual string Content { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [Display(Name = "Список домов")]
        public virtual List<long> HouseId { get; set; }

        /// <summary>
        /// Список адресов домов
        /// </summary>
        [Display(Name = "Список адресов домов")]
        public virtual List<string> HouseFullAddress { get; set; }

        /// <summary>
        /// Проекция сущности на модель
        /// </summary>
        public static Expression<Func<Notification, AbonentNotificationModel>> ProjectionExpression =
            x => new AbonentNotificationModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreationTime = x.CreationTime,
                ChangedTime = x.ChangedTime
            };


        /// <summary>
        /// Заполнение сущности по модели
        /// </summary>
        public static void ApplyToEntity(Notification entity, AbonentNotificationModel model)
        {
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.ChangedTime = DateTime.Now;
        }
    }
}