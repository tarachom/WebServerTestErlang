﻿/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.    

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/

using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Константи = ConfTrade_v1_1.Константи;
using Довідники = ConfTrade_v1_1.Довідники;
using Документи = ConfTrade_v1_1.Документи;
using Перелічення = ConfTrade_v1_1.Перелічення;
using РегістриВідомостей = ConfTrade_v1_1.РегістриВідомостей;
using РегістриНакопичення = ConfTrade_v1_1.РегістриНакопичення;

namespace ConfTrade
{
	public partial class ConfTrade
	{



		static void Test2()
		{

			//Документи.Договір2_Товари_TablePart.Record record1 = new Документи.Договір2_Товари_TablePart.Record();

			//РегістриВідомостей.Валюти_RecordsSet валюти_RecordsSet = new РегістриВідомостей.Валюти_RecordsSet();
			//foreach (РегістриВідомостей.Валюти_RecordsSet.Record record in валюти_RecordsSet.Records) 
			//{

			//}

			//Довідники.Номенклатура_Select номенклатура_Select = new Довідники.Номенклатура_Select();
			//Довідники.Номенклатура_Pointer номенклатура_Pointer = номенклатура_Select.FindByField("Назва", 0);

			//РегістриВідомостей.First_RecordsSet first_RecordsSet = new РегістриВідомостей.First_RecordsSet();

			//РегістриВідомостей.First_Record first_Record1 = new РегістриВідомостей.First_Record();
			//first_Record1.field0 = new Довідники.КлассификаторЕдИзм_Select().FindByField("Код", "1");

			//first_RecordsSet.Records.Add(first_Record1);

			//first_RecordsSet.Read();
			//foreach (РегістриВідомостей.First_Record first_Record in first_RecordsSet.Records)
			//{
			//	first_Record.field1 = "";
			//}

			//first_RecordsSet.Save();
		}

		static string Run4()
		{
			Console.WriteLine(Conf.Config.Kernel.DataBase.Test());

			
			//Довідники.Номенклатура_Objest Номенклатура = new Довідники.Номенклатура_Objest();
			//Номенклатура.New();
			//Номенклатура.Назва = "Тест";
			//Номенклатура.Код = "1";
			//Номенклатура.Save();

			//Довідники.Номенклатура_Select НоменклатураВибірка = new Довідники.Номенклатура_Select();
			//НоменклатураВибірка.QuerySelect.Limit = 5;
			//НоменклатураВибірка.Select();

			//while (НоменклатураВибірка.MoveNext())
			//{
			//	//Об'єкт із вказівника
			//	Довідники.Номенклатура_Objest Номенклатура = НоменклатураВибірка.Current.GetDirectoryObject();

			//	//Модифікація назви
			//	Номенклатура.Назва = Номенклатура.Назва + " *";
			//	Номенклатура.Save();
			//}

			////Вказівник

			////Новий запис в довідник Валюти
			//Довідники.Валюти_Objest валюти_Objest = new Довідники.Валюти_Objest();
			//валюти_Objest.New();
			//валюти_Objest.Назва = "Гривня";
			//валюти_Objest.Код = "1";
			//валюти_Objest.Save();

			////Вибірка вказівника на запис. Відбір по полю Назва
			//Довідники.Валюти_Select валюти_Select = new Довідники.Валюти_Select();
			//валюти_Select.QuerySelect.Where.Add(new Where(валюти_Select.Alias["Назва"], Comparison.EQ, "Гривня"));

			//Довідники.Валюти_Pointer валюти_Pointer = 
			//	(валюти_Select.SelectSingle() ? валюти_Select.Current : new Довідники.Валюти_Pointer());

			//Довідники.Номенклатура_Objest Номенклатура = new Довідники.Номенклатура_Objest();
			//Номенклатура.New();
			//Номенклатура.Назва = "Товар А";
			//Номенклатура.Валюта = валюти_Pointer;
			//Номенклатура.Save();

			/*
			Довідники.Валюти_Objest валюти_Objest = new Довідники.Валюти_Objest();
			валюти_Objest.New();
			валюти_Objest.Назва = "Евро";
			валюти_Objest.Код = "0003";
			валюти_Objest.Save();

			Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();
			номенклатура_Objest.New();
			номенклатура_Objest.Назва = "Товар 3";
			номенклатура_Objest.ПолнНаименование = "Товар 3 повна назва";
			номенклатура_Objest.Код = "2";
			номенклатура_Objest.ВалютаУчета = валюти_Objest.GetDirectoryPointer();
			номенклатура_Objest.Вес = 10.45m;
			номенклатура_Objest.Save();

	        */

			return "";
		}

		static string Run2()
		{
			//1. Знайти родителя по ключу
			//2. Рекурсивно вибрати всіх предків
			//3. Вибрати потомків
			//4. Вибрати Номенклатуру поточного родітеля

			//Довідники.Групи_Номенклатура_Список_View групиНоменклатура_Список_View = new Довідники.Групи_Номенклатура_Список_View();
			//групиНоменклатура_Список_View.QuerySelect.Where.Add(
			//	new Where(групиНоменклатура_Список_View.Alias["Родитель"], Comparison.EQ, new EmptyPointer()));

			//Довідники.Номенклатура_Список_View номенклатура_Список_View = new Довідники.Номенклатура_Список_View();

			//Довідники.Групи_МестаХранения_Список_View групи_МестаХранения_Список_View = new Довідники.Групи_МестаХранения_Список_View();
			//групи_МестаХранения_Список_View.Read();


			return "";
		}

		static string Run3()
		{
			//Довідники.Валюти_Список_View валюти_Список_View = new Довідники.Валюти_Список_View();
			//валюти_Список_View.QuerySelect.CreateTempTable = true;
			//валюти_Список_View.Read();

			//Довідники.КлассификаторЕдИзм_Список_View классификаторЕдИзм_Список_View = new Довідники.КлассификаторЕдИзм_Список_View();
			//классификаторЕдИзм_Список_View.QuerySelect.Where.Add(
			//	new Where("owner", Comparison.EQ, "(SELECT uid FROM " + валюти_Список_View.QuerySelect.TempTable + ")", true));

			//классификаторЕдИзм_Список_View.Read();

			return "";
		}
	}
}