using FileStorage;
using StudyAssistInterfaces;
using System;
using System.Collections.ObjectModel;

namespace StudyAssistModel
{
    /// <summary>
    /// Категория с темами.
    /// </summary>
    [Serializable]
    public class XCategory : ICategory, IStorageItem
    {
        #region Fields

        String _name;

        private ObservableCollection<ITheme> _themes;

        /// <summary>
        /// Сохраняет данные категории.
        /// </summary>
        [NonSerialized]
        Action<IStorageItem> _saveItem;

        /// <summary>
        /// Удаляет данные категории.
        /// </summary>
        [NonSerialized]
        Action<IStorageItem> _deleteItem;

        #endregion 

        #region ctors
            
        public XCategory()
        {
            _themes = new ObservableCollection<ITheme>();
        }

        #endregion

        #region properties

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name
        {
            get { return _name; }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Название категории не может быть пустым");

                _name = value;
            }
        }

        /// <summary>
        /// Сохраняет данные категории.
        /// </summary>
        public Action<IStorageItem> SaveItem
        {
            get { return _saveItem; }

            set { _saveItem = value; }
        }

        /// <summary>
        /// Удаляет данные категории.
        /// </summary>
        public Action<IStorageItem> DeleteItem
        {
            get { return _deleteItem; }

            set { _deleteItem = value; }
        }

        /// <summary>
        /// Список тем. //TODO переделать на список?
        /// </summary>
        public ObservableCollection<ITheme> Themes
        {
            get { return _themes; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Сохраняет данные категории из хранилища.
        /// </summary>
        public void Save()
        {
            SaveItem?.Invoke(this);
        }

        /// <summary>
        /// Удаляет данные категории из хранилища.
        /// </summary>
        public void Delete()
        {
            DeleteItem?.Invoke(this);
        }

        #endregion


        /// <summary>
        /// TODO
        /// </summary>
        public void RemoveFromStudy()
        {
            throw new NotImplementedException();
        }
    }
}
