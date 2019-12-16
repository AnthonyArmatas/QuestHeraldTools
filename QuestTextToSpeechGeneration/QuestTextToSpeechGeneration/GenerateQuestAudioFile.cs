using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestTextToSpeechGeneration
{
    public class GenerateQuestAudioFile
    {
        public GenerateQuestAudioFile()
        {
            RaceList = new List<string>
            {
                "humanmale",
                "humanfemale",
                "dwarfmale",
                "dwarffemale",
                "gnomemale",
                "gnomefemale",
                "nightelfmale",
                "nightelffemale",
                "orcmale",
                "orcfemale",
                "trollmale",
                "trollfemale",
                "undeadmale",
                "undeadfemale",
                "taurenmale",
                "taurenfemale",
                "highelfmale",
                "highelffemale",
                "goblinmale"
            };
        }

        public List<string> RaceList { get; set; }

        public void GenerateAudioFile(int CurrentQuestNumber, StreamWriter LoggingObj, WebPageQuery PageGrabber)
        {
            string baseQuestWebAddress = "https://classic.wowhead.com/quest=" + CurrentQuestNumber.ToString();
            string questWebPage = PageGrabber.GetWebPageData(baseQuestWebAddress, CurrentQuestNumber);
            if(QuestExists(questWebPage))
            {
                GatherQuestInformation questInfoParser = new GatherQuestInformation(
                    CurrentQuestNumber, questWebPage, new Dictionary<string, string>(),
                    LoggingObj, PageGrabber, RaceList);

                Dictionary<string, string> AllQuestData = questInfoParser.GetQuestInformation(questWebPage);
                TextToSpeechGeneration ttsGenerator = new TextToSpeechGeneration();
                ttsGenerator.CreateSoundFiles(CurrentQuestNumber, AllQuestData);
            }
            
        }


        public bool QuestExists(string QuestPageText)
        {
            Regex rx = new Regex("(Quest #[0-9]* doesn't exist\\. It may have been removed from the game\\.)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(QuestPageText);
            if(matches.Count > 0)
            {
                return false;
            }

            return true;
        }
    }
}
