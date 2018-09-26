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
        #region Constructors

        public XProblem()
        {
            this.IsStudy = true;
            IsAutoRepeate = true;
            this.AddedToStudyDate = DateTime.Today;
            this.CreationDate = DateTime.Today;
            this.RepeatDate = null;
            this.StudyLevelUp();
        }

        #endregion Constructors

        #region Properties

        public Boolean IsAutoRepeate { get; set; }

        /// <summary>
        /// Объяснение проблемы (вопроса).
        /// </summary>
        public String Answer { get; set; }

        /// <summary>
        /// Находится ли проблема на изучении
        /// </summary>
        public Boolean IsStudy { get; set; }

        /// <summary>
        /// Формулировка проблемы (вопроса).
        /// </summary>
        public String Question { get; set; }

        /// <summary>
        /// Уровень изученности.
        /// </summary>
        public byte StudyLevel { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата добавления проблемы на изучение.
        /// </summary>
        public DateTime? AddedToStudyDate { get; set; }

        /// <summary>
        /// Дата следующего повторения вопроса.
        /// </summary>
        public DateTime? RepeatDate { get; set; }

        #endregion Properties
        
        #region Methods

        /// <summary>
        /// Увеличивает уровень изученности.
        /// </summary>
        public void StudyLevelUp()
        {
            if (IsStudy == false) 
                return;

            StudyLevel += 1;
            _SpecifyRepeatDate();
        }

        /// <summary>
        /// Уменьшает уровень изученности.
        /// </summary>
        public void StudyLevelDown()
        {
            if (IsStudy == false || StudyLevel <= 1)
                return;

            StudyLevel -= 1;

            _SpecifyRepeatDate();
        }

        /// <summary>
        /// Переносит дату повторения на завтра.
        /// </summary>
        public void MoveToTomorrow()
        {
            RepeatDate = DateTime.Today.AddDays(1);
        }

        public void RemoveFromStudy()
        {
            IsStudy = false;
            RepeatDate = null;
        }

        public void ResetLevel()
        {
            StudyLevel = 0;
        }

        public void AddToStudy(DateTime? repeateDate)
        {
            if(repeateDate.HasValue == false)
                throw new ArgumentNullException(nameof(repeateDate));

            IsAutoRepeate = false;
            IsStudy = true;
            RepeatDate = repeateDate;
        }

        public void SetRepeateDate(DateTime? repeateDate)
        {
            if (repeateDate.HasValue == false)
                throw new ArgumentNullException(nameof(repeateDate));

            RepeatDate = repeateDate;
        }

        /// <summary>
        /// Добавляет проблему к изучению.
        /// </summary>
        /// <param name="level">Уровень изученности.</param>
        public void AddToStudy(byte level)
        {
            AddedToStudyDate = DateTime.Today;
            IsStudy = true;
            StudyLevel = level;
            _SpecifyRepeatDate();
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// Устанавливает дату повторения.
        /// </summary>
        private void _SpecifyRepeatDate()
        {
            if(IsAutoRepeate == false)
                return;

            if(IsStudy == false)
                return;

            if(StudyLevel == 0)
                return;

            if (RepeatDate == null)
                RepeatDate = DateTime.Today;

            switch (StudyLevel)
            {
                case 1:
                    if(RepeatDate > DateTime.Today)
                        break;
                    RepeatDate = RepeatDate?.AddDays(1);
                    break;
                case 2:
                    if (RepeatDate > DateTime.Today)
                        break;
                    RepeatDate = RepeatDate?.AddDays(3);
                    break;
                case 3:
                    if (RepeatDate > DateTime.Today)
                        break;
                    RepeatDate = RepeatDate?.AddDays(7);
                    break;
                case 4:
                    if (RepeatDate > DateTime.Today)
                        break;
                    RepeatDate = RepeatDate?.AddDays(14);
                    break;
                case 5:
                    if (RepeatDate > DateTime.Today)
                        break;
                    RepeatDate = RepeatDate?.AddDays(30);
                    break;
                //case 6:
                //    _repeateDate = _repeateDate.AddDays(45);
                //break;
                default:
                    RepeatDate = RepeatDate?.AddDays(45);
                    break;
            }
        }

        #endregion Utilities
    }
}
