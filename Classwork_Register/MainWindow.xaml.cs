using Microsoft.Win32;
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
using System.Web;

namespace Classwork_Register
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegistryKey key = Registry.CurrentUser;

        public MainWindow()
        {
            InitializeComponent();
            AddProg();
        }
        public void AddProg()
        {
            var run = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            var items = run.GetValueNames();
            foreach (var item in items)
            {
                listBox.Items.Add(item);
            }
        }

        private void btmStop_Click(object sender, RoutedEventArgs e)
        {
            var run = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            run.DeleteValue(listBox.SelectedItem.ToString());
            listBox.Items.Clear();
            AddProg();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog{Filter = "EXE|*.exe" };
            var run = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (openFileDialog.ShowDialog() == true)
            {
                var path = openFileDialog.FileName;
                var nameFile = System.IO.Path.GetFileName(path);
                run.SetValue(nameFile, path);
                listBox.Items.Clear();
                AddProg();
            }

        }
    }
}
