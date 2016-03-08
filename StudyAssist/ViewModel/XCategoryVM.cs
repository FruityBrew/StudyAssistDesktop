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
                RaisePropertyChanged(this, "Name");
            }
        } 

        public ICollectionView ThemesCollView
        {
            get
            {
                return _themesCVS.View;
            }
        }

        private XThemeVM SelectedTheme
        {
            get
            {
                return ThemesCollView.CurrentItem as XThemeVM;
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
            foreach(var theme in Category.Themes)
            {
                _themesObsColl.Add(new XThemeVM(theme));
            }
            _themesObsColl.CollectionChanged += ThemesObsColl_CollectionChanged;
            _themesCVS = new CollectionViewSource();
            _themesCVS.Source = _themesObsColl;
            _themesCVS.View.CurrentChanged += View_CurrentChanged;
            PropertyChanged += XCategoryVM_PropertyChanged;
         }


        #endregion

        #region eventHandlers

        private void ThemesObsColl_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                  Category.Themes.Add(((XThemeVM)e.NewItems[0]).Theme);            
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                Category.Themes.Remove((ITheme)e.OldItems[0]);  
        }


        private void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedTheme");
        }

        private void XCategoryVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IStorageItem storItem = ((IStorageItem)this.Category);
            storItem.SaveItem(storItem);
        }
        #endregion



    }
}
