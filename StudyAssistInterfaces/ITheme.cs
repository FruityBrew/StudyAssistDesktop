using System;
using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    public interface ITheme
    {
        String Name { get; set; }
        DateTime CreateDate { get; set; }
        DateTime RepeatDate { get; set; }

        Byte Study { get; set; }

        Boolean IsStudy { get; set; }

        ObservableCollection<IProblem> Problems { get; }

    }
}
