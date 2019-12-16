using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.IO;

namespace QuestTextToSpeechGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            WebPageQuery pageGrabber = new WebPageQuery();
            GenerateQuestAudioFile questAudioCaller = new GenerateQuestAudioFile();
            //HIghest quest number I have found is
            //https://classic.wowhead.com/quest=9051/toxic-test
            int FinalQuestEndNumber = 10100;

            string logPath = @"F:\Workshop\QuestTextToSpeechGeneration\RunTimeLog.txt";
            // Started at 2 emded at 3281
            // Started at 3281 emded at 9665
            for (int currentQuestNumber = 2; currentQuestNumber < FinalQuestEndNumber; currentQuestNumber++)
            {
                using (StreamWriter tw = new StreamWriter(logPath, true))
                {
                    tw.WriteLine("STARTING QUEST " + currentQuestNumber + "!");
                    questAudioCaller.GenerateAudioFile(currentQuestNumber, tw, pageGrabber);
                    tw.WriteLine("ENDING QUEST " + currentQuestNumber + "!");
                }
            }
        }
    }
}
