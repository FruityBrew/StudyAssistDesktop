using System;
using StudyAssistInterfaces;
using StudyAssistIoC;
using Ninject;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;

namespace StudyAssist.ViewModel
{
    public class MainViewModel : XBaseViewModel
    {
        #region fields

        IModel _model;
        ObservableCollection<XCategoryVM> _categoriesObsColl;
        //ObservableCollection<XCategoryVM> _categoriesObsCollToRepeat;
       // CollectionViewSource _categoryToRepeatCVS;
        CollectionViewSource _categoriesCVS;

        #endregion


        #region ctors
        public MainViewModel()
        {
            _model = XKernel.Instance.Get<IModel>();
            _categoriesObsColl = new ObservableCollection<XCategoryVM>();
       //     _categoriesObsCollToRepeat = new ObservableCollection<XCategoryVM>();
            foreach(var category in _model.Categories)
            {
                _categoriesObsColl.Add(new XCategoryVM(category));
            }
            _categoriesObsColl.CollectionChanged += CategoriesObsColl_CollectionChanged;
            _categoriesCVS = new CollectionViewSource();
            _categoriesCVS.Source = _categoriesObsColl;
            _categoriesCVS.View.CurrentChanged += View_CurrentChanged;

            RemoveAllFromStudy = new XCommand(_RemoveAllFromStudy);
        }


        #endregion

        #region Utilities

        private void _RemoveAllFromStudy()
        {
            if(_categoriesObsColl?.Any() == false)
                return;

            foreach (XCategoryVM category in _categoriesObsColl)
            {
                foreach (XThemeVM theme in category.ThemesCollView.SourceCollection)
                {
                    foreach (XProblemVM problem in theme.ProblemsObsColl)
                    {
                        problem.RemoveFromStudy();
                    }
                }
            }
        }

        #endregion Utilities



        #region properties

        public XCommand RemoveAllFromStudy { get; set; }

        public ICollectionView CategoriesCollView
            {
                get 
                {
                    return _categoriesCVS.View;
                }
            }

            public XCategoryVM SelectedCategory
            {
                get
                {
                    return CategoriesCollView.CurrentItem as XCategoryVM;
                }
            }
        #endregion

        #region eventHandlers

        private void CategoriesObsColl_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {                
                _model.Categories.Add(((XCategoryVM)e.NewItems[0]).Category);
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                _model.Categories.Remove((ICategory)e.OldItems[0]);

            //_model.SaveChange();
        }

        private void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedCategory");
        }
        #endregion
    }
}
