using System;
using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    public interface ITheme
    {
        String Name { get; set; }

        Boolean IsStudy { get; set; }

        ObservableCollection<IProblem> Problems { get; }

        void RemoveFromStudy();


    }
}
