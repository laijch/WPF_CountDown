using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        Thread thread_labelNum = null;
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
                    ln.Num = 1000;
                });
            MessageBox.Show("任务完成！");
        }

        private void btn_startCountDown_Click(object sender, RoutedEventArgs e)
        {
            thread_labelNum = new Thread(() => updateNum());
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
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Num"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
