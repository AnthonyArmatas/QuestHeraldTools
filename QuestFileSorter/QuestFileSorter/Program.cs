using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestFileSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] zoneQuestUrls = new string[]
            {
                "https://classic.wowhead.com/alterac-mountains-quests",
                "https://classic.wowhead.com/alterac-valley-eastern-kingdoms-quests",
                "https://classic.wowhead.com/arathi-highlands-quests",
                "https://classic.wowhead.com/badlands-quests",
                "https://classic.wowhead.com/blackrock-mountain-quests",
                "https://classic.wowhead.com/blasted-lands-quests",
                "https://classic.wowhead.com/burning-steppes-quests",
                "https://classic.wowhead.com/deeprun-tram-quests",
                "https://classic.wowhead.com/dun-morogh-quests",
                "https://classic.wowhead.com/duskwood-quests",
                "https://classic.wowhead.com/eastern-plaguelands-quests",
                "https://classic.wowhead.com/elwynn-forest-quests",
                "https://classic.wowhead.com/hillsbrad-foothills-quests",
                "https://classic.wowhead.com/ironforge-quests",
                "https://classic.wowhead.com/kharanos-quests",
                "https://classic.wowhead.com/loch-modan-quests",
                "https://classic.wowhead.com/redridge-mountains-quests",
                "https://classic.wowhead.com/searing-gorge-quests",
                "https://classic.wowhead.com/eastern-kingdoms-shadowfang-keep-quests",
                "https://classic.wowhead.com/silverpine-forest-quests",
                "https://classic.wowhead.com/stormwind-city-quests",
                "https://classic.wowhead.com/stranglethorn-vale-quests",
                "https://classic.wowhead.com/swamp-of-sorrows-quests",
                "https://classic.wowhead.com/hinterlands-quests",
                "https://classic.wowhead.com/tirisfal-glades-quests",
                "https://classic.wowhead.com/undercity-quests",
                "https://classic.wowhead.com/western-plaguelands-quests",
                "https://classic.wowhead.com/westfall-quests",
                "https://classic.wowhead.com/wetlands-quests",
                "https://classic.wowhead.com/ashenvale-quests",
                "https://classic.wowhead.com/azshara-quests",
                "https://classic.wowhead.com/darkshore-quests",
                "https://classic.wowhead.com/darnassus-quests",
                "https://classic.wowhead.com/desolace-quests",
                "https://classic.wowhead.com/durotar-quests",
                "https://classic.wowhead.com/dustwallow-marsh-quests",
                "https://classic.wowhead.com/felwood-quests",
                "https://classic.wowhead.com/feralas-quests",
                "https://classic.wowhead.com/moonglade-quests",
                "https://classic.wowhead.com/mulgore-quests",
                "https://classic.wowhead.com/orgrimmar-quests",
                "https://classic.wowhead.com/ruttheran-village-quests",
                "https://classic.wowhead.com/silithus-quests",
                "https://classic.wowhead.com/stonetalon-mountains-quests",
                "https://classic.wowhead.com/tanaris-quests",
                "https://classic.wowhead.com/teldrassil-quests",
                "https://classic.wowhead.com/barrens-quests",
                "https://classic.wowhead.com/thousand-needles-quests",
                "https://classic.wowhead.com/thunder-bluff-quests",
                "https://classic.wowhead.com/timbermaw-hold-quests",
                "https://classic.wowhead.com/ungoro-crater-quests",
                "https://classic.wowhead.com/winterspring-quests",
            };
            BaseZoneSorter Sorter = new BaseZoneSorter();
            string logPath = @"F:\Workshop\QuestHerald 1.1.1\QuestAudio\RunTimeLog.txt";
            using (StreamWriter tw = new StreamWriter(logPath, true))
            {
                tw.WriteLine("Quests Added");
                foreach(string pageUrl in zoneQuestUrls)
                {
                    Sorter.checkZoneForQuests(pageUrl, tw, pageUrl);
                }

            }
        }
    }
}
