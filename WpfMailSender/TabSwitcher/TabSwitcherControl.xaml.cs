using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace TabSwitcher
{
    /// <summary>
    /// Interaction logic for TabSwitcherControl.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        #region События

        public event RoutedEventHandler BtnNextClick;
        public event RoutedEventHandler BtnPrevClick;

        #endregion

        #region Поля

        private bool _hideBtnPrev;
        private bool _hideBtnNext;

        #endregion

        #region Свойства

        /// <summary>
        /// Будет ли скрыта кнопка Предыдущий
        /// </summary>
        public bool IsHideBtnPrev
        {
            get => _hideBtnPrev;
            set
            {
                _hideBtnPrev = value;
                SetButtons();
            }
        }

        /// <summary>
        /// Будет ли скрыта кнопка Следующий
        /// </summary>
        public bool IsHideBtnNext
        {
            get => _hideBtnNext;
            set
            {
                _hideBtnNext = value;
                SetButtons();
            }
        }

        #endregion

        #region Конструктор

        public TabSwitcherControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка ситуации
        /// кнопка Следующий: не доступна
        /// кнопка Предыдущий: доступна
        /// </summary>
        private void NextTruePrevFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrev.Visibility = Visibility.Visible;
            btnPrev.SetValue(Grid.ColumnSpanProperty, 2);
        }

        /// <summary>
        /// Обработка ситуации
        /// кнопка Следующий: не доступна
        /// кнопка Предыдущий: не доступна
        /// </summary>
        private void NextTruePrevTrue()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrev.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Обработка ситуации
        /// кнопка Следующий: доступна
        /// кнопка Предыдущий: доступна
        /// </summary>
        private void NextFalsePrevFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrev.Visibility = Visibility.Visible;
            btnPrev.SetValue(Grid.ColumnSpanProperty, 1);
            btnNext.SetValue(Grid.ColumnProperty, 1);
            btnNext.SetValue(Grid.ColumnSpanProperty, 1);
        }

        /// <summary>
        /// Обработка ситуации
        /// кнопка Следующий: доступна
        /// кнопка Предыдущий: не доступна
        /// </summary>
        private void NextFalsePrevTrue()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrev.Visibility = Visibility.Hidden;
            btnNext.SetValue(Grid.ColumnProperty, 0);
            btnNext.SetValue(Grid.ColumnSpanProperty, 2);
        }

        /// <summary>
        /// Метод отрисовки кнопок
        /// </summary>
        private void SetButtons()
        {
            if (_hideBtnNext && !_hideBtnPrev)
                NextTruePrevFalse();
            else if (_hideBtnNext && _hideBtnPrev)
                NextTruePrevTrue();
            else if (!_hideBtnNext && !_hideBtnPrev)
                NextFalsePrevFalse();
            else if (!_hideBtnNext && _hideBtnPrev)
                NextFalsePrevTrue();
        }

        #endregion

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            BtnNextClick?.Invoke(sender, e);
        }

        private void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            BtnPrevClick?.Invoke(sender, e);
        }
    }
}