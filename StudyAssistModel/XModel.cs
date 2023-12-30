using FileStorage;
using StudyAssistInterfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace StudyAssistModel
{
    /// <summary>
    /// Модель.
    /// </summary>
    public class XModel : IModel
    {
        #region fields

        ObservableCollection<ICategory> _categories;

        #endregion


        #region ctors

        /// <summary>
        /// Конструктор.
        /// </summary>
        public XModel()
        {
            _Init();
        }

        #endregion  


        #region properties

        /// <summary>
        /// Коллекция категорий.
        /// </summary>
        public ObservableCollection<ICategory> Categories
        {
            get { return _categories; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Инициализирует модель.
        /// </summary>
        private void _Init()
        {
            var categories = XStorage.Instance.LoadItems();
            _categories = new ObservableCollection<ICategory>();
            foreach (var item in categories)
            {
                _categories.Add((ICategory)item);
            }

            _categories.CollectionChanged += _Categories_CollectionChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Сохраняет данные модели.
        /// </summary>
        public void SaveChange()
        {
            XStorage.Instance.SaveChange();
        }

        #endregion

        #region eventHandlers

        /// <summary>
        /// Обработчик события изменения в коллеции категорий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Categories_CollectionChanged(
            object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                IStorageItem c = (IStorageItem)e.NewItems[0];

                //c.SaveItem?.Invoke(c); //todo ?? 

                c.SaveItem = XStorage.Instance.SaveItem;
                ((XCategory)c).Save();
            }
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
               // ((IStorageItem)e.OldItems[0]).Delete();
            }
        }

        #endregion
    }
}
