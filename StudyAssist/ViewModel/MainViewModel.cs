using Ninject;
using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace StudyAssist.ViewModel
{
    /// <summary>
    /// Вьюмодель.
    /// </summary>
    public class MainViewModel : XBaseViewModel
    {
        #region Fields

        IModel _model;
        ObservableCollection<XCategoryVM> _categoriesObsColl;
        CollectionViewSource _categoriesCVS;
        private ObservableCollection<XCategoryVM> _categoriesToRepeat;
        private CollectionViewSource _categoriesToRepeatCVS;

        #endregion Fields

        #region Properties

        public XCommand RemoveAllFromStudy { get; set; }

        /// <summary>
        /// Коллекция категорий.
        /// </summary>
        public ICollectionView CategoriesCollView
        {
            get { return _categoriesCVS.View; }
        }

        /// <summary>
        /// Выбранная категория.
        /// </summary>
        public XCategoryVM SelectedCategory
        {
            get { return CategoriesCollView.CurrentItem as XCategoryVM; }
        }

        public ICollectionView CategoriesToRepeatCollView
        {
            get { return _categoriesToRepeatCVS.View; }
        }

        public XCategoryVM SelectedToRepeatCategory
        {
            get { return CategoriesToRepeatCollView.CurrentItem as XCategoryVM; }
        }

        #endregion Properties

        #region Сtors

        public MainViewModel()
        {
            _model = XKernel.Instance.Get<IModel>();

            _categoriesObsColl = new ObservableCollection<XCategoryVM>();
            _categoriesToRepeat = new ObservableCollection<XCategoryVM>();

            foreach(var category in _model.Categories)
            {
                XCategoryVM categoryVM = new XCategoryVM(category);
                _categoriesObsColl.Add(categoryVM);

                if(!categoryVM.IsProblemRepeatEmpty)
                    _categoriesToRepeat.Add(categoryVM);
            }

            _categoriesObsColl.CollectionChanged += 
                CategoriesObsColl_CollectionChanged;

            _categoriesCVS = new CollectionViewSource();
            _categoriesCVS.Source = _categoriesObsColl;
            _categoriesCVS.View.CurrentChanged += View_CurrentChanged;

            _categoriesToRepeatCVS = new CollectionViewSource();
            _categoriesToRepeatCVS.Source = _categoriesToRepeat; ;
            _categoriesToRepeatCVS.View.CurrentChanged += 
                CategoriesRepeatView_CurrentChanged;

            RemoveAllFromStudy = new XCommand(_RemoveAllFromStudy);
        }

        #endregion Ctors

        #region Utilities

        /// <summary>
        /// Снимает все проблемы с обучения.
        /// </summary>
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

        #region Methods
        public void RemoveRepeat()
        {
            SelectedToRepeatCategory.SelectedToRepeatTheme.RemoveFromRepeat();
            if (SelectedToRepeatCategory.SelectedToRepeatTheme.IsProblemRepeatEmpty)
            {
                SelectedToRepeatCategory.RemoveRepeat();

                if (SelectedToRepeatCategory.IsProblemRepeatEmpty)
                    _categoriesToRepeat.Remove(SelectedToRepeatCategory);
            }
        }

        #endregion Methods

        #region EentHandlers

        private void CategoriesRepeatView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, nameof(SelectedToRepeatCategory));
        }

        private void CategoriesObsColl_CollectionChanged(
            object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {                
                _model.Categories.Add(((XCategoryVM)e.NewItems[0]).Category);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
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
