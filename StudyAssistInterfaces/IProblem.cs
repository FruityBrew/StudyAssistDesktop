using System;

namespace StudyAssistInterfaces
{
    public interface IProblem
    {
        String Question { get; set; }
        String Answer { get; set; }

        DateTime CreationDate
        { get; }

        DateTime AddingToStudyDate { get; }

        DateTime RepeatDate { get; }

        Byte StudyLevel { get; }

        Boolean IsStudy { get; set; }

        void StudyLevelUp();
        void StudyLevelDown();
        void MoveToTomorrow();
        void RemoveFromStudy();
        void AddToStudy();
    }
}
