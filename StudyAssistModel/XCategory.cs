using FileStorage;
using StudyAssistInterfaces;
using System;
using System.Collections.ObjectModel;

namespace StudyAssistModel
{
    [Serializable]
    public class XCategory : ICategory, IStorageItem
    {
        #region fields

        String _name;
        ObservableCollection<ITheme> _themes;

        [NonSerialized]
        Action<IStorageItem> _saveItem;
        [NonSerialized]
        Action<IStorageItem> _deleteItem;



        #endregion

        #region ctors
            
        public XCategory()
        {
            _themes = new ObservableCollection<ITheme>();
            _themes.CollectionChanged += Themes_CollectionChanged;
        }



        #endregion

        #region properties
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Название категории не может быть пустым");
                else
                    _name = value;
            }
        }


        public Action<IStorageItem> SaveItem
        {
            get
            {
                return _saveItem;
            }

            set
            {
                _saveItem = value;
            }
        }

        public Action<IStorageItem> DeleteItem
        {
            get
            {
                return _deleteItem;
            }

            set
            {
                _deleteItem = value;
            }
        }

        public ObservableCollection<ITheme> Themes
        {
            get
            {
                return _themes;
            }
        }



        #endregion

        #region methods

        internal void Init()
        {
           // _themes = new ObservableCollection<ITheme>();
            _themes.CollectionChanged += Themes_CollectionChanged;
        }

        public void Save()
        {
            if (SaveItem != null)
                SaveItem(this);
        }

        public void Delete()
        {
            if (DeleteItem != null)
                DeleteItem(this);
        }

        #endregion

        #region eventHandlers

        private void Themes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Save();
        }

        #endregion
    }
}
