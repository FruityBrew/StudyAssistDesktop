using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudyAssist.ViewModel;

namespace StudyAssist
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _mainVM;

        

        public MainWindow()
        {
            
            _mainVM = new MainViewModel();
            
            InitializeComponent();

            this.DataContext = _mainVM;
            //this.XDatePickerSetDate.CalendarOpened += ButtonSetDate_Click;
        //    this.XButtonSetDate.Click += 
        }


        private void ButtonSetDate_Click(object sender, RoutedEventArgs e)
        {
          //  XDatePickerSetDate.CalendarOpened
        }

        private void DatePickerSetDate_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
