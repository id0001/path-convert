using DocoptNet;
using PathConvert.Properties;
using System;
using System.Threading.Tasks;
using TextCopy;

namespace PathConvert
{
	class Program
	{
		public async static Task Main(string[] args)
		{
			var usage = Resources.docopt;
			var arguments = new Docopt().Apply(usage, args, version: "Path Convert 1.0", exit: true);

			bool clipboard = arguments["--clipboard"].IsTrue;
			bool windows = arguments["--windows"].IsTrue;
			string path = arguments["<path>"]?.ToString();

			if (clipboard && path == null)
			{
				path = await Clipboard.GetTextAsync();
			}

			if (windows)
			{
				path = path.Replace('/', '\\');
			}
			else
			{
				path = path.Replace(@"\\", "/");
				path = path.Replace('\\', '/');
			}

			if (clipboard)
			{
				await Clipboard.SetTextAsync(path);
			}

			Console.WriteLine(path);
		}
	}
}
