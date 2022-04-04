

namespace GardenaApi.Gardena.WebSocketBody
{
    public class WebSocketJsonBody
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public string id { get; set; }
        public string type { get; set; }

        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public string locationId { get; set; }
    }
}
