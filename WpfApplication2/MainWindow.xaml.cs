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
            this.ResizeMode = ResizeMode.NoResize;
            this.Closing += MainWindow_Closing;
            ln.Num = 1000;
            tb_number.SetBinding(TextBox.TextProperty, new Binding("Num") { Source = ln });
            cs.Speed = 10;
            tb_speed.SetBinding(TextBox.TextProperty, new Binding("Speed") { Source = cs });
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (thread_labelNum != null)
            {
                thread_labelNum.Abort();
            }
        }
    }
}
