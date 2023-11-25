using FileStorage;
using Ninject;
using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace StudyAssist.ViewModel
{
    public class XCategoryVM : XBaseViewModel
    {
        public class T
        {
            public string Name { get; set; }
        }

        #region Fields

        ICategory _category;
        ObservableCollection<XThemeVM> _themesObsColl;
        CollectionViewSource _themesCVS;
        ObservableCollection<XThemeVM> _themesToRepeatObsColl;
        CollectionViewSource _themesToRepeatCVS;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Название категории.
        /// </summary>
        public String Name
        {
            get { return Category.Name; }

            set
            {
                Category.Name = value;

                //     RaisePropertyChanged(this, "Name");
                Save();
            }
        }

        /// <summary>
        /// Коллекция тем.
        /// </summary>
        public ICollectionView ThemesCollView
        {
            get { return _themesCVS.View; }
        }

        /// <summary>
        /// Коллекция тем для повтора.
        /// </summary>
        public ICollectionView ThemesToRepeatCollView
        {
            get { return _themesToRepeatCVS.View; }
        }

        /// <summary>
        /// Выбранная тема.
        /// </summary>
        public XThemeVM SelectedTheme
        {
            get { return ThemesCollView.CurrentItem as XThemeVM; }
        }

        /// <summary>
        /// Выбранная тема из тем для повтора.
        /// </summary>
        public XThemeVM SelectedToRepeatTheme
        {
            get { return ThemesToRepeatCollView.CurrentItem as XThemeVM; }
        }

        /// <summary>
        /// Интрефейс категории.
        /// </summary>
        public ICategory Category
        {
            get { return _category; }
        }

        /// <summary>
        /// Признак того, что список проблем на повторение пуст.
        /// </summary>
        public Boolean IsProblemRepeatEmpty
        {
            get
            {
                if (_themesToRepeatObsColl.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion Properties

        #region Ctors

        public XCategoryVM(ICategory category)
        {
            _category = category;
            Init();
        } 

        public XCategoryVM()
        {
            _category = XKernel.Instance.Get<ICategory>();
            Init();
        }

        #endregion Ctors

        #region Methods

        public void RemoveRepeat()
        {
            _themesToRepeatObsColl.Remove(SelectedToRepeatTheme);
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// Инициализирует внутренности.
        /// </summary>
        private void Init()
        {
            _themesObsColl = new ObservableCollection<XThemeVM>();
            _themesToRepeatObsColl = new ObservableCollection<XThemeVM>();

            foreach(var theme in Category.Themes.OrderBy(o => o.Name))
            {
                XThemeVM th = new XThemeVM(theme, Save);
                th.Save = this.Save;

                _themesObsColl.Add(th);

                if (!th.IsProblemRepeatEmpty)
                    _themesToRepeatObsColl.Add(th);
            }
            _themesObsColl.CollectionChanged += 
                ThemesObsColl_CollectionChanged;

            _themesCVS = new CollectionViewSource();
            _themesCVS.Source = _themesObsColl;
            _themesCVS.View.CurrentChanged += View_CurrentChanged;

            _themesToRepeatCVS = new CollectionViewSource();
            _themesToRepeatCVS.Source = _themesToRepeatObsColl;
            _themesToRepeatCVS.View.CurrentChanged += 
                ThemesRepeatView_CurrentChanged;
         }

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        private void Save()
         {
            IStorageItem storItem = ((IStorageItem)this.Category);
            storItem.SaveItem(storItem);
        }

        #endregion Utilities

        #region EventHandlers

        private void ThemesObsColl_CollectionChanged(
            object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                 XThemeVM theme = e.NewItems[0] as XThemeVM;
                 theme.Save = this.Save;
                 Category.Themes.Add(theme.Theme);
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
                Category.Themes.Remove((ITheme)e.OldItems[0]);

            Save();
        }

        private void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedTheme");
        }

        private void ThemesRepeatView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedToRepeatTheme");
        }

        #endregion EventHandlers
    }
}
