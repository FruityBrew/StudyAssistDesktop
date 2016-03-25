using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using Ninject;

namespace StudyAssist.ViewModel
{
    public class XProblemVM : XBaseViewModel
    {
        #region fields

        IProblem _problem;
        Action _save;

        String _repeatAnswer;
        #endregion

        #region ctors

        public XProblemVM()
        {
            _problem = XKernel.Instance.Get<IProblem>();

            LevelUpCommand = new XCommand(StudyLevelUp);
            ShowAnswerCommand = new XCommand(ShowAnswer);
            MoveToTomorrowCommand = new XCommand(MoveToTomorrow);

        }

        public XProblemVM(IProblem problem, Action save)
        {
            _problem = problem;
            this.Save = save;
            LevelUpCommand = new XCommand(StudyLevelUp);
            ShowAnswerCommand = new XCommand(ShowAnswer);
            MoveToTomorrowCommand = new XCommand(MoveToTomorrow);



        }
        #endregion


        #region properties

        public IProblem Problem
        {
            get { return _problem; }
        }

        public String Question
        {
            get
            {
                return _problem.Question;
            }
            set
            {
                _problem.Question = value;
                RaisePropertyChanged(this, "Question");
                Save();
            }
        }

        public String Answer
        {
            get
            {
                return Problem.Answer;
            }
            set
            {
                Problem.Answer = value;
              //  RaisePropertyChanged(this,"Answer");
                Save();
            }
        }

        public Boolean IsStudy
        {
            get
            {
                return Problem.IsStudy;
            }
            set
            {
                Problem.IsStudy = value;
                Save();
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

        public DateTime RepeatDate
        {
            get
            {
                return Problem.RepeatDate;
            }
        }

        public string RepeatAnswer
        {
            get
            {
                return _repeatAnswer;
            }
            set
            {
                _repeatAnswer = value;
            }
        }

        #endregion


        #region methods

        public void StudyLevelUp()
        {
            this.Problem.StudyLevelUp();
            Save();
        }

        public void StudyLevelDown()
        {
            this.Problem.StudyLevelDown();
            Save(); 
        }

        public void MoveToTomorrow()
        {
            Problem.MoveToTomorrow();
            Save();
        }

        public void RemoveFromStudy()
        {
            this.Problem.RemoveFromStudy();
            Save();

        }

        public void AddToStudy()
        {
            Problem.AddToStudy();
            Save();

        }

        private void ShowAnswer()
        {
            _repeatAnswer = Problem.Answer;
            RaisePropertyChanged(this, "RepeatAnswer");
        }

        #endregion

        public XCommand LevelUpCommand { get; set; }

        public XCommand ShowAnswerCommand { get; set; }

        public XCommand MoveToTomorrowCommand { get; set; }


        #region
        #endregion
    }
}
