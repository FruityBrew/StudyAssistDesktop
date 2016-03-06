using System.Collections.ObjectModel;

namespace StudyAssistInterfaces
{
    public interface IModel
    {
        void Init();

        ObservableCollection<ICategory> Categories
        { get; }
    }
}
