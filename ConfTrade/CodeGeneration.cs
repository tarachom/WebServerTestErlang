﻿
/*
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
  
/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 17.03.2020 20:50:21
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;

namespace ConfTrade_v1_1
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
        
        public static bool StartInit { get; set; }
        
        public static void InitAllConstants()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Kernel.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a1", "const_2", "const_3", "const_4", "const_5", "const_6", "const_7", "const_8", "const_9", "const_10", "const_11", "const_12", "const_13", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }, fieldValue);
            
            if (IsSelect)
            {
                StartInit = true;
                Константи.Основні.Контрагент = new Довідники.Контрагенти_Pointer(fieldValue["col_a1"]);
                Константи.Основні.ОсновнийСклад = new Довідники.МестаХранения_Pointer(fieldValue["const_2"]);
                Константи.Основні.Перелічення = (fieldValue["const_3"] != DBNull.Value) ? (Перелічення.ВидиКонтрагентов)fieldValue["const_3"] : 0;
                Константи.Основні.Склад = fieldValue["const_4"].ToString();
                Константи.Додаткові.A = new EmptyPointer();
                Константи.Додаткові.B = fieldValue["const_6"].ToString();
                Константи.ПоштовіНастройки.іваіваddd = new EmptyPointer();
                Константи.ПоштовіНастройки.ваіва = fieldValue["const_8"].ToString();
                Константи.ПоштовіНастройки.A = (fieldValue["const_9"] != DBNull.Value) ? (Перелічення.Перелічення2)fieldValue["const_9"] : 0;
                Константи.ПоштовіНастройки.Ф2 = new Довідники.МестаХранения_Pointer(fieldValue["const_10"]);
                Константи.ПоштовіНастройки.Ntcn1 = new EmptyPointer();
                Константи.ПоштовіНастройки.Ntcn2 = new EmptyPointer();
                Константи.РегламентніЗавдання.Стан = fieldValue["const_13"].ToString();
                Константи.Робот.Старт = (fieldValue["col_a2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a2"].ToString()) : DateTime.MinValue;
                Константи.Робот.Стоп = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                Константи.Робот.Стан = fieldValue["col_a4"].ToString();
                Константи.Робот.Коментар = fieldValue["col_a5"].ToString();
                Константи.Робот.Число = (fieldValue["col_a6"] != DBNull.Value) ? (int)fieldValue["col_a6"] : 0;
                
                StartInit = false;
            }
        }
    }
}

namespace ConfTrade_v1_1.Константи
{
    
    static class Основні
    {
        private static Довідники.Контрагенти_Pointer _Контрагент;
        public static Довідники.Контрагенти_Pointer Контрагент
        {
            get { return _Контрагент; }
            set
            {
                _Контрагент = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a1", _Контрагент.ToString());
            }
        }
        private static Довідники.МестаХранения_Pointer _ОсновнийСклад;
        public static Довідники.МестаХранения_Pointer ОсновнийСклад
        {
            get { return _ОсновнийСклад; }
            set
            {
                _ОсновнийСклад = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_2", _ОсновнийСклад.ToString());
            }
        }
        private static Перелічення.ВидиКонтрагентов _Перелічення;
        public static Перелічення.ВидиКонтрагентов Перелічення
        {
            get { return _Перелічення; }
            set
            {
                _Перелічення = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_3", (int)_Перелічення);
            }
        }
        private static string _Склад;
        public static string Склад
        {
            get { return _Склад; }
            set
            {
                _Склад = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_4", _Склад);
            }
        }
        
        public class Контрагент_Історія_TablePart : ConstantsTablePart
        {
            public Контрагент_Історія_TablePart() : base(Config.Kernel, "tab_a60",
                 new string[] { "col_a1", "col_a2" }) 
            {
                Records = new List<Історія_Record>();
            }
                
            public List<Історія_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Історія_Record record = new Історія_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                    record.Значення = fieldValue["col_a2"].ToString();
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                if (Records.Count > 0)
                {
                    base.BaseBeginTransaction();
                
                    if (clear_all_before_save)
                        base.BaseDelete();

                    foreach (Історія_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a1", record.Дата);
                        fieldValue.Add("col_a2", record.Значення);
                        
                        base.BaseSave(record.UID, fieldValue);
                    }
                
                    base.BaseCommitTransaction();
                }
            }
        
            public void Delete()
            {
                base.BaseBeginTransaction();
                base.BaseCommitTransaction();
            }
            
            public class Історія_Record : ConstantsTablePartRecord
            {
                public Історія_Record()
                {
                    Дата = DateTime.MinValue;
                    Значення = "";
                    
                }
        
                
                public Історія_Record(
                    DateTime?  _Дата = null, string _Значення = "")
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Значення = _Значення;
                    
                }
                public DateTime Дата { get; set; }
                public string Значення { get; set; }
                
            }            
        }
          
        public class ОсновнийСклад_Історія_TablePart : ConstantsTablePart
        {
            public ОсновнийСклад_Історія_TablePart() : base(Config.Kernel, "tab_a62",
                 new string[] { "col_a4", "col_a5" }) 
            {
                Records = new List<Історія_Record>();
            }
                
            public List<Історія_Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Історія_Record record = new Історія_Record();
                    
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.Дата = (fieldValue["col_a4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a4"].ToString()) : DateTime.MinValue;
                    record.Значенн = fieldValue["col_a5"].ToString();
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                if (Records.Count > 0)
                {
                    base.BaseBeginTransaction();
                
                    if (clear_all_before_save)
                        base.BaseDelete();

                    foreach (Історія_Record record in Records)
                    {
                        Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                        fieldValue.Add("col_a4", record.Дата);
                        fieldValue.Add("col_a5", record.Значенн);
                        
                        base.BaseSave(record.UID, fieldValue);
                    }
                
                    base.BaseCommitTransaction();
                }
            }
        
            public void Delete()
            {
                base.BaseBeginTransaction();
                base.BaseCommitTransaction();
            }
            
            public class Історія_Record : ConstantsTablePartRecord
            {
                public Історія_Record()
                {
                    Дата = DateTime.MinValue;
                    Значенн = "";
                    
                }
        
                
                public Історія_Record(
                    DateTime?  _Дата = null, string _Значенн = "")
                {
                    Дата = _Дата ?? DateTime.MinValue;
                    Значенн = _Значенн;
                    
                }
                public DateTime Дата { get; set; }
                public string Значенн { get; set; }
                
            }            
        }
               
    }
    
    static class Додаткові
    {
        private static EmptyPointer _A;
        public static EmptyPointer A
        {
            get { return _A; }
            set
            {
                _A = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_5", _A.ToString());
            }
        }
        private static string _B;
        public static string B
        {
            get { return _B; }
            set
            {
                _B = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_6", _B);
            }
        }
             
    }
    
    static class ПоштовіНастройки
    {
        private static EmptyPointer _іваіваddd;
        public static EmptyPointer іваіваddd
        {
            get { return _іваіваddd; }
            set
            {
                _іваіваddd = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_7", _іваіваddd.ToString());
            }
        }
        private static string _ваіва;
        public static string ваіва
        {
            get { return _ваіва; }
            set
            {
                _ваіва = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_8", _ваіва);
            }
        }
        private static Перелічення.Перелічення2 _A;
        public static Перелічення.Перелічення2 A
        {
            get { return _A; }
            set
            {
                _A = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_9", (int)_A);
            }
        }
        private static Довідники.МестаХранения_Pointer _Ф2;
        public static Довідники.МестаХранения_Pointer Ф2
        {
            get { return _Ф2; }
            set
            {
                _Ф2 = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_10", _Ф2.ToString());
            }
        }
        private static EmptyPointer _Ntcn1;
        public static EmptyPointer Ntcn1
        {
            get { return _Ntcn1; }
            set
            {
                _Ntcn1 = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_11", _Ntcn1.ToString());
            }
        }
        private static EmptyPointer _Ntcn2;
        public static EmptyPointer Ntcn2
        {
            get { return _Ntcn2; }
            set
            {
                _Ntcn2 = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_12", _Ntcn2.ToString());
            }
        }
             
    }
    
    static class РегламентніЗавдання
    {
        private static string _Стан;
        public static string Стан
        {
            get { return _Стан; }
            set
            {
                _Стан = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "const_13", _Стан);
            }
        }
             
    }
    
    static class Робот
    {
        private static DateTime _Старт;
        public static DateTime Старт
        {
            get { return _Старт; }
            set
            {
                _Старт = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a2", _Старт);
            }
        }
        private static DateTime _Стоп;
        public static DateTime Стоп
        {
            get { return _Стоп; }
            set
            {
                _Стоп = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a3", _Стоп);
            }
        }
        private static string _Стан;
        public static string Стан
        {
            get { return _Стан; }
            set
            {
                _Стан = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a4", _Стан);
            }
        }
        private static string _Коментар;
        public static string Коментар
        {
            get { return _Коментар; }
            set
            {
                _Коментар = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a5", _Коментар);
            }
        }
        private static int _Число;
        public static int Число
        {
            get { return _Число; }
            set
            {
                _Число = value;
                if (!Config.StartInit)
                    Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a6", _Число);
            }
        }
             
    }
    
}

namespace ConfTrade_v1_1.Довідники
{
    
    #region DIRECTORY "Валюти"
    
    class Валюти_Objest : DirectoryObject
    {
        public Валюти_Objest() : base(Config.Kernel, "tab_a02",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Курс = 0;
            Кратность = 0;
            Кратко = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            КурсНаДату_TablePart = new Валюти_КурсНаДату_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Курс = (base.FieldValue["col_a1"] != DBNull.Value) ? (decimal)base.FieldValue["col_a1"] : 0;
                Кратность = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Кратко = base.FieldValue["col_a3"].ToString();
                Назва = base.FieldValue["col_a4"].ToString();
                Код = base.FieldValue["col_a5"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Курс;
            base.FieldValue["col_a2"] = Кратность;
            base.FieldValue["col_a3"] = Кратко;
            base.FieldValue["col_a4"] = Назва;
            base.FieldValue["col_a5"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Валюти_Pointer GetDirectoryPointer()
        {
            Валюти_Pointer directoryPointer = new Валюти_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public decimal Курс { get; set; }
        public int Кратность { get; set; }
        public string Кратко { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Валюти_КурсНаДату_TablePart КурсНаДату_TablePart { get; set; }
        
    }
    
    
    class Валюти_Pointer : DirectoryPointer
    {
        public Валюти_Pointer(object uid = null) : base(Config.Kernel, "tab_a02")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Валюти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a02")
        {
            base.Init(uid, fields);
        }
        
        public Валюти_Objest GetDirectoryObject()
        {
            Валюти_Objest ВалютиObjestItem = new Валюти_Objest();
            ВалютиObjestItem.Read(base.UnigueID);
            return ВалютиObjestItem;
        }
    }
    
    
    class Валюти_Select : DirectorySelect, IDisposable
    {
        public Валюти_Select() : base(Config.Kernel, "tab_a02",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" },
            new string[] { "Курс", "Кратность", "Кратко", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Валюти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Валюти_Pointer Current { get; private set; }
        
        public Валюти_Pointer FindByField(string name, object value)
        {
            Валюти_Pointer itemPointer = new Валюти_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Валюти_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Валюти_Pointer> directoryPointerList = new List<Валюти_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Валюти_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Валюти_КурсНаДату_TablePart : DirectoryTablePart
    {
        public Валюти_КурсНаДату_TablePart(Валюти_Objest owner) : base(Config.Kernel, "tab_a03",
             new string[] { "col_a4", "col_a5", "col_a6", "col_a1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Валюти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a4"].ToString()) : DateTime.MinValue;
                record.Курс = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0;
                record.Кратность = (fieldValue["col_a6"] != DBNull.Value) ? (int)fieldValue["col_a6"] : 0;
                record.НовеПоле = new Довідники.Пользователи_Pointer(fieldValue["col_a1"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a4", record.Дата);
                    fieldValue.Add("col_a5", record.Курс);
                    fieldValue.Add("col_a6", record.Кратность);
                    fieldValue.Add("col_a1", record.НовеПоле.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Курс = 0;
                Кратность = 0;
                НовеПоле = new Довідники.Пользователи_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, decimal _Курс = 0, int _Кратность = 0, Довідники.Пользователи_Pointer _НовеПоле = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Курс = _Курс;
                Кратность = _Кратность;
                НовеПоле = _НовеПоле ?? new Довідники.Пользователи_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public decimal Курс { get; set; }
            public int Кратность { get; set; }
            public Довідники.Пользователи_Pointer НовеПоле { get; set; }
            
        }
    }
      
    class Валюти_Список_View : DirectoryView
    {
        public Валюти_Список_View() : base(Config.Kernel, "tab_a02", 
             new string[] { "col_a4", "col_a1" },
             new string[] { "Назва", "Курс" },
             new string[] { "string", "numeric" },
             "Довідники.Валюти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Единици"
    ///<summary>
    ///Единицы измерения товара.
    ///</summary>
    class Единици_Objest : DirectoryObject
    {
        public Единици_Objest() : base(Config.Kernel, "tab_a04",
             new string[] { "col_a7", "col_a8", "col_a9", "col_b1", "col_a1", "col_a2" }) 
        {
            Вес = 0;
            Коеффициент = 0;
            Единица = new Довідники.КлассификаторЕдИзм_Pointer();
            ШтрихКод = 0;
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Вес = (base.FieldValue["col_a7"] != DBNull.Value) ? (decimal)base.FieldValue["col_a7"] : 0;
                Коеффициент = (base.FieldValue["col_a8"] != DBNull.Value) ? (decimal)base.FieldValue["col_a8"] : 0;
                Единица = new Довідники.КлассификаторЕдИзм_Pointer(base.FieldValue["col_a9"]);
                ШтрихКод = (base.FieldValue["col_b1"] != DBNull.Value) ? (int)base.FieldValue["col_b1"] : 0;
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a7"] = Вес;
            base.FieldValue["col_a8"] = Коеффициент;
            base.FieldValue["col_a9"] = Единица.ToString();
            base.FieldValue["col_b1"] = ШтрихКод;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Единици_Pointer GetDirectoryPointer()
        {
            Единици_Pointer directoryPointer = new Единици_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public decimal Вес { get; set; }
        public decimal Коеффициент { get; set; }
        public Довідники.КлассификаторЕдИзм_Pointer Единица { get; set; }
        public int ШтрихКод { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Единицы измерения товара.
    ///</summary>
    class Единици_Pointer : DirectoryPointer
    {
        public Единици_Pointer(object uid = null) : base(Config.Kernel, "tab_a04")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Единици_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a04")
        {
            base.Init(uid, fields);
        }
        
        public Единици_Objest GetDirectoryObject()
        {
            Единици_Objest ЕдинициObjestItem = new Единици_Objest();
            ЕдинициObjestItem.Read(base.UnigueID);
            return ЕдинициObjestItem;
        }
    }
    
    ///<summary>
    ///Единицы измерения товара.
    ///</summary>
    class Единици_Select : DirectorySelect, IDisposable
    {
        public Единици_Select() : base(Config.Kernel, "tab_a04",
            new string[] { "col_a7", "col_a8", "col_a9", "col_b1", "col_a1", "col_a2" },
            new string[] { "Вес", "Коеффициент", "Единица", "ШтрихКод", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Единици_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Единици_Pointer Current { get; private set; }
        
        public Единици_Pointer FindByField(string name, object value)
        {
            Единици_Pointer itemPointer = new Единици_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Единици_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Единици_Pointer> directoryPointerList = new List<Единици_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Единици_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Единици_Список_View : DirectoryView
    {
        public Единици_Список_View() : base(Config.Kernel, "tab_a04", 
             new string[] { "col_a9", "col_a1" },
             new string[] { "Единица", "Назва" },
             new string[] { "pointer", "string" },
             "Довідники.Единици_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КлассификаторЕдИзм"
    ///<summary>
    ///Классификатор единиц измерений.
    ///</summary>
    class КлассификаторЕдИзм_Objest : DirectoryObject
    {
        public КлассификаторЕдИзм_Objest() : base(Config.Kernel, "tab_a05",
             new string[] { "col_b2", "col_b3", "col_a1", "col_a2" }) 
        {
            ПолнНаименование = "";
            КодЕдИзмерения = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ПолнНаименование = base.FieldValue["col_b2"].ToString();
                КодЕдИзмерения = base.FieldValue["col_b3"].ToString();
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b2"] = ПолнНаименование;
            base.FieldValue["col_b3"] = КодЕдИзмерения;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КлассификаторЕдИзм_Pointer GetDirectoryPointer()
        {
            КлассификаторЕдИзм_Pointer directoryPointer = new КлассификаторЕдИзм_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ПолнНаименование { get; set; }
        public string КодЕдИзмерения { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Классификатор единиц измерений.
    ///</summary>
    class КлассификаторЕдИзм_Pointer : DirectoryPointer
    {
        public КлассификаторЕдИзм_Pointer(object uid = null) : base(Config.Kernel, "tab_a05")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КлассификаторЕдИзм_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a05")
        {
            base.Init(uid, fields);
        }
        
        public КлассификаторЕдИзм_Objest GetDirectoryObject()
        {
            КлассификаторЕдИзм_Objest КлассификаторЕдИзмObjestItem = new КлассификаторЕдИзм_Objest();
            КлассификаторЕдИзмObjestItem.Read(base.UnigueID);
            return КлассификаторЕдИзмObjestItem;
        }
    }
    
    ///<summary>
    ///Классификатор единиц измерений.
    ///</summary>
    class КлассификаторЕдИзм_Select : DirectorySelect, IDisposable
    {
        public КлассификаторЕдИзм_Select() : base(Config.Kernel, "tab_a05",
            new string[] { "col_b2", "col_b3", "col_a1", "col_a2" },
            new string[] { "ПолнНаименование", "КодЕдИзмерения", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new КлассификаторЕдИзм_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public КлассификаторЕдИзм_Pointer Current { get; private set; }
        
        public КлассификаторЕдИзм_Pointer FindByField(string name, object value)
        {
            КлассификаторЕдИзм_Pointer itemPointer = new КлассификаторЕдИзм_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<КлассификаторЕдИзм_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<КлассификаторЕдИзм_Pointer> directoryPointerList = new List<КлассификаторЕдИзм_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new КлассификаторЕдИзм_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class КлассификаторЕдИзм_Список_View : DirectoryView
    {
        public КлассификаторЕдИзм_Список_View() : base(Config.Kernel, "tab_a05", 
             new string[] { "col_a1", "col_b3" },
             new string[] { "Назва", "КодЕдИзмерения" },
             new string[] { "string", "string" },
             "Довідники.КлассификаторЕдИзм_Список")
        {
            
        }
        
    }
      
    class КлассификаторЕдИзм_Список2_View : DirectoryView
    {
        public КлассификаторЕдИзм_Список2_View() : base(Config.Kernel, "tab_a05", 
             new string[] { "col_b2", "col_b3", "col_a1", "col_a2" },
             new string[] { "ПолнНаименование", "КодЕдИзмерения", "Назва", "Код" },
             new string[] { "string", "string", "string", "string" },
             "Довідники.КлассификаторЕдИзм_Список2")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Категории"
    ///<summary>
    ///Категории товаров и контрагентов.
    ///</summary>
    class Категории_Objest : DirectoryObject
    {
        public Категории_Objest() : base(Config.Kernel, "tab_a06",
             new string[] { "col_b4", "col_a1", "col_a2" }) 
        {
            Комментарий = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Комментарий = base.FieldValue["col_b4"].ToString();
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b4"] = Комментарий;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Категории_Pointer GetDirectoryPointer()
        {
            Категории_Pointer directoryPointer = new Категории_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Категории товаров и контрагентов.
    ///</summary>
    class Категории_Pointer : DirectoryPointer
    {
        public Категории_Pointer(object uid = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Категории_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a06")
        {
            base.Init(uid, fields);
        }
        
        public Категории_Objest GetDirectoryObject()
        {
            Категории_Objest КатегорииObjestItem = new Категории_Objest();
            КатегорииObjestItem.Read(base.UnigueID);
            return КатегорииObjestItem;
        }
    }
    
    ///<summary>
    ///Категории товаров и контрагентов.
    ///</summary>
    class Категории_Select : DirectorySelect, IDisposable
    {
        public Категории_Select() : base(Config.Kernel, "tab_a06",
            new string[] { "col_b4", "col_a1", "col_a2" },
            new string[] { "Комментарий", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Категории_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Категории_Pointer Current { get; private set; }
        
        public Категории_Pointer FindByField(string name, object value)
        {
            Категории_Pointer itemPointer = new Категории_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Категории_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Категории_Pointer> directoryPointerList = new List<Категории_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Категории_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Категории_Список_View : DirectoryView
    {
        public Категории_Список_View() : base(Config.Kernel, "tab_a06", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.Категории_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КатегорииКонтрагентов"
    ///<summary>
    ///Категории контрагентов.
    ///</summary>
    class КатегорииКонтрагентов_Objest : DirectoryObject
    {
        public КатегорииКонтрагентов_Objest() : base(Config.Kernel, "tab_a07",
             new string[] { "col_b5", "col_a1", "col_a2" }) 
        {
            Категория = new Довідники.Категории_Pointer();
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Категория = new Довідники.Категории_Pointer(base.FieldValue["col_b5"]);
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b5"] = Категория.ToString();
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КатегорииКонтрагентов_Pointer GetDirectoryPointer()
        {
            КатегорииКонтрагентов_Pointer directoryPointer = new КатегорииКонтрагентов_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Категории_Pointer Категория { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Категории контрагентов.
    ///</summary>
    class КатегорииКонтрагентов_Pointer : DirectoryPointer
    {
        public КатегорииКонтрагентов_Pointer(object uid = null) : base(Config.Kernel, "tab_a07")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КатегорииКонтрагентов_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a07")
        {
            base.Init(uid, fields);
        }
        
        public КатегорииКонтрагентов_Objest GetDirectoryObject()
        {
            КатегорииКонтрагентов_Objest КатегорииКонтрагентовObjestItem = new КатегорииКонтрагентов_Objest();
            КатегорииКонтрагентовObjestItem.Read(base.UnigueID);
            return КатегорииКонтрагентовObjestItem;
        }
    }
    
    ///<summary>
    ///Категории контрагентов.
    ///</summary>
    class КатегорииКонтрагентов_Select : DirectorySelect, IDisposable
    {
        public КатегорииКонтрагентов_Select() : base(Config.Kernel, "tab_a07",
            new string[] { "col_b5", "col_a1", "col_a2" },
            new string[] { "Категория", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new КатегорииКонтрагентов_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public КатегорииКонтрагентов_Pointer Current { get; private set; }
        
        public КатегорииКонтрагентов_Pointer FindByField(string name, object value)
        {
            КатегорииКонтрагентов_Pointer itemPointer = new КатегорииКонтрагентов_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<КатегорииКонтрагентов_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<КатегорииКонтрагентов_Pointer> directoryPointerList = new List<КатегорииКонтрагентов_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new КатегорииКонтрагентов_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class КатегорииКонтрагентов_Список_View : DirectoryView
    {
        public КатегорииКонтрагентов_Список_View() : base(Config.Kernel, "tab_a07", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.КатегорииКонтрагентов_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КатегорииТоваров"
    ///<summary>
    ///КатегорииТоваров.
    ///</summary>
    class КатегорииТоваров_Objest : DirectoryObject
    {
        public КатегорииТоваров_Objest() : base(Config.Kernel, "tab_a08",
             new string[] { "col_b6", "col_a1", "col_a2" }) 
        {
            Категория = new Довідники.Категории_Pointer();
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Категория = new Довідники.Категории_Pointer(base.FieldValue["col_b6"]);
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b6"] = Категория.ToString();
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КатегорииТоваров_Pointer GetDirectoryPointer()
        {
            КатегорииТоваров_Pointer directoryPointer = new КатегорииТоваров_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Категории_Pointer Категория { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///КатегорииТоваров.
    ///</summary>
    class КатегорииТоваров_Pointer : DirectoryPointer
    {
        public КатегорииТоваров_Pointer(object uid = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КатегорииТоваров_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(uid, fields);
        }
        
        public КатегорииТоваров_Objest GetDirectoryObject()
        {
            КатегорииТоваров_Objest КатегорииТоваровObjestItem = new КатегорииТоваров_Objest();
            КатегорииТоваровObjestItem.Read(base.UnigueID);
            return КатегорииТоваровObjestItem;
        }
    }
    
    ///<summary>
    ///КатегорииТоваров.
    ///</summary>
    class КатегорииТоваров_Select : DirectorySelect, IDisposable
    {
        public КатегорииТоваров_Select() : base(Config.Kernel, "tab_a08",
            new string[] { "col_b6", "col_a1", "col_a2" },
            new string[] { "Категория", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new КатегорииТоваров_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public КатегорииТоваров_Pointer Current { get; private set; }
        
        public КатегорииТоваров_Pointer FindByField(string name, object value)
        {
            КатегорииТоваров_Pointer itemPointer = new КатегорииТоваров_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<КатегорииТоваров_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<КатегорииТоваров_Pointer> directoryPointerList = new List<КатегорииТоваров_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new КатегорииТоваров_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class КатегорииТоваров_Список_View : DirectoryView
    {
        public КатегорииТоваров_Список_View() : base(Config.Kernel, "tab_a08", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.КатегорииТоваров_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КатегорииЦен"
    ///<summary>
    ///Типы цен.
    ///</summary>
    class КатегорииЦен_Objest : DirectoryObject
    {
        public КатегорииЦен_Objest() : base(Config.Kernel, "tab_a09",
             new string[] { "col_b7", "col_b8", "col_a1", "col_a2" }) 
        {
            Комментарий = "";
            ТорговаяНаценка = 0;
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Комментарий = base.FieldValue["col_b7"].ToString();
                ТорговаяНаценка = (base.FieldValue["col_b8"] != DBNull.Value) ? (decimal)base.FieldValue["col_b8"] : 0;
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b7"] = Комментарий;
            base.FieldValue["col_b8"] = ТорговаяНаценка;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КатегорииЦен_Pointer GetDirectoryPointer()
        {
            КатегорииЦен_Pointer directoryPointer = new КатегорииЦен_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Комментарий { get; set; }
        public decimal ТорговаяНаценка { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Типы цен.
    ///</summary>
    class КатегорииЦен_Pointer : DirectoryPointer
    {
        public КатегорииЦен_Pointer(object uid = null) : base(Config.Kernel, "tab_a09")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КатегорииЦен_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a09")
        {
            base.Init(uid, fields);
        }
        
        public КатегорииЦен_Objest GetDirectoryObject()
        {
            КатегорииЦен_Objest КатегорииЦенObjestItem = new КатегорииЦен_Objest();
            КатегорииЦенObjestItem.Read(base.UnigueID);
            return КатегорииЦенObjestItem;
        }
    }
    
    ///<summary>
    ///Типы цен.
    ///</summary>
    class КатегорииЦен_Select : DirectorySelect, IDisposable
    {
        public КатегорииЦен_Select() : base(Config.Kernel, "tab_a09",
            new string[] { "col_b7", "col_b8", "col_a1", "col_a2" },
            new string[] { "Комментарий", "ТорговаяНаценка", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new КатегорииЦен_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public КатегорииЦен_Pointer Current { get; private set; }
        
        public КатегорииЦен_Pointer FindByField(string name, object value)
        {
            КатегорииЦен_Pointer itemPointer = new КатегорииЦен_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<КатегорииЦен_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<КатегорииЦен_Pointer> directoryPointerList = new List<КатегорииЦен_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new КатегорииЦен_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class КатегорииЦен_Список_View : DirectoryView
    {
        public КатегорииЦен_Список_View() : base(Config.Kernel, "tab_a09", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.КатегорииЦен_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "КодиУКТВЕД"
    ///<summary>
    ///Классификатор кодов для НН.
    ///</summary>
    class КодиУКТВЕД_Objest : DirectoryObject
    {
        public КодиУКТВЕД_Objest() : base(Config.Kernel, "tab_a10",
             new string[] { "col_b9", "col_c1", "col_a1", "col_a2" }) 
        {
            ПолноеНаименование = "";
            Вид = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ПолноеНаименование = base.FieldValue["col_b9"].ToString();
                Вид = base.FieldValue["col_c1"].ToString();
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b9"] = ПолноеНаименование;
            base.FieldValue["col_c1"] = Вид;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public КодиУКТВЕД_Pointer GetDirectoryPointer()
        {
            КодиУКТВЕД_Pointer directoryPointer = new КодиУКТВЕД_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ПолноеНаименование { get; set; }
        public string Вид { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Классификатор кодов для НН.
    ///</summary>
    class КодиУКТВЕД_Pointer : DirectoryPointer
    {
        public КодиУКТВЕД_Pointer(object uid = null) : base(Config.Kernel, "tab_a10")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public КодиУКТВЕД_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a10")
        {
            base.Init(uid, fields);
        }
        
        public КодиУКТВЕД_Objest GetDirectoryObject()
        {
            КодиУКТВЕД_Objest КодиУКТВЕДObjestItem = new КодиУКТВЕД_Objest();
            КодиУКТВЕДObjestItem.Read(base.UnigueID);
            return КодиУКТВЕДObjestItem;
        }
    }
    
    ///<summary>
    ///Классификатор кодов для НН.
    ///</summary>
    class КодиУКТВЕД_Select : DirectorySelect, IDisposable
    {
        public КодиУКТВЕД_Select() : base(Config.Kernel, "tab_a10",
            new string[] { "col_b9", "col_c1", "col_a1", "col_a2" },
            new string[] { "ПолноеНаименование", "Вид", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new КодиУКТВЕД_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public КодиУКТВЕД_Pointer Current { get; private set; }
        
        public КодиУКТВЕД_Pointer FindByField(string name, object value)
        {
            КодиУКТВЕД_Pointer itemPointer = new КодиУКТВЕД_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<КодиУКТВЕД_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<КодиУКТВЕД_Pointer> directoryPointerList = new List<КодиУКТВЕД_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new КодиУКТВЕД_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class КодиУКТВЕД_Список_View : DirectoryView
    {
        public КодиУКТВЕД_Список_View() : base(Config.Kernel, "tab_a10", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.КодиУКТВЕД_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Комплектация"
    ///<summary>
    ///Состав наборов.
    ///</summary>
    class Комплектация_Objest : DirectoryObject
    {
        public Комплектация_Objest() : base(Config.Kernel, "tab_a11",
             new string[] { "col_c2", "col_c3", "col_a1", "col_a2" }) 
        {
            Кво = 0;
            Товар = new EmptyPointer();
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Кво = (base.FieldValue["col_c2"] != DBNull.Value) ? (decimal)base.FieldValue["col_c2"] : 0;
                Товар = new EmptyPointer();
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c2"] = Кво;
            base.FieldValue["col_c3"] = Товар.ToString();
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Комплектация_Pointer GetDirectoryPointer()
        {
            Комплектация_Pointer directoryPointer = new Комплектация_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public decimal Кво { get; set; }
        public EmptyPointer Товар { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Состав наборов.
    ///</summary>
    class Комплектация_Pointer : DirectoryPointer
    {
        public Комплектация_Pointer(object uid = null) : base(Config.Kernel, "tab_a11")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Комплектация_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a11")
        {
            base.Init(uid, fields);
        }
        
        public Комплектация_Objest GetDirectoryObject()
        {
            Комплектация_Objest КомплектацияObjestItem = new Комплектация_Objest();
            КомплектацияObjestItem.Read(base.UnigueID);
            return КомплектацияObjestItem;
        }
    }
    
    ///<summary>
    ///Состав наборов.
    ///</summary>
    class Комплектация_Select : DirectorySelect, IDisposable
    {
        public Комплектация_Select() : base(Config.Kernel, "tab_a11",
            new string[] { "col_c2", "col_c3", "col_a1", "col_a2" },
            new string[] { "Кво", "Товар", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Комплектация_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Комплектация_Pointer Current { get; private set; }
        
        public Комплектация_Pointer FindByField(string name, object value)
        {
            Комплектация_Pointer itemPointer = new Комплектация_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Комплектация_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Комплектация_Pointer> directoryPointerList = new List<Комплектация_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Комплектация_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Комплектация_Список_View : DirectoryView
    {
        public Комплектация_Список_View() : base(Config.Kernel, "tab_a11", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.Комплектация_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Контрагенти"
    ///<summary>
    ///Поставщики и покупатели.
    ///</summary>
    class Контрагенти_Objest : DirectoryObject
    {
        public Контрагенти_Objest() : base(Config.Kernel, "tab_a12",
             new string[] { "col_c4", "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3", "col_c5", "col_c6", "col_c7", "col_c8", "col_c9", "col_d1" }) 
        {
            ВалютаВзаиморасчетов = new Довідники.Валюти_Pointer();
            ВалютаКредита = new Довідники.Валюти_Pointer();
            ВалютаКредитаПоставщика = new Довідники.Валюти_Pointer();
            ВидКонтрагента = 0;
            Глубина = 0;
            ГлубинаКредитаПоставщика = 0;
            ДокументДатаВидачи = DateTime.MinValue;
            ДокументКемВидан = "";
            ДокументНомер = "";
            ДокументСерия = "";
            ЕГРПОУ = "";
            ИНН = "";
            КатегорияЦен = new Довідники.КатегорииЦен_Pointer();
            КатегорияЦенПоставщика = new Довідники.КатегорииЦен_Pointer();
            Комментарий = "";
            НомерСвидетельства = "";
            ОсновнойДоговорТорг = new EmptyPointer();
            ПолнНаименование = "";
            ПочтовийАдрес = "";
            СлужебнийДоговорТорг = new EmptyPointer();
            СуммаКредита = 0;
            СуммаКредитаПоставщика = 0;
            Телефони = "";
            ЮридическийАдрес = "";
            Страна = "";
            ПлательщикНалогаНаПрибиль = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            Глубина_TablePart = new Контрагенти_Глубина_TablePart(this);
            ГлубинаКредитаПоставщика_TablePart = new Контрагенти_ГлубинаКредитаПоставщика_TablePart(this);
            СуммаКредита_TablePart = new Контрагенти_СуммаКредита_TablePart(this);
            СуммаКредитаПоставщика_TablePart = new Контрагенти_СуммаКредитаПоставщика_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ВалютаВзаиморасчетов = new Довідники.Валюти_Pointer(base.FieldValue["col_c4"]);
                ВалютаКредита = new Довідники.Валюти_Pointer(base.FieldValue["col_a1"]);
                ВалютаКредитаПоставщика = new Довідники.Валюти_Pointer(base.FieldValue["col_a2"]);
                ВидКонтрагента = (base.FieldValue["col_a3"] != DBNull.Value) ? (Перелічення.ВидиКонтрагентов)base.FieldValue["col_a3"] : 0;
                Глубина = (base.FieldValue["col_a4"] != DBNull.Value) ? (int)base.FieldValue["col_a4"] : 0;
                ГлубинаКредитаПоставщика = (base.FieldValue["col_a5"] != DBNull.Value) ? (int)base.FieldValue["col_a5"] : 0;
                ДокументДатаВидачи = (base.FieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a6"].ToString()) : DateTime.MinValue;
                ДокументКемВидан = base.FieldValue["col_a7"].ToString();
                ДокументНомер = base.FieldValue["col_a8"].ToString();
                ДокументСерия = base.FieldValue["col_a9"].ToString();
                ЕГРПОУ = base.FieldValue["col_b1"].ToString();
                ИНН = base.FieldValue["col_b2"].ToString();
                КатегорияЦен = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_b3"]);
                КатегорияЦенПоставщика = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_b4"]);
                Комментарий = base.FieldValue["col_b5"].ToString();
                НомерСвидетельства = base.FieldValue["col_b6"].ToString();
                ОсновнойДоговорТорг = new EmptyPointer();
                ПолнНаименование = base.FieldValue["col_b8"].ToString();
                ПочтовийАдрес = base.FieldValue["col_b9"].ToString();
                СлужебнийДоговорТорг = new EmptyPointer();
                СуммаКредита = (base.FieldValue["col_c2"] != DBNull.Value) ? (decimal)base.FieldValue["col_c2"] : 0;
                СуммаКредитаПоставщика = (base.FieldValue["col_c3"] != DBNull.Value) ? (decimal)base.FieldValue["col_c3"] : 0;
                Телефони = base.FieldValue["col_c5"].ToString();
                ЮридическийАдрес = base.FieldValue["col_c6"].ToString();
                Страна = base.FieldValue["col_c7"].ToString();
                ПлательщикНалогаНаПрибиль = (base.FieldValue["col_c8"] != DBNull.Value) ? (int)base.FieldValue["col_c8"] : 0;
                Назва = base.FieldValue["col_c9"].ToString();
                Код = base.FieldValue["col_d1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c4"] = ВалютаВзаиморасчетов.ToString();
            base.FieldValue["col_a1"] = ВалютаКредита.ToString();
            base.FieldValue["col_a2"] = ВалютаКредитаПоставщика.ToString();
            base.FieldValue["col_a3"] = (int)ВидКонтрагента;
            base.FieldValue["col_a4"] = Глубина;
            base.FieldValue["col_a5"] = ГлубинаКредитаПоставщика;
            base.FieldValue["col_a6"] = ДокументДатаВидачи;
            base.FieldValue["col_a7"] = ДокументКемВидан;
            base.FieldValue["col_a8"] = ДокументНомер;
            base.FieldValue["col_a9"] = ДокументСерия;
            base.FieldValue["col_b1"] = ЕГРПОУ;
            base.FieldValue["col_b2"] = ИНН;
            base.FieldValue["col_b3"] = КатегорияЦен.ToString();
            base.FieldValue["col_b4"] = КатегорияЦенПоставщика.ToString();
            base.FieldValue["col_b5"] = Комментарий;
            base.FieldValue["col_b6"] = НомерСвидетельства;
            base.FieldValue["col_b7"] = ОсновнойДоговорТорг.ToString();
            base.FieldValue["col_b8"] = ПолнНаименование;
            base.FieldValue["col_b9"] = ПочтовийАдрес;
            base.FieldValue["col_c1"] = СлужебнийДоговорТорг.ToString();
            base.FieldValue["col_c2"] = СуммаКредита;
            base.FieldValue["col_c3"] = СуммаКредитаПоставщика;
            base.FieldValue["col_c5"] = Телефони;
            base.FieldValue["col_c6"] = ЮридическийАдрес;
            base.FieldValue["col_c7"] = Страна;
            base.FieldValue["col_c8"] = ПлательщикНалогаНаПрибиль;
            base.FieldValue["col_c9"] = Назва;
            base.FieldValue["col_d1"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Контрагенти_Pointer GetDirectoryPointer()
        {
            Контрагенти_Pointer directoryPointer = new Контрагенти_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Валюти_Pointer ВалютаВзаиморасчетов { get; set; }
        public Довідники.Валюти_Pointer ВалютаКредита { get; set; }
        public Довідники.Валюти_Pointer ВалютаКредитаПоставщика { get; set; }
        public Перелічення.ВидиКонтрагентов ВидКонтрагента { get; set; }
        public int Глубина { get; set; }
        public int ГлубинаКредитаПоставщика { get; set; }
        public DateTime ДокументДатаВидачи { get; set; }
        public string ДокументКемВидан { get; set; }
        public string ДокументНомер { get; set; }
        public string ДокументСерия { get; set; }
        public string ЕГРПОУ { get; set; }
        public string ИНН { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦен { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦенПоставщика { get; set; }
        public string Комментарий { get; set; }
        public string НомерСвидетельства { get; set; }
        public EmptyPointer ОсновнойДоговорТорг { get; set; }
        public string ПолнНаименование { get; set; }
        public string ПочтовийАдрес { get; set; }
        public EmptyPointer СлужебнийДоговорТорг { get; set; }
        public decimal СуммаКредита { get; set; }
        public decimal СуммаКредитаПоставщика { get; set; }
        public string Телефони { get; set; }
        public string ЮридическийАдрес { get; set; }
        public string Страна { get; set; }
        public int ПлательщикНалогаНаПрибиль { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Контрагенти_Глубина_TablePart Глубина_TablePart { get; set; }
        public Контрагенти_ГлубинаКредитаПоставщика_TablePart ГлубинаКредитаПоставщика_TablePart { get; set; }
        public Контрагенти_СуммаКредита_TablePart СуммаКредита_TablePart { get; set; }
        public Контрагенти_СуммаКредитаПоставщика_TablePart СуммаКредитаПоставщика_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Поставщики и покупатели.
    ///</summary>
    class Контрагенти_Pointer : DirectoryPointer
    {
        public Контрагенти_Pointer(object uid = null) : base(Config.Kernel, "tab_a12")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Контрагенти_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a12")
        {
            base.Init(uid, fields);
        }
        
        public Контрагенти_Objest GetDirectoryObject()
        {
            Контрагенти_Objest КонтрагентиObjestItem = new Контрагенти_Objest();
            КонтрагентиObjestItem.Read(base.UnigueID);
            return КонтрагентиObjestItem;
        }
    }
    
    ///<summary>
    ///Поставщики и покупатели.
    ///</summary>
    class Контрагенти_Select : DirectorySelect, IDisposable
    {
        public Контрагенти_Select() : base(Config.Kernel, "tab_a12",
            new string[] { "col_c4", "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3", "col_c5", "col_c6", "col_c7", "col_c8", "col_c9", "col_d1" },
            new string[] { "ВалютаВзаиморасчетов", "ВалютаКредита", "ВалютаКредитаПоставщика", "ВидКонтрагента", "Глубина", "ГлубинаКредитаПоставщика", "ДокументДатаВидачи", "ДокументКемВидан", "ДокументНомер", "ДокументСерия", "ЕГРПОУ", "ИНН", "КатегорияЦен", "КатегорияЦенПоставщика", "Комментарий", "НомерСвидетельства", "ОсновнойДоговорТорг", "ПолнНаименование", "ПочтовийАдрес", "СлужебнийДоговорТорг", "СуммаКредита", "СуммаКредитаПоставщика", "Телефони", "ЮридическийАдрес", "Страна", "ПлательщикНалогаНаПрибиль", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Контрагенти_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Контрагенти_Pointer Current { get; private set; }
        
        public Контрагенти_Pointer FindByField(string name, object value)
        {
            Контрагенти_Pointer itemPointer = new Контрагенти_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Контрагенти_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Контрагенти_Pointer> directoryPointerList = new List<Контрагенти_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Контрагенти_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Контрагенти_Глубина_TablePart : DirectoryTablePart
    {
        public Контрагенти_Глубина_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a13",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.Глубина = (fieldValue["col_a2"] != DBNull.Value) ? (int)fieldValue["col_a2"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.Глубина);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Глубина = 0;
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, int _Глубина = 0)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Глубина = _Глубина;
                
            }
            public DateTime Дата { get; set; }
            public int Глубина { get; set; }
            
        }
    }
      
    class Контрагенти_ГлубинаКредитаПоставщика_TablePart : DirectoryTablePart
    {
        public Контрагенти_ГлубинаКредитаПоставщика_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a17",
             new string[] { "col_a3", "col_a4" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                record.ГлубинаКредитаПоставщика = (fieldValue["col_a4"] != DBNull.Value) ? (int)fieldValue["col_a4"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a3", record.Дата);
                    fieldValue.Add("col_a4", record.ГлубинаКредитаПоставщика);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ГлубинаКредитаПоставщика = 0;
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, int _ГлубинаКредитаПоставщика = 0)
            {
                Дата = _Дата ?? DateTime.MinValue;
                ГлубинаКредитаПоставщика = _ГлубинаКредитаПоставщика;
                
            }
            public DateTime Дата { get; set; }
            public int ГлубинаКредитаПоставщика { get; set; }
            
        }
    }
      
    class Контрагенти_СуммаКредита_TablePart : DirectoryTablePart
    {
        public Контрагенти_СуммаКредита_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a18",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.СуммаКредита = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.СуммаКредита);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                СуммаКредита = 0;
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, decimal _СуммаКредита = 0)
            {
                Дата = _Дата ?? DateTime.MinValue;
                СуммаКредита = _СуммаКредита;
                
            }
            public DateTime Дата { get; set; }
            public decimal СуммаКредита { get; set; }
            
        }
    }
      
    class Контрагенти_СуммаКредитаПоставщика_TablePart : DirectoryTablePart
    {
        public Контрагенти_СуммаКредитаПоставщика_TablePart(Контрагенти_Objest owner) : base(Config.Kernel, "tab_a19",
             new string[] { "col_a3", "col_a4" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Контрагенти_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a3"].ToString()) : DateTime.MinValue;
                record.СуммаКредитаПоставщика = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a3", record.Дата);
                    fieldValue.Add("col_a4", record.СуммаКредитаПоставщика);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                СуммаКредитаПоставщика = 0;
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, decimal _СуммаКредитаПоставщика = 0)
            {
                Дата = _Дата ?? DateTime.MinValue;
                СуммаКредитаПоставщика = _СуммаКредитаПоставщика;
                
            }
            public DateTime Дата { get; set; }
            public decimal СуммаКредитаПоставщика { get; set; }
            
        }
    }
      
    class Контрагенти_Список_View : DirectoryView
    {
        public Контрагенти_Список_View() : base(Config.Kernel, "tab_a12", 
             new string[] { "col_c9", "col_a3" },
             new string[] { "Назва", "ВидКонтрагента" },
             new string[] { "string", "enum" },
             "Довідники.Контрагенти_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "МестаХранения"
    ///<summary>
    ///Склады или МОЛ.
    ///</summary>
    class МестаХранения_Objest : DirectoryObject
    {
        public МестаХранения_Objest() : base(Config.Kernel, "tab_a20",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
        {
            ВидСклада = new EmptyPointer();
            МОЛ = new EmptyPointer();
            Комментарий = "";
            Назва = "";
            Код = "";
            Група = new Довідники.Групи_МестаХранения_Pointer();
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ВидСклада = new EmptyPointer();
                МОЛ = new EmptyPointer();
                Комментарий = base.FieldValue["col_a3"].ToString();
                Назва = base.FieldValue["col_a4"].ToString();
                Код = base.FieldValue["col_a5"].ToString();
                Група = new Довідники.Групи_МестаХранения_Pointer(base.FieldValue["col_a6"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = ВидСклада.ToString();
            base.FieldValue["col_a2"] = МОЛ.ToString();
            base.FieldValue["col_a3"] = Комментарий;
            base.FieldValue["col_a4"] = Назва;
            base.FieldValue["col_a5"] = Код;
            base.FieldValue["col_a6"] = Група.ToString();
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public МестаХранения_Pointer GetDirectoryPointer()
        {
            МестаХранения_Pointer directoryPointer = new МестаХранения_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public EmptyPointer ВидСклада { get; set; }
        public EmptyPointer МОЛ { get; set; }
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Групи_МестаХранения_Pointer Група { get; set; }
        
    }
    
    ///<summary>
    ///Склады или МОЛ.
    ///</summary>
    class МестаХранения_Pointer : DirectoryPointer
    {
        public МестаХранения_Pointer(object uid = null) : base(Config.Kernel, "tab_a20")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public МестаХранения_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a20")
        {
            base.Init(uid, fields);
        }
        
        public МестаХранения_Objest GetDirectoryObject()
        {
            МестаХранения_Objest МестаХраненияObjestItem = new МестаХранения_Objest();
            МестаХраненияObjestItem.Read(base.UnigueID);
            return МестаХраненияObjestItem;
        }
    }
    
    ///<summary>
    ///Склады или МОЛ.
    ///</summary>
    class МестаХранения_Select : DirectorySelect, IDisposable
    {
        public МестаХранения_Select() : base(Config.Kernel, "tab_a20",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" },
            new string[] { "ВидСклада", "МОЛ", "Комментарий", "Назва", "Код", "Група" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new МестаХранения_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public МестаХранения_Pointer Current { get; private set; }
        
        public МестаХранения_Pointer FindByField(string name, object value)
        {
            МестаХранения_Pointer itemPointer = new МестаХранения_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<МестаХранения_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<МестаХранения_Pointer> directoryPointerList = new List<МестаХранения_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new МестаХранения_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class МестаХранения_Список_View : DirectoryView
    {
        public МестаХранения_Список_View() : base(Config.Kernel, "tab_a20", 
             new string[] { "col_a4" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.МестаХранения_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НалоговиеИнспекции"
    ///<summary>
    ///Содержит список налоговых инспекций.
    ///</summary>
    class НалоговиеИнспекции_Objest : DirectoryObject
    {
        public НалоговиеИнспекции_Objest() : base(Config.Kernel, "tab_a21",
             new string[] { "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4" }) 
        {
            ЕДРПОУ = "";
            Адрес = "";
            ТипДПИ = "";
            КодАдмРайона = "";
            КодДляПоиска = "";
            НаименованиеАдмРайона = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ЕДРПОУ = base.FieldValue["col_a6"].ToString();
                Адрес = base.FieldValue["col_a7"].ToString();
                ТипДПИ = base.FieldValue["col_a8"].ToString();
                КодАдмРайона = base.FieldValue["col_a9"].ToString();
                КодДляПоиска = base.FieldValue["col_b1"].ToString();
                НаименованиеАдмРайона = base.FieldValue["col_b2"].ToString();
                Назва = base.FieldValue["col_b3"].ToString();
                Код = base.FieldValue["col_b4"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a6"] = ЕДРПОУ;
            base.FieldValue["col_a7"] = Адрес;
            base.FieldValue["col_a8"] = ТипДПИ;
            base.FieldValue["col_a9"] = КодАдмРайона;
            base.FieldValue["col_b1"] = КодДляПоиска;
            base.FieldValue["col_b2"] = НаименованиеАдмРайона;
            base.FieldValue["col_b3"] = Назва;
            base.FieldValue["col_b4"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НалоговиеИнспекции_Pointer GetDirectoryPointer()
        {
            НалоговиеИнспекции_Pointer directoryPointer = new НалоговиеИнспекции_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ЕДРПОУ { get; set; }
        public string Адрес { get; set; }
        public string ТипДПИ { get; set; }
        public string КодАдмРайона { get; set; }
        public string КодДляПоиска { get; set; }
        public string НаименованиеАдмРайона { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Содержит список налоговых инспекций.
    ///</summary>
    class НалоговиеИнспекции_Pointer : DirectoryPointer
    {
        public НалоговиеИнспекции_Pointer(object uid = null) : base(Config.Kernel, "tab_a21")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public НалоговиеИнспекции_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a21")
        {
            base.Init(uid, fields);
        }
        
        public НалоговиеИнспекции_Objest GetDirectoryObject()
        {
            НалоговиеИнспекции_Objest НалоговиеИнспекцииObjestItem = new НалоговиеИнспекции_Objest();
            НалоговиеИнспекцииObjestItem.Read(base.UnigueID);
            return НалоговиеИнспекцииObjestItem;
        }
    }
    
    ///<summary>
    ///Содержит список налоговых инспекций.
    ///</summary>
    class НалоговиеИнспекции_Select : DirectorySelect, IDisposable
    {
        public НалоговиеИнспекции_Select() : base(Config.Kernel, "tab_a21",
            new string[] { "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4" },
            new string[] { "ЕДРПОУ", "Адрес", "ТипДПИ", "КодАдмРайона", "КодДляПоиска", "НаименованиеАдмРайона", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new НалоговиеИнспекции_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public НалоговиеИнспекции_Pointer Current { get; private set; }
        
        public НалоговиеИнспекции_Pointer FindByField(string name, object value)
        {
            НалоговиеИнспекции_Pointer itemPointer = new НалоговиеИнспекции_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<НалоговиеИнспекции_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<НалоговиеИнспекции_Pointer> directoryPointerList = new List<НалоговиеИнспекции_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new НалоговиеИнспекции_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class НалоговиеИнспекции_Список_View : DirectoryView
    {
        public НалоговиеИнспекции_Список_View() : base(Config.Kernel, "tab_a21", 
             new string[] { "col_b3" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.НалоговиеИнспекции_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НашиДенежниеСчета"
    ///<summary>
    ///Банковские счета.
    ///</summary>
    class НашиДенежниеСчета_Objest : DirectoryObject
    {
        public НашиДенежниеСчета_Objest() : base(Config.Kernel, "tab_a22",
             new string[] { "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3", "col_c4", "col_c5", "col_a1", "col_a2" }) 
        {
            БанкНазвание = "";
            БанкАдрес = "";
            БанкМФО = "";
            БанкСчет = "";
            Телефони = "";
            БезНал = false;
            Валюта = new Довідники.Валюти_Pointer();
            Комментарий = "";
            ПоследнийРасхДок = 0;
            ПоследнийПрихДок = 0;
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                БанкНазвание = base.FieldValue["col_b5"].ToString();
                БанкАдрес = base.FieldValue["col_b6"].ToString();
                БанкМФО = base.FieldValue["col_b7"].ToString();
                БанкСчет = base.FieldValue["col_b8"].ToString();
                Телефони = base.FieldValue["col_b9"].ToString();
                БезНал = (bool)base.FieldValue["col_c1"];
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_c2"]);
                Комментарий = base.FieldValue["col_c3"].ToString();
                ПоследнийРасхДок = (base.FieldValue["col_c4"] != DBNull.Value) ? (int)base.FieldValue["col_c4"] : 0;
                ПоследнийПрихДок = (base.FieldValue["col_c5"] != DBNull.Value) ? (int)base.FieldValue["col_c5"] : 0;
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b5"] = БанкНазвание;
            base.FieldValue["col_b6"] = БанкАдрес;
            base.FieldValue["col_b7"] = БанкМФО;
            base.FieldValue["col_b8"] = БанкСчет;
            base.FieldValue["col_b9"] = Телефони;
            base.FieldValue["col_c1"] = БезНал;
            base.FieldValue["col_c2"] = Валюта.ToString();
            base.FieldValue["col_c3"] = Комментарий;
            base.FieldValue["col_c4"] = ПоследнийРасхДок;
            base.FieldValue["col_c5"] = ПоследнийПрихДок;
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НашиДенежниеСчета_Pointer GetDirectoryPointer()
        {
            НашиДенежниеСчета_Pointer directoryPointer = new НашиДенежниеСчета_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string БанкНазвание { get; set; }
        public string БанкАдрес { get; set; }
        public string БанкМФО { get; set; }
        public string БанкСчет { get; set; }
        public string Телефони { get; set; }
        public bool БезНал { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public string Комментарий { get; set; }
        public int ПоследнийРасхДок { get; set; }
        public int ПоследнийПрихДок { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Банковские счета.
    ///</summary>
    class НашиДенежниеСчета_Pointer : DirectoryPointer
    {
        public НашиДенежниеСчета_Pointer(object uid = null) : base(Config.Kernel, "tab_a22")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public НашиДенежниеСчета_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a22")
        {
            base.Init(uid, fields);
        }
        
        public НашиДенежниеСчета_Objest GetDirectoryObject()
        {
            НашиДенежниеСчета_Objest НашиДенежниеСчетаObjestItem = new НашиДенежниеСчета_Objest();
            НашиДенежниеСчетаObjestItem.Read(base.UnigueID);
            return НашиДенежниеСчетаObjestItem;
        }
    }
    
    ///<summary>
    ///Банковские счета.
    ///</summary>
    class НашиДенежниеСчета_Select : DirectorySelect, IDisposable
    {
        public НашиДенежниеСчета_Select() : base(Config.Kernel, "tab_a22",
            new string[] { "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3", "col_c4", "col_c5", "col_a1", "col_a2" },
            new string[] { "БанкНазвание", "БанкАдрес", "БанкМФО", "БанкСчет", "Телефони", "БезНал", "Валюта", "Комментарий", "ПоследнийРасхДок", "ПоследнийПрихДок", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new НашиДенежниеСчета_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public НашиДенежниеСчета_Pointer Current { get; private set; }
        
        public НашиДенежниеСчета_Pointer FindByField(string name, object value)
        {
            НашиДенежниеСчета_Pointer itemPointer = new НашиДенежниеСчета_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<НашиДенежниеСчета_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<НашиДенежниеСчета_Pointer> directoryPointerList = new List<НашиДенежниеСчета_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new НашиДенежниеСчета_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class НашиДенежниеСчета_Список_View : DirectoryView
    {
        public НашиДенежниеСчета_Список_View() : base(Config.Kernel, "tab_a22", 
             new string[] { "col_a1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.НашиДенежниеСчета_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Номенклатура"
    ///<summary>
    ///Товары, услуги, наборы.
    ///</summary>
    class Номенклатура_Objest : DirectoryObject
    {
        public Номенклатура_Objest() : base(Config.Kernel, "tab_a23",
             new string[] { "col_c6", "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6", "col_e7", "col_e8", "col_a1", "col_a2", "col_a3" }) 
        {
            ПолнНаименование = "";
            ВидТовара = 0;
            Артикул = "";
            БазоваяЕдиница = new Довідники.КлассификаторЕдИзм_Pointer();
            Вес = 0;
            ЕдиницаПоУмолчанию = new Довідники.Единици_Pointer();
            ВалютаУчета = new Довідники.Валюти_Pointer();
            УчетнаяЦена = 0;
            МинимальнийОстаток = 0;
            СтавкаНДС = new EmptyPointer();
            СтатьяИздержекУслуги = new EmptyPointer();
            ТипТовара = new EmptyPointer();
            ТорговаяНаценка = 0;
            ШтрихКод = 0;
            Комментарий = "";
            Транспорт = false;
            УслугиНаСебестоимость = false;
            ЛьготаНДС = 0;
            КодЛьготи = "";
            КвоДляНН = "";
            КодУКТВЕД = new Довідники.КодиУКТВЕД_Pointer();
            Назва = "";
            Код = "";
            Група = new Довідники.Групи_Номенклатура_Pointer();
            
            //Табличні частини
            СтавкаНДС_TablePart = new Номенклатура_СтавкаНДС_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ПолнНаименование = base.FieldValue["col_c6"].ToString();
                ВидТовара = (base.FieldValue["col_c7"] != DBNull.Value) ? (Перелічення.ВидиТоварів)base.FieldValue["col_c7"] : 0;
                Артикул = base.FieldValue["col_c8"].ToString();
                БазоваяЕдиница = new Довідники.КлассификаторЕдИзм_Pointer(base.FieldValue["col_c9"]);
                Вес = (base.FieldValue["col_d1"] != DBNull.Value) ? (decimal)base.FieldValue["col_d1"] : 0;
                ЕдиницаПоУмолчанию = new Довідники.Единици_Pointer(base.FieldValue["col_d2"]);
                ВалютаУчета = new Довідники.Валюти_Pointer(base.FieldValue["col_d3"]);
                УчетнаяЦена = (base.FieldValue["col_d4"] != DBNull.Value) ? (decimal)base.FieldValue["col_d4"] : 0;
                МинимальнийОстаток = (base.FieldValue["col_d5"] != DBNull.Value) ? (decimal)base.FieldValue["col_d5"] : 0;
                СтавкаНДС = new EmptyPointer();
                СтатьяИздержекУслуги = new EmptyPointer();
                ТипТовара = new EmptyPointer();
                ТорговаяНаценка = (base.FieldValue["col_d9"] != DBNull.Value) ? (decimal)base.FieldValue["col_d9"] : 0;
                ШтрихКод = (base.FieldValue["col_e1"] != DBNull.Value) ? (int)base.FieldValue["col_e1"] : 0;
                Комментарий = base.FieldValue["col_e2"].ToString();
                Транспорт = (bool)base.FieldValue["col_e3"];
                УслугиНаСебестоимость = (bool)base.FieldValue["col_e4"];
                ЛьготаНДС = (base.FieldValue["col_e5"] != DBNull.Value) ? (int)base.FieldValue["col_e5"] : 0;
                КодЛьготи = base.FieldValue["col_e6"].ToString();
                КвоДляНН = base.FieldValue["col_e7"].ToString();
                КодУКТВЕД = new Довідники.КодиУКТВЕД_Pointer(base.FieldValue["col_e8"]);
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Група = new Довідники.Групи_Номенклатура_Pointer(base.FieldValue["col_a3"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c6"] = ПолнНаименование;
            base.FieldValue["col_c7"] = (int)ВидТовара;
            base.FieldValue["col_c8"] = Артикул;
            base.FieldValue["col_c9"] = БазоваяЕдиница.ToString();
            base.FieldValue["col_d1"] = Вес;
            base.FieldValue["col_d2"] = ЕдиницаПоУмолчанию.ToString();
            base.FieldValue["col_d3"] = ВалютаУчета.ToString();
            base.FieldValue["col_d4"] = УчетнаяЦена;
            base.FieldValue["col_d5"] = МинимальнийОстаток;
            base.FieldValue["col_d6"] = СтавкаНДС.ToString();
            base.FieldValue["col_d7"] = СтатьяИздержекУслуги.ToString();
            base.FieldValue["col_d8"] = ТипТовара.ToString();
            base.FieldValue["col_d9"] = ТорговаяНаценка;
            base.FieldValue["col_e1"] = ШтрихКод;
            base.FieldValue["col_e2"] = Комментарий;
            base.FieldValue["col_e3"] = Транспорт;
            base.FieldValue["col_e4"] = УслугиНаСебестоимость;
            base.FieldValue["col_e5"] = ЛьготаНДС;
            base.FieldValue["col_e6"] = КодЛьготи;
            base.FieldValue["col_e7"] = КвоДляНН;
            base.FieldValue["col_e8"] = КодУКТВЕД.ToString();
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            base.FieldValue["col_a3"] = Група.ToString();
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Номенклатура_Pointer GetDirectoryPointer()
        {
            Номенклатура_Pointer directoryPointer = new Номенклатура_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string ПолнНаименование { get; set; }
        public Перелічення.ВидиТоварів ВидТовара { get; set; }
        public string Артикул { get; set; }
        public Довідники.КлассификаторЕдИзм_Pointer БазоваяЕдиница { get; set; }
        public decimal Вес { get; set; }
        public Довідники.Единици_Pointer ЕдиницаПоУмолчанию { get; set; }
        public Довідники.Валюти_Pointer ВалютаУчета { get; set; }
        public decimal УчетнаяЦена { get; set; }
        public decimal МинимальнийОстаток { get; set; }
        public EmptyPointer СтавкаНДС { get; set; }
        public EmptyPointer СтатьяИздержекУслуги { get; set; }
        public EmptyPointer ТипТовара { get; set; }
        public decimal ТорговаяНаценка { get; set; }
        public int ШтрихКод { get; set; }
        public string Комментарий { get; set; }
        public bool Транспорт { get; set; }
        public bool УслугиНаСебестоимость { get; set; }
        public int ЛьготаНДС { get; set; }
        public string КодЛьготи { get; set; }
        public string КвоДляНН { get; set; }
        public Довідники.КодиУКТВЕД_Pointer КодУКТВЕД { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.Групи_Номенклатура_Pointer Група { get; set; }
        
        //Табличні частини
        public Номенклатура_СтавкаНДС_TablePart СтавкаНДС_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Товары, услуги, наборы.
    ///</summary>
    class Номенклатура_Pointer : DirectoryPointer
    {
        public Номенклатура_Pointer(object uid = null) : base(Config.Kernel, "tab_a23")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Номенклатура_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a23")
        {
            base.Init(uid, fields);
        }
        
        public Номенклатура_Objest GetDirectoryObject()
        {
            Номенклатура_Objest НоменклатураObjestItem = new Номенклатура_Objest();
            НоменклатураObjestItem.Read(base.UnigueID);
            return НоменклатураObjestItem;
        }
    }
    
    ///<summary>
    ///Товары, услуги, наборы.
    ///</summary>
    class Номенклатура_Select : DirectorySelect, IDisposable
    {
        public Номенклатура_Select() : base(Config.Kernel, "tab_a23",
            new string[] { "col_c6", "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6", "col_e7", "col_e8", "col_a1", "col_a2", "col_a3" },
            new string[] { "ПолнНаименование", "ВидТовара", "Артикул", "БазоваяЕдиница", "Вес", "ЕдиницаПоУмолчанию", "ВалютаУчета", "УчетнаяЦена", "МинимальнийОстаток", "СтавкаНДС", "СтатьяИздержекУслуги", "ТипТовара", "ТорговаяНаценка", "ШтрихКод", "Комментарий", "Транспорт", "УслугиНаСебестоимость", "ЛьготаНДС", "КодЛьготи", "КвоДляНН", "КодУКТВЕД", "Назва", "Код", "Група" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Номенклатура_Pointer Current { get; private set; }
        
        public Номенклатура_Pointer FindByField(string name, object value)
        {
            Номенклатура_Pointer itemPointer = new Номенклатура_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Номенклатура_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Номенклатура_Pointer> directoryPointerList = new List<Номенклатура_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Номенклатура_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Номенклатура_СтавкаНДС_TablePart : DirectoryTablePart
    {
        public Номенклатура_СтавкаНДС_TablePart(Номенклатура_Objest owner) : base(Config.Kernel, "tab_a14",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Номенклатура_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString()) : DateTime.MinValue;
                record.СтавкаНДС = new EmptyPointer();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.СтавкаНДС.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                СтавкаНДС = new EmptyPointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, EmptyPointer _СтавкаНДС = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                СтавкаНДС = _СтавкаНДС ?? new EmptyPointer();
                
            }
            public DateTime Дата { get; set; }
            public EmptyPointer СтавкаНДС { get; set; }
            
        }
    }
      
    class Номенклатура_Список_View : DirectoryView
    {
        public Номенклатура_Список_View() : base(Config.Kernel, "tab_a23", 
             new string[] { "col_a1", "col_a2", "col_d3" },
             new string[] { "Назва", "Код", "ВалютаУчета" },
             new string[] { "string", "string", "pointer" },
             "Довідники.Номенклатура_Список")
        {
            
        }
        
    }
      
    class Номенклатура_Список2_View : DirectoryView
    {
        public Номенклатура_Список2_View() : base(Config.Kernel, "tab_a23", 
             new string[] { "col_c7", "col_c8", "col_c9", "col_d1", "col_d3", "col_a1", "col_a2", "col_a3" },
             new string[] { "ВидТовара", "Артикул", "БазоваяЕдиница", "Вес", "ВалютаУчета", "Назва", "Код", "Група" },
             new string[] { "enum", "string", "pointer", "numeric", "pointer", "string", "string", "pointer" },
             "Довідники.Номенклатура_Список2")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "НомераГТД"
    ///<summary>
    ///Номера ГТД.
    ///</summary>
    class НомераГТД_Objest : DirectoryObject
    {
        public НомераГТД_Objest() : base(Config.Kernel, "tab_a24",
             new string[] { "col_e9", "col_f1", "col_f2", "col_f3" }) 
        {
            ДатаГТД = DateTime.MinValue;
            Комментарий = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаГТД = (base.FieldValue["col_e9"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_e9"].ToString()) : DateTime.MinValue;
                Комментарий = base.FieldValue["col_f1"].ToString();
                Назва = base.FieldValue["col_f2"].ToString();
                Код = base.FieldValue["col_f3"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_e9"] = ДатаГТД;
            base.FieldValue["col_f1"] = Комментарий;
            base.FieldValue["col_f2"] = Назва;
            base.FieldValue["col_f3"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public НомераГТД_Pointer GetDirectoryPointer()
        {
            НомераГТД_Pointer directoryPointer = new НомераГТД_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаГТД { get; set; }
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Номера ГТД.
    ///</summary>
    class НомераГТД_Pointer : DirectoryPointer
    {
        public НомераГТД_Pointer(object uid = null) : base(Config.Kernel, "tab_a24")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public НомераГТД_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a24")
        {
            base.Init(uid, fields);
        }
        
        public НомераГТД_Objest GetDirectoryObject()
        {
            НомераГТД_Objest НомераГТДObjestItem = new НомераГТД_Objest();
            НомераГТДObjestItem.Read(base.UnigueID);
            return НомераГТДObjestItem;
        }
    }
    
    ///<summary>
    ///Номера ГТД.
    ///</summary>
    class НомераГТД_Select : DirectorySelect, IDisposable
    {
        public НомераГТД_Select() : base(Config.Kernel, "tab_a24",
            new string[] { "col_e9", "col_f1", "col_f2", "col_f3" },
            new string[] { "ДатаГТД", "Комментарий", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new НомераГТД_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public НомераГТД_Pointer Current { get; private set; }
        
        public НомераГТД_Pointer FindByField(string name, object value)
        {
            НомераГТД_Pointer itemPointer = new НомераГТД_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<НомераГТД_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<НомераГТД_Pointer> directoryPointerList = new List<НомераГТД_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new НомераГТД_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class НомераГТД_Список_View : DirectoryView
    {
        public НомераГТД_Список_View() : base(Config.Kernel, "tab_a24", 
             new string[] { "col_f2" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.НомераГТД_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Пользователи"
    ///<summary>
    ///Пользователи.
    ///</summary>
    class Пользователи_Objest : DirectoryObject
    {
        public Пользователи_Objest() : base(Config.Kernel, "tab_a25",
             new string[] { "col_f4", "col_f5", "col_f6", "col_f7", "col_f8" }) 
        {
            ОсновнаяФирма = new EmptyPointer();
            КатегорияЦен = new Довідники.КатегорииЦен_Pointer();
            Отпустил = new EmptyPointer();
            Назва = "";
            Код = 0;
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ОсновнаяФирма = new EmptyPointer();
                КатегорияЦен = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_f5"]);
                Отпустил = new EmptyPointer();
                Назва = base.FieldValue["col_f7"].ToString();
                Код = (base.FieldValue["col_f8"] != DBNull.Value) ? (int)base.FieldValue["col_f8"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_f4"] = ОсновнаяФирма.ToString();
            base.FieldValue["col_f5"] = КатегорияЦен.ToString();
            base.FieldValue["col_f6"] = Отпустил.ToString();
            base.FieldValue["col_f7"] = Назва;
            base.FieldValue["col_f8"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Пользователи_Pointer GetDirectoryPointer()
        {
            Пользователи_Pointer directoryPointer = new Пользователи_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public EmptyPointer ОсновнаяФирма { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦен { get; set; }
        public EmptyPointer Отпустил { get; set; }
        public string Назва { get; set; }
        public int Код { get; set; }
        
    }
    
    ///<summary>
    ///Пользователи.
    ///</summary>
    class Пользователи_Pointer : DirectoryPointer
    {
        public Пользователи_Pointer(object uid = null) : base(Config.Kernel, "tab_a25")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Пользователи_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a25")
        {
            base.Init(uid, fields);
        }
        
        public Пользователи_Objest GetDirectoryObject()
        {
            Пользователи_Objest ПользователиObjestItem = new Пользователи_Objest();
            ПользователиObjestItem.Read(base.UnigueID);
            return ПользователиObjestItem;
        }
    }
    
    ///<summary>
    ///Пользователи.
    ///</summary>
    class Пользователи_Select : DirectorySelect, IDisposable
    {
        public Пользователи_Select() : base(Config.Kernel, "tab_a25",
            new string[] { "col_f4", "col_f5", "col_f6", "col_f7", "col_f8" },
            new string[] { "ОсновнаяФирма", "КатегорияЦен", "Отпустил", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Пользователи_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Пользователи_Pointer Current { get; private set; }
        
        public Пользователи_Pointer FindByField(string name, object value)
        {
            Пользователи_Pointer itemPointer = new Пользователи_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Пользователи_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Пользователи_Pointer> directoryPointerList = new List<Пользователи_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Пользователи_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Пользователи_Список_View : DirectoryView
    {
        public Пользователи_Список_View() : base(Config.Kernel, "tab_a25", 
             new string[] { "col_f7" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.Пользователи_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Прайс_лист"
    
    class Прайс_лист_Objest : DirectoryObject
    {
        public Прайс_лист_Objest() : base(Config.Kernel, "tab_a26",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4" }) 
        {
            Товар = new Довідники.Номенклатура_Pointer();
            Комментарий = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Товар = new Довідники.Номенклатура_Pointer(base.FieldValue["col_a1"]);
                Комментарий = base.FieldValue["col_a2"].ToString();
                Назва = base.FieldValue["col_a3"].ToString();
                Код = base.FieldValue["col_a4"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Товар.ToString();
            base.FieldValue["col_a2"] = Комментарий;
            base.FieldValue["col_a3"] = Назва;
            base.FieldValue["col_a4"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Прайс_лист_Pointer GetDirectoryPointer()
        {
            Прайс_лист_Pointer directoryPointer = new Прайс_лист_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Номенклатура_Pointer Товар { get; set; }
        public string Комментарий { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class Прайс_лист_Pointer : DirectoryPointer
    {
        public Прайс_лист_Pointer(object uid = null) : base(Config.Kernel, "tab_a26")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Прайс_лист_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a26")
        {
            base.Init(uid, fields);
        }
        
        public Прайс_лист_Objest GetDirectoryObject()
        {
            Прайс_лист_Objest Прайс_листObjestItem = new Прайс_лист_Objest();
            Прайс_листObjestItem.Read(base.UnigueID);
            return Прайс_листObjestItem;
        }
    }
    
    
    class Прайс_лист_Select : DirectorySelect, IDisposable
    {
        public Прайс_лист_Select() : base(Config.Kernel, "tab_a26",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4" },
            new string[] { "Товар", "Комментарий", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Прайс_лист_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Прайс_лист_Pointer Current { get; private set; }
        
        public Прайс_лист_Pointer FindByField(string name, object value)
        {
            Прайс_лист_Pointer itemPointer = new Прайс_лист_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Прайс_лист_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Прайс_лист_Pointer> directoryPointerList = new List<Прайс_лист_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Прайс_лист_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Прайс_лист_Список_View : DirectoryView
    {
        public Прайс_лист_Список_View() : base(Config.Kernel, "tab_a26", 
             new string[] { "col_a3", "col_a1" },
             new string[] { "Назва", "Товар" },
             new string[] { "string", "pointer" },
             "Довідники.Прайс_лист_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "РасчетниеСчета"
    ///<summary>
    ///Расчетные счета контрагентов.
    ///</summary>
    class РасчетниеСчета_Objest : DirectoryObject
    {
        public РасчетниеСчета_Objest() : base(Config.Kernel, "tab_a27",
             new string[] { "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8" }) 
        {
            БанкНазвание = "";
            БанкМФО = "";
            БанкСчет = "";
            НомерСчетаУстаревший = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                БанкНазвание = base.FieldValue["col_a3"].ToString();
                БанкМФО = base.FieldValue["col_a4"].ToString();
                БанкСчет = base.FieldValue["col_a5"].ToString();
                НомерСчетаУстаревший = base.FieldValue["col_a6"].ToString();
                Назва = base.FieldValue["col_a7"].ToString();
                Код = base.FieldValue["col_a8"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a3"] = БанкНазвание;
            base.FieldValue["col_a4"] = БанкМФО;
            base.FieldValue["col_a5"] = БанкСчет;
            base.FieldValue["col_a6"] = НомерСчетаУстаревший;
            base.FieldValue["col_a7"] = Назва;
            base.FieldValue["col_a8"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public РасчетниеСчета_Pointer GetDirectoryPointer()
        {
            РасчетниеСчета_Pointer directoryPointer = new РасчетниеСчета_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string БанкНазвание { get; set; }
        public string БанкМФО { get; set; }
        public string БанкСчет { get; set; }
        public string НомерСчетаУстаревший { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Расчетные счета контрагентов.
    ///</summary>
    class РасчетниеСчета_Pointer : DirectoryPointer
    {
        public РасчетниеСчета_Pointer(object uid = null) : base(Config.Kernel, "tab_a27")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public РасчетниеСчета_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a27")
        {
            base.Init(uid, fields);
        }
        
        public РасчетниеСчета_Objest GetDirectoryObject()
        {
            РасчетниеСчета_Objest РасчетниеСчетаObjestItem = new РасчетниеСчета_Objest();
            РасчетниеСчетаObjestItem.Read(base.UnigueID);
            return РасчетниеСчетаObjestItem;
        }
    }
    
    ///<summary>
    ///Расчетные счета контрагентов.
    ///</summary>
    class РасчетниеСчета_Select : DirectorySelect, IDisposable
    {
        public РасчетниеСчета_Select() : base(Config.Kernel, "tab_a27",
            new string[] { "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8" },
            new string[] { "БанкНазвание", "БанкМФО", "БанкСчет", "НомерСчетаУстаревший", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new РасчетниеСчета_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public РасчетниеСчета_Pointer Current { get; private set; }
        
        public РасчетниеСчета_Pointer FindByField(string name, object value)
        {
            РасчетниеСчета_Pointer itemPointer = new РасчетниеСчета_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<РасчетниеСчета_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<РасчетниеСчета_Pointer> directoryPointerList = new List<РасчетниеСчета_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new РасчетниеСчета_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class РасчетниеСчета_Список_View : DirectoryView
    {
        public РасчетниеСчета_Список_View() : base(Config.Kernel, "tab_a27", 
             new string[] { "col_a7", "col_a3" },
             new string[] { "Назва", "БанкНазвание" },
             new string[] { "string", "string" },
             "Довідники.РасчетниеСчета_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Словарь"
    ///<summary>
    ///Содержит переводы ключевых слов и фраз с русского на украинский язык.
    ///</summary>
    class Словарь_Objest : DirectoryObject
    {
        public Словарь_Objest() : base(Config.Kernel, "tab_a28",
             new string[] { "col_a9", "col_b1", "col_b2" }) 
        {
            Перевод = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Перевод = base.FieldValue["col_a9"].ToString();
                Назва = base.FieldValue["col_b1"].ToString();
                Код = base.FieldValue["col_b2"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a9"] = Перевод;
            base.FieldValue["col_b1"] = Назва;
            base.FieldValue["col_b2"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Словарь_Pointer GetDirectoryPointer()
        {
            Словарь_Pointer directoryPointer = new Словарь_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Перевод { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Содержит переводы ключевых слов и фраз с русского на украинский язык.
    ///</summary>
    class Словарь_Pointer : DirectoryPointer
    {
        public Словарь_Pointer(object uid = null) : base(Config.Kernel, "tab_a28")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Словарь_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a28")
        {
            base.Init(uid, fields);
        }
        
        public Словарь_Objest GetDirectoryObject()
        {
            Словарь_Objest СловарьObjestItem = new Словарь_Objest();
            СловарьObjestItem.Read(base.UnigueID);
            return СловарьObjestItem;
        }
    }
    
    ///<summary>
    ///Содержит переводы ключевых слов и фраз с русского на украинский язык.
    ///</summary>
    class Словарь_Select : DirectorySelect, IDisposable
    {
        public Словарь_Select() : base(Config.Kernel, "tab_a28",
            new string[] { "col_a9", "col_b1", "col_b2" },
            new string[] { "Перевод", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Словарь_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Словарь_Pointer Current { get; private set; }
        
        public Словарь_Pointer FindByField(string name, object value)
        {
            Словарь_Pointer itemPointer = new Словарь_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Словарь_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Словарь_Pointer> directoryPointerList = new List<Словарь_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Словарь_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Словарь_Список_View : DirectoryView
    {
        public Словарь_Список_View() : base(Config.Kernel, "tab_a28", 
             new string[] { "col_b1" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.Словарь_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Сотрудники"
    ///<summary>
    ///Сотрудники.
    ///</summary>
    class Сотрудники_Objest : DirectoryObject
    {
        public Сотрудники_Objest() : base(Config.Kernel, "tab_a29",
             new string[] { "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1" }) 
        {
            ДатаВидачиПаспорта = DateTime.MinValue;
            КемВиданПаспорт = "";
            НомерПаспорта = "";
            СерияПаспорта = "";
            Должность = "";
            ИНН = "";
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаВидачиПаспорта = (base.FieldValue["col_b3"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_b3"].ToString()) : DateTime.MinValue;
                КемВиданПаспорт = base.FieldValue["col_b4"].ToString();
                НомерПаспорта = base.FieldValue["col_b5"].ToString();
                СерияПаспорта = base.FieldValue["col_b6"].ToString();
                Должность = base.FieldValue["col_b7"].ToString();
                ИНН = base.FieldValue["col_b8"].ToString();
                Назва = base.FieldValue["col_b9"].ToString();
                Код = base.FieldValue["col_c1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_b3"] = ДатаВидачиПаспорта;
            base.FieldValue["col_b4"] = КемВиданПаспорт;
            base.FieldValue["col_b5"] = НомерПаспорта;
            base.FieldValue["col_b6"] = СерияПаспорта;
            base.FieldValue["col_b7"] = Должность;
            base.FieldValue["col_b8"] = ИНН;
            base.FieldValue["col_b9"] = Назва;
            base.FieldValue["col_c1"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Сотрудники_Pointer GetDirectoryPointer()
        {
            Сотрудники_Pointer directoryPointer = new Сотрудники_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаВидачиПаспорта { get; set; }
        public string КемВиданПаспорт { get; set; }
        public string НомерПаспорта { get; set; }
        public string СерияПаспорта { get; set; }
        public string Должность { get; set; }
        public string ИНН { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    ///<summary>
    ///Сотрудники.
    ///</summary>
    class Сотрудники_Pointer : DirectoryPointer
    {
        public Сотрудники_Pointer(object uid = null) : base(Config.Kernel, "tab_a29")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Сотрудники_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a29")
        {
            base.Init(uid, fields);
        }
        
        public Сотрудники_Objest GetDirectoryObject()
        {
            Сотрудники_Objest СотрудникиObjestItem = new Сотрудники_Objest();
            СотрудникиObjestItem.Read(base.UnigueID);
            return СотрудникиObjestItem;
        }
    }
    
    ///<summary>
    ///Сотрудники.
    ///</summary>
    class Сотрудники_Select : DirectorySelect, IDisposable
    {
        public Сотрудники_Select() : base(Config.Kernel, "tab_a29",
            new string[] { "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1" },
            new string[] { "ДатаВидачиПаспорта", "КемВиданПаспорт", "НомерПаспорта", "СерияПаспорта", "Должность", "ИНН", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Сотрудники_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Сотрудники_Pointer Current { get; private set; }
        
        public Сотрудники_Pointer FindByField(string name, object value)
        {
            Сотрудники_Pointer itemPointer = new Сотрудники_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Сотрудники_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Сотрудники_Pointer> directoryPointerList = new List<Сотрудники_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Сотрудники_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Сотрудники_Список_View : DirectoryView
    {
        public Сотрудники_Список_View() : base(Config.Kernel, "tab_a29", 
             new string[] { "col_b9" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.Сотрудники_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "ТорговоеОборудование"
    
    class ТорговоеОборудование_Objest : DirectoryObject
    {
        public ТорговоеОборудование_Objest() : base(Config.Kernel, "tab_a30",
             new string[] { "col_c2", "col_c3", "col_c4", "col_c5", "col_c6" }) 
        {
            РаботаСК = false;
            ТипСканера = false;
            ЕстьПрефикс = false;
            Назва = "";
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                РаботаСК = (bool)base.FieldValue["col_c2"];
                ТипСканера = (bool)base.FieldValue["col_c3"];
                ЕстьПрефикс = (bool)base.FieldValue["col_c4"];
                Назва = base.FieldValue["col_c5"].ToString();
                Код = base.FieldValue["col_c6"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c2"] = РаботаСК;
            base.FieldValue["col_c3"] = ТипСканера;
            base.FieldValue["col_c4"] = ЕстьПрефикс;
            base.FieldValue["col_c5"] = Назва;
            base.FieldValue["col_c6"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ТорговоеОборудование_Pointer GetDirectoryPointer()
        {
            ТорговоеОборудование_Pointer directoryPointer = new ТорговоеОборудование_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public bool РаботаСК { get; set; }
        public bool ТипСканера { get; set; }
        public bool ЕстьПрефикс { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class ТорговоеОборудование_Pointer : DirectoryPointer
    {
        public ТорговоеОборудование_Pointer(object uid = null) : base(Config.Kernel, "tab_a30")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ТорговоеОборудование_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a30")
        {
            base.Init(uid, fields);
        }
        
        public ТорговоеОборудование_Objest GetDirectoryObject()
        {
            ТорговоеОборудование_Objest ТорговоеОборудованиеObjestItem = new ТорговоеОборудование_Objest();
            ТорговоеОборудованиеObjestItem.Read(base.UnigueID);
            return ТорговоеОборудованиеObjestItem;
        }
    }
    
    
    class ТорговоеОборудование_Select : DirectorySelect, IDisposable
    {
        public ТорговоеОборудование_Select() : base(Config.Kernel, "tab_a30",
            new string[] { "col_c2", "col_c3", "col_c4", "col_c5", "col_c6" },
            new string[] { "РаботаСК", "ТипСканера", "ЕстьПрефикс", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ТорговоеОборудование_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public ТорговоеОборудование_Pointer Current { get; private set; }
        
        public ТорговоеОборудование_Pointer FindByField(string name, object value)
        {
            ТорговоеОборудование_Pointer itemPointer = new ТорговоеОборудование_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<ТорговоеОборудование_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<ТорговоеОборудование_Pointer> directoryPointerList = new List<ТорговоеОборудование_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new ТорговоеОборудование_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class ТорговоеОборудование_Список_View : DirectoryView
    {
        public ТорговоеОборудование_Список_View() : base(Config.Kernel, "tab_a30", 
             new string[] { "col_c5" },
             new string[] { "Назва" },
             new string[] { "string" },
             "Довідники.ТорговоеОборудование_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Фирми"
    ///<summary>
    ///Справочник собственных фирм.
    ///</summary>
    class Фирми_Objest : DirectoryObject
    {
        public Фирми_Objest() : base(Config.Kernel, "tab_a31",
             new string[] { "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6", "col_e7", "col_e8", "col_e9", "col_f1" }) 
        {
            ГлавнийБухгалтер = new Довідники.Сотрудники_Pointer();
            ГНИ = "";
            ДатаРегистрации = DateTime.MinValue;
            ЕГРПОУ = "";
            ИНН = "";
            Кассир = new Довідники.Сотрудники_Pointer();
            Комментарий = "";
            МетодРасчетаСебестоимостиФинансовогоУчета = new EmptyPointer();
            НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer();
            НомерСвидетельства = "";
            ОфициальноеНаименование = "";
            ПлательщикНалогаНаПрибиль = false;
            ПолнНаименование = "";
            ПочтовийАдрес = "";
            ПрефиксНомеровДокументов = "";
            Руководитель = new Довідники.Сотрудники_Pointer();
            СчетПоУмолчанию = new Довідники.НашиДенежниеСчета_Pointer();
            Телефони = "";
            ЮридическийАдрес = "";
            ИнфОСтатусеПлательщикаНалогов = "";
            Назва = "";
            Код = "";
            
            //Табличні частини
            ГлавнийБухгалтер_TablePart = new Фирми_ГлавнийБухгалтер_TablePart(this);
            ГНИ_TablePart = new Фирми_ГНИ_TablePart(this);
            Кассир_TablePart = new Фирми_Кассир_TablePart(this);
            МетодРасчетаСебестоимостиФинансовогоУчета_TablePart = new Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart(this);
            НалоговаяИнспекция_TablePart = new Фирми_НалоговаяИнспекция_TablePart(this);
            ОфициальноеНаименование_TablePart = new Фирми_ОфициальноеНаименование_TablePart(this);
            ПлательщикНалогаНаПрибиль_TablePart = new Фирми_ПлательщикНалогаНаПрибиль_TablePart(this);
            ПолнНаименование_TablePart = new Фирми_ПолнНаименование_TablePart(this);
            ПочтовийАдрес_TablePart = new Фирми_ПочтовийАдрес_TablePart(this);
            Руководитель_TablePart = new Фирми_Руководитель_TablePart(this);
            ЮридическийАдрес_TablePart = new Фирми_ЮридическийАдрес_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ГлавнийБухгалтер = new Довідники.Сотрудники_Pointer(base.FieldValue["col_c7"]);
                ГНИ = base.FieldValue["col_c8"].ToString();
                ДатаРегистрации = (base.FieldValue["col_c9"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_c9"].ToString()) : DateTime.MinValue;
                ЕГРПОУ = base.FieldValue["col_d1"].ToString();
                ИНН = base.FieldValue["col_d2"].ToString();
                Кассир = new Довідники.Сотрудники_Pointer(base.FieldValue["col_d3"]);
                Комментарий = base.FieldValue["col_d4"].ToString();
                МетодРасчетаСебестоимостиФинансовогоУчета = new EmptyPointer();
                НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer(base.FieldValue["col_d6"]);
                НомерСвидетельства = base.FieldValue["col_d7"].ToString();
                ОфициальноеНаименование = base.FieldValue["col_d8"].ToString();
                ПлательщикНалогаНаПрибиль = (bool)base.FieldValue["col_d9"];
                ПолнНаименование = base.FieldValue["col_e1"].ToString();
                ПочтовийАдрес = base.FieldValue["col_e2"].ToString();
                ПрефиксНомеровДокументов = base.FieldValue["col_e3"].ToString();
                Руководитель = new Довідники.Сотрудники_Pointer(base.FieldValue["col_e4"]);
                СчетПоУмолчанию = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_e5"]);
                Телефони = base.FieldValue["col_e6"].ToString();
                ЮридическийАдрес = base.FieldValue["col_e7"].ToString();
                ИнфОСтатусеПлательщикаНалогов = base.FieldValue["col_e8"].ToString();
                Назва = base.FieldValue["col_e9"].ToString();
                Код = base.FieldValue["col_f1"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c7"] = ГлавнийБухгалтер.ToString();
            base.FieldValue["col_c8"] = ГНИ;
            base.FieldValue["col_c9"] = ДатаРегистрации;
            base.FieldValue["col_d1"] = ЕГРПОУ;
            base.FieldValue["col_d2"] = ИНН;
            base.FieldValue["col_d3"] = Кассир.ToString();
            base.FieldValue["col_d4"] = Комментарий;
            base.FieldValue["col_d5"] = МетодРасчетаСебестоимостиФинансовогоУчета.ToString();
            base.FieldValue["col_d6"] = НалоговаяИнспекция.ToString();
            base.FieldValue["col_d7"] = НомерСвидетельства;
            base.FieldValue["col_d8"] = ОфициальноеНаименование;
            base.FieldValue["col_d9"] = ПлательщикНалогаНаПрибиль;
            base.FieldValue["col_e1"] = ПолнНаименование;
            base.FieldValue["col_e2"] = ПочтовийАдрес;
            base.FieldValue["col_e3"] = ПрефиксНомеровДокументов;
            base.FieldValue["col_e4"] = Руководитель.ToString();
            base.FieldValue["col_e5"] = СчетПоУмолчанию.ToString();
            base.FieldValue["col_e6"] = Телефони;
            base.FieldValue["col_e7"] = ЮридическийАдрес;
            base.FieldValue["col_e8"] = ИнфОСтатусеПлательщикаНалогов;
            base.FieldValue["col_e9"] = Назва;
            base.FieldValue["col_f1"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Фирми_Pointer GetDirectoryPointer()
        {
            Фирми_Pointer directoryPointer = new Фирми_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Сотрудники_Pointer ГлавнийБухгалтер { get; set; }
        public string ГНИ { get; set; }
        public DateTime ДатаРегистрации { get; set; }
        public string ЕГРПОУ { get; set; }
        public string ИНН { get; set; }
        public Довідники.Сотрудники_Pointer Кассир { get; set; }
        public string Комментарий { get; set; }
        public EmptyPointer МетодРасчетаСебестоимостиФинансовогоУчета { get; set; }
        public Довідники.НалоговиеИнспекции_Pointer НалоговаяИнспекция { get; set; }
        public string НомерСвидетельства { get; set; }
        public string ОфициальноеНаименование { get; set; }
        public bool ПлательщикНалогаНаПрибиль { get; set; }
        public string ПолнНаименование { get; set; }
        public string ПочтовийАдрес { get; set; }
        public string ПрефиксНомеровДокументов { get; set; }
        public Довідники.Сотрудники_Pointer Руководитель { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer СчетПоУмолчанию { get; set; }
        public string Телефони { get; set; }
        public string ЮридическийАдрес { get; set; }
        public string ИнфОСтатусеПлательщикаНалогов { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Фирми_ГлавнийБухгалтер_TablePart ГлавнийБухгалтер_TablePart { get; set; }
        public Фирми_ГНИ_TablePart ГНИ_TablePart { get; set; }
        public Фирми_Кассир_TablePart Кассир_TablePart { get; set; }
        public Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart МетодРасчетаСебестоимостиФинансовогоУчета_TablePart { get; set; }
        public Фирми_НалоговаяИнспекция_TablePart НалоговаяИнспекция_TablePart { get; set; }
        public Фирми_ОфициальноеНаименование_TablePart ОфициальноеНаименование_TablePart { get; set; }
        public Фирми_ПлательщикНалогаНаПрибиль_TablePart ПлательщикНалогаНаПрибиль_TablePart { get; set; }
        public Фирми_ПолнНаименование_TablePart ПолнНаименование_TablePart { get; set; }
        public Фирми_ПочтовийАдрес_TablePart ПочтовийАдрес_TablePart { get; set; }
        public Фирми_Руководитель_TablePart Руководитель_TablePart { get; set; }
        public Фирми_ЮридическийАдрес_TablePart ЮридическийАдрес_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Справочник собственных фирм.
    ///</summary>
    class Фирми_Pointer : DirectoryPointer
    {
        public Фирми_Pointer(object uid = null) : base(Config.Kernel, "tab_a31")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Фирми_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a31")
        {
            base.Init(uid, fields);
        }
        
        public Фирми_Objest GetDirectoryObject()
        {
            Фирми_Objest ФирмиObjestItem = new Фирми_Objest();
            ФирмиObjestItem.Read(base.UnigueID);
            return ФирмиObjestItem;
        }
    }
    
    ///<summary>
    ///Справочник собственных фирм.
    ///</summary>
    class Фирми_Select : DirectorySelect, IDisposable
    {
        public Фирми_Select() : base(Config.Kernel, "tab_a31",
            new string[] { "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6", "col_e7", "col_e8", "col_e9", "col_f1" },
            new string[] { "ГлавнийБухгалтер", "ГНИ", "ДатаРегистрации", "ЕГРПОУ", "ИНН", "Кассир", "Комментарий", "МетодРасчетаСебестоимостиФинансовогоУчета", "НалоговаяИнспекция", "НомерСвидетельства", "ОфициальноеНаименование", "ПлательщикНалогаНаПрибиль", "ПолнНаименование", "ПочтовийАдрес", "ПрефиксНомеровДокументов", "Руководитель", "СчетПоУмолчанию", "Телефони", "ЮридическийАдрес", "ИнфОСтатусеПлательщикаНалогов", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Фирми_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Фирми_Pointer Current { get; private set; }
        
        public Фирми_Pointer FindByField(string name, object value)
        {
            Фирми_Pointer itemPointer = new Фирми_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Фирми_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Фирми_Pointer> directoryPointerList = new List<Фирми_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Фирми_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Фирми_ГлавнийБухгалтер_TablePart : DirectoryTablePart
    {
        public Фирми_ГлавнийБухгалтер_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a32",
             new string[] { "col_f2", "col_f3" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_f2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f2"].ToString()) : DateTime.MinValue;
                record.ГлавнийБухгалтер = fieldValue["col_f3"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f2", record.Дата);
                    fieldValue.Add("col_f3", record.ГлавнийБухгалтер);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ГлавнийБухгалтер = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, string _ГлавнийБухгалтер = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                ГлавнийБухгалтер = _ГлавнийБухгалтер;
                
            }
            public DateTime Дата { get; set; }
            public string ГлавнийБухгалтер { get; set; }
            
        }
    }
      
    class Фирми_ГНИ_TablePart : DirectoryTablePart
    {
        public Фирми_ГНИ_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a33",
             new string[] { "col_f4", "col_f5" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_f4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f4"].ToString()) : DateTime.MinValue;
                record.ГНИ = fieldValue["col_f5"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f4", record.Дата);
                    fieldValue.Add("col_f5", record.ГНИ);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ГНИ = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, string _ГНИ = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                ГНИ = _ГНИ;
                
            }
            public DateTime Дата { get; set; }
            public string ГНИ { get; set; }
            
        }
    }
      ///<summary>
    ///Кассир фирмы.
    ///</summary>
    class Фирми_Кассир_TablePart : DirectoryTablePart
    {
        public Фирми_Кассир_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a34",
             new string[] { "col_f6", "col_f7" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_f6"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f6"].ToString()) : DateTime.MinValue;
                record.Кассир = new Довідники.Сотрудники_Pointer(fieldValue["col_f7"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f6", record.Дата);
                    fieldValue.Add("col_f7", record.Кассир.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        ///<summary>
    ///Кассир фирмы.
    ///</summary>
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Кассир = new Довідники.Сотрудники_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, Довідники.Сотрудники_Pointer _Кассир = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Кассир = _Кассир ?? new Довідники.Сотрудники_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public Довідники.Сотрудники_Pointer Кассир { get; set; }
            
        }
    }
      
    class Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart : DirectoryTablePart
    {
        public Фирми_МетодРасчетаСебестоимостиФинансовогоУчета_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a35",
             new string[] { "col_f8", "col_f9" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_f8"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_f8"].ToString()) : DateTime.MinValue;
                record.МетодРасчетаСебестоимостиФинансовогоУчета = new EmptyPointer();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_f8", record.Дата);
                    fieldValue.Add("col_f9", record.МетодРасчетаСебестоимостиФинансовогоУчета.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                МетодРасчетаСебестоимостиФинансовогоУчета = new EmptyPointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, EmptyPointer _МетодРасчетаСебестоимостиФинансовогоУчета = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                МетодРасчетаСебестоимостиФинансовогоУчета = _МетодРасчетаСебестоимостиФинансовогоУчета ?? new EmptyPointer();
                
            }
            public DateTime Дата { get; set; }
            public EmptyPointer МетодРасчетаСебестоимостиФинансовогоУчета { get; set; }
            
        }
    }
      
    class Фирми_НалоговаяИнспекция_TablePart : DirectoryTablePart
    {
        public Фирми_НалоговаяИнспекция_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a36",
             new string[] { "col_g1", "col_g2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_g1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g1"].ToString()) : DateTime.MinValue;
                record.НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer(fieldValue["col_g2"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g1", record.Дата);
                    fieldValue.Add("col_g2", record.НалоговаяИнспекция.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                НалоговаяИнспекция = new Довідники.НалоговиеИнспекции_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, Довідники.НалоговиеИнспекции_Pointer _НалоговаяИнспекция = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                НалоговаяИнспекция = _НалоговаяИнспекция ?? new Довідники.НалоговиеИнспекции_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public Довідники.НалоговиеИнспекции_Pointer НалоговаяИнспекция { get; set; }
            
        }
    }
      
    class Фирми_ОфициальноеНаименование_TablePart : DirectoryTablePart
    {
        public Фирми_ОфициальноеНаименование_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a37",
             new string[] { "col_g3", "col_g4" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_g3"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g3"].ToString()) : DateTime.MinValue;
                record.ОфициальноеНаименование = fieldValue["col_g4"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g3", record.Дата);
                    fieldValue.Add("col_g4", record.ОфициальноеНаименование);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ОфициальноеНаименование = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, string _ОфициальноеНаименование = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                ОфициальноеНаименование = _ОфициальноеНаименование;
                
            }
            public DateTime Дата { get; set; }
            public string ОфициальноеНаименование { get; set; }
            
        }
    }
      
    class Фирми_ПлательщикНалогаНаПрибиль_TablePart : DirectoryTablePart
    {
        public Фирми_ПлательщикНалогаНаПрибиль_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a38",
             new string[] { "col_g5", "col_g6" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_g5"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g5"].ToString()) : DateTime.MinValue;
                record.ПлательщикНалогаНаПрибиль = (bool)fieldValue["col_g6"];
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g5", record.Дата);
                    fieldValue.Add("col_g6", record.ПлательщикНалогаНаПрибиль);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ПлательщикНалогаНаПрибиль = false;
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, bool _ПлательщикНалогаНаПрибиль = false)
            {
                Дата = _Дата ?? DateTime.MinValue;
                ПлательщикНалогаНаПрибиль = _ПлательщикНалогаНаПрибиль;
                
            }
            public DateTime Дата { get; set; }
            public bool ПлательщикНалогаНаПрибиль { get; set; }
            
        }
    }
      
    class Фирми_ПолнНаименование_TablePart : DirectoryTablePart
    {
        public Фирми_ПолнНаименование_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a39",
             new string[] { "col_g7", "col_g8" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_g7"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g7"].ToString()) : DateTime.MinValue;
                record.ПолнНаименование = fieldValue["col_g8"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g7", record.Дата);
                    fieldValue.Add("col_g8", record.ПолнНаименование);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ПолнНаименование = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, string _ПолнНаименование = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                ПолнНаименование = _ПолнНаименование;
                
            }
            public DateTime Дата { get; set; }
            public string ПолнНаименование { get; set; }
            
        }
    }
      
    class Фирми_ПочтовийАдрес_TablePart : DirectoryTablePart
    {
        public Фирми_ПочтовийАдрес_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a40",
             new string[] { "col_g9", "col_h1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_g9"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_g9"].ToString()) : DateTime.MinValue;
                record.ПочтовийАдрес = fieldValue["col_h1"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_g9", record.Дата);
                    fieldValue.Add("col_h1", record.ПочтовийАдрес);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ПочтовийАдрес = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, string _ПочтовийАдрес = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                ПочтовийАдрес = _ПочтовийАдрес;
                
            }
            public DateTime Дата { get; set; }
            public string ПочтовийАдрес { get; set; }
            
        }
    }
      
    class Фирми_Руководитель_TablePart : DirectoryTablePart
    {
        public Фирми_Руководитель_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a41",
             new string[] { "col_h2", "col_h3" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_h2"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_h2"].ToString()) : DateTime.MinValue;
                record.Руководитель = new Довідники.Сотрудники_Pointer(fieldValue["col_h3"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_h2", record.Дата);
                    fieldValue.Add("col_h3", record.Руководитель.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Руководитель = new Довідники.Сотрудники_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, Довідники.Сотрудники_Pointer _Руководитель = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Руководитель = _Руководитель ?? new Довідники.Сотрудники_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public Довідники.Сотрудники_Pointer Руководитель { get; set; }
            
        }
    }
      
    class Фирми_ЮридическийАдрес_TablePart : DirectoryTablePart
    {
        public Фирми_ЮридическийАдрес_TablePart(Фирми_Objest owner) : base(Config.Kernel, "tab_a42",
             new string[] { "col_h4", "col_h5" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Фирми_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_h4"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_h4"].ToString()) : DateTime.MinValue;
                record.ЮридическийАдрес = fieldValue["col_h5"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_h4", record.Дата);
                    fieldValue.Add("col_h5", record.ЮридическийАдрес);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                ЮридическийАдрес = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, string _ЮридическийАдрес = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                ЮридическийАдрес = _ЮридическийАдрес;
                
            }
            public DateTime Дата { get; set; }
            public string ЮридическийАдрес { get; set; }
            
        }
    }
      
    class Фирми_Список_View : DirectoryView
    {
        public Фирми_Список_View() : base(Config.Kernel, "tab_a31", 
             new string[] { "col_e9", "col_e4", "col_e6", "col_e2" },
             new string[] { "Назва", "Руководитель", "Телефони", "ПочтовийАдрес" },
             new string[] { "string", "pointer", "string", "string" },
             "Довідники.Фирми_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Цени"
    ///<summary>
    ///Цены товаров.
    ///</summary>
    class Цени_Objest : DirectoryObject
    {
        public Цени_Objest() : base(Config.Kernel, "tab_a43",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            Валюта = new Довідники.Валюти_Pointer();
            Единица = new Довідники.Единици_Pointer();
            КатегорияЦени = new Довідники.КатегорииЦен_Pointer();
            Наценка = 0;
            Цена = 0;
            Назва = "";
            Код = "";
            
            //Табличні частини
            Валюта_TablePart = new Цени_Валюта_TablePart(this);
            Единица_TablePart = new Цени_Единица_TablePart(this);
            Цена_TablePart = new Цени_Цена_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_a1"]);
                Единица = new Довідники.Единици_Pointer(base.FieldValue["col_a2"]);
                КатегорияЦени = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_a3"]);
                Наценка = (base.FieldValue["col_a4"] != DBNull.Value) ? (decimal)base.FieldValue["col_a4"] : 0;
                Цена = (base.FieldValue["col_a5"] != DBNull.Value) ? (decimal)base.FieldValue["col_a5"] : 0;
                Назва = base.FieldValue["col_a6"].ToString();
                Код = base.FieldValue["col_a7"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Валюта.ToString();
            base.FieldValue["col_a2"] = Единица.ToString();
            base.FieldValue["col_a3"] = КатегорияЦени.ToString();
            base.FieldValue["col_a4"] = Наценка;
            base.FieldValue["col_a5"] = Цена;
            base.FieldValue["col_a6"] = Назва;
            base.FieldValue["col_a7"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Цени_Pointer GetDirectoryPointer()
        {
            Цени_Pointer directoryPointer = new Цени_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public Довідники.Единици_Pointer Единица { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦени { get; set; }
        public decimal Наценка { get; set; }
        public decimal Цена { get; set; }
        public string Назва { get; set; }
        public string Код { get; set; }
        
        //Табличні частини
        public Цени_Валюта_TablePart Валюта_TablePart { get; set; }
        public Цени_Единица_TablePart Единица_TablePart { get; set; }
        public Цени_Цена_TablePart Цена_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Цены товаров.
    ///</summary>
    class Цени_Pointer : DirectoryPointer
    {
        public Цени_Pointer(object uid = null) : base(Config.Kernel, "tab_a43")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Цени_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a43")
        {
            base.Init(uid, fields);
        }
        
        public Цени_Objest GetDirectoryObject()
        {
            Цени_Objest ЦениObjestItem = new Цени_Objest();
            ЦениObjestItem.Read(base.UnigueID);
            return ЦениObjestItem;
        }
    }
    
    ///<summary>
    ///Цены товаров.
    ///</summary>
    class Цени_Select : DirectorySelect, IDisposable
    {
        public Цени_Select() : base(Config.Kernel, "tab_a43",
            new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" },
            new string[] { "Валюта", "Единица", "КатегорияЦени", "Наценка", "Цена", "Назва", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Цени_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Цени_Pointer Current { get; private set; }
        
        public Цени_Pointer FindByField(string name, object value)
        {
            Цени_Pointer itemPointer = new Цени_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Цени_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Цени_Pointer> directoryPointerList = new List<Цени_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Цени_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Цени_Валюта_TablePart : DirectoryTablePart
    {
        public Цени_Валюта_TablePart(Цени_Objest owner) : base(Config.Kernel, "tab_a44",
             new string[] { "col_a6", "col_a7" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Цени_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a6"].ToString()) : DateTime.MinValue;
                record.Валюта = new Довідники.Валюти_Pointer(fieldValue["col_a7"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a6", record.Дата);
                    fieldValue.Add("col_a7", record.Валюта.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Валюта = new Довідники.Валюти_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, Довідники.Валюти_Pointer _Валюта = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Валюта = _Валюта ?? new Довідники.Валюти_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public Довідники.Валюти_Pointer Валюта { get; set; }
            
        }
    }
      
    class Цени_Единица_TablePart : DirectoryTablePart
    {
        public Цени_Единица_TablePart(Цени_Objest owner) : base(Config.Kernel, "tab_a45",
             new string[] { "col_a8", "col_a9" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Цени_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_a8"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a8"].ToString()) : DateTime.MinValue;
                record.Единица = new Довідники.Единици_Pointer(fieldValue["col_a9"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a8", record.Дата);
                    fieldValue.Add("col_a9", record.Единица.ToString());
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Единица = new Довідники.Единици_Pointer();
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, Довідники.Единици_Pointer _Единица = null)
            {
                Дата = _Дата ?? DateTime.MinValue;
                Единица = _Единица ?? new Довідники.Единици_Pointer();
                
            }
            public DateTime Дата { get; set; }
            public Довідники.Единици_Pointer Единица { get; set; }
            
        }
    }
      
    class Цени_Цена_TablePart : DirectoryTablePart
    {
        public Цени_Цена_TablePart(Цени_Objest owner) : base(Config.Kernel, "tab_a46",
             new string[] { "col_b1", "col_b2", "col_a1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Цени_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Дата = (fieldValue["col_b1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_b1"].ToString()) : DateTime.MinValue;
                record.Цена = (fieldValue["col_b2"] != DBNull.Value) ? (decimal)fieldValue["col_b2"] : 0;
                record.Інфо = fieldValue["col_a1"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_b1", record.Дата);
                    fieldValue.Add("col_b2", record.Цена);
                    fieldValue.Add("col_a1", record.Інфо);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DirectoryTablePartRecord
        {
            public Record()
            {
                Дата = DateTime.MinValue;
                Цена = 0;
                Інфо = "";
                
            }
        
            
            public Record(
                DateTime?  _Дата = null, decimal _Цена = 0, string _Інфо = "")
            {
                Дата = _Дата ?? DateTime.MinValue;
                Цена = _Цена;
                Інфо = _Інфо;
                
            }
            public DateTime Дата { get; set; }
            public decimal Цена { get; set; }
            public string Інфо { get; set; }
            
        }
    }
      
    class Цени_Список_View : DirectoryView
    {
        public Цени_Список_View() : base(Config.Kernel, "tab_a43", 
             new string[] { "col_a6", "col_a1", "col_a3", "col_a4" },
             new string[] { "Назва", "Валюта", "КатегорияЦени", "Наценка" },
             new string[] { "string", "pointer", "pointer", "numeric" },
             "Довідники.Цени_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Групи_Номенклатура"
    
    class Групи_Номенклатура_Objest : DirectoryObject
    {
        public Групи_Номенклатура_Objest() : base(Config.Kernel, "tab_a01",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Родитель = new Довідники.Групи_Номенклатура_Pointer();
            Код = "";
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Родитель = new Довідники.Групи_Номенклатура_Pointer(base.FieldValue["col_a2"]);
                Код = base.FieldValue["col_a3"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Родитель.ToString();
            base.FieldValue["col_a3"] = Код;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Групи_Номенклатура_Pointer GetDirectoryPointer()
        {
            Групи_Номенклатура_Pointer directoryPointer = new Групи_Номенклатура_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public Довідники.Групи_Номенклатура_Pointer Родитель { get; set; }
        public string Код { get; set; }
        
    }
    
    
    class Групи_Номенклатура_Pointer : DirectoryPointer
    {
        public Групи_Номенклатура_Pointer(object uid = null) : base(Config.Kernel, "tab_a01")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Групи_Номенклатура_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a01")
        {
            base.Init(uid, fields);
        }
        
        public Групи_Номенклатура_Objest GetDirectoryObject()
        {
            Групи_Номенклатура_Objest Групи_НоменклатураObjestItem = new Групи_Номенклатура_Objest();
            Групи_НоменклатураObjestItem.Read(base.UnigueID);
            return Групи_НоменклатураObjestItem;
        }
    }
    
    
    class Групи_Номенклатура_Select : DirectorySelect, IDisposable
    {
        public Групи_Номенклатура_Select() : base(Config.Kernel, "tab_a01",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Родитель", "Код" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Групи_Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Групи_Номенклатура_Pointer Current { get; private set; }
        
        public Групи_Номенклатура_Pointer FindByField(string name, object value)
        {
            Групи_Номенклатура_Pointer itemPointer = new Групи_Номенклатура_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Групи_Номенклатура_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Групи_Номенклатура_Pointer> directoryPointerList = new List<Групи_Номенклатура_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Групи_Номенклатура_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Групи_Номенклатура_Список_View : DirectoryView
    {
        public Групи_Номенклатура_Список_View() : base(Config.Kernel, "tab_a01", 
             new string[] { "col_a1", "col_a2" },
             new string[] { "Назва", "Родитель" },
             new string[] { "string", "pointer" },
             "Довідники.Групи_Номенклатура_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
    #region DIRECTORY "Групи_МестаХранения"
    
    class Групи_МестаХранения_Objest : DirectoryObject
    {
        public Групи_МестаХранения_Objest() : base(Config.Kernel, "tab_a15",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Назва = "";
            Код = "";
            Родитель = new Довідники.МестаХранения_Pointer();
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["col_a1"].ToString();
                Код = base.FieldValue["col_a2"].ToString();
                Родитель = new Довідники.МестаХранения_Pointer(base.FieldValue["col_a3"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Назва;
            base.FieldValue["col_a2"] = Код;
            base.FieldValue["col_a3"] = Родитель.ToString();
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Групи_МестаХранения_Pointer GetDirectoryPointer()
        {
            Групи_МестаХранения_Pointer directoryPointer = new Групи_МестаХранения_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string Назва { get; set; }
        public string Код { get; set; }
        public Довідники.МестаХранения_Pointer Родитель { get; set; }
        
    }
    
    
    class Групи_МестаХранения_Pointer : DirectoryPointer
    {
        public Групи_МестаХранения_Pointer(object uid = null) : base(Config.Kernel, "tab_a15")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Групи_МестаХранения_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a15")
        {
            base.Init(uid, fields);
        }
        
        public Групи_МестаХранения_Objest GetDirectoryObject()
        {
            Групи_МестаХранения_Objest Групи_МестаХраненияObjestItem = new Групи_МестаХранения_Objest();
            Групи_МестаХраненияObjestItem.Read(base.UnigueID);
            return Групи_МестаХраненияObjestItem;
        }
    }
    
    
    class Групи_МестаХранения_Select : DirectorySelect, IDisposable
    {
        public Групи_МестаХранения_Select() : base(Config.Kernel, "tab_a15",
            new string[] { "col_a1", "col_a2", "col_a3" },
            new string[] { "Назва", "Код", "Родитель" }) { }
    
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Групи_МестаХранения_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Групи_МестаХранения_Pointer Current { get; private set; }
        
        public Групи_МестаХранения_Pointer FindByField(string name, object value)
        {
            Групи_МестаХранения_Pointer itemPointer = new Групи_МестаХранения_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(base.Alias[name], value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Групи_МестаХранения_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Групи_МестаХранения_Pointer> directoryPointerList = new List<Групи_МестаХранения_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(base.Alias[name], value, limit, offset)) 
                directoryPointerList.Add(new Групи_МестаХранения_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
    class Групи_МестаХранения_Список_View : DirectoryView
    {
        public Групи_МестаХранения_Список_View() : base(Config.Kernel, "tab_a15", 
             new string[] { "col_a1", "col_a2", "col_a3" },
             new string[] { "Назва", "Код", "Родитель" },
             new string[] { "string", "string", "pointer" },
             "Довідники.Групи_МестаХранения_Список")
        {
            
        }
        
    }
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Перелічення
{
    ///<summary>
    ///test.
    ///</summary>
    public enum Перелічення
    {
         Один = 1,
         Два = 2,
         Три = 3
    }
    
    ///<summary>
    ///test.
    ///</summary>
    public enum Перелічення2
    {
         Один = 1,
         Два = 2,
         Три = 3
    }
    
    ///<summary>
    ///ВидыКонтрагентов.
    ///</summary>
    public enum ВидиКонтрагентов
    {
         Организация = 1,
         ЧастноеЛицо = 2,
         Нерезидент = 3,
         Безналоговые = 4
    }
    
    ///<summary>
    ///fgcchf.
    ///</summary>
    public enum test2
    {
         sdfsdfsd = 3,
         sdfsdf = 2,
         sdfsd = 1
    }
    
    ///<summary>
    ///Види товарів.
    ///</summary>
    public enum ВидиТоварів
    {
         Товар = 1,
         Послуга = 2,
         Бартер = 3
    }
    
    ///<summary>
    ///sfas.
    ///</summary>
    public enum dsfasdfas
    {
         asdasd = 15,
         asdas = 16,
         sdfsdfsd = 11,
         ass = 13,
         asas = 14,
         sfas = 17
    }
    
    
    public enum Список
    {
         Один = 1,
         Два = 2,
         Три = 3
    }
    
    
}

namespace ConfTrade_v1_1.Документи
{
    
    #region DOCUMENT "РасходнаяНакладная"
    
    
    class РасходнаяНакладная_Objest : DocumentObject
    {
        public РасходнаяНакладная_Objest() : base(Config.Kernel, "tab_a512",
             new string[] { "col_c3", "col_c2", "col_c1", "col_b9", "col_c4", "col_c5", "col_c6", "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_e1", "col_e2", "col_e3", "col_e4", "col_e5", "col_e6" }) 
        {
            Валюта = new Довідники.Валюти_Pointer();
            ДокументОснование = new EmptyPointer();
            Договор = new EmptyPointer();
            Контрагент = new Довідники.Контрагенти_Pointer();
            Дата_курса = DateTime.MinValue;
            Курс = 0;
            Глубина = 0;
            ДатаОплати = DateTime.MinValue;
            КатегорияЦен = new Довідники.КатегорииЦен_Pointer();
            ВидОперации = 0;
            Склад = new Довідники.МестаХранения_Pointer();
            ВидТорговли = "";
            ЗасчитиватьОплатуПоСлужДоговору = false;
            ДовСерия = "";
            ДовНомер = "";
            ДовДата = DateTime.MinValue;
            Отпустил = new Довідники.Сотрудники_Pointer();
            Получил = "";
            РежимПроведения = 0;
            Касса = new Довідники.НашиДенежниеСчета_Pointer();
            МестоСоставления = "";
            ПолучилПоДругомуДокументу = false;
            ДокументПолучил = "";
            ДатаДок = DateTime.MinValue;
            НомерДок = "";
            
            //Табличні частини
            Товари_TablePart = new РасходнаяНакладная_Товари_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_c3"]);
                ДокументОснование = new EmptyPointer();
                Договор = new EmptyPointer();
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_b9"]);
                Дата_курса = (base.FieldValue["col_c4"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_c4"].ToString()) : DateTime.MinValue;
                Курс = (base.FieldValue["col_c5"] != DBNull.Value) ? (decimal)base.FieldValue["col_c5"] : 0;
                Глубина = (base.FieldValue["col_c6"] != DBNull.Value) ? (int)base.FieldValue["col_c6"] : 0;
                ДатаОплати = (base.FieldValue["col_c7"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_c7"].ToString()) : DateTime.MinValue;
                КатегорияЦен = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_c8"]);
                ВидОперации = (base.FieldValue["col_c9"] != DBNull.Value) ? (int)base.FieldValue["col_c9"] : 0;
                Склад = new Довідники.МестаХранения_Pointer(base.FieldValue["col_d1"]);
                ВидТорговли = base.FieldValue["col_d2"].ToString();
                ЗасчитиватьОплатуПоСлужДоговору = (bool)base.FieldValue["col_d3"];
                ДовСерия = base.FieldValue["col_d4"].ToString();
                ДовНомер = base.FieldValue["col_d5"].ToString();
                ДовДата = (base.FieldValue["col_d6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_d6"].ToString()) : DateTime.MinValue;
                Отпустил = new Довідники.Сотрудники_Pointer(base.FieldValue["col_d7"]);
                Получил = base.FieldValue["col_d8"].ToString();
                РежимПроведения = (base.FieldValue["col_d9"] != DBNull.Value) ? (int)base.FieldValue["col_d9"] : 0;
                Касса = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_e1"]);
                МестоСоставления = base.FieldValue["col_e2"].ToString();
                ПолучилПоДругомуДокументу = (bool)base.FieldValue["col_e3"];
                ДокументПолучил = base.FieldValue["col_e4"].ToString();
                ДатаДок = (base.FieldValue["col_e5"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_e5"].ToString()) : DateTime.MinValue;
                НомерДок = base.FieldValue["col_e6"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c3"] = Валюта.ToString();
            base.FieldValue["col_c2"] = ДокументОснование.ToString();
            base.FieldValue["col_c1"] = Договор.ToString();
            base.FieldValue["col_b9"] = Контрагент.ToString();
            base.FieldValue["col_c4"] = Дата_курса;
            base.FieldValue["col_c5"] = Курс;
            base.FieldValue["col_c6"] = Глубина;
            base.FieldValue["col_c7"] = ДатаОплати;
            base.FieldValue["col_c8"] = КатегорияЦен.ToString();
            base.FieldValue["col_c9"] = ВидОперации;
            base.FieldValue["col_d1"] = Склад.ToString();
            base.FieldValue["col_d2"] = ВидТорговли;
            base.FieldValue["col_d3"] = ЗасчитиватьОплатуПоСлужДоговору;
            base.FieldValue["col_d4"] = ДовСерия;
            base.FieldValue["col_d5"] = ДовНомер;
            base.FieldValue["col_d6"] = ДовДата;
            base.FieldValue["col_d7"] = Отпустил.ToString();
            base.FieldValue["col_d8"] = Получил;
            base.FieldValue["col_d9"] = РежимПроведения;
            base.FieldValue["col_e1"] = Касса.ToString();
            base.FieldValue["col_e2"] = МестоСоставления;
            base.FieldValue["col_e3"] = ПолучилПоДругомуДокументу;
            base.FieldValue["col_e4"] = ДокументПолучил;
            base.FieldValue["col_e5"] = ДатаДок;
            base.FieldValue["col_e6"] = НомерДок;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public РасходнаяНакладная_Pointer GetDocumentPointer()
        {
            РасходнаяНакладная_Pointer directoryPointer = new РасходнаяНакладная_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public EmptyPointer ДокументОснование { get; set; }
        public EmptyPointer Договор { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public DateTime Дата_курса { get; set; }
        public decimal Курс { get; set; }
        public int Глубина { get; set; }
        public DateTime ДатаОплати { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦен { get; set; }
        public int ВидОперации { get; set; }
        public Довідники.МестаХранения_Pointer Склад { get; set; }
        public string ВидТорговли { get; set; }
        public bool ЗасчитиватьОплатуПоСлужДоговору { get; set; }
        public string ДовСерия { get; set; }
        public string ДовНомер { get; set; }
        public DateTime ДовДата { get; set; }
        public Довідники.Сотрудники_Pointer Отпустил { get; set; }
        public string Получил { get; set; }
        public int РежимПроведения { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer Касса { get; set; }
        public string МестоСоставления { get; set; }
        public bool ПолучилПоДругомуДокументу { get; set; }
        public string ДокументПолучил { get; set; }
        public DateTime ДатаДок { get; set; }
        public string НомерДок { get; set; }
        
        //Табличні частини
        public РасходнаяНакладная_Товари_TablePart Товари_TablePart { get; set; }
        
    }
    
    
    class РасходнаяНакладная_Pointer : DocumentPointer
    {
        public РасходнаяНакладная_Pointer(object uid = null) : base(Config.Kernel, "tab_a512")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public РасходнаяНакладная_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a512")
        {
            base.Init(uid, fields);
        } 
        
        public РасходнаяНакладная_Objest GetDocumentObject()
        {
            РасходнаяНакладная_Objest РасходнаяНакладнаяObjestItem = new РасходнаяНакладная_Objest();
            РасходнаяНакладнаяObjestItem.Read(base.UnigueID);
            return РасходнаяНакладнаяObjestItem;
        }
    }
    
    
    class РасходнаяНакладная_Select : DocumentSelect, IDisposable
    {
        public РасходнаяНакладная_Select() : base(Config.Kernel, "tab_a512") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new РасходнаяНакладная_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public РасходнаяНакладная_Pointer Current { get; private set; }
    }
    
      
    class РасходнаяНакладная_Товари_TablePart : DocumentTablePart
    {
        public РасходнаяНакладная_Товари_TablePart(РасходнаяНакладная_Objest owner) : base(Config.Kernel, "tab_a53",
             new string[] { "col_b1", "col_b2", "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1", "col_c2", "col_c3" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public РасходнаяНакладная_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_b1"]);
                record.Предпочтение = new EmptyPointer();
                record.Единица = new Довідники.Единици_Pointer(fieldValue["col_b3"]);
                record.Количество = (fieldValue["col_b4"] != DBNull.Value) ? (decimal)fieldValue["col_b4"] : 0;
                record.Коеффициент = (fieldValue["col_b5"] != DBNull.Value) ? (decimal)fieldValue["col_b5"] : 0;
                record.ЦенаБезНДС = (fieldValue["col_b6"] != DBNull.Value) ? (decimal)fieldValue["col_b6"] : 0;
                record.ЦенаСНДС = (fieldValue["col_b7"] != DBNull.Value) ? (decimal)fieldValue["col_b7"] : 0;
                record.СуммаБезСкидки = (fieldValue["col_b8"] != DBNull.Value) ? (decimal)fieldValue["col_b8"] : 0;
                record.СуммаСкидки = (fieldValue["col_b9"] != DBNull.Value) ? (decimal)fieldValue["col_b9"] : 0;
                record.СуммаБезНДС = (fieldValue["col_c1"] != DBNull.Value) ? (decimal)fieldValue["col_c1"] : 0;
                record.СуммаСНДС = (fieldValue["col_c2"] != DBNull.Value) ? (decimal)fieldValue["col_c2"] : 0;
                record.Набор = new Довідники.Номенклатура_Pointer(fieldValue["col_c3"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_b1", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_b2", record.Предпочтение.UnigueID.UGuid);
                    fieldValue.Add("col_b3", record.Единица.UnigueID.UGuid);
                    fieldValue.Add("col_b4", record.Количество);
                    fieldValue.Add("col_b5", record.Коеффициент);
                    fieldValue.Add("col_b6", record.ЦенаБезНДС);
                    fieldValue.Add("col_b7", record.ЦенаСНДС);
                    fieldValue.Add("col_b8", record.СуммаБезСкидки);
                    fieldValue.Add("col_b9", record.СуммаСкидки);
                    fieldValue.Add("col_c1", record.СуммаБезНДС);
                    fieldValue.Add("col_c2", record.СуммаСНДС);
                    fieldValue.Add("col_c3", record.Набор.UnigueID.UGuid);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                Предпочтение = new EmptyPointer();
                Единица = new Довідники.Единици_Pointer();
                Количество = 0;
                Коеффициент = 0;
                ЦенаБезНДС = 0;
                ЦенаСНДС = 0;
                СуммаБезСкидки = 0;
                СуммаСкидки = 0;
                СуммаБезНДС = 0;
                СуммаСНДС = 0;
                Набор = new Довідники.Номенклатура_Pointer();
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Товар = null, EmptyPointer _Предпочтение = null, Довідники.Единици_Pointer _Единица = null, decimal _Количество = 0, decimal _Коеффициент = 0, decimal _ЦенаБезНДС = 0, decimal _ЦенаСНДС = 0, decimal _СуммаБезСкидки = 0, decimal _СуммаСкидки = 0, decimal _СуммаБезНДС = 0, decimal _СуммаСНДС = 0, Довідники.Номенклатура_Pointer _Набор = null)
            {
                Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
                Предпочтение = _Предпочтение ?? new EmptyPointer();
                Единица = _Единица ?? new Довідники.Единици_Pointer();
                Количество = _Количество;
                Коеффициент = _Коеффициент;
                ЦенаБезНДС = _ЦенаБезНДС;
                ЦенаСНДС = _ЦенаСНДС;
                СуммаБезСкидки = _СуммаБезСкидки;
                СуммаСкидки = _СуммаСкидки;
                СуммаБезНДС = _СуммаБезНДС;
                СуммаСНДС = _СуммаСНДС;
                Набор = _Набор ?? new Довідники.Номенклатура_Pointer();
                
            }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public EmptyPointer Предпочтение { get; set; }
            public Довідники.Единици_Pointer Единица { get; set; }
            public decimal Количество { get; set; }
            public decimal Коеффициент { get; set; }
            public decimal ЦенаБезНДС { get; set; }
            public decimal ЦенаСНДС { get; set; }
            public decimal СуммаБезСкидки { get; set; }
            public decimal СуммаСкидки { get; set; }
            public decimal СуммаБезНДС { get; set; }
            public decimal СуммаСНДС { get; set; }
            public Довідники.Номенклатура_Pointer Набор { get; set; }
            
        }
    }
      
    
    #endregion
    
    #region DOCUMENT "ПриходнаяНакладная"
    
    ///<summary>
    ///Приходная накладная.
    ///</summary>
    class ПриходнаяНакладная_Objest : DocumentObject
    {
        public ПриходнаяНакладная_Objest() : base(Config.Kernel, "tab_a512r4",
             new string[] { "col_a3", "col_a2", "col_a1", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4", "col_b5", "col_b6", "col_b7", "col_b8" }) 
        {
            Контрагент = new Довідники.Контрагенти_Pointer();
            ДатаДок = DateTime.MinValue;
            НомерДок = "";
            Договор = new EmptyPointer();
            ДокументОснование = new EmptyPointer();
            Валюта = new Довідники.Валюти_Pointer();
            Дата_курса = DateTime.MinValue;
            Курс = 0;
            Глубина = 0;
            ДатаОплати = DateTime.MinValue;
            КатегорияЦен = new Довідники.КатегорииЦен_Pointer();
            ВидОперации = 0;
            Склад = new Довідники.МестаХранения_Pointer();
            ВидТорговли = "";
            ЗасчитиватьОплатуПоСлужДоговору = 0;
            Касса = new Довідники.НашиДенежниеСчета_Pointer();
            НомерРасходнойНакладной = "";
            
            //Табличні частини
            Товари_TablePart = new ПриходнаяНакладная_Товари_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a3"]);
                ДатаДок = (base.FieldValue["col_a2"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a2"].ToString()) : DateTime.MinValue;
                НомерДок = base.FieldValue["col_a1"].ToString();
                Договор = new EmptyPointer();
                ДокументОснование = new EmptyPointer();
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_a6"]);
                Дата_курса = (base.FieldValue["col_a7"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a7"].ToString()) : DateTime.MinValue;
                Курс = (base.FieldValue["col_a8"] != DBNull.Value) ? (decimal)base.FieldValue["col_a8"] : 0;
                Глубина = (base.FieldValue["col_a9"] != DBNull.Value) ? (int)base.FieldValue["col_a9"] : 0;
                ДатаОплати = (base.FieldValue["col_b1"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_b1"].ToString()) : DateTime.MinValue;
                КатегорияЦен = new Довідники.КатегорииЦен_Pointer(base.FieldValue["col_b2"]);
                ВидОперации = (base.FieldValue["col_b3"] != DBNull.Value) ? (int)base.FieldValue["col_b3"] : 0;
                Склад = new Довідники.МестаХранения_Pointer(base.FieldValue["col_b4"]);
                ВидТорговли = base.FieldValue["col_b5"].ToString();
                ЗасчитиватьОплатуПоСлужДоговору = (base.FieldValue["col_b6"] != DBNull.Value) ? (int)base.FieldValue["col_b6"] : 0;
                Касса = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_b7"]);
                НомерРасходнойНакладной = base.FieldValue["col_b8"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a3"] = Контрагент.ToString();
            base.FieldValue["col_a2"] = ДатаДок;
            base.FieldValue["col_a1"] = НомерДок;
            base.FieldValue["col_a4"] = Договор.ToString();
            base.FieldValue["col_a5"] = ДокументОснование.ToString();
            base.FieldValue["col_a6"] = Валюта.ToString();
            base.FieldValue["col_a7"] = Дата_курса;
            base.FieldValue["col_a8"] = Курс;
            base.FieldValue["col_a9"] = Глубина;
            base.FieldValue["col_b1"] = ДатаОплати;
            base.FieldValue["col_b2"] = КатегорияЦен.ToString();
            base.FieldValue["col_b3"] = ВидОперации;
            base.FieldValue["col_b4"] = Склад.ToString();
            base.FieldValue["col_b5"] = ВидТорговли;
            base.FieldValue["col_b6"] = ЗасчитиватьОплатуПоСлужДоговору;
            base.FieldValue["col_b7"] = Касса.ToString();
            base.FieldValue["col_b8"] = НомерРасходнойНакладной;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ПриходнаяНакладная_Pointer GetDocumentPointer()
        {
            ПриходнаяНакладная_Pointer directoryPointer = new ПриходнаяНакладная_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public DateTime ДатаДок { get; set; }
        public string НомерДок { get; set; }
        public EmptyPointer Договор { get; set; }
        public EmptyPointer ДокументОснование { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public DateTime Дата_курса { get; set; }
        public decimal Курс { get; set; }
        public int Глубина { get; set; }
        public DateTime ДатаОплати { get; set; }
        public Довідники.КатегорииЦен_Pointer КатегорияЦен { get; set; }
        public int ВидОперации { get; set; }
        public Довідники.МестаХранения_Pointer Склад { get; set; }
        public string ВидТорговли { get; set; }
        public int ЗасчитиватьОплатуПоСлужДоговору { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer Касса { get; set; }
        public string НомерРасходнойНакладной { get; set; }
        
        //Табличні частини
        public ПриходнаяНакладная_Товари_TablePart Товари_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Приходная накладная.
    ///</summary>
    class ПриходнаяНакладная_Pointer : DocumentPointer
    {
        public ПриходнаяНакладная_Pointer(object uid = null) : base(Config.Kernel, "tab_a512r4")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПриходнаяНакладная_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a512r4")
        {
            base.Init(uid, fields);
        } 
        
        public ПриходнаяНакладная_Objest GetDocumentObject()
        {
            ПриходнаяНакладная_Objest ПриходнаяНакладнаяObjestItem = new ПриходнаяНакладная_Objest();
            ПриходнаяНакладнаяObjestItem.Read(base.UnigueID);
            return ПриходнаяНакладнаяObjestItem;
        }
    }
    
    ///<summary>
    ///Приходная накладная.
    ///</summary>
    class ПриходнаяНакладная_Select : DocumentSelect, IDisposable
    {
        public ПриходнаяНакладная_Select() : base(Config.Kernel, "tab_a512r4") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ПриходнаяНакладная_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public ПриходнаяНакладная_Pointer Current { get; private set; }
    }
    
      
    class ПриходнаяНакладная_Товари_TablePart : DocumentTablePart
    {
        public ПриходнаяНакладная_Товари_TablePart(ПриходнаяНакладная_Objest owner) : base(Config.Kernel, "tab_a52",
             new string[] { "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public ПриходнаяНакладная_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a2"]);
                record.Единица = new Довідники.Единици_Pointer(fieldValue["col_a3"]);
                record.Количество = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                record.Коеффициент = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0;
                record.ЦенаБезНДС = (fieldValue["col_a6"] != DBNull.Value) ? (decimal)fieldValue["col_a6"] : 0;
                record.ЦенаСНДС = (fieldValue["col_a7"] != DBNull.Value) ? (decimal)fieldValue["col_a7"] : 0;
                record.СуммаБезНДС = (fieldValue["col_a8"] != DBNull.Value) ? (decimal)fieldValue["col_a8"] : 0;
                record.СуммаСНДС = (fieldValue["col_a9"] != DBNull.Value) ? (decimal)fieldValue["col_a9"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a2", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_a3", record.Единица.UnigueID.UGuid);
                    fieldValue.Add("col_a4", record.Количество);
                    fieldValue.Add("col_a5", record.Коеффициент);
                    fieldValue.Add("col_a6", record.ЦенаБезНДС);
                    fieldValue.Add("col_a7", record.ЦенаСНДС);
                    fieldValue.Add("col_a8", record.СуммаБезНДС);
                    fieldValue.Add("col_a9", record.СуммаСНДС);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                Единица = new Довідники.Единици_Pointer();
                Количество = 0;
                Коеффициент = 0;
                ЦенаБезНДС = 0;
                ЦенаСНДС = 0;
                СуммаБезНДС = 0;
                СуммаСНДС = 0;
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Товар = null, Довідники.Единици_Pointer _Единица = null, decimal _Количество = 0, decimal _Коеффициент = 0, decimal _ЦенаБезНДС = 0, decimal _ЦенаСНДС = 0, decimal _СуммаБезНДС = 0, decimal _СуммаСНДС = 0)
            {
                Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
                Единица = _Единица ?? new Довідники.Единици_Pointer();
                Количество = _Количество;
                Коеффициент = _Коеффициент;
                ЦенаБезНДС = _ЦенаБезНДС;
                ЦенаСНДС = _ЦенаСНДС;
                СуммаБезНДС = _СуммаБезНДС;
                СуммаСНДС = _СуммаСНДС;
                
            }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.Единици_Pointer Единица { get; set; }
            public decimal Количество { get; set; }
            public decimal Коеффициент { get; set; }
            public decimal ЦенаБезНДС { get; set; }
            public decimal ЦенаСНДС { get; set; }
            public decimal СуммаБезНДС { get; set; }
            public decimal СуммаСНДС { get; set; }
            
        }
    }
      
    
    #endregion
    
    #region DOCUMENT "ПрихіднийКасовийОрдер"
    
    ///<summary>
    ///Прихідний касовий ордер.
    ///</summary>
    class ПрихіднийКасовийОрдер_Objest : DocumentObject
    {
        public ПрихіднийКасовийОрдер_Objest() : base(Config.Kernel, "tab_a522",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Контрагент = new Довідники.Контрагенти_Pointer();
            Каса = new Довідники.НашиДенежниеСчета_Pointer();
            Сума = 0;
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a1"]);
                Каса = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_a2"]);
                Сума = (base.FieldValue["col_a3"] != DBNull.Value) ? (decimal)base.FieldValue["col_a3"] : 0;
                ДатаДок = (base.FieldValue["col_a4"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a4"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a5"] != DBNull.Value) ? (int)base.FieldValue["col_a5"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = Контрагент.ToString();
            base.FieldValue["col_a2"] = Каса.ToString();
            base.FieldValue["col_a3"] = Сума;
            base.FieldValue["col_a4"] = ДатаДок;
            base.FieldValue["col_a5"] = НомерДок;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public ПрихіднийКасовийОрдер_Pointer GetDocumentPointer()
        {
            ПрихіднийКасовийОрдер_Pointer directoryPointer = new ПрихіднийКасовийОрдер_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer Каса { get; set; }
        public decimal Сума { get; set; }
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        
    }
    
    ///<summary>
    ///Прихідний касовий ордер.
    ///</summary>
    class ПрихіднийКасовийОрдер_Pointer : DocumentPointer
    {
        public ПрихіднийКасовийОрдер_Pointer(object uid = null) : base(Config.Kernel, "tab_a522")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПрихіднийКасовийОрдер_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a522")
        {
            base.Init(uid, fields);
        } 
        
        public ПрихіднийКасовийОрдер_Objest GetDocumentObject()
        {
            ПрихіднийКасовийОрдер_Objest ПрихіднийКасовийОрдерObjestItem = new ПрихіднийКасовийОрдер_Objest();
            ПрихіднийКасовийОрдерObjestItem.Read(base.UnigueID);
            return ПрихіднийКасовийОрдерObjestItem;
        }
    }
    
    ///<summary>
    ///Прихідний касовий ордер.
    ///</summary>
    class ПрихіднийКасовийОрдер_Select : DocumentSelect, IDisposable
    {
        public ПрихіднийКасовийОрдер_Select() : base(Config.Kernel, "tab_a522") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ПрихіднийКасовийОрдер_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public ПрихіднийКасовийОрдер_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DOCUMENT "РозхіднийКасовийОрдер"
    
    
    class РозхіднийКасовийОрдер_Objest : DocumentObject
    {
        public РозхіднийКасовийОрдер_Objest() : base(Config.Kernel, "tab_a533",
             new string[] { "col_a6", "col_a7", "col_a8", "col_a9", "col_b1" }) 
        {
            Контрагент = new Довідники.Контрагенти_Pointer();
            Каса = new Довідники.НашиДенежниеСчета_Pointer();
            Сума = 0;
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a6"]);
                Каса = new Довідники.НашиДенежниеСчета_Pointer(base.FieldValue["col_a7"]);
                Сума = (base.FieldValue["col_a8"] != DBNull.Value) ? (decimal)base.FieldValue["col_a8"] : 0;
                ДатаДок = (base.FieldValue["col_a9"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a9"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_b1"] != DBNull.Value) ? (int)base.FieldValue["col_b1"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a6"] = Контрагент.ToString();
            base.FieldValue["col_a7"] = Каса.ToString();
            base.FieldValue["col_a8"] = Сума;
            base.FieldValue["col_a9"] = ДатаДок;
            base.FieldValue["col_b1"] = НомерДок;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public РозхіднийКасовийОрдер_Pointer GetDocumentPointer()
        {
            РозхіднийКасовийОрдер_Pointer directoryPointer = new РозхіднийКасовийОрдер_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.НашиДенежниеСчета_Pointer Каса { get; set; }
        public decimal Сума { get; set; }
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        
    }
    
    
    class РозхіднийКасовийОрдер_Pointer : DocumentPointer
    {
        public РозхіднийКасовийОрдер_Pointer(object uid = null) : base(Config.Kernel, "tab_a533")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public РозхіднийКасовийОрдер_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a533")
        {
            base.Init(uid, fields);
        } 
        
        public РозхіднийКасовийОрдер_Objest GetDocumentObject()
        {
            РозхіднийКасовийОрдер_Objest РозхіднийКасовийОрдерObjestItem = new РозхіднийКасовийОрдер_Objest();
            РозхіднийКасовийОрдерObjestItem.Read(base.UnigueID);
            return РозхіднийКасовийОрдерObjestItem;
        }
    }
    
    
    class РозхіднийКасовийОрдер_Select : DocumentSelect, IDisposable
    {
        public РозхіднийКасовийОрдер_Select() : base(Config.Kernel, "tab_a533") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new РозхіднийКасовийОрдер_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public РозхіднийКасовийОрдер_Pointer Current { get; private set; }
    }
    
      
    
    #endregion
    
    #region DOCUMENT "Инвентаризация"
    
    ///<summary>
    ///Инвентаризация товаров.
    ///</summary>
    class Инвентаризация_Objest : DocumentObject
    {
        public Инвентаризация_Objest() : base(Config.Kernel, "tab_a52565",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            Склад = new Довідники.МестаХранения_Pointer();
            Контрагент = new Довідники.Контрагенти_Pointer();
            Валюта = new Довідники.Валюти_Pointer();
            Дата_курса = DateTime.MinValue;
            Курс = 0;
            ТипИнвентаризации = 0;
            ПредседательКомиссии = new Довідники.Сотрудники_Pointer();
            ЧленКомиссии1 = new Довідники.Сотрудники_Pointer();
            ЧленКомиссии2 = new Довідники.Сотрудники_Pointer();
            ЧленКомиссии3 = new Довідники.Сотрудники_Pointer();
            ПровелПроверку = new Довідники.Сотрудники_Pointer();
            
            //Табличні частини
            Товари_TablePart = new Инвентаризация_Товари_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a1"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Склад = new Довідники.МестаХранения_Pointer(base.FieldValue["col_a3"]);
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a4"]);
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_a5"]);
                Дата_курса = (base.FieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a6"].ToString()) : DateTime.MinValue;
                Курс = (base.FieldValue["col_a7"] != DBNull.Value) ? (decimal)base.FieldValue["col_a7"] : 0;
                ТипИнвентаризации = (base.FieldValue["col_a8"] != DBNull.Value) ? (int)base.FieldValue["col_a8"] : 0;
                ПредседательКомиссии = new Довідники.Сотрудники_Pointer(base.FieldValue["col_a9"]);
                ЧленКомиссии1 = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b1"]);
                ЧленКомиссии2 = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b2"]);
                ЧленКомиссии3 = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b3"]);
                ПровелПроверку = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b4"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = ДатаДок;
            base.FieldValue["col_a2"] = НомерДок;
            base.FieldValue["col_a3"] = Склад.ToString();
            base.FieldValue["col_a4"] = Контрагент.ToString();
            base.FieldValue["col_a5"] = Валюта.ToString();
            base.FieldValue["col_a6"] = Дата_курса;
            base.FieldValue["col_a7"] = Курс;
            base.FieldValue["col_a8"] = ТипИнвентаризации;
            base.FieldValue["col_a9"] = ПредседательКомиссии.ToString();
            base.FieldValue["col_b1"] = ЧленКомиссии1.ToString();
            base.FieldValue["col_b2"] = ЧленКомиссии2.ToString();
            base.FieldValue["col_b3"] = ЧленКомиссии3.ToString();
            base.FieldValue["col_b4"] = ПровелПроверку.ToString();
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Инвентаризация_Pointer GetDocumentPointer()
        {
            Инвентаризация_Pointer directoryPointer = new Инвентаризация_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        public Довідники.МестаХранения_Pointer Склад { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public DateTime Дата_курса { get; set; }
        public decimal Курс { get; set; }
        public int ТипИнвентаризации { get; set; }
        public Довідники.Сотрудники_Pointer ПредседательКомиссии { get; set; }
        public Довідники.Сотрудники_Pointer ЧленКомиссии1 { get; set; }
        public Довідники.Сотрудники_Pointer ЧленКомиссии2 { get; set; }
        public Довідники.Сотрудники_Pointer ЧленКомиссии3 { get; set; }
        public Довідники.Сотрудники_Pointer ПровелПроверку { get; set; }
        
        //Табличні частини
        public Инвентаризация_Товари_TablePart Товари_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Инвентаризация товаров.
    ///</summary>
    class Инвентаризация_Pointer : DocumentPointer
    {
        public Инвентаризация_Pointer(object uid = null) : base(Config.Kernel, "tab_a52565")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Инвентаризация_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a52565")
        {
            base.Init(uid, fields);
        } 
        
        public Инвентаризация_Objest GetDocumentObject()
        {
            Инвентаризация_Objest ИнвентаризацияObjestItem = new Инвентаризация_Objest();
            ИнвентаризацияObjestItem.Read(base.UnigueID);
            return ИнвентаризацияObjestItem;
        }
    }
    
    ///<summary>
    ///Инвентаризация товаров.
    ///</summary>
    class Инвентаризация_Select : DocumentSelect, IDisposable
    {
        public Инвентаризация_Select() : base(Config.Kernel, "tab_a52565") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Инвентаризация_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public Инвентаризация_Pointer Current { get; private set; }
    }
    
      
    class Инвентаризация_Товари_TablePart : DocumentTablePart
    {
        public Инвентаризация_Товари_TablePart(Инвентаризация_Objest owner) : base(Config.Kernel, "tab_a5344",
             new string[] { "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Инвентаризация_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_b5"]);
                record.Единица = new Довідники.Единици_Pointer(fieldValue["col_b6"]);
                record.Количество = (fieldValue["col_b7"] != DBNull.Value) ? (decimal)fieldValue["col_b7"] : 0;
                record.Коеффициент = (fieldValue["col_b8"] != DBNull.Value) ? (decimal)fieldValue["col_b8"] : 0;
                record.ИнвКоличество = (fieldValue["col_b9"] != DBNull.Value) ? (decimal)fieldValue["col_b9"] : 0;
                record.Стоимость = (fieldValue["col_c1"] != DBNull.Value) ? (decimal)fieldValue["col_c1"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_b5", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_b6", record.Единица.UnigueID.UGuid);
                    fieldValue.Add("col_b7", record.Количество);
                    fieldValue.Add("col_b8", record.Коеффициент);
                    fieldValue.Add("col_b9", record.ИнвКоличество);
                    fieldValue.Add("col_c1", record.Стоимость);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                Единица = new Довідники.Единици_Pointer();
                Количество = 0;
                Коеффициент = 0;
                ИнвКоличество = 0;
                Стоимость = 0;
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Товар = null, Довідники.Единици_Pointer _Единица = null, decimal _Количество = 0, decimal _Коеффициент = 0, decimal _ИнвКоличество = 0, decimal _Стоимость = 0)
            {
                Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
                Единица = _Единица ?? new Довідники.Единици_Pointer();
                Количество = _Количество;
                Коеффициент = _Коеффициент;
                ИнвКоличество = _ИнвКоличество;
                Стоимость = _Стоимость;
                
            }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.Единици_Pointer Единица { get; set; }
            public decimal Количество { get; set; }
            public decimal Коеффициент { get; set; }
            public decimal ИнвКоличество { get; set; }
            public decimal Стоимость { get; set; }
            
        }
    }
      
    
    #endregion
    
    #region DOCUMENT "Договор"
    
    ///<summary>
    ///Договор с контрагентом.
    ///</summary>
    class Договор_Objest : DocumentObject
    {
        public Договор_Objest() : base(Config.Kernel, "tab_a55",
             new string[] { "col_c6", "col_c7", "col_c8", "col_c9", "col_d1", "col_d2", "col_d3", "col_d4", "col_d5", "col_d6", "col_d7", "col_d8", "col_d9", "col_a1", "col_a2" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            НомерДоговора = "";
            Контрагент = new Довідники.Контрагенти_Pointer();
            Валюта = new Довідники.Валюти_Pointer();
            Дата_курса = DateTime.MinValue;
            Курс = 0;
            ДатаНачала = DateTime.MinValue;
            ДатаОкончания = DateTime.MinValue;
            ВидТорговли = "";
            СпособФормированияНалоговихДокументов = 0;
            ПлательщикНалогаНаПрибиль = false;
            ВидДоговора = "";
            Ед = new Довідники.КлассификаторЕдИзм_Pointer();
            Один = 0;
            
            //Табличні частини
            Тест_TablePart = new Договор_Тест_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_c6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_c6"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_c7"] != DBNull.Value) ? (int)base.FieldValue["col_c7"] : 0;
                НомерДоговора = base.FieldValue["col_c8"].ToString();
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_c9"]);
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_d1"]);
                Дата_курса = (base.FieldValue["col_d2"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_d2"].ToString()) : DateTime.MinValue;
                Курс = (base.FieldValue["col_d3"] != DBNull.Value) ? (decimal)base.FieldValue["col_d3"] : 0;
                ДатаНачала = (base.FieldValue["col_d4"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_d4"].ToString()) : DateTime.MinValue;
                ДатаОкончания = (base.FieldValue["col_d5"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_d5"].ToString()) : DateTime.MinValue;
                ВидТорговли = base.FieldValue["col_d6"].ToString();
                СпособФормированияНалоговихДокументов = (base.FieldValue["col_d7"] != DBNull.Value) ? (int)base.FieldValue["col_d7"] : 0;
                ПлательщикНалогаНаПрибиль = (bool)base.FieldValue["col_d8"];
                ВидДоговора = base.FieldValue["col_d9"].ToString();
                Ед = new Довідники.КлассификаторЕдИзм_Pointer(base.FieldValue["col_a1"]);
                Один = (base.FieldValue["col_a2"] != DBNull.Value) ? (Перелічення.Список)base.FieldValue["col_a2"] : 0;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_c6"] = ДатаДок;
            base.FieldValue["col_c7"] = НомерДок;
            base.FieldValue["col_c8"] = НомерДоговора;
            base.FieldValue["col_c9"] = Контрагент.ToString();
            base.FieldValue["col_d1"] = Валюта.ToString();
            base.FieldValue["col_d2"] = Дата_курса;
            base.FieldValue["col_d3"] = Курс;
            base.FieldValue["col_d4"] = ДатаНачала;
            base.FieldValue["col_d5"] = ДатаОкончания;
            base.FieldValue["col_d6"] = ВидТорговли;
            base.FieldValue["col_d7"] = СпособФормированияНалоговихДокументов;
            base.FieldValue["col_d8"] = ПлательщикНалогаНаПрибиль;
            base.FieldValue["col_d9"] = ВидДоговора;
            base.FieldValue["col_a1"] = Ед.ToString();
            base.FieldValue["col_a2"] = (int)Один;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Договор_Pointer GetDocumentPointer()
        {
            Договор_Pointer directoryPointer = new Договор_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        public string НомерДоговора { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public DateTime Дата_курса { get; set; }
        public decimal Курс { get; set; }
        public DateTime ДатаНачала { get; set; }
        public DateTime ДатаОкончания { get; set; }
        public string ВидТорговли { get; set; }
        public int СпособФормированияНалоговихДокументов { get; set; }
        public bool ПлательщикНалогаНаПрибиль { get; set; }
        public string ВидДоговора { get; set; }
        public Довідники.КлассификаторЕдИзм_Pointer Ед { get; set; }
        public Перелічення.Список Один { get; set; }
        
        //Табличні частини
        public Договор_Тест_TablePart Тест_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Договор с контрагентом.
    ///</summary>
    class Договор_Pointer : DocumentPointer
    {
        public Договор_Pointer(object uid = null) : base(Config.Kernel, "tab_a55")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Договор_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a55")
        {
            base.Init(uid, fields);
        } 
        
        public Договор_Objest GetDocumentObject()
        {
            Договор_Objest ДоговорObjestItem = new Договор_Objest();
            ДоговорObjestItem.Read(base.UnigueID);
            return ДоговорObjestItem;
        }
    }
    
    ///<summary>
    ///Договор с контрагентом.
    ///</summary>
    class Договор_Select : DocumentSelect, IDisposable
    {
        public Договор_Select() : base(Config.Kernel, "tab_a55") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Договор_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public Договор_Pointer Current { get; private set; }
    }
    
      
    class Договор_Тест_TablePart : DocumentTablePart
    {
        public Договор_Тест_TablePart(Договор_Objest owner) : base(Config.Kernel, "tab_a64",
             new string[] { "col_a1", "col_a2" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Договор_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Ед = new Довідники.КлассификаторЕдИзм_Pointer(fieldValue["col_a1"]);
                record.Один = (fieldValue["col_a2"] != DBNull.Value) ? (Перелічення.Список)fieldValue["col_a2"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Ед.UnigueID.UGuid);
                    fieldValue.Add("col_a2", record.Один);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Ед = new Довідники.КлассификаторЕдИзм_Pointer();
                Один = 0;
                
            }
        
            
            public Record(
                Довідники.КлассификаторЕдИзм_Pointer _Ед = null, Перелічення.Список _Один = 0)
            {
                Ед = _Ед ?? new Довідники.КлассификаторЕдИзм_Pointer();
                Один = _Один;
                
            }
            public Довідники.КлассификаторЕдИзм_Pointer Ед { get; set; }
            public Перелічення.Список Один { get; set; }
            
        }
    }
      
    
    #endregion
    
    #region DOCUMENT "Договір2"
    
    
    class Договір2_Objest : DocumentObject
    {
        public Договір2_Objest() : base(Config.Kernel, "tab_a72",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            Контрагент = new Довідники.Контрагенти_Pointer();
            Договір = new Документи.Договор_Pointer();
            Активність = false;
            ОстанняРеакція = DateTime.MinValue;
            Коментар = "";
            
            //Табличні частини
            Товари_TablePart = new Договір2_Товари_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a1"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a3"]);
                Договір = new Документи.Договор_Pointer(base.FieldValue["col_a4"]);
                Активність = (bool)base.FieldValue["col_a5"];
                ОстанняРеакція = (base.FieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a6"].ToString()) : DateTime.MinValue;
                Коментар = base.FieldValue["col_a7"].ToString();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = ДатаДок;
            base.FieldValue["col_a2"] = НомерДок;
            base.FieldValue["col_a3"] = Контрагент.ToString();
            base.FieldValue["col_a4"] = Договір.ToString();
            base.FieldValue["col_a5"] = Активність;
            base.FieldValue["col_a6"] = ОстанняРеакція;
            base.FieldValue["col_a7"] = Коментар;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Договір2_Pointer GetDocumentPointer()
        {
            Договір2_Pointer directoryPointer = new Договір2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Документи.Договор_Pointer Договір { get; set; }
        public bool Активність { get; set; }
        public DateTime ОстанняРеакція { get; set; }
        public string Коментар { get; set; }
        
        //Табличні частини
        public Договір2_Товари_TablePart Товари_TablePart { get; set; }
        
    }
    
    
    class Договір2_Pointer : DocumentPointer
    {
        public Договір2_Pointer(object uid = null) : base(Config.Kernel, "tab_a72")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Договір2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a72")
        {
            base.Init(uid, fields);
        } 
        
        public Договір2_Objest GetDocumentObject()
        {
            Договір2_Objest Договір2ObjestItem = new Договір2_Objest();
            Договір2ObjestItem.Read(base.UnigueID);
            return Договір2ObjestItem;
        }
    }
    
    
    class Договір2_Select : DocumentSelect, IDisposable
    {
        public Договір2_Select() : base(Config.Kernel, "tab_a72") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Договір2_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public Договір2_Pointer Current { get; private set; }
    }
    
      
    class Договір2_Товари_TablePart : DocumentTablePart
    {
        public Договір2_Товари_TablePart(Договір2_Objest owner) : base(Config.Kernel, "tab_a73",
             new string[] { "col_a4", "col_a1", "col_a2", "col_a3" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Договір2_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a4"]);
                record.Кво = (fieldValue["col_a1"] != DBNull.Value) ? (int)fieldValue["col_a1"] : 0;
                record.Ціна = (fieldValue["col_a2"] != DBNull.Value) ? (int)fieldValue["col_a2"] : 0;
                record.Сума = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a4", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_a1", record.Кво);
                    fieldValue.Add("col_a2", record.Ціна);
                    fieldValue.Add("col_a3", record.Сума);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                Кво = 0;
                Ціна = 0;
                Сума = 0;
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Товар = null, int _Кво = 0, int _Ціна = 0, decimal _Сума = 0)
            {
                Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
                Кво = _Кво;
                Ціна = _Ціна;
                Сума = _Сума;
                
            }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public int Кво { get; set; }
            public int Ціна { get; set; }
            public decimal Сума { get; set; }
            
        }
    }
      
    
    #endregion
    
    #region DOCUMENT "Инвентаризация2"
    
    ///<summary>
    ///Инвентаризация товаров.
    ///</summary>
    class Инвентаризация2_Objest : DocumentObject
    {
        public Инвентаризация2_Objest() : base(Config.Kernel, "tab_a16",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8", "col_a9", "col_b1", "col_b2", "col_b3", "col_b4" }) 
        {
            ДатаДок = DateTime.MinValue;
            НомерДок = 0;
            Склад = new Довідники.МестаХранения_Pointer();
            Контрагент = new Довідники.Контрагенти_Pointer();
            Валюта = new Довідники.Валюти_Pointer();
            Дата_курса = DateTime.MinValue;
            Курс = 0;
            ТипИнвентаризации = 0;
            ПредседательКомиссии = new Довідники.Сотрудники_Pointer();
            ЧленКомиссии1 = new Довідники.Сотрудники_Pointer();
            ЧленКомиссии2 = new Довідники.Сотрудники_Pointer();
            ЧленКомиссии3 = new Довідники.Сотрудники_Pointer();
            ПровелПроверку = new Довідники.Сотрудники_Pointer();
            
            //Табличні частини
            Товари_TablePart = new Инвентаризация2_Товари_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                ДатаДок = (base.FieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a1"].ToString()) : DateTime.MinValue;
                НомерДок = (base.FieldValue["col_a2"] != DBNull.Value) ? (int)base.FieldValue["col_a2"] : 0;
                Склад = new Довідники.МестаХранения_Pointer(base.FieldValue["col_a3"]);
                Контрагент = new Довідники.Контрагенти_Pointer(base.FieldValue["col_a4"]);
                Валюта = new Довідники.Валюти_Pointer(base.FieldValue["col_a5"]);
                Дата_курса = (base.FieldValue["col_a6"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["col_a6"].ToString()) : DateTime.MinValue;
                Курс = (base.FieldValue["col_a7"] != DBNull.Value) ? (decimal)base.FieldValue["col_a7"] : 0;
                ТипИнвентаризации = (base.FieldValue["col_a8"] != DBNull.Value) ? (int)base.FieldValue["col_a8"] : 0;
                ПредседательКомиссии = new Довідники.Сотрудники_Pointer(base.FieldValue["col_a9"]);
                ЧленКомиссии1 = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b1"]);
                ЧленКомиссии2 = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b2"]);
                ЧленКомиссии3 = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b3"]);
                ПровелПроверку = new Довідники.Сотрудники_Pointer(base.FieldValue["col_b4"]);
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["col_a1"] = ДатаДок;
            base.FieldValue["col_a2"] = НомерДок;
            base.FieldValue["col_a3"] = Склад.ToString();
            base.FieldValue["col_a4"] = Контрагент.ToString();
            base.FieldValue["col_a5"] = Валюта.ToString();
            base.FieldValue["col_a6"] = Дата_курса;
            base.FieldValue["col_a7"] = Курс;
            base.FieldValue["col_a8"] = ТипИнвентаризации;
            base.FieldValue["col_a9"] = ПредседательКомиссии.ToString();
            base.FieldValue["col_b1"] = ЧленКомиссии1.ToString();
            base.FieldValue["col_b2"] = ЧленКомиссии2.ToString();
            base.FieldValue["col_b3"] = ЧленКомиссии3.ToString();
            base.FieldValue["col_b4"] = ПровелПроверку.ToString();
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Инвентаризация2_Pointer GetDocumentPointer()
        {
            Инвентаризация2_Pointer directoryPointer = new Инвентаризация2_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public DateTime ДатаДок { get; set; }
        public int НомерДок { get; set; }
        public Довідники.МестаХранения_Pointer Склад { get; set; }
        public Довідники.Контрагенти_Pointer Контрагент { get; set; }
        public Довідники.Валюти_Pointer Валюта { get; set; }
        public DateTime Дата_курса { get; set; }
        public decimal Курс { get; set; }
        public int ТипИнвентаризации { get; set; }
        public Довідники.Сотрудники_Pointer ПредседательКомиссии { get; set; }
        public Довідники.Сотрудники_Pointer ЧленКомиссии1 { get; set; }
        public Довідники.Сотрудники_Pointer ЧленКомиссии2 { get; set; }
        public Довідники.Сотрудники_Pointer ЧленКомиссии3 { get; set; }
        public Довідники.Сотрудники_Pointer ПровелПроверку { get; set; }
        
        //Табличні частини
        public Инвентаризация2_Товари_TablePart Товари_TablePart { get; set; }
        
    }
    
    ///<summary>
    ///Инвентаризация товаров.
    ///</summary>
    class Инвентаризация2_Pointer : DocumentPointer
    {
        public Инвентаризация2_Pointer(object uid = null) : base(Config.Kernel, "tab_a16")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Инвентаризация2_Pointer(UnigueID uid, Dictionary<string, object> fields = null) : base(Config.Kernel, "tab_a16")
        {
            base.Init(uid, fields);
        } 
        
        public Инвентаризация2_Objest GetDocumentObject()
        {
            Инвентаризация2_Objest Инвентаризация2ObjestItem = new Инвентаризация2_Objest();
            Инвентаризация2ObjestItem.Read(base.UnigueID);
            return Инвентаризация2ObjestItem;
        }
    }
    
    ///<summary>
    ///Инвентаризация товаров.
    ///</summary>
    class Инвентаризация2_Select : DocumentSelect, IDisposable
    {
        public Инвентаризация2_Select() : base(Config.Kernel, "tab_a16") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Инвентаризация2_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public Инвентаризация2_Pointer Current { get; private set; }
    }
    
      
    class Инвентаризация2_Товари_TablePart : DocumentTablePart
    {
        public Инвентаризация2_Товари_TablePart(Инвентаризация2_Objest owner) : base(Config.Kernel, "tab_a47",
             new string[] { "col_b5", "col_b6", "col_b7", "col_b8", "col_b9", "col_c1" }) 
        {
            if (owner == null) throw new Exception("owner null");
            
            Owner = owner;
            Records = new List<Record>();
        }
        
        public Инвентаризация2_Objest Owner { get; private set; }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_b5"]);
                record.Единица = new Довідники.Единици_Pointer(fieldValue["col_b6"]);
                record.Количество = (fieldValue["col_b7"] != DBNull.Value) ? (decimal)fieldValue["col_b7"] : 0;
                record.Коеффициент = (fieldValue["col_b8"] != DBNull.Value) ? (decimal)fieldValue["col_b8"] : 0;
                record.ИнвКоличество = (fieldValue["col_b9"] != DBNull.Value) ? (decimal)fieldValue["col_b9"] : 0;
                record.Стоимость = (fieldValue["col_c1"] != DBNull.Value) ? (decimal)fieldValue["col_c1"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_b5", record.Товар.UnigueID.UGuid);
                    fieldValue.Add("col_b6", record.Единица.UnigueID.UGuid);
                    fieldValue.Add("col_b7", record.Количество);
                    fieldValue.Add("col_b8", record.Коеффициент);
                    fieldValue.Add("col_b9", record.ИнвКоличество);
                    fieldValue.Add("col_c1", record.Стоимость);
                    
                    base.BaseSave(record.UID, Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete(Owner.UnigueID);
            base.BaseCommitTransaction();
        }
        
        
        public class Record : DocumentTablePartRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                Единица = new Довідники.Единици_Pointer();
                Количество = 0;
                Коеффициент = 0;
                ИнвКоличество = 0;
                Стоимость = 0;
                
            }
        
            
            public Record(
                Довідники.Номенклатура_Pointer _Товар = null, Довідники.Единици_Pointer _Единица = null, decimal _Количество = 0, decimal _Коеффициент = 0, decimal _ИнвКоличество = 0, decimal _Стоимость = 0)
            {
                Товар = _Товар ?? new Довідники.Номенклатура_Pointer();
                Единица = _Единица ?? new Довідники.Единици_Pointer();
                Количество = _Количество;
                Коеффициент = _Коеффициент;
                ИнвКоличество = _ИнвКоличество;
                Стоимость = _Стоимость;
                
            }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.Единици_Pointer Единица { get; set; }
            public decimal Количество { get; set; }
            public decimal Коеффициент { get; set; }
            public decimal ИнвКоличество { get; set; }
            public decimal Стоимость { get; set; }
            
        }
    }
      
    
    #endregion
    
}

namespace ConfTrade_v1_1.Журнали
{

}

namespace ConfTrade_v1_1.РегістриВідомостей
{
    
    #region REGISTER "Перший"
    
    
    class Перший_RecordsSet : RegisterInformationRecordsSet
    {
        public Перший_RecordsSet() : base(Config.Kernel, "register_1",
             new string[] { "col_field0", "col_field12", "col_field13", "col_field11", "col_field1", "col_field2", "col_field3", "col_field4", "col_fiel5", "col_field6", "col_a1" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            bool isExistPreceding = false;
            if (Filter.field00 != null)
            {
                base.BaseFilter.Add(new Where("col_field0", Comparison.EQ, Filter.field00.ToString(), false));
                
                isExistPreceding = true;
                
            }
            
            if (Filter.field12 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_field12", Comparison.EQ, Filter.field12, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_field12", Comparison.EQ, Filter.field12, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.field13 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_field13", Comparison.EQ, Filter.field13, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_field13", Comparison.EQ, Filter.field13, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.field11 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_field11", Comparison.EQ, Filter.field11, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_field11", Comparison.EQ, Filter.field11, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.field1 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_field1", Comparison.EQ, Filter.field1, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_field1", Comparison.EQ, Filter.field1, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.field2 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_field2", Comparison.EQ, Filter.field2, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_field2", Comparison.EQ, Filter.field2, false));
                    isExistPreceding = true; 
                }
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.field00 = new Довідники.КлассификаторЕдИзм_Pointer(fieldValue["col_field0"]);
                record.field12 = (fieldValue["col_field12"] != DBNull.Value) ? (int)fieldValue["col_field12"] : 0;
                record.field13 = (fieldValue["col_field13"] != DBNull.Value) ? (int[])fieldValue["col_field13"] : new int[] { };
                record.field11 = (fieldValue["col_field11"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_field11"].ToString()) : DateTime.MinValue;
                record.field1 = (fieldValue["col_field1"] != DBNull.Value) ? TimeSpan.Parse(fieldValue["col_field1"].ToString()) : DateTime.MinValue.TimeOfDay;
                record.field2 = fieldValue["col_field2"].ToString();
                record.field3 = fieldValue["col_field3"].ToString();
                record.field4 = fieldValue["col_field4"].ToString();
                record.field5 = fieldValue["col_fiel5"].ToString();
                record.field6 = fieldValue["col_field6"].ToString();
                record.Один = (fieldValue["col_a1"] != DBNull.Value) ? (Перелічення.Список)fieldValue["col_a1"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_field0", record.field00.ToString());
                    fieldValue.Add("col_field12", record.field12);
                    fieldValue.Add("col_field13", record.field13);
                    fieldValue.Add("col_field11", record.field11);
                    fieldValue.Add("col_field1", record.field1);
                    fieldValue.Add("col_field2", record.field2);
                    fieldValue.Add("col_field3", record.field3);
                    fieldValue.Add("col_field4", record.field4);
                    fieldValue.Add("col_fiel5", record.field5);
                    fieldValue.Add("col_field6", record.field6);
                    fieldValue.Add("col_a1", record.Один);
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                field00 = new Довідники.КлассификаторЕдИзм_Pointer();
                field12 = 0;
                field13 = new int[] { };
                field11 = DateTime.MinValue;
                field1 = DateTime.MinValue.TimeOfDay;
                field2 = "";
                field3 = "";
                field4 = "";
                field5 = "";
                field6 = "";
                Один = 0;
                
            }
        
            public Довідники.КлассификаторЕдИзм_Pointer field00 { get; set; }
            public int field12 { get; set; }
            public int[] field13 { get; set; }
            public DateTime field11 { get; set; }
            public TimeSpan field1 { get; set; }
            public string field2 { get; set; }
            public string field3 { get; set; }
            public string field4 { get; set; }
            public string field5 { get; set; }
            public string field6 { get; set; }
            public Перелічення.Список Один { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 field00 = null;
                 field12 = null;
                 field13 = null;
                 field11 = null;
                 field1 = null;
                 field2 = null;
                 
            }
        
            public Довідники.КлассификаторЕдИзм_Pointer field00 { get; set; }
            public int? field12 { get; set; }
            public int[] field13 { get; set; }
            public DateTime? field11 { get; set; }
            public TimeSpan? field1 { get; set; }
            public string field2 { get; set; }
            
        }
    }
    
    #endregion
  
    #region REGISTER "Другий"
    
    
    class Другий_RecordsSet : RegisterInformationRecordsSet
    {
        public Другий_RecordsSet() : base(Config.Kernel, "register_2",
             new string[] { "col_field1", "col_field2", "col_field3" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            
            if (Filter.field1 != null)
            {
                base.BaseFilter.Add(new Where("col_field1", Comparison.EQ, Filter.field1, false));
                
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.field1 = fieldValue["col_field1"].ToString();
                record.field2 = fieldValue["col_field2"].ToString();
                record.field3 = fieldValue["col_field3"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_field1", record.field1);
                    fieldValue.Add("col_field2", record.field2);
                    fieldValue.Add("col_field3", record.field3);
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                field1 = "";
                field2 = "";
                field3 = "";
                
            }
        
            public string field1 { get; set; }
            public string field2 { get; set; }
            public string field3 { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 field1 = null;
                 
            }
        
            public string field1 { get; set; }
            
        }
    }
    
    #endregion
  
    #region REGISTER "Валюти"
    
    
    class Валюти_RecordsSet : RegisterInformationRecordsSet
    {
        public Валюти_RecordsSet() : base(Config.Kernel, "register_3",
             new string[] { "col1", "col2", "col3", "col4", "col5", "col6", "col_a7", "col_a8", "col_a9", "col_a1", "col_a2" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            bool isExistPreceding = false;
            if (Filter.zSAdAS != null)
            {
                base.BaseFilter.Add(new Where("col1", Comparison.EQ, Filter.zSAdAS, false));
                
                isExistPreceding = true;
                
            }
            
            if (Filter.SDFASDFA != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col2", Comparison.EQ, Filter.SDFASDFA, false));
                else
                {
                    base.BaseFilter.Add(new Where("col2", Comparison.EQ, Filter.SDFASDFA, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.werwew != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col3", Comparison.EQ, Filter.werwew, false));
                else
                {
                    base.BaseFilter.Add(new Where("col3", Comparison.EQ, Filter.werwew, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.s13 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col4", Comparison.EQ, Filter.s13, false));
                else
                {
                    base.BaseFilter.Add(new Where("col4", Comparison.EQ, Filter.s13, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.s11 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col5", Comparison.EQ, Filter.s11, false));
                else
                {
                    base.BaseFilter.Add(new Where("col5", Comparison.EQ, Filter.s11, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.s12 != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col6", Comparison.EQ, Filter.s12, false));
                else
                {
                    base.BaseFilter.Add(new Where("col6", Comparison.EQ, Filter.s12, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.sdfsdfasd != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a7", Comparison.EQ, Filter.sdfsdfasd, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a7", Comparison.EQ, Filter.sdfsdfasd, false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.Склад != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a8", Comparison.EQ, Filter.Склад.ToString(), false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a8", Comparison.EQ, Filter.Склад.ToString(), false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.Вид != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a9", Comparison.EQ, Filter.Вид, false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a9", Comparison.EQ, Filter.Вид, false));
                    isExistPreceding = true; 
                }
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.zSAdAS = fieldValue["col1"].ToString();
                record.SDFASDFA = fieldValue["col2"].ToString();
                record.werwew = fieldValue["col3"].ToString();
                record.s13 = fieldValue["col4"].ToString();
                record.s11 = (bool)fieldValue["col5"];
                record.s12 = fieldValue["col6"].ToString();
                record.sdfsdfasd = fieldValue["col_a7"].ToString();
                record.Склад = new Довідники.МестаХранения_Pointer(fieldValue["col_a8"]);
                record.Вид = (fieldValue["col_a9"] != DBNull.Value) ? (Перелічення.ВидиТоварів)fieldValue["col_a9"] : 0;
                record.цйуцу = fieldValue["col_a1"].ToString();
                record.івафіваф = fieldValue["col_a2"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col1", record.zSAdAS);
                    fieldValue.Add("col2", record.SDFASDFA);
                    fieldValue.Add("col3", record.werwew);
                    fieldValue.Add("col4", record.s13);
                    fieldValue.Add("col5", record.s11);
                    fieldValue.Add("col6", record.s12);
                    fieldValue.Add("col_a7", record.sdfsdfasd);
                    fieldValue.Add("col_a8", record.Склад.ToString());
                    fieldValue.Add("col_a9", record.Вид);
                    fieldValue.Add("col_a1", record.цйуцу);
                    fieldValue.Add("col_a2", record.івафіваф);
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                zSAdAS = "";
                SDFASDFA = "";
                werwew = "";
                s13 = "";
                s11 = false;
                s12 = "";
                sdfsdfasd = "";
                Склад = new Довідники.МестаХранения_Pointer();
                Вид = 0;
                цйуцу = "";
                івафіваф = "";
                
            }
        
            public string zSAdAS { get; set; }
            public string SDFASDFA { get; set; }
            public string werwew { get; set; }
            public string s13 { get; set; }
            public bool s11 { get; set; }
            public string s12 { get; set; }
            public string sdfsdfasd { get; set; }
            public Довідники.МестаХранения_Pointer Склад { get; set; }
            public Перелічення.ВидиТоварів Вид { get; set; }
            public string цйуцу { get; set; }
            public string івафіваф { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 zSAdAS = null;
                 SDFASDFA = null;
                 werwew = null;
                 s13 = null;
                 s11 = null;
                 s12 = null;
                 sdfsdfasd = null;
                 Склад = null;
                 Вид = null;
                 
            }
        
            public string zSAdAS { get; set; }
            public string SDFASDFA { get; set; }
            public string werwew { get; set; }
            public string s13 { get; set; }
            public bool? s11 { get; set; }
            public string s12 { get; set; }
            public string sdfsdfasd { get; set; }
            public Довідники.МестаХранения_Pointer Склад { get; set; }
            public Перелічення.ВидиТоварів? Вид { get; set; }
            
        }
    }
    
    #endregion
  
}

namespace ConfTrade_v1_1.РегістриНакопичення
{
    
    #region REGISTER "Перший"
    
    
    class Перший_RecordsSet : RegisterAccumulationRecordsSet
    {
        public Перший_RecordsSet() : base(Config.Kernel, "register_4",
             new string[] { "col_field1", "col_a1", "col_a3", "col_a4", "col_field2", "col_a8", "col_field3", "col_a2", "col_a5" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            bool isExistPreceding = false;
            if (Filter.Період != null)
            {
                base.BaseFilter.Add(new Where("col_field1", Comparison.EQ, Filter.Період, false));
                
                isExistPreceding = true;
                
            }
            
            if (Filter.Фірма != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a1", Comparison.EQ, Filter.Фірма.ToString(), false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a1", Comparison.EQ, Filter.Фірма.ToString(), false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.Товар != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a3", Comparison.EQ, Filter.Товар.ToString(), false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a3", Comparison.EQ, Filter.Товар.ToString(), false));
                    isExistPreceding = true; 
                }
            }
            
            if (Filter.Склад != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a4", Comparison.EQ, Filter.Склад.ToString(), false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a4", Comparison.EQ, Filter.Склад.ToString(), false));
                    isExistPreceding = true; 
                }
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.Період = (fieldValue["col_field1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_field1"].ToString()) : DateTime.MinValue;
                record.Фірма = new Довідники.Фирми_Pointer(fieldValue["col_a1"]);
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a3"]);
                record.Склад = new Довідники.Номенклатура_Pointer(fieldValue["col_a4"]);
                record.Кво = (fieldValue["col_field2"] != DBNull.Value) ? (decimal)fieldValue["col_field2"] : 0;
                record.Сума = (fieldValue["col_a8"] != DBNull.Value) ? (decimal)fieldValue["col_a8"] : 0;
                record.Коментар = fieldValue["col_field3"].ToString();
                record.Один = (fieldValue["col_a2"] != DBNull.Value) ? (Перелічення.Список)fieldValue["col_a2"] : 0;
                record.sdfsdfs = fieldValue["col_a5"].ToString();
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_field1", record.Період);
                    fieldValue.Add("col_a1", record.Фірма.ToString());
                    fieldValue.Add("col_a3", record.Товар.ToString());
                    fieldValue.Add("col_a4", record.Склад.ToString());
                    fieldValue.Add("col_field2", record.Кво);
                    fieldValue.Add("col_a8", record.Сума);
                    fieldValue.Add("col_field3", record.Коментар);
                    fieldValue.Add("col_a2", record.Один);
                    fieldValue.Add("col_a5", record.sdfsdfs);
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                Період = DateTime.MinValue;
                Фірма = new Довідники.Фирми_Pointer();
                Товар = new Довідники.Номенклатура_Pointer();
                Склад = new Довідники.Номенклатура_Pointer();
                Кво = 0;
                Сума = 0;
                Коментар = "";
                Один = 0;
                sdfsdfs = "";
                
            }
        
            public DateTime Період { get; set; }
            public Довідники.Фирми_Pointer Фірма { get; set; }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.Номенклатура_Pointer Склад { get; set; }
            public decimal Кво { get; set; }
            public decimal Сума { get; set; }
            public string Коментар { get; set; }
            public Перелічення.Список Один { get; set; }
            public string sdfsdfs { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 Період = null;
                 Фірма = null;
                 Товар = null;
                 Склад = null;
                 
            }
        
            public DateTime? Період { get; set; }
            public Довідники.Фирми_Pointer Фірма { get; set; }
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.Номенклатура_Pointer Склад { get; set; }
            
        }
    }
    
    #endregion
  
    #region REGISTER "Остатки"
    
    
    class Остатки_RecordsSet : RegisterAccumulationRecordsSet
    {
        public Остатки_RecordsSet() : base(Config.Kernel, "register_5",
             new string[] { "col_a1", "col_a4", "col_a3", "col_a6" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            bool isExistPreceding = false;
            if (Filter.Товар != null)
            {
                base.BaseFilter.Add(new Where("col_a1", Comparison.EQ, Filter.Товар.ToString(), false));
                
                isExistPreceding = true;
                
            }
            
            if (Filter.Склад != null)
            {
                if (isExistPreceding)
                    base.BaseFilter.Add(new Where(Comparison.AND, "col_a4", Comparison.EQ, Filter.Склад.ToString(), false));
                else
                {
                    base.BaseFilter.Add(new Where("col_a4", Comparison.EQ, Filter.Склад.ToString(), false));
                    isExistPreceding = true; 
                }
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a1"]);
                record.Склад = new Довідники.МестаХранения_Pointer(fieldValue["col_a4"]);
                record.Кількість = (fieldValue["col_a3"] != DBNull.Value) ? (int)fieldValue["col_a3"] : 0;
                record.Договір = new Документи.Договор_Pointer(fieldValue["col_a6"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Товар.ToString());
                    fieldValue.Add("col_a4", record.Склад.ToString());
                    fieldValue.Add("col_a3", record.Кількість);
                    fieldValue.Add("col_a6", record.Договір.ToString());
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                Склад = new Довідники.МестаХранения_Pointer();
                Кількість = 0;
                Договір = new Документи.Договор_Pointer();
                
            }
        
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.МестаХранения_Pointer Склад { get; set; }
            public int Кількість { get; set; }
            public Документи.Договор_Pointer Договір { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 Товар = null;
                 Склад = null;
                 
            }
        
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            public Довідники.МестаХранения_Pointer Склад { get; set; }
            
        }
    }
    
    #endregion
  
    #region REGISTER "Обороти"
    
    ///<summary>
    ///Обороти.
    ///</summary>
    class Обороти_RecordsSet : RegisterAccumulationRecordsSet
    {
        public Обороти_RecordsSet() : base(Config.Kernel, "register_6",
             new string[] { "col_a1" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            
            if (Filter.Товар != null)
            {
                base.BaseFilter.Add(new Where("col_a1", Comparison.EQ, Filter.Товар.ToString(), false));
                
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.Товар = new Довідники.Номенклатура_Pointer(fieldValue["col_a1"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Товар.ToString());
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        ///<summary>
    ///Обороти.
    ///</summary>
        public class Record : RegisterRecord
        {
            public Record()
            {
                Товар = new Довідники.Номенклатура_Pointer();
                
            }
        
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 Товар = null;
                 
            }
        
            public Довідники.Номенклатура_Pointer Товар { get; set; }
            
        }
    }
    
    #endregion
  
    #region REGISTER "НДС"
    
    
    class НДС_RecordsSet : RegisterAccumulationRecordsSet
    {
        public НДС_RecordsSet() : base(Config.Kernel, "tab_a63",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a5" }) 
        {
            Records = new List<Record>();
            Filter = new SelectFilter();
        }
        
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            
            if (Filter.Документ != null)
            {
                base.BaseFilter.Add(new Where("col_a1", Comparison.EQ, Filter.Документ.ToString(), false));
                
            }
            

            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                
                record.UID = (Guid)fieldValue["uid"];
                  
                record.Документ = new Документи.РасходнаяНакладная_Pointer(fieldValue["col_a1"]);
                record.Кво = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0;
                record.Коментар = fieldValue["col_a3"].ToString();
                record.Додатково = new Довідники.Прайс_лист_Pointer(fieldValue["col_a5"]);
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Документ.ToString());
                    fieldValue.Add("col_a2", record.Кво);
                    fieldValue.Add("col_a3", record.Коментар);
                    fieldValue.Add("col_a5", record.Додатково.ToString());
                    
                    base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Delete()
        {
            base.BaseBeginTransaction();
            base.BaseDelete();
            base.BaseCommitTransaction();
        }
        
        public SelectFilter Filter { get; set; }
        
        
        public class Record : RegisterRecord
        {
            public Record()
            {
                Документ = new Документи.РасходнаяНакладная_Pointer();
                Кво = 0;
                Коментар = "";
                Додатково = new Довідники.Прайс_лист_Pointer();
                
            }
        
            public Документи.РасходнаяНакладная_Pointer Документ { get; set; }
            public decimal Кво { get; set; }
            public string Коментар { get; set; }
            public Довідники.Прайс_лист_Pointer Додатково { get; set; }
            
        }
    
        public class SelectFilter
        {
            public SelectFilter()
            {
                 Документ = null;
                 
            }
        
            public Документи.РасходнаяНакладная_Pointer Документ { get; set; }
            
        }
    }
    
    #endregion
  
}
  