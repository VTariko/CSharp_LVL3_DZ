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

namespace BarComboBox
{
    /// <summary>
    /// Interaction logic for ToolBarControlButton.xaml
    /// </summary>
    public partial class ToolBarControlButton : UserControl
    {
        #region События

        /// <summary>
        /// Обработка нажатия клавиши Добавить
        /// </summary>
        public event RoutedEventHandler BtnAddClick;

        /// <summary>
        /// Обработка нажатия клавиши Редактировать
        /// </summary>
        public event RoutedEventHandler BtnEditClick;

        /// <summary>
        /// Обработка нажатия клавиши Удалить
        /// </summary>
        public event RoutedEventHandler BtnDelClick;

        #endregion

        #region Свойства

        public string LabelText
        {
            get => (string) lblHeader.Content;
            set => lblHeader.Content = value;
        }

        public Dictionary<string, object> ComboBoxCollection { get; set; }


        #endregion

        #region Конструктор

        public ToolBarControlButton()
        {
            InitializeComponent();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка нажатия кнопки Добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            BtnAddClick?.Invoke(sender, e);
        }

        /// <summary>
        /// Обработка нажатия кнопки Редактировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            BtnEditClick?.Invoke(sender, e);
        }

        /// <summary>
        /// Обработка нажатия кнопки Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDel_OnClick(object sender, RoutedEventArgs e)
        {
            BtnDelClick?.Invoke(sender, e);
        }

        #endregion
    }
}
