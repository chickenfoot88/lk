using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;
using WebAppAuth.Entities;

namespace WebAppAuth.Models
{
    /// <summary>
    /// Должность
    /// </summary>
    public class PositionModel : BaseEntity
    {
        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Список доступных видов заявок
        /// </summary>
        [Display(Name = "Список доступных видов заявок")]
        public ICollection<DictClaimKind> ClaimKinds { get; set; }
        
        /// <summary>
        /// Проекция сущности на модель
        /// </summary>
        public static Expression<Func<Position, PositionModel>> ProjectionExpression =
            x => new PositionModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationTime = x.CreationTime,
                ChangedTime = x.ChangedTime
            };


        /// <summary>
        /// Заполнение сущности по модели
        /// </summary>
        public static void ApplyToEntity(Position entity, PositionModel model)
        {
            entity.Name = model.Name;
            entity.ChangedTime = DateTime.Now;
        }
    }
}