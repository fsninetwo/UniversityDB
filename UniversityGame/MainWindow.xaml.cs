using Npgsql;
using System.Windows;
using System.Windows.Controls;

namespace UniversityGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Connection conn;
        public LoginWindow()
        {
            InitializeComponent();
            ChangeLogin(false);
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == false) conn = new Connection(userField.Text, passwordField.Text);
            else conn = new Connection(hostField.Text, portField.Text, userField.Text, passwordField.Text, dbField.Text);
            if (CheckConnection())
            {
                conn.SaveData();
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signup = new SignUpWindow();
            signup.Show();
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true) ChangeLogin(true);
            else ChangeLogin(false);
        }
        private void ChangeLogin(bool com)
        {
            hostField.IsEnabled = dbField.IsEnabled = portField.IsEnabled = com;
        }

        private bool CheckConnection()
        {
            try
            {
                conn.getConnection().Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("This user is not in base, try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
