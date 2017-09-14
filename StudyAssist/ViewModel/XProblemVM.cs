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

            CommandsInit();

        }

        public XProblemVM(IProblem problem, Action save)
        {
            _problem = problem;
            this.Save = save;
            CommandsInit();
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
            set
            {
                if (value <= DateTime.Today)
                    throw new ArgumentOutOfRangeException("OutOfRangeDate");
                else
                {
                    Problem.RepeatDate = value;
                    RaisePropertyChanged(this, "RepeatDate");
                    RaisePropertyChanged(this, "RepeatDateString");
                    this.Save();
                }
            }

        }

        public String RepeatDateString
        {
            get
            {
                return RepeatDate.ToString("d.MM.yyyy");
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
                Answer = _repeatAnswer = value;
            }
        }

        public String StudyLevel
        {
            get 
            {
                return Problem.StudyLevel.ToString();
             }
        }

        #endregion


        #region methods

        public void StudyLevelUp()
        {
            this.Problem.StudyLevelUp();
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
        }

        public void StudyLevelDown()
        {
            this.Problem.StudyLevelDown();
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
        }

        public void MoveToTomorrow()
        {
            Problem.MoveToTomorrow();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
            Save();
        }

        public void RemoveFromStudy()
        {
            this.Problem.RemoveFromStudy();
            Save();

        }

        private void AddToStudy()
        {
            Problem.AddToStudy(0);
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
        }

        private void ShowAnswer()
        {
            _repeatAnswer = Problem.Answer;
            RaisePropertyChanged(this, "RepeatAnswer");
        }

        private void LevelDown()
        {
            Problem.StudyLevelDown();
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
        }


        private void CommandsInit()
        {
            LevelUpCommand = new XCommand(StudyLevelUp);
            LevelDownCommand = new XCommand(StudyLevelDown);
            ShowAnswerCommand = new XCommand(ShowAnswer);
            MoveToTomorrowCommand = new XCommand(MoveToTomorrow);
            AddToStudyCommand = new XCommand(AddToStudy);
        }


        #endregion



        #region commands

        public XCommand LevelUpCommand { get; set; }

        public XCommand LevelDownCommand { get; set; }

        public XCommand ShowAnswerCommand { get; set; }

        public XCommand MoveToTomorrowCommand { get; set; }

        public XCommand AddToStudyCommand { get; set; }


        #endregion
    }
}
