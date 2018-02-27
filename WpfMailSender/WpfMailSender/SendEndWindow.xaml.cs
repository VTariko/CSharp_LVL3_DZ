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
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SendEndWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор с передачей текста в соответстующий блок
        /// </summary>
        /// <param name="text"></param>
        public SendEndWindow(string text)
        {
            InitializeComponent();
            this.tbSendEnd.Text = text;
        }

        /// <summary>
        /// Обработка нажатия кнопки "Ок"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
