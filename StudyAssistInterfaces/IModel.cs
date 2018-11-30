using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    /// <summary>
    /// Интерфейс модели.
    /// </summary>
    public interface IModel
    {
        #region Properties

        /// <summary>
        /// Коллекция категорий с темами.
        /// </summary>
        ObservableCollection<ICategory> Categories { get; }

        #endregion Properties

        #region Methods 

        /// <summary>
        /// Инициализирует модель.
        /// </summary>
        void Init();

        /// <summary>
        /// Сохраняет изменения в модели.
        /// </summary>
        void SaveChange();

        #endregion Methods 
    }
}
