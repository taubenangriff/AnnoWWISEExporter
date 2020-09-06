using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using AnnoWWISEExporter.lib;
using AnnoWWISEExporter.Exceptions; 

namespace AnnoWWISEExporter.JsonWWISEConvert
    
{
    class Converter
    {
        WWISEJsonObject French;
        WWISEJsonObject German;
        WWISEJsonObject English;

        public Converter(String french, String german, String english) {
            String FrenchFile = File.ReadAllText(french + ".json");
            French = JsonSerializer.Deserialize<WWISEJsonObject>(FrenchFile);

            String GermanFile = File.ReadAllText(german+".json");
            German = JsonSerializer.Deserialize<WWISEJsonObject>(GermanFile);

            String EnglishFile = File.ReadAllText(english+".json");
            English = JsonSerializer.Deserialize<WWISEJsonObject>(EnglishFile);
        }

        private List<MultiLanguageEvent> MapFiles() {
            //get all the needed data. First collect a list of events from the first file, then map the second and third file on top, they have to be equal anyway.
            List<MultiLanguageEvent> Events = new List<MultiLanguageEvent>();
            foreach (Soundbank bank in English.SoundBanksInfo.SoundBanks)
            {
                foreach (Event e in bank.IncludedEvents)
                {
                    MultiLanguageEvent multiLanguage = new MultiLanguageEvent();
                    multiLanguage.Id = e.Id;
                    multiLanguage.Name = e.Name;

                    Duration english = new Duration();
                    english.DurationMaximum = e.DurationMax;
                    english.DurationMinimum = e.DurationMin;
                    multiLanguage.DurationEng = english;

                    Events.Add(multiLanguage);
                }
            }

            //now map the other ones on top
            foreach (Soundbank bank in French.SoundBanksInfo.SoundBanks)
            {
                foreach (Event e in bank.IncludedEvents)
                {
                    //get the MultiLanguageEvent that has the same wwise ID
                    MultiLanguageEvent Event = Events.Find(x => x.Id.Equals(e.Id));
                    if (Event == null)
                    {
                        throw new FilesNotSimilarException();
                    }
                    else
                    {
                        Duration french = new Duration();
                        french.DurationMaximum = e.DurationMax;
                        french.DurationMinimum = e.DurationMin;
                        Event.DurationFr = french;
                    }
                }
            }
            foreach (Soundbank bank in German.SoundBanksInfo.SoundBanks)
            {
                foreach (Event e in bank.IncludedEvents)
                {
                    //get the MultiLanguageEvent that has the same wwise ID
                    MultiLanguageEvent Event = Events.Find(x => x.Id.Equals(e.Id));
                    if (Event == null)
                    {
                        throw new FilesNotSimilarException();
                    }
                    else
                    {
                        Duration german = new Duration();
                        german.DurationMaximum = e.DurationMax;
                        german.DurationMinimum = e.DurationMin;
                        Event.DurationGer = german;
                    }
                }
            }
            return Events; 
        }

        public void Convert()
        {
            List<MultiLanguageEvent> Events = MapFiles();
            XmlExporter exporter = new XmlExporter();
            foreach (MultiLanguageEvent e in Events) {
                exporter.AddAsset(e.Name, e.Id, e.DurationGer, e.DurationEng, e.DurationFr);
            }
            exporter.Save();
        }
        

    }
}
