using StudyAssistInterfaces;
using System;

namespace StudyAssistModel
{

/*
*   При создании вопроса записывается дата его создания
*   Назначается уровень 1
*   При сохранении устанавливается дата повторения вопроса
*   
*   В дальнейшем при загрузке:
*   Проверяется дата повторения и по дате формируется список вопросов на повторение
*   Если пользователь повторил вопрос - он должен повысить его уровень на 1 поз вверх
*   При этом устанавливается новая дата повторения
*
*   
*   
*/
    /// <summary>
    /// Запись для сохранения информации по определенной проблеме, включающая
    /// вопрос и ответ.
    /// </summary>
    [Serializable]
    public class XProblem : IProblem
    {
        #region Fields

        private String _answer;

        private String _question;

        private Boolean _isStudy;

        private readonly DateTime _creationDate;

        private DateTime _addedToStudyDate;

        private DateTime _repeateDate;

        private Byte _studyLevel;

        #endregion Fields

        #region Constructors

        public XProblem()
        {
            this._isStudy = false;
            this._addedToStudyDate = DateTime.MinValue;
            this._creationDate = DateTime.Today;
            this._repeateDate = DateTime.MaxValue;
            this.StudyLevelUp();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Объяснение проблемы (вопроса).
        /// </summary>
        public String Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        /// <summary>
        /// Признак того, окончательно ли изучена проблема.
        /// </summary>
        public Boolean IsStudy
        {
            get { return _isStudy; }
            set { _isStudy = value; }
        }

        /// <summary>
        /// Формулировка проблемы (вопроса).
        /// </summary>
        public String Question
        {
            get { return _question; }
            set { _question = value; }
        }

        /// <summary>
        /// Уровень изученности.
        /// </summary>
        public byte StudyLevel
        {
            get { return _studyLevel; }
        }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreationDate
        {
            get { return _creationDate; }
        }

        /// <summary>
        /// Дата добавления проблемы на изучение.
        /// </summary>
        public DateTime AddToStudyDate
        {
            get { return _addedToStudyDate; }
        }

        /// <summary>
        /// Дата следующего повторения вопроса.
        /// </summary>
        public DateTime RepeatDate
        {
            get { return _repeateDate; }
            set { _repeateDate = value; }
        }

        #endregion Properties
        
        #region Methods

        /// <summary>
        /// Увеличивает уровень изученности.
        /// </summary>
        public void StudyLevelUp()
        {
            if (IsStudy) 
                return;

            _studyLevel += 1;
            _SpecifyRepeatDate();
        }

        /// <summary>
        /// Уменьшает уровень изученности.
        /// </summary>
        public void StudyLevelDown()
        {
            if (_studyLevel <= 1)
                return;

            _studyLevel -= 1;

            _SpecifyRepeatDate();
        }

        /// <summary>
        /// Переносит дату повторения на завтра.
        /// </summary>
        public void MoveToTomorrow()
        {
            _repeateDate = DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// Помечает проблему как изученную.
        /// </summary>
        public void RemoveFromStudy()
        {
            _isStudy = true;
        }

        /// <summary>
        /// Добавляет проблему к изучению.
        /// </summary>
        /// <param name="level">Уровень изученности.</param>
        public void AddToStudy(byte level)
        {
            _addedToStudyDate = DateTime.Today;
            IsStudy = false;
            _studyLevel = level;
            _SpecifyRepeatDate();
        }

        /// <summary>
        /// Установить дату повторения.
        /// </summary>
        /// <param name="repeatDate">Дата повторенияю.</param>
        public void SetRepeatDate(DateTime repeatDate)
        {
            _repeateDate = repeatDate;
            _studyLevel = 1;
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// Устанавливает дату повторения.
        /// </summary>
        private void _SpecifyRepeatDate()
        {
            switch (_studyLevel)
            {
                //case 1:
                //    _repeateDate = DateTime.Today.AddDays(1);
                //    break;
                //case 2:
                //    _repeateDate = DateTime.Today.AddDays(3);
                //    break;
                //case 3:
                //    _repeateDate = DateTime.Today.AddDays(7);
                //    break;
                //case 4:
                //    _repeateDate = DateTime.Today.AddDays(14);
                //    break;
                //case 5:
                //    _repeateDate = DateTime.Today.AddDays(30);
                //    break;
                //case 6:
                //    _repeateDate = DateTime.Today.AddDays(60);
                //    break;
                //default:
                //    _repeateDate = DateTime.Today.AddDays(120);
                //    break;

                case 1:
                    _repeateDate = _repeateDate.AddDays(1);
                    break;
                case 2:
                    _repeateDate = _repeateDate.AddDays(3);
                    break;
                case 3:
                    _repeateDate = _repeateDate.AddDays(7);
                    break;
                case 4:
                    _repeateDate = _repeateDate.AddDays(14);
                    break;
                case 5:
                    _repeateDate = _repeateDate.AddDays(30);
                    break;
                //case 6:
                //    _repeateDate = _repeateDate.AddDays(45);
                //break;
                default:
                    _repeateDate = _repeateDate.AddDays(45);
                    break;
            }
        }

        #endregion Utilities
    }
}
