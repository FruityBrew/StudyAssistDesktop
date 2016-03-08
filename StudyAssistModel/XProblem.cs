using StudyAssistInterfaces;
using System;

namespace StudyAssistModel
{
    [Serializable]
    public class XProblem : IProblem
    {
        #region fields

        String _answer;
        String _question;
        Boolean _isStudy;
        #endregion

        #region properties
        public String Answer
        {
            get
            {
                return _answer;
            }

            set
            {
                _answer = value;
            }
        }

        public Boolean IsStudy
        {
            get
            {
                return _isStudy;
            }

            set
            {
                _isStudy = value;
            }
        }

        public String Question
        {
            get
            {
                return _question;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Вопрос не может быть пустым");
                else
                  _question = value;
            }
        }

        public byte StudyLevel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}
