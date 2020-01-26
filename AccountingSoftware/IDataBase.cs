﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public interface IDataBase
	{
		void Open();

		void Close();

		string ConnectionString { get; set; }

		void SelectDirectory(DirectorySelect sender, List<DirectoryPointer> listDirectoryPointer);
	}
}
