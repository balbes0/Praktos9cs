using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Spire.Doc;
using System.Windows.Shapes;

namespace Praktos9
{
    public partial class MainWindow : Window
    {
        private string path;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WordWindow word = new WordWindow("");
            word.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word Documents|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                WordWindow word = new WordWindow(path);
                word.Show();
                Close();
            }
        }
    }
}