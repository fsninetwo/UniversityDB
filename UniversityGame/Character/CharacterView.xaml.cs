using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using UniversityGame;

namespace UniversityGame.Character
{
    /// <summary>
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : UserControl
    {
        private DAO<Character> dao;
        private int id = 0;
        private string search;
        public CharacterView()
        {
            InitializeComponent();
            dao = new CharacterDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToGroupChoice();
        }
        private bool checkColumns()
        {
            if (!Regex.IsMatch(nicknameField.Text, "[\\w]{1,40}"))
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
            if (groupChoice.SelectedItem == null)
            {
                MessageBox.Show("Group box must have a value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns())
            {
                dao.Insert(new Character(nicknameField.Text, passwordField.Text, emailField.Text, admin.IsChecked.Value, groupChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
            
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)characterTable.SelectedItem);
            AddFromDatabase();
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new Character(id, nicknameField.Text, passwordField.Text, emailField.Text, admin.IsChecked.Value, groupChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
        }
        private void updateField_Click(object sender, RoutedEventArgs e)
        {
            AddFromDatabase();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.ValidateNames = true;
            sfd.ShowDialog();
            if (!sfd.FileName.Equals(""))
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = app.ActiveSheet;
                app.Visible = false;
                for (int i = 0; i < characterTable.Items.Count; i++)
                {
                    characterTable.SelectedIndex = i;
                    Character selectedItem = (dynamic)characterTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.nickname.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.password.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.email.ToString();
                    ws.Cells[i + 1, 5] = selectedItem.group.ToString();
                    ws.Cells[i + 1, 6] = selectedItem.admin.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }
        public void AddToGroupChoice()
        {
            groupChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("group");
            foreach(string item in items) groupChoice.Items.Add(item);
        }
        public void CleanFields()
        {
            nicknameField.Text = "";
            passwordField.Text = "";
            emailField.Text = "";
            groupChoice.SelectedItem = null;
            admin.IsChecked = false;
            id = 0;
        }
        private void AddFromDatabase()
        {
            characterTable.Items.Clear();
            List<Character> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach(Character item in items) characterTable.Items.Add(item);
        }
        private void characterTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (characterTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)characterTable.SelectedItem;
                nicknameField.Text = selectedItem.nickname;
                emailField.Text = selectedItem.email;
                groupChoice.SelectedItem = selectedItem.group;
                admin.IsChecked = selectedItem.admin;
                id = selectedItem.id;
            }
        }
        private void characterTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
