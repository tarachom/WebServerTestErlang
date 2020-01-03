﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	public class Kernel
	{
		public Kernel()
		{
			DataBase = new PostgreSQL();
			DataBase.ConnectionString = "Server=localhost;User Id=postgres;Password=525491;Database=ConfTrade;";
			DataBase.Open();
		}

		~Kernel()
		{
			DataBase.Close();
		}

		public IDataBase DataBase { get; set; }

		public void SelectDirectory(string query)
		{
			DataBase.SelectDirectory(query);
		}
	}
}
