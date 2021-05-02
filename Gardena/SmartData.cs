using System.Collections.Generic;


namespace GardenaApi.Gardena
{
    public class SmartData
    {
        public List<Data> Data { get; set; }
    }
    public class Data 
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
    }
    public class Attributes 
    {
        public string Name { get; set; }
    }


    
}
