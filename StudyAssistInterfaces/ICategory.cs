using System;
using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    public interface ICategory
    {
        String Name { get; set;}

        ObservableCollection<ITheme> Themes { get; }

        void RemoveFromStudy();
    }
}
