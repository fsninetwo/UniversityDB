using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using UniversityGame.Character;

namespace UniversityGame
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private DAO<Character.Character> dao;
        public SignUpWindow()
        {
            InitializeComponent();
            dao = new CharacterDAO();
            AddToGroupChoice();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns()) dao.Insert(new Character.Character(nameField.Text, passwordField.Text, emailField.Text, false, groupChoice.SelectedItem.ToString()));
            Close();
        }
        public void AddToGroupChoice()
        {
            groupChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("group");
            foreach (string item in items) groupChoice.Items.Add(item);
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(nameField.Text, "[\\w]{1,40}"))
            {
                MessageBox.Show("Nickname field must have letters, numbers and doesn't have more than 40 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(passwordField.Text, "[\\w]{1,30}"))
            {
                MessageBox.Show("Password field must have letters, numbers and doesn't have more than 30 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(emailField.Text, "([\\w]+@[\\w]+.[\\w]+){1,40}"))
            {
                MessageBox.Show("Nickname field must have correct email \"Example: example@here.com\" and doesn't have more than 40 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
