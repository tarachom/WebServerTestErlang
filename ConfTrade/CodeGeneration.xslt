﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

  <!-- Для задання типу поля -->
  <xsl:template name="FieldType">
    <xsl:choose>
      <xsl:when test="Type = 'string'">
        <xsl:text>string</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'string[]'">
        <xsl:text>string[]</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer'">
        <xsl:text>int</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer[]'">
        <xsl:text>int[]</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric'">
        <xsl:text>decimal</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric[]'">
        <xsl:text>decimal[]</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'boolean'">
        <xsl:text>bool</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'time'">
        <xsl:text>TimeSpan</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'date' or Type = 'datetime'">
        <xsl:text>DateTime</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'pointer'">
        <xsl:value-of select="Pointer"/>
        <xsl:text>_Pointer</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'empty_pointer'">
        <xsl:text>DirectoryEmptyPointer</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'enum'">
        <xsl:text>Enums.</xsl:text>
        <xsl:value-of select="Pointer"/>
      </xsl:when>
    </xsl:choose>    
  </xsl:template>
  
  <!-- Для конструкторів. Значення поля по замовчуванню відповідно до типу -->
  <xsl:template name="DefaultFieldValue">
    <xsl:choose>
      <xsl:when test="Type = 'string'">
        <xsl:text>""</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'string[]'">
        <xsl:text>new string[] { }</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer[]'">
        <xsl:text>new int[] { }</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric[]'">
        <xsl:text>new decimal[] { }</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'boolean'">
        <xsl:text>false</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'time'">
        <xsl:text>DateTime.MinValue.TimeOfDay</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'date' or Type = 'datetime'">
        <xsl:text>DateTime.MinValue</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'pointer'">
        <xsl:text>new </xsl:text>
        <xsl:value-of select="Pointer"/>
        <xsl:text>_Pointer()</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'empty_pointer'">
        <xsl:text>new DirectoryEmptyPointer()</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'enum'">
        <xsl:text>0</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!-- Для параметрів функцій. Значення параметра по замовчуванню відповідно до типу -->
  <xsl:template name="DefaultParamValue">
    <xsl:choose>
      <xsl:when test="Type = 'string'">
        <xsl:text>""</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'string[]'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer[]'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric[]'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'boolean'">
        <xsl:text>false</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'time'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'date' or Type = 'datetime'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'pointer'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'empty_pointer'">
        <xsl:text>null</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'enum'">
        <xsl:text>0</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <!-- Для присвоєння значення полям -->
  <xsl:template name="ReadFieldValue">
     <xsl:param name="BaseFieldContainer" />
     
     <xsl:choose>
        <xsl:when test="Type = 'string'">
          <xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"].ToString()</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'string[]'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(string[])</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : new string[] { }</xsl:text>
        </xsl:when>
       <xsl:when test="Type = 'integer'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(int)</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : 0</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'integer[]'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(int[])</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : new int[] { }</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'numeric'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(decimal)</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : 0</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'numeric[]'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>(decimal[])</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
          <xsl:text> : new decimal[] { }</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'boolean'">
          <xsl:text>(bool)</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'time'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>TimeSpan.Parse(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"].ToString())</xsl:text>
          <xsl:text> : DateTime.MinValue.TimeOfDay</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'date' or Type = 'datetime'">
          <xsl:text>(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] != DBNull.Value) ? </xsl:text>
          <xsl:text>DateTime.Parse(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"].ToString())</xsl:text>
          <xsl:text> : DateTime.MinValue</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'pointer'">
          <xsl:text>new </xsl:text><xsl:value-of select="Pointer"/>
          <xsl:text>_Pointer(</xsl:text><xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"])</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'empty_pointer'">
          <xsl:text>new DirectoryEmptyPointer()</xsl:text>
        </xsl:when>
        <xsl:when test="Type = 'enum'">
          <xsl:text>(Enums.</xsl:text><xsl:value-of select="Pointer"/><xsl:text>)</xsl:text>
          <xsl:value-of select="$BaseFieldContainer"/><xsl:text>["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"]</xsl:text>
        </xsl:when>
     </xsl:choose>
  </xsl:template>

  <!-- Документування коду -->
  <xsl:template name="CommentSummary">
    <xsl:variable name="normalize_space_Desc" select="normalize-space(Desc)" />
    <xsl:if test="$normalize_space_Desc != ''">
    <xsl:text>///&lt;summary</xsl:text>&gt;
    <xsl:text>///</xsl:text>
    <xsl:value-of select="$normalize_space_Desc"/>.
    <xsl:text>///&lt;/summary&gt;</xsl:text>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="/">

/*
 *
 * Конфігурації "<xsl:value-of select="Configuration/Name"/>"
 * Автор <xsl:value-of select="Configuration/Author"/>
 * Дата конфігурації: <xsl:value-of select="Configuration/DateTimeSave"/>
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;
using Enums = <xsl:value-of select="Configuration/NameSpace"/>.Enums;

namespace <xsl:value-of select="Configuration/NameSpace"/>
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Directory
{
    <xsl:for-each select="Configuration/Directories/Directory">
      <xsl:variable name="DirectoryName" select="Name"/>
    #region DIRECTORY "<xsl:value-of select="$DirectoryName"/>"
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DirectoryName"/>_Objest : DirectoryObject
    {
        public <xsl:value-of select="$DirectoryName"/>_Objest() : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
            //Табличні частини
            <xsl:for-each select="TabularParts/TablePart">
                <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
                <xsl:value-of select="$TablePartName"/><xsl:text> = new </xsl:text>
                <xsl:value-of select="concat($DirectoryName, '_', $TablePartName)"/><xsl:text>(this)</xsl:text>;
            </xsl:for-each>
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                <xsl:for-each select="Fields/Field">
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">base.FieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:text>base.FieldValue["</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"] = </xsl:text>
              <xsl:if test="Type = 'enum'">
                  <xsl:text>(int)</xsl:text>      
              </xsl:if>
              <xsl:value-of select="Name"/>
              <xsl:choose>
                <xsl:when test="Type = 'pointer' or Type = 'empty_pointer'">
                  <xsl:text>.UnigueID.UGuid</xsl:text>
                </xsl:when>
              </xsl:choose>;
            </xsl:for-each>
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer GetDirectoryPointer()
        {
            <xsl:value-of select="$DirectoryName"/>_Pointer directoryPointer = new <xsl:value-of select="$DirectoryName"/>_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
        //Табличні частини
        <xsl:for-each select="TabularParts/TablePart">
            <xsl:variable name="TablePartName" select="concat(Name, '_TablePart')"/>
            <xsl:text>public </xsl:text><xsl:value-of select="concat($DirectoryName, '_', $TablePartName)"/><xsl:text> </xsl:text>
            <xsl:value-of select="$TablePartName"/><xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DirectoryName"/>_Pointer : DirectoryPointer
    {
        public <xsl:value-of select="$DirectoryName"/>_Pointer(object uid = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Pointer(UnigueID uid, Dictionary&lt;string, object&gt; fields = null) : base(Config.Kernel, "<xsl:value-of select="Table"/>")
        {
            base.Init(uid, fields);
        } 
        
        public <xsl:value-of select="$DirectoryName"/>_Objest GetDirectoryObject()
        {
            <xsl:value-of select="$DirectoryName"/>_Objest <xsl:value-of select="$DirectoryName"/>ObjestItem = new <xsl:value-of select="$DirectoryName"/>_Objest();
            <xsl:value-of select="$DirectoryName"/>ObjestItem.Read(base.UnigueID);
            return <xsl:value-of select="$DirectoryName"/>ObjestItem;
        }
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$DirectoryName"/>_Select : DirectorySelect, IDisposable
    {
        public <xsl:value-of select="$DirectoryName"/>_Select() : base(Config.Kernel, "<xsl:value-of select="Table"/>") { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new <xsl:value-of select="$DirectoryName"/>_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public <xsl:value-of select="$DirectoryName"/>_Pointer Current { get; private set; }
    }
    
      <xsl:for-each select="TabularParts/TablePart"> <!-- TableParts -->
        <xsl:variable name="TablePartName" select="Name"/>
        <xsl:variable name="TablePartFullName" select="concat($DirectoryName, '_', $TablePartName)"/>
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$TablePartFullName"/>_TablePart : DirectoryTablePart
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePart(<xsl:value-of select="$DirectoryName"/>_Objest owner) : base(Config.Kernel, "<xsl:value-of select="Table"/>",
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> }) 
        {
            Owner = owner;
            Records = new List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt;();
        }
        
        public <xsl:value-of select="$DirectoryName"/>_Objest Owner { get; private set; }
        
        public List&lt;<xsl:value-of select="$TablePartFullName"/>_TablePartRecord&gt; Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary&lt;string, object&gt; fieldValue in base.FieldValueList) 
            {
                <xsl:value-of select="$TablePartFullName"/>_TablePartRecord record = new <xsl:value-of select="$TablePartFullName"/>_TablePartRecord();

                <xsl:for-each select="Fields/Field">
                  <xsl:text>record.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text> = </xsl:text>
                  <xsl:call-template name="ReadFieldValue">
                    <xsl:with-param name="BaseFieldContainer">fieldValue</xsl:with-param>
                  </xsl:call-template>;
                </xsl:for-each>
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        /// &lt;summary&gt;
        /// Зберегти колекцію Records в базу.
        /// &lt;/summary&gt;
        /// &lt;param name="clear_all_before_save"&gt;
        /// Щоб не очищати всю колекцію в базі перед записом треба поставити clear_all_before_save = false.
        /// &lt;/param&gt;
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (<xsl:value-of select="$TablePartFullName"/>_TablePartRecord record in Records)
                {
                    Dictionary&lt;string, object&gt; fieldValue = new Dictionary&lt;string, object&gt;();

                    <xsl:for-each select="Fields/Field">
                      <xsl:text>fieldValue.Add("</xsl:text>
                      <xsl:value-of select="NameInTable"/><xsl:text>", record.</xsl:text><xsl:value-of select="Name"/>
                      <xsl:choose>
                        <xsl:when test="Type = 'pointer'">
                          <xsl:text>.UnigueID.UGuid</xsl:text>
                        </xsl:when>
                        <xsl:when test="Type = 'empty_pointer'">
                          <xsl:text>.UnigueID.UGuid</xsl:text>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:text>)</xsl:text>;
                    </xsl:for-each>
                    base.BaseSave(Owner.UnigueID, fieldValue);
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
    }
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$TablePartFullName"/>_TablePartRecord : DirectoryTablePartRecord
    {
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord()
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
        }
        
        <xsl:if test="count(Fields/Field) > 0">
        public <xsl:value-of select="$TablePartFullName"/>_TablePartRecord(
            <xsl:for-each select="Fields/Field">
              <xsl:if test="position() != 1"><xsl:text>, </xsl:text></xsl:if>
              <xsl:call-template name="FieldType" />
              <xsl:if test="Type = 'date' or Type = 'datetime' or Type = 'time'">
                   <xsl:text>? </xsl:text>    
              </xsl:if>
              <xsl:text> _</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:text> = </xsl:text>
              <xsl:call-template name="DefaultParamValue" />
            </xsl:for-each>)
        {
            <xsl:for-each select="Fields/Field">
              <xsl:value-of select="Name"/>
              <xsl:text> = _</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:if test="Type = 'date' or Type = 'datetime' or Type = 'time' or Type = 'pointer' or Type = 'empty_pointer'">
                   <xsl:text> ?? </xsl:text>
                   <xsl:call-template name="DefaultFieldValue" />
              </xsl:if>;
            </xsl:for-each>
        }
        </xsl:if>
        
        <xsl:for-each select="Fields/Field">
          <xsl:text>public </xsl:text>
          <xsl:call-template name="FieldType" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name"/>
          <xsl:text> { get; set; </xsl:text>}
        </xsl:for-each>
    }
      </xsl:for-each> <!-- TableParts -->
      
      <xsl:for-each select="Views/View"> <!-- Views -->
        <xsl:variable name="ViewsName" select="Name"/>
        <xsl:variable name="ViewsFullName" select="concat($DirectoryName, '_', $ViewsName)"/>
    
    <xsl:call-template name="CommentSummary" />
    class <xsl:value-of select="$ViewsFullName"/>_View : DirectoryView
    {
        public <xsl:value-of select="$ViewsFullName"/>_View() : base(Config.Kernel, "<xsl:value-of select="Table"/>", 
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>"</xsl:text>
             </xsl:for-each> },
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="Name"/><xsl:text>"</xsl:text>
             </xsl:for-each> },
             <xsl:text>new string[] { </xsl:text>
             <xsl:for-each select="Fields/Field">
               <xsl:if test="position() != 1">
                 <xsl:text>, </xsl:text>
               </xsl:if>
               <xsl:text>"</xsl:text><xsl:value-of select="Type"/><xsl:text>"</xsl:text>
             </xsl:for-each> },
             "<xsl:value-of select="$ViewsFullName"/>")
        {
            <!--
            base.QuerySelect.PrimaryField = "<xsl:value-of select="PrimaryField"/>";
            -->
            <!--
            <xsl:for-each select="Where/Field">
              <xsl:text>Where_</xsl:text><xsl:value-of select="NameInTable"/><xsl:text> = new Where("</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>", Comparison.EQ, null)</xsl:text>;
              <xsl:text>base.QuerySelect.Where.Add(Where_</xsl:text><xsl:value-of select="NameInTable"/><xsl:text>)</xsl:text>;
            </xsl:for-each>
            -->
        }
        <!--
        <xsl:for-each select="Where/Field">
          <xsl:text>public Where Where_</xsl:text><xsl:value-of select="NameInTable"/><xsl:text> { get; set; }</xsl:text>
        </xsl:for-each>
        -->
    }
      </xsl:for-each> <!-- Views -->
    
    #endregion
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Enums
{
    <xsl:for-each select="Configuration/Enums/Enum">
    <xsl:call-template name="CommentSummary" />
    public enum <xsl:value-of select="Name"/>
    {
         <xsl:variable name="CountEnumField" select="count(Fields/Field)" />
         <xsl:for-each select="Fields/Field">
             <xsl:value-of select="Name"/>
             <xsl:text> = </xsl:text>
             <xsl:value-of select="Value"/>
             <xsl:if test="position() &lt; $CountEnumField">
         <xsl:text>,
         </xsl:text>
             </xsl:if>
         </xsl:for-each>
    }
    
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Document
{
    <xsl:for-each select="Configuration/Documents/Document">
      <xsl:variable name="DocumentName" select="Name"/>
    #region DOCUMENT "<xsl:value-of select="$DocumentName"/>"
    
    #endregion
    </xsl:for-each>
}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Journal
{

}

namespace <xsl:value-of select="Configuration/NameSpace"/>.Register
{

}
  </xsl:template>
</xsl:stylesheet>