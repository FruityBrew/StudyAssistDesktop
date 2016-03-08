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


        #endregion


        #region ctors
        public XThemeVM()
        {
            _theme = XKernel.Instance.Get<ITheme>();
            Init();
        }



        public XThemeVM(ITheme theme)
        {
            _theme = theme;
            Init();
        }
        
        private void Init()
        {
            _problemsObsColl = new ObservableCollection<XProblemVM>();
            _problemsObsColl.CollectionChanged += ProblemsObsColl_CollectionChanged;
            foreach (var problem in _theme.Problems)
            {
                _problemsObsColl.Add(new XProblemVM(problem));
            }
            _problemsCVS = new CollectionViewSource();
            _problemsCVS.Source = _problemsObsColl;
            _problemsCVS.View.CollectionChanged += View_CollectionChanged;
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
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return _theme.CreationDate;
            }
        }

        public DateTime RepeatDate
        {
            get
            {
                return _theme.RepeatDate;
            }
            set
            {
                _theme.RepeatDate = value;
            }

        }

        public ICollectionView ProblemsCollView
        {
            get
            {
                return _problemsCVS.View;
            }
        }

        private XProblemVM SelectedProblem
        {
            get 
            {
                return ProblemsCollView.CurrentItem as XProblemVM;
            }
        }

        public ITheme Theme
        {
            get
            {
                return _theme;
            }
        }

        #endregion


        #region eventHandlers

        private void ProblemsObsColl_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                _theme.Problems.Add((IProblem)e.NewItems[0]);
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                _theme.Problems.Remove((IProblem)e.OldItems[0]);
        }

        private void View_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(this, "SelectedProblem");
        }

        #endregion
    }
}
