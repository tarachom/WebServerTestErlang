﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AccountingSoftware;
using System.Xml;
using System.Xml.XPath;

namespace Configurator
{
	public partial class SaveConfigurationForm : Form
	{
		public SaveConfigurationForm()
		{
			InitializeComponent();
		}

		public Configuration Conf { get; set; }

		private void ApendLine(string head, string bodySelect, string futer = "")
		{
			if (richTextBoxInfo.InvokeRequired)
			{
				richTextBoxInfo.Invoke(new Action<string, string, string>(ApendLine), head, bodySelect, futer);
			}
			else
			{
				richTextBoxInfo.AppendText(head);

				if (!String.IsNullOrEmpty(bodySelect))
				{
					richTextBoxInfo.SelectionFont = new Font("Consolas"/*"Microsoft Sans Serif"*/, 10, FontStyle.Underline);
					richTextBoxInfo.SelectionColor = Color.DarkBlue;
					richTextBoxInfo.AppendText(bodySelect);
				}

				if (!String.IsNullOrEmpty(bodySelect))
				{
					richTextBoxInfo.SelectionFont = new Font("Consolas", 10);
					richTextBoxInfo.SelectionColor = Color.Black;
				}

				richTextBoxInfo.AppendText(" " + futer + "\n");
				richTextBoxInfo.ScrollToCaret();
			}
		}

		private string GetNameFromType(string Type)
		{
			switch (Type)
			{
				case "Constants":
					return "Константи";

				case "Directory":
					return "Довідник";

				case "Directory.TablePart":
					return "Довідник.Таблична частина";

				case "Document":
					return "Документ";

				case "Document.TablePart":
					return "Документ.Таблична частина";

				case "RegisterInformation":
					return "Регістер відомостей";

				case "RegisterInformation.TablePart":
					return "Регістер відомостей.Таблична частина";

				case "RegisterAccumulation":
					return "Регістер накопичення";

				case "RegisterAccumulation.TablePart":
					return "Регістер накопичення.Таблична частина";

				default:
					return "<Невідомий тип>";
			}
		}

