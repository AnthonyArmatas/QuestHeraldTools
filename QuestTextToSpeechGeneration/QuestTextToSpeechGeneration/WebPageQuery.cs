using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuestTextToSpeechGeneration
{
    public class WebPageQuery
    {
        public WebPageQuery()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Console.SetBufferSize(Console.BufferWidth, 32766);
        }

        public string GetWebPageData(string WebPageUrl, int currentQuestNumber = -1)
        {
            WebClient client = new WebClient();
            string viewPageSourceText = client.DownloadString(WebPageUrl);

            if (string.IsNullOrEmpty(viewPageSourceText))
            {
                Console.WriteLine();
                string path = @"F:\Workshop\QuestTextToSpeechGeneration\ErrorLog.txt";
                if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("This file contains errors from the GetWebPageData!");
                        tw.WriteLine("Something went wrong downloading the page " + WebPageUrl + " it returned an empty string");
                        if(currentQuestNumber != -1)
                        {
                            tw.WriteLine("The questID in question is: " + currentQuestNumber);
                        }
                    }
                }
                else if (File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("Something went wrong downloading the page " + WebPageUrl + " it returned an empty string");
                        if (currentQuestNumber != -1)
                        {
                            tw.WriteLine("The questID in question is: " + currentQuestNumber);
                        }
                    }
                }
            }

            return viewPageSourceText;
        }
    }
}
