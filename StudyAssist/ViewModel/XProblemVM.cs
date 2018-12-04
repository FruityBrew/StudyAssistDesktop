using Ninject;
using StudyAssistInterfaces;
using StudyAssistIoC;
using System;

namespace StudyAssist.ViewModel
{
    public class XProblemVM : XBaseViewModel
    {
        #region fields

        IProblem _problem;

        Action _save;

        String _repeatAnswer;

        #endregion

        #region properties

        /// <summary>
        /// Проблема.
        /// </summary>
        public IProblem Problem
        {
            get { return _problem; }
        }

        /// <summary>
        /// Вопрос проблемы.
        /// </summary>
        public String Question
        {
            get { return _problem.Question; }
            set
            {
                _problem.Question = value;
                RaisePropertyChanged(this, "Question");
                Save();
            }
        }

        /// <summary>
        /// Объяснение.
        /// </summary>
        public String Answer
        {
            get { return Problem.Answer; }
            set
            {
                Problem.Answer = value;

                //  RaisePropertyChanged(this,"Answer");
                Save();
            }
        }

        /// <summary>
        /// Находится ли проблема на изучении.
        /// </summary>
        public Boolean IsStudy
        {
            get { return Problem.IsStudy; }
            set
            {
                Problem.IsStudy = value;
                Save();
                RaisePropertyChanged(this, "IsStudy");
            }
        }

        /// <summary>
        /// Включены ли автоповторы.
        /// </summary>
        public Boolean IsAutoRepeate
        {
            get { return Problem.IsAutoRepeate; }
            set
            {
                Problem.IsAutoRepeate = value;
                Problem.AddToStudy(Problem.StudyLevel);
                Save();

                RaisePropertyChanged(this, "IsAutoRepeate");
                RaisePropertyChanged(this, "StudyLevel");
                RaisePropertyChanged(this, "RepeatDate");
                RaisePropertyChanged(this, "RepeatDateString");
            }
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
        /// Дата повтора проблемы.
        /// </summary>
        public DateTime? RepeatDate
        {
            get { return Problem.RepeatDate; }
            set
            {
                Problem.RepeatDate = value;
                RaisePropertyChanged(this, "RepeatDate");
                RaisePropertyChanged(this, "RepeatDateString");
                this.Save();
            }
        }

        /// <summary>
        /// Дата повторения в виде строки.
        /// </summary>
        public String RepeatDateString
        {
            get
            {
                if (RepeatDate.HasValue == false)
                    return String.Empty;

                return RepeatDate.Value.ToString("d.MM.yyyy");
            }
        }

        /// <summary>
        /// Объяснение пробоемы.
        /// </summary>
        public string RepeatAnswer
        {
            get { return _repeatAnswer; }
            set { Answer = _repeatAnswer = value; }
        }

        /// <summary>
        /// Уровень изученности проблемы.
        /// </summary>
        public String StudyLevel
        {
            get { return Problem.StudyLevel.ToString(); }
        }

        #endregion Properties

        #region Constructors

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

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Увеличивает уровень изученности проблемы.
        /// </summary>
        public void StudyLevelUp()
        {
            this.Problem.StudyLevelUp();
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
        }

        /// <summary>
        /// Уменьшает уровень изученности проблемы.
        /// </summary>
        public void StudyLevelDown()
        {
            this.Problem.StudyLevelDown();
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
        }

        /// <summary>
        /// Переносит повторение проблемы на завтра.
        /// </summary>
        public void MoveToTomorrow()
        {
            Problem.MoveToTomorrow();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
            Save();
        }

        /// <summary>
        /// Удаляет проблему с обучения.
        /// </summary>
        public void RemoveFromStudy()
        {
            this.Problem.RemoveFromStudy();
            Problem.ResetLevel();
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
            RaisePropertyChanged(this, "IsStudy");
            RaisePropertyChanged(this, "RepeatDate");

        }

        /// <summary>
        /// Добавляет проблему на обучение.
        /// </summary>
        public void AddToStudy()
        {
            Problem.AddToStudy(1);
            Save();
            RaisePropertyChanged(this, "RepeatDateString");
            RaisePropertyChanged(this, "StudyLevel");
            RaisePropertyChanged(this, "IsStudy");
            RaisePropertyChanged(this, "RepeatDate");
        }

        /// <summary>
        /// Показывает объяснение проблемы.
        /// </summary>
        public void ShowAnswer()
        {
            _repeatAnswer = Problem.Answer;
            RaisePropertyChanged(this, "RepeatAnswer");
        }

        /// <summary>
        /// Инициализирует команды.
        /// </summary>
        private void CommandsInit()
        {
            LevelUpCommand = new XCommand(StudyLevelUp);
            LevelDownCommand = new XCommand(StudyLevelDown);
            ShowAnswerCommand = new XCommand(ShowAnswer);
            MoveToTomorrowCommand = new XCommand(MoveToTomorrow);
            AddToStudyCommand = new XCommand(AddToStudy);
            RemoveFromStudyCommand = new XCommand(RemoveFromStudy);
        }

        #endregion Methods

        #region commands

        public XCommand LevelUpCommand { get; set; }

        public XCommand LevelDownCommand { get; set; }

        public XCommand ShowAnswerCommand { get; set; }

        public XCommand MoveToTomorrowCommand { get; set; }

        public XCommand AddToStudyCommand { get; set; }

        public XCommand RemoveFromStudyCommand { get; set; }

        #endregion
    }
}
