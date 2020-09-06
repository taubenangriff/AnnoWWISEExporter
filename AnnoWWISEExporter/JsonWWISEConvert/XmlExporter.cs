using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace AnnoWWISEExporter.JsonWWISEConvert
{
    class XmlExporter
    {
        XmlDocument doc;
        XmlElement Root;
        DocumentationManager DocumentationManager;

        public XmlExporter() {
            doc = new XmlDocument();
            Root = doc.CreateElement("Audios");
            doc.AppendChild(Root);
            DocumentationManager = new DocumentationManager(); 
        }

        public void AddAsset(String WwiseName, String WwiseID, Duration DurationGer, Duration DurationEng, Duration DurationFr) {
            XmlElement Asset = doc.CreateElement("Asset");
            XmlElement Values = doc.CreateElement("Values");
            XmlElement Template = doc.CreateElement("Template");
            Template.InnerText = "Audio";

            Asset.AppendChild(Template);
            Asset.AppendChild(Values);

            //Property Standard
            XmlElement Standard = doc.CreateElement("Standard");
            XmlElement GUID = doc.CreateElement("GUID");
            String AssetGUID = AutoGuiding.GiveGuid().ToString();
            GUID.InnerText = AssetGUID;

            XmlElement Name = doc.CreateElement("Name");
            Name.InnerText = WwiseName;

            Standard.AppendChild(GUID);
            Standard.AppendChild(Name);

            Values.AppendChild(Standard);
            //Property Audio
            XmlElement Audio = doc.CreateElement("Audio");
            XmlElement DurationLanguageArray = doc.CreateElement("DurationLanguageArray");

            Audio.AppendChild(DurationLanguageArray);
            DurationLanguageArray.AppendChild(CreateDurationLanguage(DurationGer, "German"));
            DurationLanguageArray.AppendChild(CreateDurationLanguage(DurationEng, "English"));
            DurationLanguageArray.AppendChild(CreateDurationLanguage(DurationFr, "French"));

            Values.AppendChild(Audio);
            //Property WWiseStandard
            XmlElement WwiseStandard = doc.CreateElement("WwiseStandard");
            XmlElement WwiseIdNode = doc.CreateElement("WwiseID");
            WwiseIdNode.InnerText = WwiseID;

            WwiseStandard.AppendChild(WwiseIdNode);
            Values.AppendChild(WwiseStandard);
            //add documentation
            DocumentationManager.AddAudio(AssetGUID, WwiseName, WwiseID);

            Root.AppendChild(Asset);
        }
        //Takes in the unconverted duration from the wwise data. Will first convert it and then returns an XmlElement with the specified Language as Name.
        private XmlElement CreateDurationLanguage(Duration Duration, String Language)
        {
            XmlElement Lang = doc.CreateElement(Language);

            //convert duration
            float DurationMin = float.Parse(Duration.DurationMinimum, CultureInfo.InvariantCulture);
            float DurationMax = float.Parse(Duration.DurationMaximum, CultureInfo.InvariantCulture);

            XmlElement DurationMinimum = doc.CreateElement("DurationMinimum");
            DurationMinimum.InnerText = Round(DurationMin);

            XmlElement DurationMaximum = doc.CreateElement("DurationMaximum");
            DurationMaximum.InnerText = Round(DurationMax);
            Lang.AppendChild(DurationMinimum);
            Lang.AppendChild(DurationMaximum);
            return Lang; 
        }

        public String Round(float number) {
            return Math.Round(number * 1000).ToString();
        }

        public void Save() {
            doc.Save("output.xml");
        }
    }
    
}
