using Ninject;
using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;


namespace StudyAssist.ViewModel
{
    public class XThemeVM : XBaseViewModel
    {
        #region Fields

        ITheme _theme;
        ObservableCollection<XProblemVM> _problemsObsColl;
        CollectionViewSource _problemsCVS;
        ObservableCollection<XProblemVM> _problemsToRepeatObsColl;
        CollectionViewSource _problemsToRepeatCVS;
        Action _save;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Коллекция проблем.
        /// </summary>
        public ObservableCollection<XProblemVM> ProblemsObsColl
        {
            get { return _problemsObsColl; }
        }

        /// <summary>
        /// Признак того, что тема на изучении.
        /// </summary>
        public Boolean IsStudy
        {
            get { return _theme.IsStudy; }
            set
            {
                _theme.IsStudy = value;
                Save();
            }
        }

        /// <summary>
        /// Название темы.
        /// </summary>
        public String Name
        {
            get { return _theme.Name; }

            set
            {
                _theme.Name = value;

                //            RaisePropertyChanged(this, "Name");
                Save();
            }
        }

        /// <summary>
        /// КоллекшнВью всех проблем.
        /// </summary>
        public ICollectionView ProblemsCollView
        {
            get { return _problemsCVS.View; }
        }

        /// <summary>
        /// Выбранная проблема.
        /// </summary>
        public XProblemVM SelectedProblem
        {
            get { return ProblemsCollView.CurrentItem as XProblemVM; }
        }

        /// <summary>
        /// Коллекция проблем для повторения.
        /// </summary>
        public ICollectionView ProblemsToRepeatView
        {
            get { return _problemsToRepeatCVS.View; }
        }

        /// <summary>
        /// Выбранная проблема для повторения.
        /// </summary>
        public XProblemVM SelectedProblemToRepeat
        {
            get { return ProblemsToRepeatView.CurrentItem as XProblemVM; }
        }

        /// <summary>
        /// Интерфейс темы.
        /// </summary>
        public ITheme Theme
        {
            get { return _theme; }
        }

        /// <summary>
        /// Метод сохранения изменений.
        /// </summary>
        public Action Save
        {
            get { return _save; }

            set { _save = value; }
        }

        /// <summary>
        /// Признак того, что список проблем на повторение пуст.
        /// </summary>
        public Boolean IsProblemRepeatEmpty
        {
            get
            {
                if (_problemsToRepeatObsColl.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion Properties

        #region Ctors

        public XThemeVM()
        {
            _theme = XKernel.Instance.Get<ITheme>();
            Init();
        }

        public XThemeVM(ITheme theme, Action save)
        {
            _theme = theme;
            this.Save = save;
            Init();
        }

        #endregion Ctors

        #region Methods

        public void RemoveFromRepeat()
        {
            _problemsToRepeatObsColl.Remove(SelectedProblemToRepeat);
        }

        public void UpdateRepeats(DateTime repeateDate)
        {
            _problemsToRepeatObsColl.Clear();

            foreach (var problem in _problemsObsColl)
            {
                if (problem.RepeatDate <= repeateDate && problem.IsStudy)
                    _problemsToRepeatObsColl.Add(problem);
            }
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// Инициализирует внутренности.
        /// </summary>
        private void Init()
        {
            _problemsObsColl = new ObservableCollection<XProblemVM>();
            _problemsToRepeatObsColl = new ObservableCollection<XProblemVM>();

            foreach (var problem in _theme.Problems)
            {
                XProblemVM prob = new XProblemVM(problem, Save);
                _problemsObsColl.Add(prob);
                if (prob.RepeatDate <= DateTime.Today && prob.IsStudy)
                    _problemsToRepeatObsColl.Add(prob);
            }

            _problemsObsColl.CollectionChanged +=
                ProblemsObsColl_CollectionChanged;

            _problemsCVS = new CollectionViewSource();
            _problemsCVS.Source = _problemsObsColl;
            _problemsCVS.View.CurrentChanged += View_CurrentChanged;

            _problemsToRepeatCVS = new CollectionViewSource();
            _problemsToRepeatCVS.Source = _problemsToRepeatObsColl;
            _problemsToRepeatCVS.View.CurrentChanged += 
                ProblemsToRepeat_CurrentChanged;
        }

        #endregion Utilities

        #region EventHandlers

        private void ProblemsObsColl_CollectionChanged(
            object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                XProblemVM problem = e.NewItems[0] as XProblemVM;
                problem.Save = this.Save;
                    _theme.Problems.Add(problem.Problem);

            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
                _theme.Problems.Remove((IProblem)e.OldItems[0]);

            Save();
        }

        private void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedProblem");
        }


        private void ProblemsToRepeat_CurrentChanged(
            object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedProblemToRepeat");
        }

        #endregion EventHandlers
    }
}