		void SaveAndAnalize()
		{
			ApendLine("\n[ КОНФІГУРАЦІЯ ]", "", "\n");

			ApendLine("1. Створення копії файлу конфігурації", "");
			Conf.PathToCopyXmlFileConfiguration = Configuration.CreateCopyConfigurationFile(Conf.PathToXmlFileConfiguration);
			ApendLine(" --> " + Conf.PathToCopyXmlFileConfiguration, "\n");

			Conf.PathToTempXmlFileConfiguration = Configuration.GetTempPathToConfigurationFile(Conf.PathToXmlFileConfiguration);

			ApendLine("2. Збереження конфігурації у тимчасовий файл", "");
			Configuration.Save(Conf.PathToTempXmlFileConfiguration, Conf);
			ApendLine(" --> " + Conf.PathToTempXmlFileConfiguration, "\n");

			ApendLine("3. Отримання структури бази даних", "");
			ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();
			Configuration.SaveInformationSchema(informationSchema, @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml");

			ApendLine("4. Порівняння конфігурації та бази даних", "", "\n");
			try
			{
				Configuration.Comparison(
					@"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
					@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
					@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml",
					Conf.PathToTempXmlFileConfiguration,
					Conf.PathToCopyXmlFileConfiguration);
			}
			catch (Exception ex)
			{
				ApendLine(ex.Message, "");
			}

			XPathDocument xPathDoc = new XPathDocument(@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml");
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator nodeDeleteDirectory = xPathDocNavigator.Select("/root/Control_Table[IsExist = 'delete']");
			while (nodeDeleteDirectory.MoveNext())
			{
				XPathNavigator nodeName = nodeDeleteDirectory.Current.SelectSingleNode("Name");
				XPathNavigator nodeTable = nodeDeleteDirectory.Current.SelectSingleNode("Table");
				XPathNavigator nodeType = nodeDeleteDirectory.Current.SelectSingleNode("Type");

				ApendLine("Видалений " + GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
			}

			XPathNodeIterator nodeNewDirectory = xPathDocNavigator.Select("/root/Control_Table[IsExist = 'no']");
			while (nodeNewDirectory.MoveNext())
			{
				XPathNavigator nodeName = nodeNewDirectory.Current.SelectSingleNode("Name");
				XPathNavigator nodeType = nodeNewDirectory.Current.SelectSingleNode("Type");
				ApendLine("Новий " + GetNameFromType(nodeType.Value) + ": ", nodeName.Value);

				InfoTableCreateFieldCreate(nodeNewDirectory.Current, "\t ");
				richTextBoxInfo.AppendText("\n");

				XPathNodeIterator nodeDirectoryTabularParts = nodeNewDirectory.Current.Select("Control_TabularParts");
				while (nodeDirectoryTabularParts.MoveNext())
				{
					XPathNavigator nodeTabularPartsName = nodeDirectoryTabularParts.Current.SelectSingleNode("Name");
					ApendLine("\t Нова таблична частина: ", nodeTabularPartsName.Value);

					InfoTableCreateFieldCreate(nodeDirectoryTabularParts.Current, "\t\t ");
				}
			}

			XPathNodeIterator nodeDirectoryExist = xPathDocNavigator.Select("/root/Control_Table[IsExist = 'yes']");
			while (nodeDirectoryExist.MoveNext())
			{
				bool flag = false;

				XPathNodeIterator nodeDirectoryNewField = nodeDirectoryExist.Current.Select("Control_Field[IsExist = 'no']");
				if (nodeDirectoryNewField.Count > 0)
				{
					XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
					XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
					ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
					flag = true;
				}
				while (nodeDirectoryNewField.MoveNext())
				{
					XPathNavigator nodeFieldName = nodeDirectoryNewField.Current.SelectSingleNode("Name");
					ApendLine("\t Нове Поле: ", nodeFieldName.Value);
				}

				XPathNodeIterator nodeDirectoryExistField = nodeDirectoryExist.Current.Select("Control_Field[IsExist = 'yes']/Type[Coincide = 'no']");
				if (nodeDirectoryExistField.Count > 0 && flag == false)
				{
					XPathNavigator nodeName = nodeDirectoryExistField.Current.SelectSingleNode("Name");
					XPathNavigator nodeType = nodeDirectoryExistField.Current.SelectSingleNode("Type");
					ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
					flag = true;
				}
				while (nodeDirectoryExistField.MoveNext())
				{
					XPathNavigator nodeFieldName = nodeDirectoryExistField.Current.SelectSingleNode("../Name");
					XPathNavigator nodeDataType = nodeDirectoryExistField.Current.SelectSingleNode("DataType");
					XPathNavigator nodeDataTypeCreate = nodeDirectoryExistField.Current.SelectSingleNode("DataTypeCreate");

					ApendLine("\t Поле: ", nodeFieldName.Value, " -> змінений тип даних (Тип в базі: " + nodeDataType.Value + " -> Новий тип: " + nodeDataTypeCreate.Value + "). Можлива втрата даних, або колонка буде скопійована!");
				}

				XPathNodeIterator nodeDirectoryNewTabularParts = nodeDirectoryExist.Current.Select("Control_TabularParts[IsExist = 'no']");
				if (nodeDirectoryNewTabularParts.Count > 0)
				{
					if (flag == false)
					{
						XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
						XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
						ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
						flag = true;
					}
				}
				while (nodeDirectoryNewTabularParts.MoveNext())
				{
					XPathNavigator nodeTabularPartsName = nodeDirectoryNewTabularParts.Current.SelectSingleNode("Name");
					ApendLine("\t Нова таблична частина : ", nodeTabularPartsName.Value);

					InfoTableCreateFieldCreate(nodeDirectoryNewTabularParts.Current, "\t\t");
				}

				XPathNodeIterator nodeDirectoryTabularParts = nodeDirectoryExist.Current.Select("Control_TabularParts[IsExist = 'yes']");
				while (nodeDirectoryTabularParts.MoveNext())
				{
					bool flagTP = false;

					XPathNodeIterator nodeDirectoryTabularPartsNewField = nodeDirectoryTabularParts.Current.Select("Control_Field[IsExist = 'no']");
					if (nodeDirectoryTabularPartsNewField.Count > 0)
					{
						if (!flag)
						{
							XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
							XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
							ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
							flag = true;
						}

						if (!flagTP)
						{
							XPathNavigator nodeTabularPartsName = nodeDirectoryTabularParts.Current.SelectSingleNode("Name");
							ApendLine("\t Таблична частина : ", nodeTabularPartsName.Value);
							flagTP = true;
						}
					}
					while (nodeDirectoryTabularPartsNewField.MoveNext())
					{
						XPathNavigator nodeFieldName = nodeDirectoryTabularPartsNewField.Current.SelectSingleNode("Name");
						XPathNavigator nodeConfType = nodeDirectoryTabularPartsNewField.Current.SelectSingleNode("FieldCreate/ConfType");

						ApendLine("\t\t Нове Поле: ", nodeFieldName.Value, "(Тип: " + nodeConfType.Value + ")");
					}

					XPathNodeIterator nodeDirectoryTabularPartsField = nodeDirectoryTabularParts.Current.Select("Control_Field[IsExist = 'yes']/Type[Coincide = 'no']");
					if (nodeDirectoryTabularPartsField.Count > 0)
					{
						if (flag == false)
						{
							XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
							XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
							ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
							flag = true;
						}

						if (!flagTP)
						{
							XPathNavigator nodeTabularPartsName = nodeDirectoryTabularParts.Current.SelectSingleNode("Name");
							ApendLine("\t Таблична частина : ", nodeTabularPartsName.Value);
							flagTP = true;
						}
					}
					while (nodeDirectoryTabularPartsField.MoveNext())
					{
						XPathNavigator nodeFieldName = nodeDirectoryTabularPartsField.Current.SelectSingleNode("../Name");
						XPathNavigator nodeDataType = nodeDirectoryTabularPartsField.Current.SelectSingleNode("DataType");
						XPathNavigator nodeDataTypeCreate = nodeDirectoryTabularPartsField.Current.SelectSingleNode("DataTypeCreate");

						ApendLine("\t\t Поле: ", nodeFieldName.Value, " -> змінений тип даних (Тип в базі: " + nodeDataType.Value + " -> Новий тип: " + nodeDataTypeCreate.Value + "). Можлива втрата даних, або колонка буде скопійована!");
					}
				}
			}
		}

		void SaveAnalizeAndCreateSQL()
		{
			ApendLine("\n\n[ АНАЛІЗ ]", "", "\n");

			string replacementColumn = (checkBoxReplacement.Checked ? "yes" : "no");

			ApendLine("1. Створення копії файлу конфігурації", "");
			Conf.PathToCopyXmlFileConfiguration = Configuration.CreateCopyConfigurationFile(Conf.PathToXmlFileConfiguration, Conf.PathToCopyXmlFileConfiguration);
			ApendLine(" --> " + Conf.PathToCopyXmlFileConfiguration, "\n");

			Conf.PathToTempXmlFileConfiguration = Configuration.GetTempPathToConfigurationFile(Conf.PathToXmlFileConfiguration, Conf.PathToTempXmlFileConfiguration);

			ApendLine("2. Збереження конфігурації у тимчасовий файл", "");
			Configuration.Save(Conf.PathToTempXmlFileConfiguration, Conf);
			ApendLine(" --> " + Conf.PathToTempXmlFileConfiguration, "\n");

			ApendLine("2. Отримання структури бази даних", "");
			ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();
			Configuration.SaveInformationSchema(informationSchema, @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml");

			ApendLine("3. Порівняння конфігурації та бази даних", "");
			Configuration.Comparison(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml",
				Conf.PathToTempXmlFileConfiguration,
				Conf.PathToCopyXmlFileConfiguration);

			ApendLine("4. Створення команд SQL", "", "\n");
			Configuration.ComparisonAnalizeGeneration(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReportAnalize.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml", replacementColumn);

			XPathDocument xPathDoc = new XPathDocument(@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator nodeInfo = xPathDocNavigator.Select("/root/info");
			if (nodeInfo.Count == 0)
			{
				ApendLine("Інформація відсутня!", "", "\n");
			}
			else
				while (nodeInfo.MoveNext())
				{
					ApendLine(nodeInfo.Current.Value, "");
				}

			ApendLine("\n[ Команди SQL ]", "", "\n");

			XPathNodeIterator nodeSQL = xPathDocNavigator.Select("/root/sql");
			if (nodeSQL.Count == 0)
			{
				ApendLine("Команди відсутні!", "");
			}
			else
				while (nodeSQL.MoveNext())
				{
					ApendLine(nodeSQL.Current.Value, "");
				}

			buttonSave.Invoke(new Action(() => buttonSave.Enabled = true));
		}

		void ExecuteSQLAndGenerateCode()
		{
			buttonSave.Invoke(new Action(() => buttonSave.Enabled = false));

			//Read SQL
			List<string> SqlList = Configuration.ListComparisonSql(@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

			ApendLine("\n[ Виконання SQL ]", "", "\n");

			if (SqlList.Count == 0)
			{
				ApendLine("Команди відсутні!", "");
			}
			else
				//Execute
				foreach (string sqlText in SqlList)
				{
					int resultSQL = Program.Kernel.DataBase.ExecuteSQL(sqlText);
					ApendLine(" -> " + sqlText + " [" + resultSQL.ToString() + "]", "");
				}

			ApendLine("\n[ Генерування коду ]", "", "\n");
			Configuration.GenerationCode(Conf.PathToXmlFileConfiguration,
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.cs");

			ApendLine("Видалення тимчасових файлів", "", "\n");
			Configuration.RewriteConfigurationFileFromTempFile(
				Conf.PathToXmlFileConfiguration, 
				Conf.PathToTempXmlFileConfiguration,
				Conf.PathToCopyXmlFileConfiguration);

			ApendLine("ГОТОВО!", "", "\n\n\n");
		}

		private void InfoTableCreateFieldCreate(XPathNavigator xPathNavigator, string tab)
		{
			XPathNodeIterator nodeField = xPathNavigator.Select("TableCreate/FieldCreate");
			while (nodeField.MoveNext())
			{
				XPathNavigator nodeName = nodeField.Current.SelectSingleNode("Name");
				XPathNavigator nodeConfType = nodeField.Current.SelectSingleNode("ConfType");

				ApendLine(tab + "Поле: ", nodeName.Value, "(Тип: " + nodeConfType.Value + ")");
			}
		}

		private void SaveConfigurationForm_Load(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(SaveAndAnalize));
			thread.Start();

			buttonSave.Enabled = false;
		}

		private void buttonAnalize_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(SaveAnalizeAndCreateSQL));
			thread.Start();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(ExecuteSQLAndGenerateCode));
			thread.Start();

			buttonSave.Enabled = false;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void SaveConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Configuration.ClearCopyAndTempConfigurationFile(
				Conf.PathToXmlFileConfiguration,
				Conf.PathToCopyXmlFileConfiguration,
				Conf.PathToTempXmlFileConfiguration);
		}
	}
}