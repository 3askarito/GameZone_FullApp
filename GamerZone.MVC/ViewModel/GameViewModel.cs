namespace GamerZone.MVC.ViewModel
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Devices { get; set; } = new List<string>();
        public List<int> DeviceIds { get; set; } = new List<int>(); 
    }
}
