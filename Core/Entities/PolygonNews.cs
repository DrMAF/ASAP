namespace Core.Entities
{
    public class PolygonNews : BaseEntity<string>
    {
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public string Author { get; set; }
        public string Tickers { get; set; }
        public DateTime Published_utc { get; set; }
        public string Article_url { get; set; }
        public string Image_url { get; set; }

        //[ForeignKey("Publisher")]
        //public int PublisherId { get; set; }
        //public virtual PolygonPublisher Publisher { get; set; }
    }

    //public class PolygonPublisher : BaseEntity<int>
    //{
    //    public string Homepage_url { get; set; }
    //    public string Logo_url { get; set; }
    //    public string Favicon_url { get; set; }
    //}
}
