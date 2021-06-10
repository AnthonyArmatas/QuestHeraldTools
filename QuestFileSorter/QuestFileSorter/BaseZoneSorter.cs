using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestFileSorter
{
    public class BaseZoneSorter
    {
        public string checkZoneForQuests(string ZoneName, StreamWriter LoggingObj, string pageUrl)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Console.SetBufferSize(Console.BufferWidth, 32766);

            //string baseQuestWebAddress = "https://classic.wowhead.com/elwynn-forest-quests";
            WebClient client = new WebClient();
            string viewPageSourceText = client.DownloadString(pageUrl);
            //Regex101 
            //(new Listview[\s\S]*<div class=\\\"clear\\\"><\/div>)
            string questSectionRegex = "(new Listview[\\s\\S]*<div class=\\\"clear\\\"><\\/div>)";
            Regex rx = new Regex(questSectionRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection sectionmatches = rx.Matches(viewPageSourceText);
            string QuestIDSection = sectionmatches[0].Groups[1].Value;
            string getQuestIDRegex = "\\\"id\\\":([\\d]*),";
            rx = new Regex(getQuestIDRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            sectionmatches = rx.Matches(QuestIDSection);
            Console.WriteLine();

            string audioFileLocation = "G:\\Workshop\\QuestHerald\\CompressedAllInOne";
            DirectoryInfo dirInfo = new DirectoryInfo(audioFileLocation);//Assuming Test is your Folder

            for (int curID = 0; curID < sectionmatches.Count; curID++)
            {
                string curQuestID = sectionmatches[curID].Groups[1].Value;
                FileInfo[] Files = dirInfo.GetFiles(curQuestID + "_*.mp3"); //Getting mp3 files
                
                string str = "";
                foreach (FileInfo file in Files)
                {
                    try
                    {
                        file.CopyTo(@"F:\Workshop\QuestHerald 1.1.1\QuestAudio\" + file.Name);
                        LoggingObj.WriteLine(curQuestID);
                    }
                    catch (Exception ex)
                    {
                        LoggingObj.WriteLine(ex.Message);
                    }
                }
            }

            //string audioFileLocation = "G:\\Workshop\\QuestHerald\\CompressedAllInOne";
            //DirectoryInfo dirInfo = new DirectoryInfo(audioFileLocation);//Assuming Test is your Folder
            //FileInfo[] Files = dirInfo.GetFiles("*.mp3"); //Getting mp3 files
            //FileInfo[] Files2 = dirInfo.GetFiles("2_*.mp3"); //Getting mp3 files

            //string str = "";
            //var fasf = Files.ToHashSet();
            //foreach (FileInfo file in Files)
            //{
            //    str = str + ", " + file.Name;
            //    //Regex rx = new Regex(questSectionRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //    //MatchCollection sectionmatches = rx.Matches(viewPageSourceText);

            //}

            //Need to walk through each of the quest IDs retrived
            //And move their audio files into a new file

            //Need to get a list of each zone and do that for each zone.

            return "ZoneQuests";
        }
    }
}
