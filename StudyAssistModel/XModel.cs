using System;
using System.Collections.ObjectModel;
using StudyAssistInterfaces;
using FileStorage;

namespace StudyAssistModel
{
    public class XModel : IModel
    {

        ObservableCollection<ICategory> _categories;    

        public ObservableCollection<ICategory> Categories
        {
            get
            {
                return _categories;
            }
        }

        public XModel()
        {
            Init();
        }


        public void Init()
        {
            var categories = XStorage.Instance.LoadItems();
            _categories = new ObservableCollection<ICategory>();
            foreach(var item in categories)
                {
                    ((XCategory)item).Init();
                    _categories.Add((ICategory)item);
                }
            _categories.CollectionChanged += Categories_CollectionChanged;
        }

        private void Categories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                IStorageItem c = (IStorageItem)e.NewItems[0];
                c.SaveItem = XStorage.Instance.SaveItem;
                c.DeleteItem = XStorage.Instance.DeleteItem;
                ((XCategory)c).Save();
            }
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
               // ((IStorageItem)e.OldItems[0]).Delete();
            }
        }
    }
}
