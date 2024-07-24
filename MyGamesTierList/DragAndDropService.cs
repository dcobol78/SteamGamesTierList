namespace MyGamesTierList
{
    public class DragAndDropService
    {
        public Game Data { get; set; }
        public string Zone { get; set; }

        public void StartDrag(Game data, string zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return this.Zone == zone;
        }
    }
}
