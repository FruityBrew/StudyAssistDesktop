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
        private DateTime _repeatDate;

        //private int _rescheduledDays;

        #endregion Fields

        #region Properties

        public string RescheduledDays { get; set; }  

        public XCommand RescheduleForFewDays { get; set; }

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

        public DateTime RepeatDate
        {
            get => _repeatDate;
            set 
            { 
                _repeatDate = value;
                RaisePropertyChanged(this, nameof(RepeatDate));
                UpdateCategoriesToRepeatWhithRepeatDate();
            }
        }

        #endregion Properties

        #region Сtors

        public MainViewModel()
        {
            _model = XKernel.Instance.Get<IModel>();
            RepeatDate = DateTime.Today;

            _categoriesObsColl = new ObservableCollection<XCategoryVM>();
            _categoriesToRepeat = new ObservableCollection<XCategoryVM>();

            List<string> defaultCats = Properties.Settings.Default.DefaultCategories
                .Split(' ')
                .ToList();

            foreach(var category in _model.Categories)
            {
                XCategoryVM categoryVM = new XCategoryVM(category);

                foreach(var defaultName in defaultCats)
                {
                    if (categoryVM.Name.Contains(defaultName))
                    {
                        _categoriesObsColl.Add(categoryVM);

                        if (!categoryVM.IsProblemRepeatEmpty)
                            _categoriesToRepeat.Add(categoryVM);
                    }
                }
            }

            _categoriesObsColl.CollectionChanged += 
                CategoriesObsColl_CollectionChanged;

            _categoriesCVS = new CollectionViewSource();
            _categoriesCVS.Source = _categoriesObsColl;
            _categoriesCVS.View.CurrentChanged += View_CurrentChanged;

            _categoriesToRepeatCVS = new CollectionViewSource();
            _categoriesToRepeatCVS.Source = _categoriesToRepeat;
            _categoriesToRepeatCVS.View.CurrentChanged += 
                CategoriesRepeatView_CurrentChanged;

            RemoveAllFromStudy = new XCommand(_RemoveAllFromStudy);
            RescheduleForFewDays = new XCommand(_RescheduleAllProblemForFewDays);
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

        /// <summary>
        /// Переносит все повторы на несколько дней
        /// </summary>
        private void _RescheduleAllProblemForFewDays()
        {
            if (_categoriesObsColl is null)
                return;

            if (!int.TryParse(RescheduledDays, out int reshDays) || reshDays <= 0)
                return;

            foreach (XCategoryVM category in _categoriesObsColl)
            {
                foreach (XThemeVM theme in category.ThemesCollView?.SourceCollection)
                {
                    foreach (XProblemVM problem in theme.ProblemsObsColl)
                    {
                        problem?.RescheduleForFewDays(reshDays);
                    }
                }
            }

            UpdateCategoriesToRepeatWhithRepeatDate();
        }

        #endregion Utilities

        #region Methods

        public void UpdateCategoriesToRepeatWhithRepeatDate()
        {
            if(_categoriesToRepeat == null) 
                return;

            _categoriesToRepeat.Clear();

            foreach(var category in _categoriesObsColl)
            {
                category.UpdateRepeats(RepeatDate);
                if(category.IsProblemRepeatEmpty == false)
                    _categoriesToRepeat.Add(category);
            }
        }

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
