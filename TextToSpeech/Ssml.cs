using System;
using System.Text;

namespace TextToSpeech
{
    class Ssml
    {
        private readonly string Xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>\n<speak version=""1.1"" >\n{0}\n</speak>";

        private readonly string TagStartVoice = @"<voice name=""{0}"">";

        private readonly string TagEndVoice = @"</voice>";

        private readonly string TagStartProsody = @"<prosody pitch=""{0}"" range=""{1}"" rate=""{2}"" volume=""{3}"">";

        private readonly string TagEndProsody = @"</prosody>";

        private readonly string TagBreakTime = @"<break time=""{0}ms"" />";

        private StringBuilder ssmlText = new StringBuilder();

        public void StartVoice(string voiceName)
        {
            ssmlText.Append(String.Format(TagStartVoice, voiceName));
        }

        public void EndVoice()
        {
            ssmlText.Append(TagEndVoice);
        }

        public void StartProsody(float pitch, float range, float rate, float volume)
        {
            ssmlText.Append(String.Format(TagStartProsody, pitch.ToString("0.0"), range.ToString("0.0"), rate.ToString("0.0"), volume.ToString("0.0")));
        }

        public void AppendDate(string date)
        {
            // not implements
        }

        public void AppendTime(string time)
        {
            // not implements
        }

        public void AppendText(string voiceText)
        {
            ssmlText.Append(voiceText);
        }

        public void AppendBreak(int param)
        {
            ssmlText.Append(String.Format(TagBreakTime, param));
        }
        
        public override string ToString()
        {
            return String.Format(Xml, ssmlText.ToString());
        }
    }
}
