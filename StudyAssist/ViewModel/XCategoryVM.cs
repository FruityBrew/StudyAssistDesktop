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
        #region Fields

        ICategory _category;
        ObservableCollection<XThemeVM> _themesObsColl;
        CollectionViewSource _themesCVS;
        ObservableCollection<XThemeVM> _themesToRepeatObsColl;
        CollectionViewSource _themesToRepeatCVS;
        private bool _isSearch;
        private string _searchText;

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

        public string SearchText 
        {
            get => _searchText;
            set
            {
               _searchText = value;
                RaisePropertyChanged(this, nameof(SearchText));
            }
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

        public XCommand SearchCommand { get; set; }

        public XCommand ResetCommand { get; set; }

        #endregion Properties

        #region Ctors

        public XCategoryVM(ICategory category)
        {
            _category = category;
            Init();
            SearchCommand = new XCommand(SearchProblems);
            ResetCommand = new XCommand(ResetProblems);
        }

        public XCategoryVM()
        {
            _category = XKernel.Instance.Get<ICategory>();
            Init();
            SearchCommand = new XCommand(SearchProblems);
            ResetCommand = new XCommand(ResetProblems);
        }

        #endregion Ctors

        #region Methods

        public void SearchProblems()
        {
            UpdateProblems(SearchText);
        }

        public void ResetProblems()
        {
            SearchText = string.Empty;
            UpdateProblems(SearchText);
        }

        public void RemoveRepeat()
        {
            _themesToRepeatObsColl.Remove(SelectedToRepeatTheme);
        }

        public void UpdateRepeats(DateTime repeateDate)
        {
            _themesToRepeatObsColl.Clear();

            foreach (var theme in _themesObsColl)
            {
                theme.UpdateRepeats(repeateDate);
                if (theme.IsProblemRepeatEmpty == false)
                    _themesToRepeatObsColl.Add(theme);
            }
        }

        public void UpdateProblems(string searchText)
        {
            _isSearch = true;
            _themesObsColl.Clear();
            //List<XThemeVM> aux = new List<XThemeVM>();

            foreach (var theme in _category.Themes)
            {
                var themeVm = new XThemeVM(theme, Save);
                themeVm.UpdateProblems(searchText);
                if (themeVm.ProblemsObsColl.Count > 0)
                    _themesObsColl.Add(themeVm); 
            }

            //foreach (var themeVm in aux)
            //    _themesObsColl.Add(themeVm);

            _isSearch = false;
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
            if (_isSearch)
                return;
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
