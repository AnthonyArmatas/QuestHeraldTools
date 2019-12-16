using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestTextToSpeechGeneration
{
    class GatherQuestInformation
    {
        public GatherQuestInformation(
            int questID,
            string questPageText,
            Dictionary<string, string> questInformation,
            StreamWriter loggingObj,
            WebPageQuery pageGrabber,
            List<string> raceList)
        {
            QuestID = questID;
            QuestPageText = questPageText;
            QuestInformation = questInformation;
            LoggingObj = loggingObj;
            PageGrabber = pageGrabber;
            RaceList = raceList;
        }

        public int QuestID { get; set; }

        public string QuestPageText { get; set; }

        public Dictionary<string, string> QuestInformation { get; set; }

        public StreamWriter LoggingObj { get; set; }

        public WebPageQuery PageGrabber { get; set; }

        public List<string> RaceList;

        public string DefaultVoice = "DefaultVoice";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="questWebText"></param>
        /// <returns></returns>
        public Dictionary<string,string> GetQuestInformation(string questWebText)
        {
            //I am not super happy about either the specificity of the regex nor the fact that I have basically the same key twice in a dictionary
            //However ripping from websites are obnoxtious and this seems to be the easiest way to go about it and honestly I cant be arsed to get 
            //Too much more detail here
            Dictionary<string, string> SearchSectionAndRegexs = new Dictionary<string, string>
            {
                {"Title","\\\"name\\\"\\:\\\"([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\>\\&\\;\\:]+)\\\",\\\"u"},
                {"Objective","<script>WH\\.WAS\\.validateUnit\\(\\\"ad-mobileMedrec\\\"\\);<\\/script>\\n[\\s]+<\\/div>\\\n<\\/div>\\\n([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\\n\\\n\\<"},
                {"Description","<h2 class=\\\"heading-size-3\\\">Description<\\/h2>\\n([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\n\\n\\n<h2"},
                {"Progress",">Progress<\\/a><\\/h2>\\\n<div id=\\\"lknlksndgg-progress\\\" style=\\\"display: none\\\">([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\\n\\\n<\\/div>\\\n\\\n\\\n"},
                {"Completion","div id=\\\"lknlksndgg-completion\\\" style=\\\"display: none\\\">([a-zA-Z0-9\\s\'\"\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\\n\\\n<\\/div>\\\n\\\n\\\n"},
                {"Completion2",">Completion<\\/h2>\\n([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:\"]+)\\n\\n<div"},
            };
            ////Regex that works in regex101 Need to update Title, Obj, Des,Prog
            ////{"Title","\\\"name\\\"\\:\\\"([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\>\\&\\;\\:]+)\\\",\\\"u"},
            ////{ "Objective","<script>WH\\.WAS\\.validateUnit\\(\\\"ad-mobileMedrec\\\"\\);<\\/script>\\n[\\s]+<\\/div>\\\n<\\/div>\\\n([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\\n\\\n\\<"},
            ////{ "Description","<h2 class=\\\"heading-size-3\\\">Description<\\/h2>\\n([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\n\\n\\n<h2"},
            ////{ "Progress",">Progress<\\/a><\\/h2>\\\n<div id=\\\"lknlksndgg-progress\\\" style=\\\"display: none\\\">([a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;\\:]+)\\\n\\\n<\\/div>\\\n\\\n\\\n"},
            ////{ "Completion","div id=\\\"lknlksndgg-completion\\\" style=\\\"display: none\\\">([a-zA-Z0-9\s\'\-\.\,\!\?\<\\\/\>\&\;\:\"]+)\\n\\n<\/div>\\n\\n\\n"},
            ////{ "Completion2",">Completion<\/h2>\\n([a-zA-Z0-9\s\'\-\.\,\!\?\<\\\/\>\&\;\:\"]+)\\n\\n<div"},

            foreach (KeyValuePair<string, string> entry in SearchSectionAndRegexs)
            {
                GetQuestSection(entry);
            }

            GetNPCInformation();

            return QuestInformation;
        }

        public void GetQuestSection(KeyValuePair<string, string> questSectionSearchInfo)
        {
            Regex rx = new Regex(questSectionSearchInfo.Value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(QuestPageText);

            if (matches.Count == 1)
            {
                string sectionText = ReplaceUnreadableText(matches[0].Groups[1].Value);
                
                string pattern = @"\d+$";
                string replacement = "";
                Regex rgx = new Regex(pattern);
                string questSectionKey = rgx.Replace(questSectionSearchInfo.Key, replacement);

                QuestInformation.Add(questSectionKey, sectionText);
                LoggingObj.WriteLine("Adding Quest {0} to QuestInformationDic: {1}", questSectionSearchInfo.Key, sectionText);
            }
            else if (matches.Count > 1)
            {
                LoggingObj.WriteLine("More than one {0} found for quest: {1}", questSectionSearchInfo.Key, QuestID);
            }
            else
            {
                LoggingObj.WriteLine("No {0} found for quest: {1}", questSectionSearchInfo.Key, QuestID);
            }
        }

        public string ReplaceUnreadableText(string OriginalQuestSectionText)
        {
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "<br />", "    ", RegexOptions.IgnoreCase);
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "&lt;class&gt;", "one", RegexOptions.IgnoreCase);
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "&lt;name&gt;", "", RegexOptions.IgnoreCase);
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "&lt;race&gt;", "one", RegexOptions.IgnoreCase);
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "&lt;", " ", RegexOptions.IgnoreCase);
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "&gt;", " ", RegexOptions.IgnoreCase);
            OriginalQuestSectionText = Regex.Replace(OriginalQuestSectionText, "&nbsp;", " ", RegexOptions.IgnoreCase);

            return OriginalQuestSectionText;
        }

        public void GetNPCInformation()
        {
            bool startAndEndNPCExsist = true;
            string startNPCRegex = "Start: \\[url=[\\\\]+\\/[a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;]+=([0-9]+)\\]";
            string endNPCRegex = "End: \\[url=[\\\\]+\\/[a-zA-Z0-9\\s\'\\-\\.\\,\\!\\?\\<\\\\/\\>\\&\\;]+=([0-9]+)\\]";

            Regex rx = new Regex(startNPCRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection startNPCmatches = rx.Matches(QuestPageText);
            
            rx = new Regex(endNPCRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection endNPCmatches = rx.Matches(QuestPageText);

            string startNpcDictionaryName = "StartNpc";
            string endNpcDictionaryName = "EndNpc";
            

            if (startNPCmatches.Count == 0)
            {
                startAndEndNPCExsist = false;
                QuestInformation.Add(startNpcDictionaryName, DefaultVoice);
            }
            if (endNPCmatches.Count == 0)
            {
                startAndEndNPCExsist = false;
                QuestInformation.Add(endNpcDictionaryName, DefaultVoice);
            }



            string npcWebPage = string.Empty;
            string npcRace = string.Empty;
            string npcBaseUrl = "https://classic.wowhead.com/npc=";

            if (startAndEndNPCExsist == true)
            {
                if (startNPCmatches[0].Groups[1].Value == endNPCmatches[0].Groups[1].Value)
                {
                    LoggingObj.WriteLine("Both the Start and End NPC are the same ID: {0}", startNPCmatches[0].Groups[1].Value);
                    npcWebPage = PageGrabber.GetWebPageData(npcBaseUrl + startNPCmatches[0].Groups[1].Value);
                    npcRace = GetNPCRace(npcWebPage);
                    QuestInformation.Add(startNpcDictionaryName, npcRace);
                    QuestInformation.Add(endNpcDictionaryName, npcRace);

                    return;
                }
            }

            if (!QuestInformation.ContainsKey(startNpcDictionaryName))
            {
                LoggingObj.WriteLine("The Start NPC is the ID: {0}", startNPCmatches[0].Groups[1].Value);
                npcWebPage = PageGrabber.GetWebPageData(npcBaseUrl + startNPCmatches[0].Groups[1].Value);
                npcRace = GetNPCRace(npcWebPage);
                QuestInformation.Add(startNpcDictionaryName, npcRace);
            }
            if (!QuestInformation.ContainsKey(endNpcDictionaryName))
            {
                LoggingObj.WriteLine("The End NPC is the ID: {0}", endNPCmatches[0].Groups[1].Value);
                npcWebPage = PageGrabber.GetWebPageData(npcBaseUrl + endNPCmatches[0].Groups[1].Value);
                npcRace = GetNPCRace(npcWebPage);
                QuestInformation.Add(endNpcDictionaryName, npcRace);
            }
        }

        public string GetNPCRace(string NpcWebPage)
        {
            foreach (string curRace in RaceList)
            {
                Regex rx = new Regex(curRace, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection Racematches = rx.Matches(NpcWebPage);
                if(Racematches.Count > 0)
                {
                    LoggingObj.WriteLine("The Race was found to be:{0}", curRace);
                    return curRace;
                }
            }

            LoggingObj.WriteLine("NO race was found and default was used");
            return DefaultVoice;

        }
    }
}
