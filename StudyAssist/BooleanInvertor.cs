using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StudyAssist
{
    public class BooleanInvertor : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));

            Boolean b = (Boolean)value;

            return !b;
        }

        public object ConvertBack(
            object value, 
            Type targetType,
            object parameter, 
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
