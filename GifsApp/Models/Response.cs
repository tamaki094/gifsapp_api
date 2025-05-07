namespace GifsApp.Models
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; } = "";
        public Object result { get; set; } = new Object();
    }
}
