using Microsoft.Win32;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Praktos9
{
    public partial class SendFile : Window
    {
        private RichTextBox rtb;
        private string path = "";
        public SendFile(RichTextBox rtb)
        {
            InitializeComponent();
            this.rtb = rtb;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextRange range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                ComboBoxItem selectedDomainItem = Domain.SelectedItem as ComboBoxItem;
                string smtp = selectedDomainItem.Tag.ToString();
                MailMessage message = new MailMessage(From.Text, To.Text, Subject.Text, range.Text);
                if (path != "")
                {
                    message.Attachments.Add(new Attachment(path));
                }
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(smtp);
                client.Credentials = new NetworkCredential(From.Text, Pass.Password);
                client.EnableSsl = true;
                client.Send(message);            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке {ex}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(path);
                Buton.Content = $"Прикреплен файл: {fileName}";
            }
        }
    }
}
