using System;
using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    public interface ITheme
    {
        String Name { get; set; }
        DateTime CreationDate { get; set; }
        DateTime RepeatDate { get; set; }

        Byte StudyLevel { get; set; }

        Boolean IsStudy { get; set; }

        ObservableCollection<IProblem> Problems { get; }

    }
}
