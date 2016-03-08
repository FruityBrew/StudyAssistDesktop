using StudyAssistInterfaces;
using StudyAssistIoC;
using System;
using Ninject;

namespace StudyAssist.ViewModel
{
    public class XProblemVM 
    {
        #region fields

        IProblem _problem;

        #endregion

        #region ctors

        public XProblemVM()
        {
            _problem = XKernel.Instance.Get<IProblem>();
        }

        public XProblemVM(IProblem problem)
        {
            _problem = problem;
        }
        #endregion


        #region properties

        public IProblem Problem
        {
            get { return _problem; }
        }

        public String Qustion
        {
            get
            {
                return _problem.Question;
            }
            set
            {
                _problem.Question = value;
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
            }

        }

        #endregion


        #region
        #endregion

        #region
        #endregion
    }
}
