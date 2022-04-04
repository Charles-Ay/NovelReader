namespace Novel
{
    public class Novel
    {
        public string name { get; private set; }
        public int totalChapters { get; private set; }
        public string initalLink { get; private set; }
        public string source { get; private set; }


        public Novel(string name, int totalChapterNum, string initalLink, string source)
        {
            this.name = name;
            totalChapters = totalChapterNum;
            this.initalLink = initalLink;
            this.source = source;
        }
    }
}