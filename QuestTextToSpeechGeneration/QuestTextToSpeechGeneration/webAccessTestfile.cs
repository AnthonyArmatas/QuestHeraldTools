using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.CSharp;
using Newtonsoft.Json;

namespace QuestTextToSpeechGeneration
{
    public class webAccessTestfile
    {
        public void webCall()
        {
            // Setup to access to webpage
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Console.SetBufferSize(Console.BufferWidth, 32766);

            string[] webSiteLinks = new string[]
            {
                "https://classic.wowhead.com/quest=176",
                "https://classic.wowhead.com/quest=180",
                "https://classic.wowhead.com/quest=181"
            };

            foreach (string curWebSite in webSiteLinks)
            {
                WebClient client = new WebClient();
                string viewPageSourceText = client.DownloadString(curWebSite);
                Console.WriteLine(viewPageSourceText);

                //string sds =".*var myMapper = new Mapper\\((.*)\\).*';
                Regex rx = new Regex(".*var myMapper = new Mapper\\((.*)\\);.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                //Regex rx = new Regex(".*var myMapper = new Mapper\((.*)[\s]\)\;", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                MatchCollection matches = rx.Matches(viewPageSourceText);

                string javaScripStr = "";
                Stopwatch sw = new Stopwatch();

                try
                {
                    sw.Start();
                    javaScripStr = matches[0].Groups[1].Value;
                    sw.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                try
                {
                    //var deserObj = serializer.DeserializeObject(mapperC);
                    JObject htmlAttributes3 = (JObject)JsonConvert.DeserializeObject(javaScripStr);

                    JObject objectivesJObject = (JObject)htmlAttributes3.GetValue("objectives");
                    List<string> questStartZone = new List<string>();
                    List<string> questEndZone = new List<string>();
                    foreach (JProperty objectivesProps in objectivesJObject.Properties())
                    {
                        //var afsf = objectivesProps.First.First.ToObject<testZoneInfoObj>();
                        JObject zoneInfoHolderObj = (JObject)objectivesProps.First();
                        string ZoneName = zoneInfoHolderObj.GetValue("zone").ToString();
                        JArray zoneInfoJArray = (JArray)zoneInfoHolderObj.GetValue("levels").First;

                        foreach (JObject zoneInfoObjs in zoneInfoJArray)
                        {
                            JToken pointValue;
                            if (zoneInfoObjs.TryGetValue("point", out pointValue))
                            {
                                if (pointValue.ToString() == "start")
                                {
                                    questStartZone.Add(pointValue.ToString());
                                }
                                if (pointValue.ToString() == "end")
                                {
                                    questEndZone.Add(pointValue.ToString());
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadLine();
            }

            Console.ReadLine();

        }

    }
}
