using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			//this.Hide();
			ProcessStartInfo ps = new ProcessStartInfo();
			ps.FileName = "cmd.exe";
			ps.WindowStyle = ProcessWindowStyle.Normal;
			ps.Arguments = Environment.CurrentDirectory + "\\WalletHistoryRecorder.exe";
			Process.Start(ps);
		}
	}
}
