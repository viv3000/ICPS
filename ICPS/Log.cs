using System;

using System.Windows.Forms;

namespace ICPS
{
	static class Log
	{
		public static void Debug(Exception ex, String message)
		{
			MessageBox.Show(message+""+ex);
		}
		public static void Message(String message)
		{
			MessageBox.Show(message);
		}
	}
}
