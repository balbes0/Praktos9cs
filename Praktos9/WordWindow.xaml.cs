using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;
using Spire.Doc;

namespace Praktos9
{
    public partial class WordWindow : Window
    {
        private string path;

        public WordWindow(string path)
        {
            InitializeComponent();
            this.path = path;
            if (path != "")
            {
                LoadDocx(path);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (path == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Documents|*.docx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    path = saveFileDialog.FileName;
                    SaveDocx(path);
                }
            }
            else
            {
                SaveDocx(path);
            }
        }

        private void SaveDocx(string path)
        {
            string rtfFilePath = Path.Combine(Path.GetTempPath(), "tempfile.rtf");
            using (FileStream fs = new FileStream(rtfFilePath, FileMode.Create))
            {
                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                textRange.Save(fs, DataFormats.Rtf);
            }
            Document document = new Document();
            document.LoadFromFile(rtfFilePath, FileFormat.Rtf);
            document.SaveToFile(path, FileFormat.Docx);
            MessageBox.Show("Файл был сохранен!");
        }

        private void LoadDocx(string path)
        {
            string tempRtfFilePath = Path.Combine(Path.GetTempPath(), "tempfile.rtf");
            Document document = new Document();
            document.LoadFromFile(path);
            document.SaveToFile(tempRtfFilePath, FileFormat.Rtf);
            using (FileStream fs = new FileStream(tempRtfFilePath, FileMode.Open))
            {
                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                textRange.Load(fs, DataFormats.Rtf);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendFile sendFile = new SendFile(rtb);
            sendFile.Show();
        }
    }
}
