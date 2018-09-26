using System;

namespace StudyAssistInterfaces
{
    public interface IProblem
    {
        String Question { get; set; }
        String Answer { get; set; }

        DateTime CreationDate
        { get; }

        DateTime? AddedToStudyDate { get; }

        DateTime? RepeatDate { get; set; }

        Byte StudyLevel { get; set; }

        Boolean IsAutoRepeate { get; set; }

        /// <summary>
        /// Находится ли проблема на изучении.
        /// </summary>
        Boolean IsStudy { get; set; }

        void StudyLevelUp();
        void StudyLevelDown();
        void MoveToTomorrow();
        void RemoveFromStudy();
        void AddToStudy(byte level);
        void AddToStudy(DateTime? repeateDate);

        void SetRepeateDate(DateTime? repeateDate);

        void ResetLevel();


    }
}
