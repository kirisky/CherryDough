namespace CherryDough.Application.EventSourcedNormalizers
{
    public class ItemHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Who { get; set; }
        public string Timestamp { get; set; }
    }
}