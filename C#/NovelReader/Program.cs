using System;

namespace NovelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            DataParser da = new DataParser();
            da.Fetch("Overgeared");
        }
    }
}