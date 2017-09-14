using StudyAssistInterfaces;
using System;

namespace StudyAssistModel
{

/*
*   При создании вопроса записывается дата его создания
*   Назначается уровень 1
*   При сохранении устанавливается дата повторения вопроса
*   
*   В дальнейшем при загрузке:
*   Проверяется дата повторения и по дате формируется список вопросов на повторение
*   Если пользователь повторил вопрос - он должен повысить его уровень на 1 поз вверх
*   При этом устанавливается новая дата повторения
*
*   При достижении __ уровня вопрос переводится в IsStudy = false;
*   При этом необходимо предусмотреть возможность отображения архивных вопросов при необходимости
*/
    [Serializable]
    public class XProblem : IProblem
    {
        #region fields

        String _answer;
        String _question;
        Boolean _isStudy;
        readonly DateTime _creationDate;
        DateTime _addingToStudyDate;
        DateTime _repeateDate;
        Byte _studyLevel;
        #endregion

        public XProblem()
        {
            this._isStudy = false;
            this._addingToStudyDate = DateTime.Today;
            this._creationDate = DateTime.Today;
            this.StudyLevelUp();

        }

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
                if (String.IsNullOrEmpty(value)) ;
                //throw new ArgumentException("Вопрос не может быть пустым");
                else
                    _question = value;
            }
        }

        public byte StudyLevel
        {
            get
            {
                return _studyLevel;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
        }

        public DateTime AddingToStudyDate
        {
            get
            {
                return _addingToStudyDate;
            }
        }

        public DateTime RepeatDate
        {
            get
            {
                return _repeateDate;
            }

            set
            {
                _repeateDate = value;
            }

        }

        #endregion

        #region methods

        public void StudyLevelUp()
        {
            if (IsStudy) return;

            _studyLevel += 1;
            SpecifyRepeatDate();
        }

        private void SpecifyRepeatDate()
        {
            switch (_studyLevel)
            {
                case 1:
                    _repeateDate = DateTime.Today.AddDays(1);
                    break;
                case 2:
                    _repeateDate = DateTime.Today.AddDays(3);
                    break;
                case 3:
                    _repeateDate = DateTime.Today.AddDays(7);
                    break;
                case 4:
                    _repeateDate = DateTime.Today.AddDays(14);
                    break;
                case 5:
                    _repeateDate = DateTime.Today.AddDays(30);
                    break;
                case 6:
                    _repeateDate = DateTime.Today.AddDays(60);
                    break;
                default:
                    _repeateDate = DateTime.Today.AddDays(120);
                    break;
            }
            
            
        }

        public void StudyLevelDown()
        {
            if (_studyLevel <= 1)
                return;

            _studyLevel -= 1;

            SpecifyRepeatDate();
        }

        public void MoveToTomorrow()
        {
            _repeateDate = DateTime.Today.AddDays(1);
        }



        public void RemoveFromStudy()
        {
            _isStudy = true;
        }

        public void AddToStudy(byte level)
        {
            _addingToStudyDate = DateTime.Today;
            IsStudy = false;
            _studyLevel = level;
        }
        #endregion


    }
}
