using System;
using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    /// <summary>
    /// Интерфейс темы (подкатегории).
    /// </summary>
    public interface ITheme
    {
        #region Properties

        /// <summary>
        /// Название темы.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Признак того, находится ли тема на изучении.
        /// </summary>
        Boolean IsStudy { get; set; }

        /// <summary>
        /// Коллекция вопросов темы.
        /// </summary>
        ObservableCollection<IProblem> Problems { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Удаляет тему с обучения.
        /// </summary>
        void RemoveFromStudy();

        #endregion Methods
    }
}
