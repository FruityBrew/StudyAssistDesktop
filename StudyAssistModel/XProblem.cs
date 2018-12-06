﻿using StudyAssistInterfaces;
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

        private string _answer;

        private bool _isStudy;

        private string _question;

        private byte _studyLevel;

        private DateTime? _creationDate;

        private DateTime? _addedToStudyDate;

        private DateTime? _repeatDate;

        private bool _isAutoRepeate;



        #endregion Fields

        #region Properties

        public Boolean IsAutoRepeate
        {
            get { return _isAutoRepeate; }
            set { _isAutoRepeate = value; }
        }

        /// <summary>
        /// Объяснение проблемы (вопроса).
        /// </summary>
        public String Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        /// <summary>
        /// Находится ли проблема на изучении
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
            private set { _studyLevel = value; }
        }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTime? CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        /// <summary>
        /// Дата добавления проблемы на изучение.
        /// </summary>
        public DateTime? AddedToStudyDate
        {
            get { return _addedToStudyDate; }
            set { _addedToStudyDate = value; }
        }

        /// <summary>
        /// Дата следующего повторения вопроса.
        /// </summary>
        public DateTime? RepeatDate
        {
            get { return _repeatDate; }
            set { _repeatDate = value; }
        }

        #endregion Properties

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

        /// <summary>
        /// Удаляет проблему с обучения.
        /// </summary>
        public void RemoveFromStudy()
        {
            IsStudy = false;
            RepeatDate = null;
        }

        /// <summary>
        /// Сбрасывает уровень изученности в 0.
        /// </summary>
        public void ResetLevel()
        {
            StudyLevel = 0;
        }
        
        /// <summary>
        /// Добавляет проблему к изучению с повтором в определенную дату.
        /// </summary>
        /// <param name="repeateDate">Дата повтора.</param>
        public void AddToStudy(DateTime? repeateDate)
        {
            if(IsStudy)
                throw new InvalidOperationException(
                    "Проблема уже на изучении");

            if(repeateDate.HasValue == false)
                throw new ArgumentNullException(nameof(repeateDate));

            AddedToStudyDate = DateTime.Today;

            // видимо, считаю, что если добавляется по дате, 
            // то автоматом неавтоповтор:
            IsAutoRepeate = false;

            IsStudy = true;
            RepeatDate = repeateDate;
        }

        /// <summary>
        /// Добавляет проблему к изучению с определенным уровнем.
        /// </summary>
        /// <param name="level">Уровень изученности.</param>
        public void AddToStudy(byte level)
        {
            AddedToStudyDate = DateTime.Today;
            IsAutoRepeate = true; // todo проверить
            IsStudy = true;
            StudyLevel = level;
            _SpecifyRepeatDate();
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// Определяет дату повторения.
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

            RepeatDateCalculator repa = new RepeatDateCalculator();

            RepeatDate = repa.GetRepeatDate(StudyLevel, RepeatDate);
        }

        #endregion Utilities
    }
}
