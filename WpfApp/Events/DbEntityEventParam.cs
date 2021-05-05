namespace WpfApp.Events
{
    /// <summary>
    /// Событие в базе данных
    /// </summary>
    public class DbEntityEventParam
    {
        public DbEntityEventParam()
        {

        }

        public DbEntityEventParam(EventType crud, Dict item, int entityId)
        {
            Crud = crud;
            Item = item;
            EntityId = entityId;
        }

        /// <summary>
        /// Действие
        /// </summary>
        public EventType Crud { get; set; }
        /// <summary>
        /// Справочник, в котором произошло обновление
        /// </summary>
        public Dict Item { get; set; }
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int EntityId { get; set; }
    }
}
