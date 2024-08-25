namespace StockMarket.ViewModels
{
    public class PolygonNewsResponse
    {
        public List<NewsResult> results { get; set; }
        public string status { get; set; }
        public string request_id { get; set; }
        public string count { get; set; }
        public string next_url { get; set; }
    }

    public class NewsResult
    {
        public string id { get; set; }
        public Publisher publisher { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public DateTime published_utc { get; set; }
        public string article_url { get; set; }
        public List<string> tickers { get; set; }
        public string image_url { get; set; }
        public string description { get; set; }
        //public List<string> keywords { get; set; }
        //public List<Insight> insights { get; set; }
    }

    public class Insight
    {
        public string ticker { get; set; }
        public string sentiment { get; set; }
        public string sentiment_reasoning { get; set; }
    }

    public class Publisher
    {
        public string name { get; set; }
        public string homepage_url { get; set; }
        public string logo_url { get; set; }
        public string favicon_url { get; set; }
    }
}
