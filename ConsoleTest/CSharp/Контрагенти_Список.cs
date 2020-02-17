﻿
    
using System.Text;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;    

namespace ConfTrade
{
    public partial class ConfTrade
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<root>");
            
            Довідники.Контрагенти_Список_View m_Контрагенти_Список_View = new Довідники.Контрагенти_Список_View();
            m_Контрагенти_Список_View.QuerySelect.CreateTempTable = true;
            sb.Append(m_Контрагенти_Список_View.Read());
            
            
    
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
  