using System;
using System.Collections.ObjectModel;
using StudyAssistInterfaces;
using FileStorage;

namespace StudyAssistModel
{
    public class XModel : IModel
    {
        #region fields

        ObservableCollection<ICategory> _categories;

        #endregion


        #region ctors

        public XModel()
        {
            Init();
        }

        #endregion  


        #region properties
        public ObservableCollection<ICategory> Categories
        {
            get
            {
                return _categories;
            }
        }

        #endregion 

        #region methods

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

        public void SaveChange()
        {
            XStorage.Instance.SaveChange();
        }


        #endregion

        #region eventHandlers

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

        #endregion
    }
}
