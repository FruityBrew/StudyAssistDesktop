using StudyAssistInterfaces;
using System;
using System.Collections.ObjectModel;

namespace StudyAssistModel
{
    [Serializable]
    public class XTheme : ITheme 
    {

        #region fields

        DateTime _dateCreation;
        DateTime _dateRepeat;
        String _name;
        Byte _studyLevel;
        Boolean _isStudy;
        ObservableCollection<IProblem> _problems;

        #endregion

        #region ctors

        public XTheme()
        {
            _problems = new ObservableCollection<IProblem>();
        }

        #endregion


        #region properties

        public DateTime CreationDate
        {
            get
            {
                return _dateCreation;
            }

            set
            {
                _dateCreation = value;
            }
        }

        public DateTime RepeatDate
        {
            get
            {
                return _dateRepeat;
            }

            set
            {
                _dateRepeat = value;
            }
        }

        public bool IsStudy
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

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Название темы на может быть пустым");
                else
                    _name = value;
            }
        }

        public ObservableCollection<IProblem> Problems
        {
            get
            {
                return _problems;
            }
        }



        public Byte StudyLevel
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


        #region methods
        #endregion

    }
}
