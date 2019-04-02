using Npgsql;
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

namespace UniversityGame
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string text;
        public MainWindow()
        {
            InitializeComponent();
            text = "";
        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl) Initialize();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = textBox.Text;
            Initialize();
        }

        private void Initialize()
        {
            if (facultyTab.IsSelected) facultyView.Initialize(text);
            if (departmentTab.IsSelected) departmentView.Initialize(text);
            if (groupTab.IsSelected) groupView.Initialize(text);
            if (characterTab.IsSelected) characterView.Initialize(text);
            if (stressTab.IsSelected) stressView.Initialize(text);
            if (subjectTab.IsSelected) subjectView.Initialize(text);
            if (semestorSubjectTab.IsSelected) semestorSubjectView.Initialize(text);
            if (skillTab.IsSelected) skillView.Initialize(text);
            if (perfomanceTab.IsSelected) perfomanceView.Initialize(text);
            if (subjectStructureTab.IsSelected) subjectStructureView.Initialize(text);
            if (classformTab.IsSelected) classformView.Initialize(text);
            if (scheduleTab.IsSelected) scheduleView.Initialize(text);
        }
    }
}
