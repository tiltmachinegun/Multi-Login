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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Net;

namespace Multi_Login
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Arrival ar = new Arrival();
            ar.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Writeoff wo = new Writeoff();
            wo.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Warehouses_show wS = new Warehouses_show();
            wS.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MoveProducts mp = new MoveProducts();
            mp.Show();
        }
    }
}
