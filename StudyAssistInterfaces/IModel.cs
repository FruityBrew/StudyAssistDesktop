using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    public interface IModel
    {
        void Init();
        void SaveChange();

        ObservableCollection<ICategory> Categories
        { get; }
    }
}
