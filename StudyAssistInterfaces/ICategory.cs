using System;
using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    /// <summary>
    /// Категория - контейнер для тем.
    /// </summary>
    public interface ICategory
    {
        #region Properties

        /// <summary>
        /// Имя категории.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Коллекция тем.
        /// </summary>
        ObservableCollection<ITheme> Themes { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Удаляет категорию с обучения.
        /// </summary>
        void RemoveFromStudy();

        #endregion Methods
    }
}
