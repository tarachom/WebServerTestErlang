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
Сайт:     find.org.ua
*/

using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник Менеджер
	/// </summary>
	public abstract class DirectoryManager
	{
		public DirectoryManager(Kernel kernel, string table, string[] fieldsNameInTableArray, string[] fieldsNameArray)
		{
			Table = table;
			Kernel = kernel;

			Alias = new Dictionary<string, string>();
			for (int i = 0; i < fieldsNameInTableArray.Length; i++)
			{
				Alias.Add(fieldsNameArray[i], fieldsNameInTableArray[i]);
			}
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		protected Dictionary<string, string> Alias { get; }

		protected DirectoryPointer BaseFindByField(string fieldName, object fieldValue)
		{
			DirectoryPointer directoryPointer = new DirectoryPointer(Kernel, Table);

			Query QuerySelect = new Query(Table);
			QuerySelect.Where.Add(new Where(fieldName, Comparison.EQ, fieldValue));

			bool isFind = Kernel.DataBase.FindDirectoryPointer(QuerySelect, ref directoryPointer);

			return directoryPointer;
		}
	}
}
