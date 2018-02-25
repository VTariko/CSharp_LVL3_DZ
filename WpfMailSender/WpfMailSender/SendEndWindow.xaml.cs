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

namespace WpfMailSender
{
    /// <summary>
    /// Interaction logic for SendEndWindow.xaml
    /// </summary>
    public partial class SendEndWindow : Window
    {
        public SendEndWindow()
        {
            InitializeComponent();
        }

        public SendEndWindow(string text)
        {
            InitializeComponent();
            this.lblSendEnd.Content = text;
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
