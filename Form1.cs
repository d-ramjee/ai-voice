using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;

namespace VoiceBot
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer s = new SpeechSynthesizer();

        Boolean wake = true;


        Choices List = new Choices();
        public Form1()
        {

            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();

            //Specifying all the different commands

            List.Add(new String[] { "hello", "how are you", "what time is it", "what is today", "open website", "wake", "sleep", "restart", "update" });

            Grammar gr = new Grammar(new GrammarBuilder(List));


            try
            {
                //Load Grammar into the recognition engine
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += Rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);


            }

            catch { return; }


            //Change Voice
            s.SelectVoiceByHints(VoiceGender.Female);

            s.Speak("Hello, My name is Friday");

            InitializeComponent();
        }



        public void restart()
        {
            Process.Start(@"C:\Desktop\Amy\VoiceBot.exe");
            Environment.Exit(0);
        }


        public void say(String H)
        {
            s.Speak(H);
            textBox2.AppendText(H + "\n");

        }


        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;

            // Sleep and Wake mode
            if (r == "wake") wake = true;

            if (r == "sleep") wake = false;


            //What you say

            if (r == "hello")
            {
                //What it says
                say("Hi");
            }

            if (r == "how are you")
            {

                say("great, and you");
            }

            //Time
            if (r == "what time is it")
            {
                say(DateTime.Now.ToString("h:mm tt"));
            }

            //Date
            if (r == "what is today")
            {
                say(DateTime.Now.ToString("M/d/yyyy"));
            }


            //Open a website
            if (r == "open website")
            {
                Process.Start("http://google.com");
            }

           if (wake == true)
            {

                if (r == "restart" || r=="update")
                {
                    restart();
                }
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
