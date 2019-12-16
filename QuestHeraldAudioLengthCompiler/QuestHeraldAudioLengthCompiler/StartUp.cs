using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestHeraldAudioLengthCompiler
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //string filePathName = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\AudioLengthLibrary.lua";
            string filePathName = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\AudioLengthLibraryWithVA.lua";
            int audioDescriptionAdded = 0;
            int audioTitleAdded = 0;
            int audioVADescriptionAdded = 0;


            //Run Time is so small, just recreate the file every time.
            using (StreamWriter sw = File.CreateText(filePathName))
            {
                sw.WriteLine("--Author: Anthony Armatas");
                sw.WriteLine("questDescriptionTable = ");
                sw.WriteLine("	{");

                //string auidoPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\QuestAudio";
                string auidoPath = "F:\\Workshop\\QuestHerald\\QuestAudio";
                string[] questObjectiveFiles =
                Directory.GetFiles(auidoPath, "*_Description.mp3", SearchOption.AllDirectories);
                foreach (string objFile in questObjectiveFiles)
                {
                    string fileName = Path.GetFileName(objFile);
                    TagLib.File tagFile = TagLib.File.Create(objFile);
                    double length = tagFile.Properties.Duration.TotalSeconds;
                    sw.WriteLine("	[\"{0}\"] =  {1},", fileName, length);
                    audioDescriptionAdded++;
                }
                sw.WriteLine("	}");

                sw.WriteLine("questTitleTable = ");
                sw.WriteLine("	{");
                questObjectiveFiles =
                Directory.GetFiles(auidoPath, "*_Title.mp3", SearchOption.AllDirectories);
                foreach (string objFile in questObjectiveFiles)
                {
                    string fileName = Path.GetFileName(objFile);
                    TagLib.File tagFile = TagLib.File.Create(objFile);
                    double length = tagFile.Properties.Duration.TotalSeconds;
                    sw.WriteLine("	[\"{0}\"] =  {1},", fileName, length);
                    audioTitleAdded++;
                }
                sw.WriteLine("	}");

                sw.WriteLine("questVADescriptionTable = ");
                sw.WriteLine("	{");
                string auidoVAPath = "E:\\Games\\World of Warcraft\\_classic_\\Interface\\AddOns\\QuestHerald\\VAQuestAudio";
                string[] questVAObjectiveFiles =
                Directory.GetFiles(auidoVAPath, "*_Description.mp3", SearchOption.AllDirectories);
                foreach (string objFile in questVAObjectiveFiles)
                {
                    string fileName = Path.GetFileName(objFile);
                    TagLib.File tagFile = TagLib.File.Create(objFile);
                    double length = tagFile.Properties.Duration.TotalSeconds;
                    sw.WriteLine("	[\"{0}\"] =  {1},", fileName, length);
                    audioVADescriptionAdded++;
                }
                sw.WriteLine("	}");

            }
            Console.WriteLine("Finsihed writing Audio Length Library.");
            Console.WriteLine(audioDescriptionAdded + " Description AudioFiles were added to the Library.");
            Console.WriteLine(audioTitleAdded + " Title AudioFiles were added to the Library.");
            Console.WriteLine(audioVADescriptionAdded + " VADescription AudioFiles were added to the Library.");

            Console.Read();
        }
    }
}
