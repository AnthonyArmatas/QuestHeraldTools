using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace QuestTextToSpeechGeneration
{
    public class TextToSpeechGeneration
    {
        public TextToSpeechGeneration()
        {
            //Daniel Default
            RaceToVoice = new Dictionary<string, string>
            {
                {"humanmale","IVONA 2 Eric" },                  //Eric
                {"humanfemale","IVONA 2 Salli" },               //Salli
                {"dwarfmale","Acapela Telecom HQ TTS English (UK) (Graham 22 kHz)" },                  //Hans  - Graham
                {"dwarffemale","IVONA 2 Emma" },               //Lotte - IVONA 2 Emma
                {"gnomemale","IVONA 2 Skippy the Chipmunk" },   //Skippy
                {"gnomefemale","IVONA 2 Ivy" },                 //Ivy,
                {"nightelfmale","IVONA 2 Geraint" },            //Geraint
                {"nightelffemale","IVONA 2 Gwyneth" },          //Gwyneth
                {"orcmale","Acapela Telecom HQ TTS English (US) (Ryan 22 kHz)" },                     //Jan - Acapela Telecom HQ TTS English (US) (Ryan 22 kHz)
                {"orcfemale","IVONA 2 Nicole" },                  //Maja - IVONA 2 Nicole
                {"trollmale","Acapela Telecom HQ TTS English (US) (Aaron 22 kHz)" },               //Enrique - Acapela Telecom HQ TTS English (US) (Aaron 22 kHz)
                {"trollfemale","IVONA 2 Kimberly" },            //Conchita - IVONA 2 Kimberly
                {"undeadmale","IVONA 2 Joey" },                 //Joey
                {"undeadfemale","IVONA 2 Jennifer" },           //Jennifer
                {"taurenmale","IVONA 2 Russell" },              //Russell
                {"taurenfemale","IVONA 2 Kendra" },             //Kendra
                {"highelfmale","IVONA 2 Brian" },               //Brian
                {"highelffemale","IVONA 2 Amy" },               //Amy
                {"goblinmale","IVONA 2 Skippy the Chipmunk" },              //Giorgio - Skippy
                {"DefaultVoice","ScanSoft Daniel_Full_22kHz" }, //Default
            };
        }

        public Dictionary<string, string> RaceToVoice { get; set; }

        public string DefaultVoice = "DefaultVoice";
        public string QuestStartNpc = "StartNpc";
        public string QuestEndNpc = "EndNpc";


        public void CreateSoundFiles(int CurrentQuestNumber, Dictionary<string, string> QuestData)
        {
            string audioFolderPath = "G:\\Workshop\\QuestHerald\\TTSQuestAudio\\";
            // Configure the audio output
            SpeechSynthesizer synth = new SpeechSynthesizer();
            string soundText = string.Empty;
            if (QuestData.TryGetValue("Title", out soundText))
            {
                string filePath = audioFolderPath + CurrentQuestNumber + "_Title.mp3";
                if (!File.Exists(filePath))
                {
                    synth.SetOutputToWaveFile(filePath);
                    synth.SelectVoice(RaceToVoice[DefaultVoice]);
                    synth.Speak(soundText);
                }
                
            }
            if (QuestData.TryGetValue("Objective", out soundText))
            {
                string filePath = audioFolderPath + CurrentQuestNumber + "_Objective.mp3";
                if (!File.Exists(filePath))
                {
                    synth.SetOutputToWaveFile(filePath);
                    synth.SelectVoice(RaceToVoice[DefaultVoice]);
                    synth.Speak(soundText);
                }
            }
            if (QuestData.TryGetValue("Description", out soundText))
            {
                string filePath = audioFolderPath + CurrentQuestNumber + "_Description.mp3";
                if (!File.Exists(filePath))
                {
                    synth.SetOutputToWaveFile(filePath);
                    synth.SelectVoice(RaceToVoice[QuestData[QuestStartNpc]]);
                    synth.Speak(soundText);
                }
            }
            if (QuestData.TryGetValue("Progress", out soundText))
            {
                string filePath = audioFolderPath + CurrentQuestNumber + "_Progress.mp3";
                if (!File.Exists(filePath))
                {
                    synth.SetOutputToWaveFile(filePath);
                    synth.SelectVoice(RaceToVoice[QuestData[QuestStartNpc]]);
                    synth.Speak(soundText);
                }
            }
            if (QuestData.TryGetValue("Completion", out soundText))
            {
                string filePath = audioFolderPath + CurrentQuestNumber + "_Completion.mp3";
                if (!File.Exists(filePath))
                {
                    synth.SetOutputToWaveFile(filePath);
                    synth.SelectVoice(RaceToVoice[QuestData[QuestEndNpc]]);
                    synth.Speak(soundText);
                }
            }

        }

        public void CreateTestSoundFiles()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            // Configure the audio output.   
            var listOfInstalledVoices = synth.GetInstalledVoices();

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\t1Graham.mp3");
            synth.SelectVoice("Acapela Telecom HQ TTS English (UK) (Graham 22 kHz)");
            synth.Speak("Hello this is the voice of Graham");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\t2Aaron.mp3");
            synth.SelectVoice("Acapela Telecom HQ TTS English (US) (Aaron 22 kHz)");
            synth.Speak("Hello this is the voice of Aaron");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\t3Ryan.mp3");
            synth.SelectVoice("Acapela Telecom HQ TTS English (US) (Ryan 22 kHz)");
            synth.Speak("Hello this is the voice of Ryan");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\t4Lucy.mp3");
            synth.SelectVoice("Acapela Telecom HQ TTS English (UK) (Lucy 22 kHz)");
            synth.Speak("Hello this is the voice of Lucy");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\t5Heather.mp3");
            synth.SelectVoice("Acapela Telecom HQ TTS English (US) (Heather 22 kHz)");
            synth.Speak("Hello this is the voice of Heather");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\t6Laura.mp3");
            synth.SelectVoice("Acapela Telecom HQ TTS English (US) (Laura 22 kHz)");
            synth.Speak("Hello this is the voice of Laura");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test1Skippy the Chipmunk.mp3");
            synth.SelectVoice("IVONA 2 Skippy the Chipmunk");
            synth.Speak("Hello this is the voice of Skippy the Chipmunk");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test2Emma.mp3");
            synth.SelectVoice("IVONA 2 Emma");
            synth.Speak("Hello this is the voice of Emma");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test3Ivy.mp3");
            synth.SelectVoice("IVONA 2 Ivy");
            synth.Speak("Hello this is the voice of Ivy");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test4Jennifer.mp3");
            synth.SelectVoice("IVONA 2 Jennifer");
            synth.Speak("Hello this is the voice of Jennifer");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test5Brian.mp3");
            synth.SelectVoice("IVONA 2 Brian");
            synth.Speak("Hello this is the voice of Brian");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test6Eric.mp3");
            synth.SelectVoice("IVONA 2 Eric");
            synth.Speak("Hello this is the voice of Eric");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test7Joey.mp3");
            synth.SelectVoice("IVONA 2 Joey");
            synth.Speak("Hello this is the voice of Joey");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test8Kendra.mp3");
            synth.SelectVoice("IVONA 2 Kendra");
            synth.Speak("Hello this is the voice of Kendra");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test9Kimberly.mp3");
            synth.SelectVoice("IVONA 2 Kimberly");
            synth.Speak("Hello this is the voice of Kimberly");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test10Salli.mp3");
            synth.SelectVoice("IVONA 2 Salli");
            synth.Speak("Hello this is the voice of Salli");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test11Amy.mp3");
            synth.SelectVoice("IVONA 2 Amy");
            synth.Speak("Hello this is the voice of Amy");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test12Daniel.mp3");
            synth.SelectVoice("ScanSoft Daniel_Full_22kHz");
            synth.Speak("Hello this is the voice of Daniel");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test13Geraint.mp3");
            synth.SelectVoice("IVONA 2 Geraint");
            synth.Speak("Hello this is the voice of Geraint");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test14Gwyneth.mp3");
            synth.SelectVoice("IVONA 2 Gwyneth");
            synth.Speak("Hello this is the voice of Gwyneth");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test15Hans.mp3");
            synth.SelectVoice("IVONA 2 Hans");
            synth.Speak("Hello this is the voice of Hans");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test16Nicole.mp3");
            synth.SelectVoice("IVONA 2 Nicole");
            synth.Speak("Hello this is the voice of Nicole");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test17Russell.mp3");
            synth.SelectVoice("IVONA 2 Russell");
            synth.Speak("Hello this is the voice of Russell");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test18Marlene.mp3");
            synth.SelectVoice("IVONA 2 Ricardo");
            synth.Speak("Hello this is the voice of Marlene");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test19Vitória.mp3");
            synth.SelectVoice("IVONA 2 Vitória");
            synth.Speak("Hello this is the voice of Vitória");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test20Miguel.mp3");
            synth.SelectVoice("IVONA 2 Miguel");
            synth.Speak("Hello this is the voice of Miguel");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test21Penélope.mp3");
            synth.SelectVoice("IVONA 2 Penélope");
            synth.Speak("Hello this is the voice of Penélope");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test22Chantal.mp3");
            synth.SelectVoice("IVONA 2 Chantal");
            synth.Speak("Hello this is the voice of Chantal");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test23Enrique.mp3");
            synth.SelectVoice("IVONA 2 Enrique");
            synth.Speak("Hello this is the voice of Enrique");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test24Conchita.mp3");
            synth.SelectVoice("IVONA 2 Conchita");
            synth.Speak("Hello this is the voice of Conchita");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test25Mads.mp3");
            synth.SelectVoice("IVONA 2 Mads");
            synth.Speak("Hello this is the voice of Mads");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test26Naja.mp3");
            synth.SelectVoice("IVONA 2 Naja");
            synth.Speak("Hello this is the voice of Naja");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test27Lotte.mp3");
            synth.SelectVoice("IVONA 2 Lotte");
            synth.Speak("Hello this is the voice of Lotte");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test28Ruben.mp3");
            synth.SelectVoice("IVONA 2 Ruben");
            synth.Speak("Hello this is the voice of Ruben");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test29Céline.mp3");
            synth.SelectVoice("IVONA 2 Céline");
            synth.Speak("Hello this is the voice of Céline");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test30Mathieu.mp3");
            synth.SelectVoice("IVONA 2 Mathieu");
            synth.Speak("Hello this is the voice of Mathieu");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test31Dóra.mp3");
            synth.SelectVoice("IVONA 2 Dóra");
            synth.Speak("Hello this is the voice of Dóra");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test32Karl.mp3");
            synth.SelectVoice("IVONA 2 Karl");
            synth.Speak("Hello this is the voice of Karl");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test33Carla.mp3");
            synth.SelectVoice("IVONA 2 Carla");
            synth.Speak("Hello this is the voice of Carla");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test34Giorgio.mp3");
            synth.SelectVoice("IVONA 2 Giorgio");
            synth.Speak("Hello this is the voice of Giorgio");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test35Agnieszka.mp3");
            synth.SelectVoice("IVONA 2 Agnieszka");
            synth.Speak("Hello this is the voice of Agnieszka");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test36Ewa.mp3");
            synth.SelectVoice("IVONA 2 Ewa");
            synth.Speak("Hello this is the voice of Ewa");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test37Jacek.mp3");
            synth.SelectVoice("IVONA 2 Jacek");
            synth.Speak("Hello this is the voice of Jacek");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test38Jan.mp3");
            synth.SelectVoice("IVONA 2 Jan");
            synth.Speak("Hello this is the voice of Jan");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test39Maja.mp3");
            synth.SelectVoice("IVONA 2 Maja");
            synth.Speak("Hello this is the voice of Maja");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test40Carmen.mp3");
            synth.SelectVoice("IVONA 2 Carmen");
            synth.Speak("Hello this is the voice of Carmen");

            synth.SetOutputToWaveFile("F:\\Workshop\\QuestTextToSpeechGeneration\\test41Tatyana.mp3");
            synth.SelectVoice("IVONA 2 Tatyana");
            synth.Speak("Hello this is the voice of Tatyana");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
