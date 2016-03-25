using StudyAssistInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAssistModel
{
    class XStadySheduler 
    {

        IList<Int32> _repeatDayNumbers;
        
        #region ctors
        
        public XStadySheduler(params Int32[]  dayNumbers)
        {
            _repeatDayNumbers = new List<Int32>(5);
            foreach(var v in dayNumbers)
            {
                _repeatDayNumbers.Add(v);
            }
        }

        #endregion

        #region properties

        public IEnumerable<Int32> RepeatDayNumbers
        {
            get
            {
                return _repeatDayNumbers;
            }
        }

        #endregion

        #region commands
        #endregion

        #region methods

        public void AddRepeatDay(Int32 numDay)
        {
            _repeatDayNumbers.Add(numDay);
        }

        public Boolean IsRepeat(IProblem problem)
        {
            return true;
        }

        #endregion

        #region eventHandlers
        #endregion


    }
}
