﻿using System;

namespace StudyAssistInterfaces
{
    /// <summary>
    /// Интерфейс изучаемого "вопроса".
    /// </summary>
    public interface IProblem
    {
        #region Properties

        /// <summary>
        /// Текст вопроса.
        /// </summary>
        String Question { get; set; }

        /// <summary>
        /// Текст ответа.
        /// </summary>
        String Answer { get; set; }

        /// <summary>
        /// Дата создания вопроса.
        /// </summary>
        DateTime? CreationDate { get; }

        /// <summary>
        /// Дата добавления на изучение.
        /// </summary>
        DateTime? AddedToStudyDate { get; }

        /// <summary>
        /// Расчетная дата повторения.
        /// </summary>
        DateTime? RepeatDate { get; set; }

        /// <summary>
        /// Уровень изученности вопроса (сколько раз был повторен вопрос).
        /// </summary>
        Byte StudyLevel { get; }

        /// <summary>
        /// Включен ли автоматический расчет даты повторения (автоповтор).
        /// </summary>
        Boolean IsAutoRepeate { get; set; }

        /// <summary>
        /// Находится ли вопрос на изучении.
        /// </summary>
        Boolean IsStudy { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Увеличивает уровень изученности вопроса.
        /// </summary>
        void StudyLevelUp();

        /// <summary>
        /// Уменьшает уровень изученности вопроса.
        /// </summary>
        void StudyLevelDown();

        /// <summary>
        /// Переносит повторение вопроса на завтра.
        /// </summary>
        void MoveToTomorrow();

        /// <summary>
        /// Переносит дату повтора на несколько дней
        /// </summary>
        /// <param name="days">Количество дней</param>
        void RescheduleFor(int days);

        /// <summary>
        /// Удаляет вопрос с обучения.
        /// </summary>
        void RemoveFromStudy();

        /// <summary>
        /// Добавляет вопрос к изучению с указанным уровнем.
        /// </summary>
        /// <param name="level">Уровень, с которого начнется изучение.</param>
        void AddToStudy(byte level);

        /// <summary>
        /// Добавляет вопрос к изучению с указанием даты, с которой начать.
        /// </summary>
        /// <param name="repeateDate">Дата, с которой нужно 
        /// начать обучение.</param>
        void AddToStudy(DateTime? repeateDate);

        /// <summary>
        /// Сбрасывает уровень изученности в 0.
        /// </summary>
        void ResetLevel();

        #endregion Methods
    }
}
