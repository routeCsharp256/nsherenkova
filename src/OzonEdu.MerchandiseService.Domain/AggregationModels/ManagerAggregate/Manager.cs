using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ManagerAggregate
{
    public class Manager: Entity
    {
        public Manager(PersonName name, Email email, int assignedTasks)
        {
            Name = name;
            Email = email;
            AssignedTasks = assignedTasks;
        }

        /// <summary>
        /// Имя менеджера
        /// </summary>
        public PersonName Name { get; }

        /// <summary>
        /// Количество назначенных задач
        /// </summary>
        public int AssignedTasks { get; private set; }

        /// <summary>
        /// Электронная почта менеджера
        /// </summary>
        public Email Email { get; }

        /// <summary>
        /// Может обработать еще одну заявку
        /// </summary>
        public bool CanHandleNewTask => AssignedTasks < 3;

        /// <summary>
        /// Назначить еще одну задачу
        /// </summary>
        public void AssignTask() => AssignedTasks++;
    }
}