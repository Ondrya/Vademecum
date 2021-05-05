using WpfApp.Events;

namespace WpfApp.Interfaces
{
    public interface IPublishEventDbEntityEvent
    {
        /// <summary>
        /// Опубликовать событие
        /// </summary>
        void PublishDbEntityEvent(DbEntityEventParam dbEntityEventParam);
    }
}
