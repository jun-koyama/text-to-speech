using System;
using System.IO;
using System.Windows.Forms;

namespace TextToSpeech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ssml ssml = new Ssml();
            ssml.StartVoice("nozomi");
            ssml.AppendText(textBox1.Text);
            ssml.EndVoice();
            byte[] result = TextToSpeechClient.SendRequest(ssml);
            string fileName = GetSaveFileName();

        }

        private string GetSaveFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "result.lpcm";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = " (.lpcm)|*.lpcm";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.Title = "保存先のファイルを選択してください";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }
        private bool WriteFile(string fileName, byte[] bytes)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            return true;
        }

    }
}
