using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebRetriever
{
    public class Scrapper
    {
        public int Scrape(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            var response = httpRequest.GetResponse();
            if(((HttpWebResponse)response).StatusDescription == "OK")
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Console.WriteLine(responseFromServer);
                }
            }
            return 1;
        }
    }
}
