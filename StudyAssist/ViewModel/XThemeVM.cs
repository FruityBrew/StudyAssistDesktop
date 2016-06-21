using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Ninject;
using System.ComponentModel;


namespace StudyAssist.ViewModel
{
    public class XThemeVM : XBaseViewModel
    {
        #region fields

        ITheme _theme;
        ObservableCollection<XProblemVM> _problemsObsColl;
        CollectionViewSource _problemsCVS;
        ObservableCollection<XProblemVM> _problemsToRepeatObsColl;
        CollectionViewSource _problemsToRepeatCVS;
        Action _save;

        #endregion


        #region ctors
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
        
        private void Init()
        {      
            _problemsObsColl = new ObservableCollection<XProblemVM>();
            _problemsToRepeatObsColl = new ObservableCollection<XProblemVM>();
            foreach (var problem in _theme.Problems)
            {
                XProblemVM prob = new XProblemVM(problem, Save);
                _problemsObsColl.Add(prob);
                if (prob.RepeatDate <= DateTime.Today && !prob.IsStudy)
                    _problemsToRepeatObsColl.Add(prob);
            }
            _problemsObsColl.CollectionChanged += ProblemsObsColl_CollectionChanged;

            _problemsCVS = new CollectionViewSource();
            _problemsCVS.Source = _problemsObsColl;
            _problemsCVS.View.CurrentChanged += View_CurrentChanged;

            _problemsToRepeatCVS = new CollectionViewSource();
            _problemsToRepeatCVS.Source = _problemsToRepeatObsColl;
            _problemsToRepeatCVS.View.CurrentChanged += ProblemsToRepeat_CurrentChanged;
        }



        #endregion


        #region properties
        public ObservableCollection<XProblemVM> ProblemsObsColl
        {
            get
            {
                return _problemsObsColl;
            }
        }

        public Boolean IsStudy
        {
            get
            {
                return _theme.IsStudy;
            }
            set
            {
                _theme.IsStudy = value;
                Save();
            }
        }

        public String Name
        {
            get 
            {
                return _theme.Name;
            }

            set
            {
                _theme.Name = value;
    //            RaisePropertyChanged(this, "Name");
                Save();
            }
        }


        public ICollectionView ProblemsCollView
        {
            get
            {
                return _problemsCVS.View;
            }
        }

        public XProblemVM SelectedProblem
        {
            get 
            {
                return ProblemsCollView.CurrentItem as XProblemVM;
            }
        }

        public ICollectionView ProblemsToRepeatView
        {
            get
            {
                return _problemsToRepeatCVS.View;
            }
        }

        public XProblemVM SelectedProblemToRepeat
        {
            get
            {
                return ProblemsToRepeatView.CurrentItem as XProblemVM;
            }
        }

        public ITheme Theme
        {
            get
            {
                return _theme;
            }
        }

        public Action Save
        {
            get
            {
                return _save;
            }

            set
            {
                _save = value;
            }
        }

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

        #endregion


        #region eventHandlers

        private void ProblemsObsColl_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                XProblemVM problem = e.NewItems[0] as XProblemVM;
                problem.Save = this.Save;
                    _theme.Problems.Add(problem.Problem);

            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                _theme.Problems.Remove((IProblem)e.OldItems[0]);

            Save();
        }

        private void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedProblem");
        }


        private void ProblemsToRepeat_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(this, "SelectedProblemToRepeat");
        }
        #endregion
    }
}
