using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.ComponentModel;
using System.Windows.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ln.Num = 1000;
            tb_number.SetBinding(TextBox.TextProperty, new Binding("Num") { Source = ln });
            cs.Speed = 10;
            tb_speed.SetBinding(TextBox.TextProperty, new Binding("Speed") { Source = cs });
        }

        private LabelNum ln = new LabelNum();
        private CountSpeed cs = new CountSpeed();

        private void updateNum()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    btn_startCountDown.IsEnabled = false;
                    tb_speed.IsReadOnly = true;
                    tb_number.IsReadOnly = true;
                    lb_endTime.Content = "end time:";
                    lb_startTime.Content = DateTime.Now.ToLongTimeString();
                });
            while (ln.Num != 0)
            {
                ln.Num--;
                Thread.Sleep(cs.Speed);
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    lb_endTime.Content = DateTime.Now.ToLongTimeString();
                    btn_startCountDown.IsEnabled = true;
                    tb_speed.IsReadOnly = false;
                    tb_number.IsReadOnly = false;
                });
        }

        private void btn_startCountDown_Click(object sender, RoutedEventArgs e)
        {
            Thread thread_labelNum = new Thread(() => updateNum());
            thread_labelNum.SetApartmentState(ApartmentState.MTA);
            thread_labelNum.Start();
        }
    }

    public class CountSpeed : INotifyPropertyChanged
    {
        private int speed { get; set; }
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Speed"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LabelNum : INotifyPropertyChanged
    {
        private int num { get; set; }
        public int Num
        {
            get { return num; }
            set
            {
                num = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Num"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
