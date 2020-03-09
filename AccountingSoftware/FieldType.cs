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
	public class FieldType
	{
		public FieldType(string confTypeName, string viewTypeName)
		{
			ConfTypeName = confTypeName;
			ViewTypeName = viewTypeName;
		}

		public string ConfTypeName { get; set; }

		public string ViewTypeName { get; set; }

		public override string ToString()
		{
			return ViewTypeName;
		}

		public static List<FieldType> DefaultList()
		{
			List<FieldType> fieldTypes = new List<FieldType>();

			fieldTypes.Add(new FieldType("string",        "[ string ] - Текст"));
			fieldTypes.Add(new FieldType("integer",       "[ integer ] - Ціле число"));
			fieldTypes.Add(new FieldType("numeric",       "[ numeric ] - Число з комою"));
			fieldTypes.Add(new FieldType("boolean",       "[ boolean ] - Логічне значення"));
			fieldTypes.Add(new FieldType("date",          "[ date ] - Дата"));
			fieldTypes.Add(new FieldType("datetime",      "[ datetime ] - Дата та час"));
			fieldTypes.Add(new FieldType("time",          "[ time ] - Час"));
			fieldTypes.Add(new FieldType("enum",          "[ enum ] - Перелічення"));
			fieldTypes.Add(new FieldType("pointer",       "[ pointer ] - Вказівник на елемент конфігурації"));
			fieldTypes.Add(new FieldType("any_pointer",   "[ any_pointer ] - Вказівник на різні елементи конфігурації"));
			fieldTypes.Add(new FieldType("empty_pointer", "[ empty_pointer ] - Пустий вказівник")); 
			fieldTypes.Add(new FieldType("uuid[]",        "[ uuid1, uuid2, uuid3 ... ] - Масив вказівників на елемент конфігурації"));
			fieldTypes.Add(new FieldType("string[]",      "[ Текст1, Текст2, Текст3 ... ] - Масив текстових даних"));
			fieldTypes.Add(new FieldType("integer[]",     "[ Число1, Число2, Число3 ... ] - Масив цілих чисел"));
			fieldTypes.Add(new FieldType("numeric[]",     "[ Число1.0, Число2.0, Число3.0 ...  ] - Масив чисел з комою"));

			return fieldTypes;
		}
	}
}