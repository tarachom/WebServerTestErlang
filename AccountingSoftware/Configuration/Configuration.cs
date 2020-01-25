﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.XPath;

namespace AccountingSoftware
{
    //Конфігурація
    //В цьому класі має міститися вся інформація про конфігурацію
    public class Configuration
    {
        public Configuration()
        {
            Constants = new Dictionary<string, ConfigurationConstants>();
            Directories = new Dictionary<string, ConfigurationDirectories>();
            Documents = new Dictionary<string, ConfigurationDocuments>();
            Enums = new Dictionary<string, ConfigurationEnums>();
            Registers = new Dictionary<string, ConfigurationRegisters>();
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public Dictionary<string, ConfigurationConstants> Constants { get; set; }

        public Dictionary<string, ConfigurationDirectories> Directories { get; set; }

        public Dictionary<string, ConfigurationDocuments> Documents { get; set; }

        public Dictionary<string, ConfigurationEnums> Enums { get; set; }

        public Dictionary<string, ConfigurationRegisters> Registers { get; set; }

        public static void Load(string pathToConf, Configuration Conf)
        {
            XPathDocument xPathDoc = new XPathDocument(pathToConf);
            XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

            LoadConfigurationInfo(Conf, xPathDocNavigator);

            LoadDirectories(Conf, xPathDocNavigator);
        }

        private static void LoadConfigurationInfo(Configuration Conf, XPathNavigator xPathDocNavigator)
        {
            XPathNavigator rootNodeConfiguration = xPathDocNavigator.SelectSingleNode("/Configuration");

            string nameConfiguration = rootNodeConfiguration.SelectSingleNode("Name").Value;
            Conf.Name = nameConfiguration;

            string authorConfiguration = rootNodeConfiguration.SelectSingleNode("Author").Value;
            Conf.Author = authorConfiguration;
        }

        private static void LoadDirectories(Configuration Conf, XPathNavigator xPathDocNavigator)
        {
            //Довідники
            XPathNodeIterator directoryNodes = xPathDocNavigator.Select("/Configuration/Directories/Directory");
            while (directoryNodes.MoveNext())
            {
                string nameNodeValue = directoryNodes.Current.SelectSingleNode("Name").Value;
                string descNodeValue = directoryNodes.Current.SelectSingleNode("Desc").Value;

                ConfigurationDirectories ConfObjectDirectories = new ConfigurationDirectories();
                ConfObjectDirectories.Name = nameNodeValue;
                ConfObjectDirectories.Desc = descNodeValue;

                Conf.Directories.Add(nameNodeValue, ConfObjectDirectories);

                LoadFields(ConfObjectDirectories.Fields, directoryNodes.Current);

                LoadTabularParts(ConfObjectDirectories.TabularParts, directoryNodes.Current);
            }
        }

        private static void LoadFields(Dictionary<string, ConfigurationObjectField> fields,
            XPathNavigator xPathDocNavigator)
        {
            XPathNodeIterator fieldNodes = xPathDocNavigator.Select("Fields/Field");
            while (fieldNodes.MoveNext())
            {
                string nameNodeValue = fieldNodes.Current.SelectSingleNode("Name").Value;
                string typeNodeValue = fieldNodes.Current.SelectSingleNode("Type").Value;
                string descNodeValue = fieldNodes.Current.SelectSingleNode("Desc").Value;

                ConfigurationObjectField ConfObjectField = new ConfigurationObjectField();
                ConfObjectField.Name = nameNodeValue;
                ConfObjectField.Type = typeNodeValue;
                ConfObjectField.Desc = descNodeValue;

                fields.Add(nameNodeValue, ConfObjectField);
            }
        }

        private static void LoadTabularParts(Dictionary<string, ConfigurationObjectTablePart> tabularParts,
            XPathNavigator xPathDocNavigator)
        {
            XPathNodeIterator tablePartNodes = xPathDocNavigator.Select("TabularParts/TablePart");
            while (tablePartNodes.MoveNext())
            {
                string nameNodeValue = tablePartNodes.Current.SelectSingleNode("Name").Value;
                string descNodeValue = tablePartNodes.Current.SelectSingleNode("Desc").Value;

                ConfigurationObjectTablePart ConfObjectTablePart = new ConfigurationObjectTablePart();
                ConfObjectTablePart.Name = nameNodeValue;
                ConfObjectTablePart.Desc = descNodeValue;

                tabularParts.Add(nameNodeValue, ConfObjectTablePart);

                LoadFields(ConfObjectTablePart.Fields, tablePartNodes.Current);
            }
        }

        public static void Save(string pathToConf, Configuration Conf)
        {
            XmlDocument xmlConfDocument = new XmlDocument();
            xmlConfDocument.AppendChild(xmlConfDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

            XmlElement rootConfiguration = xmlConfDocument.CreateElement("Configuration");
            xmlConfDocument.AppendChild(rootConfiguration);

            SaveConfigurationInfo(Conf, xmlConfDocument, rootConfiguration);

            SaveDirectories(Conf.Directories, xmlConfDocument, rootConfiguration);

            xmlConfDocument.Save(pathToConf);
        }

        private static void SaveConfigurationInfo(Configuration Conf, XmlDocument xmlConfDocument, XmlElement rootNode)
        {
            XmlElement rootConfigurationName = xmlConfDocument.CreateElement("Name");
            rootConfigurationName.InnerText = Conf.Name;
            rootNode.AppendChild(rootConfigurationName);

            XmlElement rootConfigurationAuthor = xmlConfDocument.CreateElement("Author");
            rootConfigurationAuthor.InnerText = Conf.Author;
            rootNode.AppendChild(rootConfigurationAuthor);
        }

        private static void SaveDirectories(Dictionary<string, ConfigurationDirectories> ConfDirectories, XmlDocument xmlConfDocument, XmlElement rootNode)
        {
            XmlElement rootDirectories = xmlConfDocument.CreateElement("Directories");
            rootNode.AppendChild(rootDirectories);

            foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in ConfDirectories)
            {
                XmlElement nodeDirectory = xmlConfDocument.CreateElement("Directory");
                rootDirectories.AppendChild(nodeDirectory);

                XmlElement nodeDirectoryName = xmlConfDocument.CreateElement("Name");
                nodeDirectoryName.InnerText = ConfDirectory.Key;
                nodeDirectory.AppendChild(nodeDirectoryName);

                XmlElement nodeDirectoryDesc = xmlConfDocument.CreateElement("Desc");
                nodeDirectoryDesc.InnerText = ConfDirectory.Value.Desc;
                nodeDirectory.AppendChild(nodeDirectoryDesc);

                SaveFields(ConfDirectory.Value.Fields, xmlConfDocument, nodeDirectory);

                SaveTabularParts(ConfDirectory.Value.TabularParts, xmlConfDocument, nodeDirectory);
            }
        }

        private static void SaveFields(Dictionary<string, ConfigurationObjectField> fields, XmlDocument xmlConfDocument, XmlElement rootNode)
        {
            XmlElement nodeFields = xmlConfDocument.CreateElement("Fields");
            rootNode.AppendChild(nodeFields);

            foreach (KeyValuePair<string, ConfigurationObjectField> field in fields)
            {
                XmlElement nodeField = xmlConfDocument.CreateElement("Field");
                nodeFields.AppendChild(nodeField);

                XmlElement nodeFieldName = xmlConfDocument.CreateElement("Name");
                nodeFieldName.InnerText = field.Key;
                nodeField.AppendChild(nodeFieldName);

                XmlElement nodeFieldType = xmlConfDocument.CreateElement("Type");
                nodeFieldType.InnerText = field.Value.Type;
                nodeField.AppendChild(nodeFieldType);

                XmlElement nodeFieldDesc = xmlConfDocument.CreateElement("Desc");
                nodeFieldDesc.InnerText = field.Value.Desc;
                nodeField.AppendChild(nodeFieldDesc);
            }
        }

        private static void SaveTabularParts(Dictionary<string, ConfigurationObjectTablePart> tabularParts, XmlDocument xmlConfDocument, XmlElement rootNode)
        {
            XmlElement nodeTabularParts = xmlConfDocument.CreateElement("TabularParts");
            rootNode.AppendChild(nodeTabularParts);

            foreach (KeyValuePair<string, ConfigurationObjectTablePart> tablePart in tabularParts)
            {
                XmlElement nodeTablePart = xmlConfDocument.CreateElement("TablePart");
                nodeTabularParts.AppendChild(nodeTablePart);

                XmlElement nodeTablePartName = xmlConfDocument.CreateElement("Name");
                nodeTablePartName.InnerText = tablePart.Key;
                nodeTabularParts.AppendChild(nodeTablePartName);

                XmlElement nodeTablePartDesc = xmlConfDocument.CreateElement("Desc");
                nodeTablePartDesc.InnerText = tablePart.Value.Desc;
                nodeTabularParts.AppendChild(nodeTablePartDesc);

                SaveFields(tablePart.Value.Fields, xmlConfDocument, nodeTabularParts);
            }
        }
    }
}