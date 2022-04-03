using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReader
{
    class Novel
    {
        public string name { get; private set; }
        public string source { get; private set; }
        public string location { get; private set; }
        public int chapter { get; private set; }

        public Novel(string name, string soruce, string location, int chapter)
        {
            
        }
    }
}
