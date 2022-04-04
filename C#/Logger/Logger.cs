using System.IO;

namespace TextLogger
{
    public class Logger
    {
        private static string NAME = Path.Combine(Directory.GetCurrentDirectory(), "Logs.txt");
        public static Logger TextLogger = new Logger();

        private Logger()
        {
            if (File.Exists(NAME))
            {
                File.Delete(NAME);
            }
        }

        public static void writeToLog(string text)
        {
            if (!File.Exists(NAME)) File.Create(NAME).Dispose();
            TextWriter writer = new StreamWriter(NAME, false);
            writer.WriteLine(text);
            writer.Close();
        }
    }
}