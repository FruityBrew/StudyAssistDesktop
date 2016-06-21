using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Ninject;
using System.ComponentModel;
using FileStorage;

namespace StudyAssist.ViewModel
{
    public class XCategoryVM : XBaseViewModel
    {
        #region fields

        ICategory _category;
        ObservableCollection<XThemeVM> _themesObsColl;
        CollectionViewSource _themesCVS;
        ObservableCollection<XThemeVM> _themesToRepeatObsColl;
        CollectionViewSource _themesToRepeatCVS;
        

        #endregion 

        #region ctors

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
        #endregion

        #region properties

        public String Name
        {
            get
            {
                return Category.Name;
            }

            set
            {
                Category.Name = value;
           //     RaisePropertyChanged(this, "Name");
                Save();
            }
        } 

        public ICollectionView ThemesCollView
        {
            get
            {
                return _themesCVS.View;
            }
        }

        public ICollectionView ThemesToRepeatCollView
        {
            get
            {
                return _themesToRepeatCVS.View;
            }
        }

        public XThemeVM SelectedTheme
        {
            get
            {
                return ThemesCollView.CurrentItem as XThemeVM;
            }
        }

        public XThemeVM SelectedToRepeatTheme
        {
            get 
            {
                return ThemesToRepeatCollView.CurrentItem as XThemeVM;
            }
        }

        public ICategory Category
        {
            get
            {
                return _category;
            }

        }

        #endregion

        #region methods
        private void Init()
        {
            _themesObsColl = new ObservableCollection<XThemeVM>();
            _themesToRepeatObsColl = new ObservableCollection<XThemeVM>();
            foreach(var theme in Category.Themes)
            {
                XThemeVM th = new XThemeVM(theme, Save);
                th.Save = this.Save;
                _themesObsColl.Add(th);
                if (!th.IsProblemRepeatEmpty)
                    _themesToRepeatObsColl.Add(th);
            }
            _themesObsColl.CollectionChanged += ThemesObsColl_CollectionChanged;
            _themesCVS = new CollectionViewSource();
            _themesCVS.Source = _themesObsColl;
            _themesCVS.View.CurrentChanged += View_CurrentChanged;
            _themesToRepeatCVS = new CollectionViewSource();
            _themesToRepeatCVS.Source = _themesToRepeatObsColl;
            _themesToRepeatCVS.View.CurrentChanged += ThemesRepeatView_CurrentChanged;

         }



        private void Save()
         {
            IStorageItem storItem = ((IStorageItem)this.Category);
            storItem.SaveItem(storItem);
        }

        #endregion

        #region eventHandlers

        private void ThemesObsColl_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                 XThemeVM theme = e.NewItems[0] as XThemeVM;
                 theme.Save = this.Save;
                 Category.Themes.Add(theme.Theme);
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
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

        #endregion


    }
}
