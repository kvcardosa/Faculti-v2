﻿using System;
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
using Faculti.Helpers;

namespace Faculti.UI.Controls
{
    /// <summary>
    /// Interaction logic for PasswordTextBox.xaml
    /// </summary>
    public partial class PasswordTextBox : UserControl
    {
        public PasswordTextBox()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public string Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Password
        {
            get { return (String)GetValue(PasswordProperty); }
            set 
            { 
                SetValue(PasswordProperty, value);
                IsError = !Syntax.IsValidPassword(value);
            }
        }

        public bool IsError
        {
            get { return (bool)GetValue(IsErrorProperty); }
            set
            {
                SetValue(IsErrorProperty, value);
                if (value == true)
                {
                    LabelTitle.Foreground = (Brush)Application.Current.Resources["ErrorRegular"];
                    PanelErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    LabelTitle.Foreground = (Brush)Application.Current.Resources["GrayDark"];
                    PanelErrorMessage.Visibility = Visibility.Hidden;
                }
            }
        }

        public string ErrorText
        {
            get { return (String)GetValue(ErrorTextProperty); }
            set { SetValue(ErrorTextProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string),
                typeof(PasswordTextBox), new PropertyMetadata(""));

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string),
                typeof(PasswordTextBox), new PropertyMetadata(""));

        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.Register("IsError", typeof(bool),
                typeof(PasswordTextBox), new PropertyMetadata(false));

        public static readonly DependencyProperty ErrorTextProperty =
            DependencyProperty.Register("ErrorText", typeof(string),
                typeof(PasswordTextBox), new PropertyMetadata(""));

        #region UI Methods
        private void PasswordHidden_LostFocus(object sender, RoutedEventArgs e)
        {
            TogglePassword.Visibility = Visibility.Hidden;
        }

        private void PasswordHidden_GotFocus(object sender, RoutedEventArgs e)
        {
            TogglePassword.Visibility = Visibility.Visible;
        }

        private void TogglePassword_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxInput.Focus();
            MaskPassword.Text = Password;
            MaskPassword.Visibility = Visibility.Visible;
        }

        private void TogglePassword_MouseLeave(object sender, MouseEventArgs e)
        {
            MaskPassword.Visibility = Visibility.Hidden;
            TextBoxInput.PasswordChar = '●';
        }

        private void PasswordHidden_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = TextBoxInput.Password;
            MaskPassword.Text = Password;
        }
        #endregion
    }
}
