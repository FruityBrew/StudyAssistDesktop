using StudyAssistInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StudyAssistModel
{
    /// <summary>
    /// Тема раздела.
    /// </summary>
    [Serializable]
    public class XTheme : ITheme 
    {
        #region fields   

        private String _name;

        private Boolean _isStudy;

        private ObservableCollection<IProblem> _problems;

        #endregion

        #region properties

        /// <summary>
        /// Показывает находится ли тема на изучении.
        /// </summary>
        public bool IsStudy
        {
            get { return _isStudy; }
            set { _isStudy = value; }
        }

        /// <summary>
        /// Название темы.
        /// </summary>
        public string Name
        {
            get { return _name; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException(
                        "Название темы на может быть пустым");
                else
                    _name = value;
            }
        }

        /// <summary>
        /// Коллекция проблем.
        /// </summary>
        public ObservableCollection<IProblem> Problems
        {
            get { return _problems; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        public XTheme()
        {
            _problems = new ObservableCollection<IProblem>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Удаляет тему и все проблемы темы. 
        /// </summary>
        public void RemoveFromStudy()
        {
            IsStudy = false;

            if(_problems == null)
                return;

            Parallel.ForEach(_problems, (a) => a.IsStudy = false);
        }

        #endregion Methods
    }
}
