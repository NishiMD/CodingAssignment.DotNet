namespace NewsFeedAPI
{
    public class StoryDetails
    {

        public string by { get; set; }
        public int descendents { get; set; }
        public int id { get; set; }
        public List<int> kids { get; set; }
        public int score {  get; set; }
        public int time {  get; set; }
        public string title { get; set; }
        public string type {  get; set; }
        public string url {  get; set; }
    }
}
